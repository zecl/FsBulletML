namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// fire の direction / speed を省いたとき、bullet と action の組み合わせ。
/// DTD は <!ELEMENT fire (direction?, speed?, (bullet | bulletRef))> なので
/// direction も speed も省ける。省いたときに何を引き継ぐかが要点。
[<TestFixture>]
[<NonParallelizable>]
type FireShapes() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  let runOr frames xml =
    try Trace.run xml frames
    with e ->
      let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
      let i = inner e
      sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r", "").Replace("\n", " "))

  /// 撃った弾の向きと速さだけ抜き出す
  let firedBullets (trace: string) =
    trace.Split('\n')
    |> Array.filter (fun l -> l.Contains "  +b")
    |> Array.map (fun l -> l.Trim())
    |> String.concat "\n"

  /// samples から実物を 1 本 引く。ビルドが吐いたコピーは外す（Corpus.fs と同じ 4 つ）
  let realSample (name: string) =
    let dir = System.IO.Path.GetFullPath(System.IO.Path.Combine(__SOURCE_DIRECTORY__, "..", "..", "samples"))
    System.IO.Directory.EnumerateFiles(dir, name, System.IO.SearchOption.AllDirectories)
    |> Seq.filter (fun p ->
        let s = p.Replace('\\', '/')
        [ "/bin/"; "/obj/"; "/Library/"; "/Temp/" ] |> List.forall (s.Contains >> not))
    |> Seq.sort
    |> Seq.tryHead

  [<SetUp>]
  member _.SetUp() =
    // 自機は (30,100)。aim = atan2(30,-100) = 2.850
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  /// direction も speed も省いた fire。何が既定になるか。
  [<Test>]
  member _.``fire の direction と speed を省く``() =
    let case body = bml body |> runOr 4 |> firedBullets
    [ "direction も speed も省く:"
      case """<action label="top">
  <fire><bullet/></fire>
  <wait>10</wait>
</action>"""
      ""
      "direction だけ省く（speed=3）:"
      case """<action label="top">
  <fire><speed>3</speed><bullet/></fire>
  <wait>10</wait>
</action>"""
      ""
      "speed だけ省く（direction=45）:"
      case """<action label="top">
  <fire><direction type="absolute">45</direction><bullet/></fire>
  <wait>10</wait>
</action>"""
      ""
      "aim = 2.850 / 45 度 = 0.785" ]
    |> String.concat "\n"
    |> Golden.check "fire-omitted-parts"

  /// 2 発つづけて撃つとき、2 発めが 1 発めの向き・速さを引き継ぐか。
  /// sequence を使わずに省略だけで見る。
  [<Test>]
  member _.``続けて撃つとき、省略した向きと速さを引き継ぐか``() =
    bml """<action label="top">
  <fire><direction type="absolute">45</direction><speed>3</speed><bullet/></fire>
  <wait>1</wait>
  <fire><bullet/></fire>
  <wait>1</wait>
  <fire><bullet/></fire>
  <wait>10</wait>
</action>"""
    |> runOr 8 |> firedBullets |> Golden.check "fire-inherit"

  /// bullet の中に action を書き、その action からさらに fire する。
  /// 親の向きを relative でどう見るかを確かめる。
  [<Test>]
  member _.``bullet の action から relative で撃つと、親の向きが基準になる``() =
    bml """<action label="top">
  <fire>
    <direction type="absolute">90</direction><speed>1</speed>
    <bullet>
      <action>
        <fire>
          <direction type="relative">45</direction>
          <speed type="relative">1</speed>
          <bullet/>
        </fire>
        <wait>10</wait>
      </action>
    </bullet>
  </fire>
  <wait>20</wait>
</action>"""
    |> runOr 5 |> firedBullets |> Golden.check "bullet-action-relative"

  /// 上で親 speed=1 に relative 1 をかけて s=1.000 になった。
  /// 親を基準にしているなら 2 のはず。何を基準にしているかを、親の速さを振って調べる。
  [<Test>]
  member _.``fire の speed relative は何を基準にしているか``() =
    let case parentSpeed rel =
      bml (sprintf """<action label="top">
  <fire>
    <direction type="absolute">0</direction><speed>%s</speed>
    <bullet>
      <action>
        <fire>
          <direction type="absolute">0</direction>
          <speed type="relative">%s</speed>
          <bullet/>
        </fire>
        <wait>10</wait>
      </action>
    </bullet>
  </fire>
  <wait>20</wait>
</action>""" parentSpeed rel)
      |> runOr 5 |> firedBullets
      |> fun s -> s.Split('\n') |> Array.tryItem 1 |> Option.defaultValue "2 発めが無い"
    [ sprintf "親 speed=1 relative 1  ->  %s" (case "1" "1")
      sprintf "親 speed=3 relative 1  ->  %s" (case "3" "1")
      sprintf "親 speed=5 relative 2  ->  %s" (case "5" "2")
      "親を基準なら 2 / 4 / 7。0 を基準なら 1 / 1 / 2" ]
    |> String.concat "\n"
    |> Golden.check "fire-speed-relative-base"

  /// 同じ label を 2 つ書いたとき。DTD は重複を禁じていないが、参照は 1 つしか指せない。
  [<Test>]
  member _.``同じ label の action を 2 つ書く``() =
    bml """<action label="top">
  <actionRef label="dup"/>
  <wait>10</wait>
</action>
<action label="dup">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
</action>
<action label="dup">
  <fire><direction type="absolute">90</direction><speed>9</speed><bullet/></fire>
</action>"""
    |> runOr 4 |> firedBullets |> Golden.check "duplicate-label"

  /// 存在しない label を参照したとき。
  [<Test>]
  member _.``存在しない label を参照する``() =
    bml """<action label="top">
  <actionRef label="nowhere"/>
  <wait>10</wait>
</action>"""
    |> runOr 3 |> Golden.check "missing-label"

  /// bullet 直下の speed type を createTask が見ているか。
  ///
  /// 直す前は attrs を束縛して 1 度も読まず、型を無視して代入していた
  /// （direction は同じ関数の 20 行 上で 4 分岐している）。
  /// 実物の当てる先は air_elemental の <bullet label="spiral"> 1 個だけなので、
  /// ここは組み立てた BulletML で 3 つの type を並べて固定する
  [<Test>]
  member _.``bullet 直下の speed type を見ているか``() =
    // root は速さ 0 なので、root から撃つと relative の差が出ない（7 で踏んだ形）。
    // 速さ 3 の弾を 1 発 撃ち、その弾の中から 2 発めを撃つ
    let case parentSpeed t =
      bml (sprintf """<action label="top">
  <fire>
    <direction type="absolute">0</direction><speed>%s</speed>
    <bullet>
      <action>
        <fire>
          <direction type="absolute">0</direction>
          <bullet><speed type="%s">1</speed><action><wait>10</wait></action></bullet>
        </fire>
        <wait>10</wait>
      </action>
    </bullet>
  </fire>
  <wait>20</wait>
</action>""" parentSpeed t)
      |> runOr 5 |> firedBullets
      |> fun s -> s.Split('\n') |> Array.tryItem 1 |> Option.defaultValue "2 発めが無い"
    [ sprintf "親 speed=3  bullet speed absolute 1  ->  %s" (case "3" "absolute")
      sprintf "親 speed=3  bullet speed relative 1  ->  %s" (case "3" "relative")
      sprintf "親 speed=3  bullet speed sequence 1  ->  %s" (case "3" "sequence")
      ""
      "absolute なら 1 / relative は親の 3 + 1 = 4 / sequence は前の fire + 1" ]
    |> String.concat "\n"
    |> Golden.check "bullet-speed-type"

  /// 7（speed type="relative"）の当てる先 7 本を、実物で全部 回す。
  ///
  /// 組み立てた BulletML の控えは 2 本あったが、**実物には 1 本も無かった**。
  /// corpus-smoke は「撃ったか」しか数えないので、速さの変化はそこに映らない
  /// （門が覆っていない先を「変わらない」と読んでいた）。
  ///
  /// 撃った弾の速さだけを集める。**当てる先が在ることと、値が動くことは別**なので、
  /// ここは「7 本を回すと何が出るか」を固定する
  [<Test>]
  member _.``実物 7 本の speed relative``() =
    let names =
      [ "[Bulletsmorph]_aba_1.xml"; "[Bulletsmorph]_aba_3.xml"; "[Bulletsmorph]_aba_7.xml"
        "[Original]_air_elemental.xml"; "[Original]_hajike.xml"; "[Original]_kunekune.xml"
        "[OtakuTwo]_self-0012.xml" ]
    let rows =
      names
      |> List.map (fun n ->
          match realSample n with
          | None -> sprintf "%-32s ★samples に無い" n
          | Some p ->
            let speeds =
              System.IO.File.ReadAllText p
              |> runOr 200
              |> fun t ->
                  t.Split('\n')
                  |> Array.filter (fun l -> l.Contains "  +b")
                  |> Array.map (fun l ->
                      let m = System.Text.RegularExpressions.Regex.Match(l, @"s=([-\d.]+)")
                      if m.Success then m.Groups.[1].Value else "?")
            sprintf "%-32s 撃った %3d 発  速さ %s"
              n speeds.Length
              (speeds |> Array.distinct |> Array.sort |> String.concat " "))
    // 0 発だらけなら網か走査が壊れている。締めの 1 行で見えるようにする
    let fired = rows |> List.filter (fun r -> not (r.Contains "撃った   0 発")) |> List.length
    (rows @ [ sprintf "―― 7 本中 %d 本が撃った（0 なら走査か網が壊れている）" fired ])
    |> String.concat "\n"
    |> Golden.check "real-speed-relative-7"
