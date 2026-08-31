namespace FsBulletML.Core.Tests

open System.Text.RegularExpressions
open NUnit.Framework
open FsBulletML.Processable

/// wait が何フレーム効くかを、発射の間隔として固める。
///
/// **ここは「正しい姿」ではなく「いまの姿」を記録している。**
/// 実装には次の 2 つの癖がある（測ったもの）。
///
///   最初の 1 周だけ間隔が 1 フレーム短い（repeat の有無に関係なく）
///   top action のループは repeat より毎周 1 フレーム多い
///
/// 後者は RunTask が Processed のとき task.Init() する設計から来ているので
/// たぶん意図したもの。
///
/// **前者は不具合の可能性が高い、という判断で現状として記録した**
/// （dba9d27 で測ったもの）。ここでは直さず、現状として控えに記録するだけにしてある。
/// リファクタリングでこの控えが動いたら、それは直ったのかもしれないし
/// 別の壊れ方かもしれない。差分を見て人が決めること。
[<TestFixture>]
[<NonParallelizable>]
type Timing() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  /// 何フレーム目で弾が産まれたかだけを取り出す
  let fireFrames (trace: string) =
    let mutable frame = -1
    [ for line in trace.Split('\n') do
        let m = Regex.Match(line, @"^f(\d+)$")
        if m.Success then frame <- int m.Groups.[1].Value
        elif line.Contains "  +b" then yield frame ]

  let inRepeat wait =
    bml (sprintf """<action label="top">
  <repeat><times>4</times>
    <action>
      <fire><direction type="absolute">0</direction><speed>2</speed><bullet/></fire>
      <wait>%d</wait>
    </action>
  </repeat>
  <wait>30</wait>
</action>""" wait)

  let noRepeat wait =
    bml (sprintf """<action label="top">
  <fire><direction type="absolute">0</direction><speed>2</speed><bullet/></fire>
  <wait>%d</wait>
</action>""" wait)

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 0.0f, 100.0f))

  [<Test>]
  member _.``wait を 1..4 で振った発射フレームと間隔``() =
    let gaps (xs: int list) = xs |> List.pairwise |> List.map (fun (p, q) -> q - p)
    let lines =
      [ for w in 1 .. 4 do
          let a = Trace.run (inRepeat w) 24 |> fireFrames
          let b = Trace.run (noRepeat w) 24 |> fireFrames
          yield sprintf "wait=%d repeat=on  fires=%A gaps=%A" w a (gaps a)
          yield sprintf "wait=%d repeat=off fires=%A gaps=%A" w b (gaps b) ]
    String.concat "\n" lines |> Golden.check "wait-intervals"
