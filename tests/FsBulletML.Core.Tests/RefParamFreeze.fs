namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML
open FsBulletML.Processable

/// ref の param に入れた $rand / $rank が、走るたびに読み直されるか。
///
/// BulletRunner.convertBulletmlTask は IntermediateParser.existRandomParam を見て、
/// **$rand を含む ref がひとつでもあれば Original に生の XML を持たせる**。
/// BulletmlTask.Init() は Original があれば toProcessable から作り直し、
/// 無ければ既にある木の可変フラグを戻すだけ（Processable.fs:272）。
///
/// existRandomParam が探すのは `$rand` の 5 文字だけで、`$rank` は見ていない。
/// **片方だけ守られているなら、値を動かせば控えが割れる。**
[<TestFixture>]
[<NonParallelizable>]
type RefParamFreeze() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  /// 撃った弾の速さだけを抜く。位置は本題ではないので落とす
  let firedSpeeds (trace: string) =
    trace.Split('\n')
    |> Array.filter (fun l -> l.Contains "  +b")
    |> Array.map (fun l -> l.Trim())
    |> String.concat "\n"

  /// top がひと回りするたびに 1 発だけ撃つ。速さは param 経由
  let viaParam expr =
    sprintf """<action label="top">
  <actionRef label="shoot"><param>%s</param></actionRef>
</action>

<action label="shoot">
  <fire><direction type="absolute">0</direction><speed>$1</speed><bullet/></fire>
  <wait>2</wait>
</action>""" expr

  /// 同じ形で param を通さないもの。これが対照
  let bare expr =
    sprintf """<action label="top">
  <fire><direction type="absolute">0</direction><speed>%s</speed><bullet/></fire>
  <wait>2</wait>
</action>""" expr

  /// param を通さず、参照した先に直接 $rank を書く。
  /// actionRef を挟んだこと自体が凍らせるのか、param の置き換えが凍らせるのかを割る
  let refNoParam expr =
    sprintf """<action label="top">
  <actionRef label="shoot"/>
</action>

<action label="shoot">
  <fire><direction type="absolute">0</direction><speed>%s</speed><bullet/></fire>
  <wait>2</wait>
</action>""" expr

  /// ひと回りの中で 3 発。param 経由なので、展開が 1 回なら 3 発とも同じ値になる
  let repeatViaParam =
    """<action label="top">
  <repeat><times>3</times>
    <action><actionRef label="shoot"><param>$rand</param></actionRef></action>
  </repeat>
  <wait>6</wait>
</action>

<action label="shoot">
  <fire><direction type="absolute">0</direction><speed>$1</speed><bullet/></fire>
  <wait>1</wait>
</action>"""

  /// フレーム 4 で値を切り替える。前後で 2 発ずつ撃つ長さにしてある
  let runSwitching (xml: string) (switch: MutableManager -> unit) =
    let m = MutableManager(0.2f, 0.2f, 30.0f, 100.0f)
    BulletMLManager.Init(m)
    let hook i = if i = 4 then switch m
    Trace.runWith hook xml 9 |> firedSpeeds

  [<Test>]
  member _.``対照 1: param を通さない $rank は、走行中に変えると追随する``() =
    runSwitching (bml (bare "1+$rank*10")) (fun m -> m.Rank <- 0.7f)
    |> fun s -> s + "\n\n0.2 のとき 3.0、0.7 のとき 8.0。切り替えはフレーム 4"
    |> Golden.check "freeze-direct-rank"

  [<Test>]
  member _.``対照 2: param を通さない $rand も追随する``() =
    runSwitching (bml (bare "1+$rand*10")) (fun m -> m.Rand <- 0.7f)
    |> fun s -> s + "\n\n0.2 のとき 3.0、0.7 のとき 8.0。切り替えはフレーム 4"
    |> Golden.check "freeze-direct-rand"

  [<Test>]
  member _.``param に置いた $rank は追随するか``() =
    runSwitching (bml (viaParam "$rank")) (fun m -> m.Rank <- 0.7f)
    |> fun s -> s + "\n\n速さ = $1 = $rank。0.2 から 0.7 に変わるなら追随、0.2 のままなら焼き付け"
    |> Golden.check "freeze-param-rank"

  [<Test>]
  member _.``param に置いた $rand は追随するか``() =
    runSwitching (bml (viaParam "$rand")) (fun m -> m.Rand <- 0.7f)
    |> fun s -> s + "\n\n速さ = $1 = $rand。existRandomParam が守っているのはこちらだけ"
    |> Golden.check "freeze-param-rand"

  [<Test>]
  member _.``actionRef の先に直接書いた $rank は追随するか``() =
    runSwitching (bml (refNoParam "1+$rank*10")) (fun m -> m.Rank <- 0.7f)
    |> fun s -> s + "\n\nparam を通さず参照だけ挟んだ形。追随するなら、凍らせているのは param の置き換え"
    |> Golden.check "freeze-ref-noparam-rank"

  [<Test>]
  member _.``param に式ごと入れた $rank は追随するか``() =
    runSwitching (bml (viaParam "1+$rank*10")) (fun m -> m.Rank <- 0.7f)
    |> fun s -> s + "\n\n$rank 単体ではなく式ごと param に入れた形。3.0 のままなら焼き付け"
    |> Golden.check "freeze-param-rank-expr"

  /// existRandomParam が守るのは「top がひと回りしたとき作り直す」ところまで。
  /// ひと回りの中では展開は 1 回なので、$rand は 1 回しか転がらないはず
  [<Test>]
  member _.``ひと回りの中で 3 発撃つと、param の $rand は何回転がるか``() =
    let m = MutableManager(0.2f, 0.2f, 30.0f, 100.0f)
    BulletMLManager.Init(m)
    let hook i =
      if i = 1 then m.Rand <- 0.5f
      elif i = 3 then m.Rand <- 0.9f
    Trace.runWith hook (bml repeatViaParam) 8
    |> firedSpeeds
    |> fun s -> s + "\n\n毎フレーム rand を動かしている（f1 で 0.5、f3 で 0.9）。\n3 発とも同じなら、ひと回りの中では 1 回しか転がっていない"
    |> Golden.check "freeze-rand-within-loop"

  /// 挙動の手前で、機構そのものを直に測る。
  /// convertBulletmlTask が Original を持たせたかどうかは公開されている
  [<Test>]
  member _.``existRandomParam が Original を持たせる条件``() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))
    let probe (name: string) (expr: string) =
      let task = BulletRunner.convertBulletmlTask (readXmlString (bml (viaParam expr)))
      sprintf "  %-24s Original = %s" name (if task.Original.IsSome then "Some（作り直す）" else "None（持ち回る）")
    [ "param の中身と、その BulletML が Original を持たされるか"
      ""
      probe "<param>3</param>" "3"
      probe "<param>$rand</param>" "$rand"
      probe "<param>$rank</param>" "$rank"
      probe "<param>1+$rand*2</param>" "1+$rand*2"
      probe "<param>1+$rank*2</param>" "1+$rank*2"
      ""
      "IntermediateParser.fs:842 の judge は x.Contains(\"$rand\") だけを見る" ]
    |> String.concat "\n"
    |> Golden.check "freeze-original-flag"
