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
        | _ -> failwith "convert error"

    match d with
    | Some (Direction(attrs, v)) ->
      let value = getValue v
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
      let value = getValue v
      bullet.Speed <- value
    | None -> ()
    let tasks = deepCopyClone children
    let bulletmlTask = new BulletmlTask(toProcessable,Tasks = tasks, Original = None)
    if bulletmlTask.FireData :> obj = null then
      bulletmlTask.FireData  <- new System.Collections.Generic.List<FireData>()
      bulletmlTask.FireData.Add(new FireData())
      tasks |> List.iter (fun t -> bulletmlTask.FireData.Add(new FireData()))
      bulletmlTask.ActiveTaskIndex <- 0
    bulletmlTask

  let internal getFinish task = 
    match task with
    | Processable.Accel(pa) -> pa.finish
    | Processable.Action (pa,_) -> pa.finish
    | Processable.Fire(pf,_) -> pf.finish
    | Processable.ChangeDirection(pd) -> pd.finish 
    | Processable.ChangeSpeed(ps) -> ps.finish 
    | Processable.Wait(pw) -> pw.finish 
    | Processable.Vanish(pv) -> pv.finish 
    | Processable.Repeat(pr,_) -> pr.finish 
    | _ -> false

  let internal setFinish task = 
    match task with
    | Processable.Accel(pa) -> pa.finish <- true
    | Processable.Action (pa,_) -> pa.finish <- true
    | Processable.Fire(pf,_) -> pf.finish <- true
    | Processable.ChangeDirection(pd) -> pd.finish <- true 
    | Processable.ChangeSpeed(ps) -> ps.finish <- true 
    | Processable.Wait(pw) -> pw.finish <- true
    | Processable.Vanish(pv) -> pv.finish <- true 
    | Processable.Repeat(pr,_) -> pr.finish <- true 
    | _ -> ()

  let rec internal runCommand (task:ProcessableBulletml) (bulletmlTask:BulletmlTask) (bullet:IBulletmlObject) =
    // Action
    let actionCommand tasks bullet = 
      let mutable bullet = bullet
      let mutable stop = false
      let mutable continue' = false
      let mutable num = 0
      let len = List.length tasks
      while num < len && not stop do
        let task = tasks.[num] 
        
        let finish = getFinish task
        if not finish then
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
          | Processable.Action (pa, tasks) -> pa,tasks
          | _ -> failwith "error"
        if not pa.finish then
          let c,r = actionCommand tasks bullet
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
              tasks |> Seq.iter (fun t-> t.Init())
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
    let fireCommand pf bulletElm = 

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
        | Processable.Bullet(attr,_,speed,_) -> 
          match speed with
          | Some (Speed(attr,s)) -> 
            newBullet.Speed  <- getValue s
            newBullet.Task |> Option.iter (fun task -> task.FireData.[task.ActiveTaskIndex].SpeedInit <- true)
          | None -> ()
        | _ -> ()

        newBullet.X <- bullet.X
        newBullet.Y <- bullet.Y
        newBullet.Dir <- bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcDir |> calcDir

        if (bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SpeedInit |> not && newBullet.Task |> Option.forall (fun task -> task.FireData.[task.ActiveTaskIndex].SpeedInit)) then
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- newBullet.Speed
          bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SpeedInit <- true
        else
          match pf.speed with
          | Some speed ->
            pf.changeSpeed <- getValue speed.speedValue 
            if (speed.speedType = SpeedType.Sequence || speed.speedType = SpeedType.Relative) then
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed + pf.changeSpeed 
            else
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- pf.changeSpeed 
          | None ->
            if newBullet.Task |> Option.forall (fun task -> task.FireData.[task.ActiveTaskIndex].SpeedInit |> not) then
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- 1.f
            else
              bulletmlTask.FireData.[bulletmlTask.ActiveTaskIndex].SrcSpeed <- newBullet.Speed 

        newBullet.Task |> Option.iter(fun task -> task.FireData.[task.ActiveTaskIndex].SpeedInit <- false)
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
    | Processable.Action(pa,tasks) -> 
      if pa.finish then bullet, RunState.End 
      else actionCommand tasks bullet 
    | Processable.Wait (pw) -> waitCommand pw
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
        bullet.Task |> Option.iter(fun task -> task.Finish <- true)
        RunResult(true, bullet.X, bullet.Y)
    | Some bulletmlTask ->
      let tasks = bulletmlTask.Tasks
      let mutable bullet = bullet
      if tasks :> obj <> null then
        let mutable stop, break', i,endCount = false, false, 0, 0
        let len = Seq.length tasks 
        while i < len && not stop do
          let task,pa = 
            match tasks.[i] with
            | Processable.Action (pa,_) -> tasks.[i],pa
            | _ -> failwith "error"
          i <- i + 1
          if not pa.finish then 
            let b,r = runCommand task bulletmlTask bullet
            bullet <- b
            match r with
            | RunState.End ->
              pa.finish <- true
              endCount <- endCount + 1
            | RunState.Stop ->
              stop <- true
            | RunState.Continue ->
              break' <- true
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
        RunResult(true, bullet.X, bullet.Y)

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
      | _ -> failwith "convert error"

    let tasks = toProcessable bulletml
    let bulletmlTask = 
      if IntermediateParser.existRandomParam recBulletml then
        new BulletmlTask(toProcessable,Tasks = tasks, Original = Some bulletml)
      else
        new BulletmlTask(toProcessable,Tasks = tasks, Original = None)
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
 
