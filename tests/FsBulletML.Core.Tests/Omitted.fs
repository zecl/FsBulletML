namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// 省略できるはずの要素を省いたとき、式を書いたとき、深く入れ子にしたとき。
/// どれも DTD が許している形で、リファクタリングで境界が動きやすい。
[<TestFixture>]
[<NonParallelizable>]
type Omitted() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  /// 落ちた場合もその場で控えに残す。落ちるなら落ちるで、それが「いまの姿」。
  let runOr frames xml =
    try Trace.run xml frames
    with e ->
      let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
      let i = inner e
      sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r", "").Replace("\n", " "))

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  /// DTD: <!ELEMENT changeDirection (direction, term)> なので term は必須。
  /// 省いたときに何が起きるかを固める。
  [<Test>]
  member _.``changeDirection の term を省く``() =
    bml """<action label="top">
  <fire>
    <direction type="absolute">0</direction><speed>1</speed>
    <bullet>
      <action>
        <changeDirection><direction type="absolute">90</direction></changeDirection>
        <wait>10</wait>
      </action>
    </bullet>
  </fire>
  <wait>20</wait>
</action>"""
    |> runOr 6 |> Golden.check "term-omitted-changedirection"

  [<Test>]
  member _.``changeSpeed の term を省く``() =
    bml """<action label="top">
  <fire>
    <direction type="absolute">0</direction><speed>1</speed>
    <bullet>
      <action>
        <changeSpeed><speed>3</speed></changeSpeed>
        <wait>10</wait>
      </action>
    </bullet>
  </fire>
  <wait>20</wait>
</action>"""
    |> runOr 6 |> Golden.check "term-omitted-changespeed"

  /// repeat の times は #PCDATA なので式が書ける。$rand を混ぜた場合も見る。
  [<Test>]
  member _.``repeat の times に式を書く``() =
    let case times =
      bml (sprintf """<action label="top">
  <repeat><times>%s</times>
    <action>
      <fire><direction type="sequence">20</direction><speed>2</speed><bullet/></fire>
      <wait>1</wait>
    </action>
  </repeat>
  <wait>20</wait>
</action>""" times)
      |> runOr 12
      |> fun t -> t.Split('\n') |> Array.filter (fun l -> l.Contains "  +b") |> Array.length
    [ sprintf "times=3        撃った数 %d" (case "3")
      sprintf "times=1+2      撃った数 %d" (case "1+2")
      sprintf "times=2*2      撃った数 %d" (case "2*2")
      sprintf "times=$rand*4  撃った数 %d" (case "$rand*4")   // rand=0.5 なので 2
      sprintf "times=0        撃った数 %d" (case "0")
      sprintf "times=7/2      撃った数 %d" (case "7/2") ]     // 3.5 を int にすると
    |> String.concat "\n"
    |> Golden.check "repeat-times-expr"

  /// action を深く入れ子にする。展開が深さで壊れないかを見る。
  [<Test>]
  member _.``action を 5 段入れ子にする``() =
    let rec nest n inner =
      if n = 0 then inner
      else nest (n - 1) (sprintf "<action>\n%s\n</action>" inner)
    let body =
      sprintf """<action label="top">
%s
  <wait>20</wait>
</action>"""
        (nest 5 """<fire><direction type="absolute">45</direction><speed>2</speed><bullet/></fire>""")
    bml body |> runOr 4 |> Golden.check "nested-action-depth"
