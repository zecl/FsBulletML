namespace FsBulletML
open System
open FsBulletML.Processable 

[<StructAttribute>]
type RunResult =
  val Processed : bool
  val X: float32
  val Y: float32
  new(processed:bool,x:float32, y: float32) = 
    { Processed = processed; X = x; Y = y }

module BulletRunner = 

  let internal calcDir dir =  
    if (float dir > 2. * Math.PI) then
      dir - float32 (2. * Math.PI)
    elif (float dir < 0.) then
      dir + float32 (2. * Math.PI)
    else
      dir

  let internal toProcessable bulletml = 
    let recBulletml = IntermediateParser.convertRecBulletml bulletml
    let taskActions = 
      recBulletml |> IntermediateParser.getAction |> List.filter (function
      | RecBulletml.Action (attrs, _) ->
        match attrs.actionLabel with
        | Some label -> 
          if label.StartsWith("top") then
            true
          else
            false
        | _ -> false
      | _ -> false)

    let recbml = taskActions |> List.map (IntermediateParser.convertRefBulletml recBulletml)
    List.map IntermediateParser.convertRecBulletmlEx recbml

  [<CompiledName "CreateTask">]
  let createTask (bulletElm:ProcessableBulletml) (bulletmlTask:BulletmlTask) (bullet:IBulletmlObject) = 
    bulletElm.Init()
    let attrs, d, s, children =
      match bulletElm with
        | ProcessableBulletml.Bullet(attrs,d,s,children) -> attrs,d,s,children
        | _ -> failwith "createTask: bullet 以外が渡された"

    match d with
    | Some (Direction(attrs, v)) ->
      // BulletML の角度は度。fireCommand と同じく、ここでもラジアンへ直す
      // （この枝は fire 側の上書きで届いていなかったので、変換が落ちていた）
      let value = getValue v * ((float32 Math.PI) / 180.f)
      match attrs with
      | Some attrs ->
        match attrs.directionType with
        | DirectionType.Sequence -> bullet.Dir <- (bulletmlTask.GetFireData().SrcDir + value) |> calcDir
        | DirectionType.Absolute -> bullet.Dir <- value |> calcDir
        | DirectionType.Relative -> bullet.Dir <- (bullet.Dir + value) |> calcDir
        | _ ->
          if bullet.BulletType = BulletType.Player then
            bullet.Dir <- (bullet.GetEnemyAimDir() + value) |> calcDir
          else
            bullet.Dir <- (bullet.GetAimDir() + value) |> calcDir
      | None -> ()
    | None -> ()

    match s with
    | Some (Speed(attrs, v)) ->
      // 上の direction と同じく type で基準が変わる。
      // ここは attrs を束縛して 1 度も読んでおらず、型を無視して代入していた
      //   sequence  前の fire の速さ
      //   relative  この弾の速さ
      //   absolute  そのまま
      let value = getValue v
      match attrs with
      | Some attrs ->
        match attrs.speedType with
        | SpeedType.Sequence -> bullet.Speed <- bulletmlTask.GetFireData().SrcSpeed + value
        | SpeedType.Relative -> bullet.Speed <- bullet.Speed + value
        | _ -> bullet.Speed <- value
      | None -> bullet.Speed <- value
    | None -> ()
    let tasks = children |> List.map cloneProcessable
    let newTask = new BulletmlTask(toProcessable,Tasks = tasks, Original = None)
    // 輪を解く入口は、撃たれた弾の task にも引き継ぐ。
    // 引き継がないと、弾の中に残った bulletRef を誰も解けない
    newTask.ResolveBulletRef <- bulletmlTask.ResolveBulletRef
    newTask.ResolveActionRef <- bulletmlTask.ResolveActionRef
    // <bulletml type> も同じ理由で引き継ぐ。撃たれた弾の task は
    // convertBulletmlTask を通らないので、ここで渡さないと未設定のまま残る。
    // ShootingDirection は enum ではなく DU なので、未設定は 0 ではなく null になる
    newTask.ShootingDirection <- bulletmlTask.ShootingDirection
    if newTask.FireData :> obj = null then
      newTask.FireData  <- new System.Collections.Generic.List<FireData>()
      newTask.FireData.Add(new FireData())
      tasks |> List.iter (fun t -> newTask.FireData.Add(new FireData()))
      newTask.ActiveTaskIndex <- 0
    newTask

  let internal getFinish task = 
    match task with
    | ProcessableBulletml.Accel(pa) -> pa.finish
    | ProcessableBulletml.Action (pa,_) -> pa.finish
    | ProcessableBulletml.Fire(pf,_) -> pf.finish
    | ProcessableBulletml.ChangeDirection(pd) -> pd.finish 
    | ProcessableBulletml.ChangeSpeed(ps) -> ps.finish 
    | ProcessableBulletml.Wait(pw) -> pw.finish 
    | ProcessableBulletml.Vanish(pv) -> pv.finish 
    | ProcessableBulletml.Repeat(pr,_) -> pr.finish 
    | _ -> false

  let internal setFinish task = 
    match task with
    | ProcessableBulletml.Accel(pa) -> pa.finish <- true
    | ProcessableBulletml.Action (pa,_) -> pa.finish <- true
    | ProcessableBulletml.Fire(pf,_) -> pf.finish <- true
    | ProcessableBulletml.ChangeDirection(pd) -> pd.finish <- true 
    | ProcessableBulletml.ChangeSpeed(ps) -> ps.finish <- true 
    | ProcessableBulletml.Wait(pw) -> pw.finish <- true
    | ProcessableBulletml.Vanish(pv) -> pv.finish <- true 
    | ProcessableBulletml.Repeat(pr,_) -> pr.finish <- true 
    | _ -> ()

  let rec internal runCommand (task:ProcessableBulletml) (bulletmlTask:BulletmlTask) (bullet:IBulletmlObject) =
    // Action
    let actionCommand (pa:ProcessableAction) tasks bullet =
      let mutable bullet = bullet
      let mutable stop = false
      let mutable continue' = false
      let mutable num = 0
      let len = List.length tasks
      while num < len && not stop do
        let task = tasks.[num]

        let finish = getFinish task
        if not finish then
          match task with
          // 輪のために展開を止めた actionRef。1 段だけ解いて、この action が
          // 次のフレームから走らせる並びを差し替える。解いた中身のうしろに
          // 残りの兄弟を繋ぐので、輪が末尾でなくても後続が落ちない。
          // 済んだ手前は捨てるので、並びの長さは解くたびに伸びない
          | ProcessableBulletml.ActionRef (attrs, prams) when not (isNull (box bulletmlTask.ResolveActionRef)) ->
            match bulletmlTask.ResolveActionRef attrs.actionRefLabel prams with
            | Some (ProcessableBulletml.Action (_, expanded)) ->
              pa.loop <- Some (expanded @ (tasks |> List.skip (num + 1)))
              stop <- true
            | _ -> ()
          | _ ->
            let c,r = runCommand task bulletmlTask bullet
            bullet <- c
            if r = RunState.Stop then
              stop <- true
            elif r = RunState.Continue then
              continue' <- true
            else
              setFinish task
        num <- num + 1

      if stop then
        bullet, RunState.Stop
      elif continue' then
        bullet, RunState.Continue 
      else
        bullet, RunState.End

    // Repeat
    let repeatCommand (pr:ProcessableRepeat) (actionElm:ProcessableBulletml) =
      let times = pr.times |> function |Times x -> getValue x |> int
      let mutable bullet, stop, continue' = bullet, false, false
      while pr.repeatNum < times && not stop && not continue' do
        let pa, tasks = actionElm |> function
          | ProcessableBulletml.Action (pa, tasks) -> pa,tasks
          | _ -> failwith "repeatCommand: repeat の子が action ではない"
        if not pa.finish then
          let running = match pa.loop with Some t -> t | None -> tasks
          let c,r = actionCommand pa running bullet
          bullet <- c
          if r = RunState.Stop then
            stop <- true
          elif r = RunState.Continue then
            continue' <- true
          else
            pr.repeatNum <- pr.repeatNum + 1
            if pr.repeatNum >= times then
              pa.finish <- true
            else
              running |> Seq.iter (fun t-> t.Init())
        else
          pr.repeatNum <- pr.repeatNum + 1
      if stop then
        bullet, RunState.Stop
      elif continue' then
        bullet, RunState.Continue 
      else
        pr.finish <- true
        bullet, RunState.End

    /// Wait
    let waitCommand pw =
      if (pw.term >= 0.f) then
        pw.term <- pw.term - 1.f
      if (pw.term >= 0.f) then
        bullet, RunState.Stop
      else
        pw.finish <- true
        bullet,RunState.End

    // Fire
    let fireCommand pf bulletElmSrc =

      // 輪のために展開を止めた bulletRef は、ここで 1 段だけ解く。
      // fire のたびに新しい弾と新しい task ができるので、1 段ずつで足りる
      let bulletElm =
        match bulletElmSrc with
        | ProcessableBulletml.BulletRef (attrs, prams) when not (isNull (box bulletmlTask.ResolveBulletRef)) ->
          match bulletmlTask.ResolveBulletRef attrs.bulletRefLabel prams with
          | Some expanded -> expanded
          | None -> bulletElmSrc
        | _ -> bulletElmSrc

      let revise = (float32 Math.PI) / 180.f
      match pf.direction with
      |Some direction ->
        pf.changeDir <- getValue direction.directionValue 
        match direction.directionType with
        | DirectionType.Sequence -> 
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir <- bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir + (pf.changeDir * revise)
        | DirectionType.Absolute -> 
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir <- pf.changeDir * revise
        | DirectionType.Relative -> 
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir <- pf.changeDir * revise + bullet.Dir
        | _ -> 
          if bullet.BulletType = BulletType.Player  then
            bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir <- pf.changeDir * revise + bullet.GetEnemyAimDir()
          else
            bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir <- pf.changeDir * revise + bullet.GetAimDir()
      | None ->
        let revise = (float32 Math.PI) / 180.f
        if bullet.BulletType = BulletType.Player then
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir <- bullet.GetEnemyAimDir() 
        else
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir <- bullet.GetAimDir()
      let newBullet = bullet.GetNewBullet()
      if (newBullet :>obj= null) then
          pf.finish <- true
          bullet,RunState.End
      else
        newBullet.Init()
        newBullet.Task <- createTask bulletElm bulletmlTask newBullet |> Some 
       
        match bulletElm with
        | ProcessableBulletml.Bullet(attr,_,speed,_) ->
          match speed with
          | Some (Speed(sattr,s)) ->
            // createTask と同じ値をここでも入れ直す。type を見ないと
            // relative / sequence が absolute と同じ扱いになる
            let value = getValue s
            newBullet.Speed <-
              match sattr with
              | Some sattr ->
                match sattr.speedType with
                | SpeedType.Sequence -> bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed + value
                | SpeedType.Relative -> bullet.Speed + value
                | _ -> value
              | None -> value
            newBullet.Task |> Option.iter (fun task -> task.FireData.[task.ActiveTaskIndex].SpeedInit <- true)
          | None -> ()
        | _ -> ()

        newBullet.X <- bullet.X
        newBullet.Y <- bullet.Y
        // 向きは fire 側の値で入れる。ただし bullet の中に direction を書いてあれば
        // そちらが勝つ（createTask が読んだ値を、ここで上書きしないようにする）。
        // 同じ bullet の中の speed は上の枝で読み直していて、向きだけ落ちていた
        let bulletHasDirection =
          match bulletElm with
          | ProcessableBulletml.Bullet(_,Some _,_,_) -> true
          | _ -> false
        if not bulletHasDirection then
          newBullet.Dir <- bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir |> calcDir

        if (bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SpeedInit |> not && newBullet.Task |> Option.forall (fun task -> task.FireData.[task.ActiveTaskIndex].SpeedInit)) then
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- newBullet.Speed
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SpeedInit <- true
        else
          match pf.speed with
          | Some speed ->
            pf.changeSpeed <- getValue speed.speedValue
            // 基準は type で違う。上の direction と同じ割り方にしてある
            //   sequence  前の fire の速さ
            //   relative  この弾の速さ
            //   absolute  そのまま
            match speed.speedType with
            | SpeedType.Sequence ->
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed + pf.changeSpeed
            | SpeedType.Relative ->
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- pf.changeSpeed + bullet.Speed
            | _ ->
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- pf.changeSpeed
          | None ->
            if newBullet.Task |> Option.forall (fun task -> task.FireData.[task.ActiveTaskIndex].SpeedInit |> not) then
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- 1.f
            else
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- newBullet.Speed 

        newBullet.Task |> Option.iter(fun task -> task.FireData.[task.ActiveTaskIndex].SpeedInit <- false)
        // 速さも向きと同じで、bullet の中に書いてあればそちらが勝つ。
        // 上の枝で type ごとに入れた値を、ここで踏み潰さないようにする
        let bulletHasSpeed =
          match bulletElm with
          | ProcessableBulletml.Bullet(_,_,Some _,_) -> true
          | _ -> false
        if not bulletHasSpeed then
          newBullet.Speed <- bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed
        pf.finish <- true
        bullet,RunState.End

    // Vanish
    let vanishCommand (pv:ProcessableVanish) =
      bullet.Vanish ()
      pv.finish <- true
      bullet,RunState.End

    let accelCommand (pa:ProcessableAccel) =
      if pa.first then
        pa.first <- false
        pa.term <- getValue pa.initTerm 

        match pa.horizontal.horizontalType with
        | HorizontalType.Sequence ->
          pa.horizontalAccel <- getValue pa.horizontal.horizontalValue 
        | HorizontalType.Relative  ->
          pa.horizontalAccel <- (getValue pa.horizontal.horizontalValue) / pa.term
        | _ ->
          pa.horizontalAccel <- ((getValue pa.horizontal.horizontalValue) - bullet.AccelerationX) / pa.term

        match pa.vertical.verticalType with
        | VerticalType.Sequence ->
          pa.verticalAccel <- getValue pa.vertical.verticalValue 
        | VerticalType.Relative  ->
          pa.verticalAccel <- (getValue pa.vertical.verticalValue) / pa.term
        | _ ->
          pa.verticalAccel <- ((getValue pa.vertical.verticalValue) - bullet.AccelerationY) / pa.term
   
      pa.term <- pa.term - 1.f
      if pa.term < 0.f then
        pa.finish <- true
        bullet,RunState.End
      else
        bullet.AccelerationX <- bullet.AccelerationX + pa.horizontalAccel
        bullet.AccelerationY <- bullet.AccelerationY + pa.verticalAccel
        bullet,RunState.Continue

    let changeDirection (pd:ProcessableDirection) =
      if pd.first then
        pd.first <- false
        pd.term <- getValue pd.initTerm 

        let value = (getValue pd.direction.directionValue |> float) * Math.PI / 180. |> float32 
        let f () = 
          if (pd.changeDir |> float > Math.PI) then
            pd.changeDir <- pd.changeDir - 2.f * (float32 Math.PI)
          if (pd.changeDir |> float < -Math.PI) then
            pd.changeDir <- pd.changeDir + 2.f * (float32 Math.PI)
          pd.changeDir <- pd.changeDir / pd.term

        pd.direction.directionType |> function
        | DirectionType.Sequence -> pd.changeDir <- value
        | x -> 
          x |> function
          | DirectionType.Absolute -> pd.changeDir <- (value - bullet.Dir); f()
          | DirectionType.Relative -> pd.changeDir <- value; f()
          | _ -> 
            if bullet.BulletType = BulletType.Player then
              pd.changeDir <- bullet.GetEnemyAimDir() + value - bullet.Dir; f()
            else
              pd.changeDir <- bullet.GetAimDir() + value - bullet.Dir; f()

      pd.term <- pd.term - 1.f
      bullet.Dir <- (bullet.Dir + pd.changeDir) |> calcDir
      
      if pd.term <= 0.f then
        pd.finish <- true
        pd.term <- getValue pd.initTerm 
        bullet,RunState.End
      else
        bullet,RunState.Continue          

    let changeSpeed (ps:ProcessableSpeed) =
      if ps.first then
        ps.first <- false
        ps.term <- getValue ps.initTerm 
        match ps.speed.speedType with
        | SpeedType.Sequence -> ps.changeSpeed <- getValue ps.speed.speedValue 
        | SpeedType.Relative -> ps.changeSpeed <- (getValue ps.speed.speedValue) / ps.term
        | _ -> ps.changeSpeed <- ((getValue ps.speed.speedValue) - bullet.Speed) / ps.term

      ps.term <- ps.term - 1.f
      bullet.Speed <- bullet.Speed + ps.changeSpeed

      if ps.term <= 0.f then
        ps.finish <- true
        ps.term <- getValue ps.initTerm 
        bullet,RunState.End
      else
        bullet,RunState.Continue

    match task with
    | ProcessableBulletml.Repeat(pr, actionElm) -> repeatCommand pr actionElm
    | ProcessableBulletml.Action(pa,tasks) ->
      if pa.finish then bullet, RunState.End
      else actionCommand pa (match pa.loop with Some t -> t | None -> tasks) bullet
    | ProcessableBulletml.Wait (pw) -> waitCommand pw
    | ProcessableBulletml.Fire (pf,bulletElm) -> fireCommand pf bulletElm
    | ProcessableBulletml.Vanish pv -> vanishCommand pv
    | ProcessableBulletml.Accel(pa) -> accelCommand pa
    | ProcessableBulletml.ChangeDirection (pd) -> changeDirection pd
    | ProcessableBulletml.ChangeSpeed (ps) -> changeSpeed ps
    | _ -> bullet, RunState.End

  [<CompiledName "Run">]
  let run (bullet:IBulletmlObject) =
    match bullet.Task with
    | None ->
        // 返すのは差分。呼ぶ側は足すので、ここで絶対値を返すと座標が膨らむ
        // （膨らむ量は呼ぶ側の係数しだい。同梱では MonoGame が 1 倍、
        //  Unity2D と C# サンプルが 1/100）。
        // 呼ぶ側 4 経路とも Task を先に見ているのでここへは届かないが、
        // ガードを 1 つでも外したら届くので、届いても壊れない形にしておく
        RunResult(true, 0.f, 0.f)
    | Some bulletmlTask ->
      let tasks = bulletmlTask.Tasks
      let mutable bullet = bullet
      // <bulletml type> を弾へ届ける。いまはここまでで、読んで分岐する所はまだ無い。
      // TODO: 縦横で何を変えるかは未決定。構想はあるが、仕様が何も定めていないので
      //       （原典のリファレンス・RELAX・同梱の readme のどれにも書かれていない）、
      //       決めた時点でこの実装が BulletML の意味を定義することになる。
      //       いちばん近い案は absolute 方向の基準を type で回すもの。
      // 未設定は null になりうるので、そのときは弾の値をそのままにする
      if not (isNull (box bulletmlTask.ShootingDirection)) then
        bullet.ShootingDirection <- bulletmlTask.ShootingDirection
      if tasks :> obj <> null then
        // top* は 1 本ずつ独立した task。ある top が wait で止まっても、
        // それはその top の話なので、後ろの top* はこのフレームでも回す
        // （前は Stop で while ごと抜けていて、先頭の wait が後ろを永久に塞いでいた）
        // 終わった task の数だけ数える。Stop / Continue はこの段では何もしない
        // （走査を止めるのに使っていたが、それが後ろの top* を塞いでいた）
        let mutable i, endCount = 0, 0
        let len = Seq.length tasks
        while i < len do
          let task,pa =
            match tasks.[i] with
            | ProcessableBulletml.Action (pa,_) -> tasks.[i],pa
            | _ -> failwith "run: top のタスクが action ではない"
          i <- i + 1
          if not pa.finish then
            let b,r = runCommand task bulletmlTask bullet
            bullet <- b
            match r with
            | RunState.End ->
              pa.finish <- true
              endCount <- endCount + 1
            | _ -> ()
          else
            endCount <- endCount + 1

        let speed = float bullet.Speed
        let direction = float bullet.Dir 
        let x = bullet.AccelerationX + (float32 (Math.Sin(direction) * speed))
        let y = bullet.AccelerationY + (float32 (-Math.Cos(direction) * speed))

        if endCount >= List.length tasks then
          if bullet.IsBullet && bullet.BulletRoot then
            bullet.Used <- false
          bullet.Task |> Option.iter(fun task -> task.Finish <- true)
          RunResult(true, x, y)
        else
          bullet.Task |> Option.iter(fun task -> task.Finish <- false)
          RunResult(false, x, y)
      else
        bullet.Task |> Option.iter(fun task -> task.Finish <- true)
        // 上の None と同じ理由で差分 0。convertBulletmlTask が必ずリストを入れるので
        // ここへ届く作り方が無く、**測れていない**
        RunResult(true, 0.f, 0.f)

  [<CompiledName "ConvertBulletmlTask">]
  let convertBulletmlTask bulletml = 
    if bulletml :> obj = null then BulletmlTask(toProcessable,Tasks = [], Original = None) else
    let recBulletml = IntermediateParser.convertRecBulletml bulletml

    let shootingDirection = 
      match recBulletml with
      | RecBulletml.Bulletml(attrs,_) -> 
        match attrs.bulletmlType with
        | Some x -> x
        | None -> ShootingDirection.BulletVertical  
      | _ -> failwith "convertBulletmlTask: 根が bulletml ではない"

    let tasks = toProcessable bulletml
    let bulletmlTask =
      new BulletmlTask(toProcessable,Tasks = tasks, Original = None)
    bulletmlTask.ResolveBulletRef <- IntermediateParser.expandBulletRefOnce recBulletml
    bulletmlTask.ResolveActionRef <- IntermediateParser.expandActionRefOnce recBulletml
    bulletmlTask.ShootingDirection <- shootingDirection
    if bulletmlTask.FireData :> obj = null then
      bulletmlTask.FireData  <- new System.Collections.Generic.List<FireData>()
      bulletmlTask.FireData.Add(new FireData())
      tasks |> List.iter (fun t -> bulletmlTask.FireData.Add(new FireData()))
      bulletmlTask.ActiveTaskIndex <- 0
    bulletmlTask

  [<CompiledName "ConvertBulletmlTaskOption">]
  let convertBulletmlTaskOption bulletml = 
    convertBulletmlTask bulletml |> Some
 
