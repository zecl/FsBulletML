namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML
open FsBulletML.IntermediateParser

/// XML を読んで書き戻したとき、属性が生き残るか。
///
/// 既存の `Parser.Tests` の `文字列からのパース` は、入力の文字列と
/// ToXmlStringForTest() の完全一致を見る。ただし TestCase 2 本が持つ属性は
/// xmlns と type だけで、name / description を持つ入力が無い。
///
/// 凍結予測: name を足すと落ちる（往復で消えるので完全一致が崩れる）。
/// 落ちなければ writer のどこかが拾っているので、その読みが外れ。
///
/// 合否ではなく返ってきた文字列そのものを控えにする。
/// どの属性が残ってどれが消えるかが 1 本で見えるので。
[<TestFixture>]
[<NonParallelizable>]
type RoundTrip() =

  let ns = "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml"

  /// 2 つの経路を別々に見る。`Parser.Tests` はこの両方を一致で当てている
  let roundTrip (source: string) =
    let xml = XmlNode.ReadXmlString source
    let bml = xml |> convertBulletmlFromXmlNode
    let viaDtd = bml.ToXmlStringForTest()
    let viaXmlNode = xml.ToXmlString()
    sprintf "  入力      %s\n  DTD 経由  %s  %s\n  XmlNode   %s  %s"
      source
      viaDtd (if viaDtd = source then "一致" else "ちがう")
      viaXmlNode (if viaXmlNode = source then "一致" else "ちがう")

  [<Test>]
  member _.``bulletml の属性は往復で生き残るか``() =
    let attrs = [ ""
                  " name=\"No Name\""
                  " description=\"a sample\""
                  " name=\"No Name\" description=\"a sample\"" ]
    [ yield "既存の TestCase 2 本が持つのは xmlns と type だけ。足すとどうなるか"
      yield ""
      for a in attrs do
        yield roundTrip (sprintf """<bulletml xmlns="%s" type="vertical"%s><bullet /></bulletml>""" ns a)
        yield "" ]
    |> String.concat "\n"
    |> Golden.check "now-roundtrip-bulletml-attrs"
