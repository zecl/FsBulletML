namespace FsBulletML.Parser.Tests

open System
open System.Xml 
open FsBulletML
open FsBulletML.IntermediateParser
open NUnit.Framework 
open FsUnit

module ``文字列からのパース`` =

  let parseFromString source =
    let xml = XmlNode.ReadXmlString source 
    let bml = xml |> convertBulletmlFromXmlNode 
    source |> should equal (bml.ToXmlStringForTest())
    source |> should equal (xml.ToXmlString())

  [<TestCase("""<bulletml xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml" type="vertical"><bullet /></bulletml>""")>]
  [<TestCase("""<bulletml xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml" type="vertical"><action><changeDirection><direction type="aim">0</direction><term>60-$rank*20</term></changeDirection><wait>5</wait></action></bulletml>""")>]
  let ``文字列からのパース`` (source) = 
    parseFromString source

module ``XMLファイル:BulletML DTDに基づくパース`` = 

  let parse xmlFile =
    let ignoreWhitespace = XmlNode.ReadIgnoreWhitespaceString xmlFile
    let indented = XmlNode.ReadIndentedString xmlFile 
    let xml = XmlNode.ReadXml xmlFile 
    let bulletml = tryBulletmlFromXmlNode xml
    let bml = xml |> convertBulletmlFromXmlNode 
    let x = bml |> (fun x -> x.ToIndentedXmlStringForTest(), x.ToXmlStringForTest())
    x |> should equal (xml.ToIndentedXmlString(), xml.ToXmlString())
    x |> should equal (indented, ignoreWhitespace)

  let parseDeclaration xmlFile =
    let ignoreWhitespace = XmlNode.ReadIgnoreWhitespaceString xmlFile
    let indented = XmlNode.ReadIndentedString xmlFile
    let xml = XmlNode.ReadXml xmlFile  
    let bulletml = tryBulletmlFromXmlNode xml
    match bulletml with 
    | Some bulletml ->
      let x = bulletml |> (fun x -> x.ToIndentedXmlStringForTest(encodingAndDoctype=EncodingAndDoctype.Exist), x.ToXmlStringForTest(EncodingAndDoctype.Exist))
      x |> should equal (xml.ToIndentedXmlString(encodingAndDoctype=EncodingAndDoctype.Exist), xml.ToXmlString(EncodingAndDoctype.Exist))
      x |> should equal (indented, ignoreWhitespace)
    | None -> failwith "convert error."

  // <!ELEMENT vertical (#PCDATA)>
  let VerticalElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\vertical\elements\success\vertical-text-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\vertical\elements\failure\vertical-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  // <!ATTLIST vertical type (absolute|relative|sequence) "absolute">
  let VerticalAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\vertical\attributes\success\vertical-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\vertical\attributes\success\vertical-type-absolute.xml")
          &> (singleParam @"..\..\..\TestData\xml\vertical\attributes\success\vertical-type-relative.xml")
          &> (singleParam @"..\..\..\TestData\xml\vertical\attributes\success\vertical-type-sequence.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\vertical\attributes\failure\vertical-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "VerticalElementsCase">]
  let ``vertical-要素`` (xmlFile) = 
    parse xmlFile


  [<Test>]
  [<TestCaseSource "VerticalAttributesCase">]
  let ``vertical-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT fireRef (param*)>
  let FireRefElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\fireRef\elements\success\fireRef-param-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\fireRef\elements\success\fireRef-param-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\fireRef\elements\success\fireRef-param-multi.xml")
    // not support
  
  // <!ATTLIST fireRef label CDATA #REQUIRED>
  let FireRefAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\fireRef\attributes\success\fireRef-label-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\fireRef\attributes\failure\fireRef-label-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "FireRefElementsCase">]
  let ``fireRef-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "FireRefAttributesCase">]
  let ``fireRef-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT changeDirection (direction, term)>
  let ChangeDirectionElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\changeDirection\elements\success\changeDirection-direction-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\changeDirection\elements\success\changeDirection-term-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\changeDirection\elements\failure\changeDirection-direction-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\changeDirection\elements\failure\changeDirection-term-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  let ChangeDirectionAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\changeDirection\attributes\failure\changeDirection-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "ChangeDirectionElementsCase">]
  let ``changeDirection-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "ChangeDirectionAttributesCase">]
  let ``changeDirection-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT bulletml (bullet | fire | action)*>
  let BulletmlElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\bulletml\elements\success\bulletml-bullet.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\elements\success\bulletml-action.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\elements\success\bulletml-fire.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\elements\success\bulletml-multi.xml")
    // not support

  // <!ATTLIST bulletml xmlns CDATA #IMPLIED>
  // <!ATTLIST bulletml type (none|vertical|horizontal) "none">
  let BulletmlAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\bulletml\attributes\success\bulletml-xmlns-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\attributes\success\bulletml-xmlns-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\attributes\success\bulletml-type-none.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\attributes\success\bulletml-type-vertical.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\attributes\success\bulletml-type-horizontal.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\attributes\success\bulletml-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletml\attributes\success\bulletml-xmlns-type-nothing.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\bulletml\attributes\failure\bulletml-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "BulletmlElementsCase">]
  let ``bulletml-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "BulletmlAttributesCase">]
  let ``bulletml-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT param (#PCDATA)>
  let ParamElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\param\elements\success\param-actionRef-text-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\param\elements\success\param-fireRef-text-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\param\elements\success\param-bulletRef-text-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\param\elements\failure\param-actionRef-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\param\elements\failure\param-fireRef-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\param\elements\failure\param-bulletRef-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  let ParamAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\param\attributes\failure\param-actionRef-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\param\attributes\failure\param-fireRef-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\param\attributes\failure\param-bulletRef-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "ParamElementsCase">]
  let ``param-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "ParamAttributesCase">]
  let ``param-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT actionRef (param*)>
  let ActionRefElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\actionRef\elements\success\actionRef-param-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\actionRef\elements\success\actionRef-param-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\actionRef\elements\success\actionRef-param-multi.xml")
    // not support

  // <!ATTLIST actionRef label CDATA #REQUIRED>
  let ActionRefAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\actionRef\attributes\success\actionRef-label-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\actionRef\attributes\failure\actionRef-label-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "ActionRefElementsCase">]
  let ``actionRef-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "ActionRefAttributesCase">]
  let ``actionRef-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT repeat (times, (action | actionRef))>
  let RepeatElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\repeat\elements\success\repeat-times-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\repeat\elements\success\repeat-action.xml")
          &> (singleParam @"..\..\..\TestData\xml\repeat\elements\success\repeat-actionRef.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\repeat\elements\failure\repeat-times-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\repeat\elements\failure\repeat-actionMulti.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\repeat\elements\failure\repeat-actionAndactionRef-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  // <!ATTLIST bullet label CDATA #IMPLIED>
  let RepeatAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\repeat\attributes\failure\repeat-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "RepeatElementsCase">]
  let ``repeat-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "RepeatAttributesCase">]
  let ``repeat-属性`` (xmlFile) = 
    parse xmlFile
    
  // <!ELEMENT accel (horizontal?, vertical?, term)>
  let AccelElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\accel\elements\success\accel-horizontal-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\accel\elements\success\accel-horizontal-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\accel\elements\success\accel-vertical-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\accel\elements\success\accel-vertical-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\accel\elements\success\accel-term.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\accel\elements\failure\accel-term-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  let AccelAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\accel\attributes\failure\accel-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "AccelElementsCase">]
  let ``accel-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "AccelAttributesCase">]
  let ``accel-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT times (#PCDATA)>
  let TimesElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\times\elements\success\times-repeat-text-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\times\elements\failure\times-repeat-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  let TimesAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\times\attributes\failure\times-repeat-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "TimesElementsCase">]
  let ``times-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "TimesAttributesCase">]
  let ``times-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT term (#PCDATA)>
  let TermElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\term\elements\success\term-changeDirection-text-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\term\elements\success\term-changeSpeed-text-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\term\elements\success\term-accel-text-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\term\elements\failure\term-changeDirection-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\term\elements\failure\term-changeSpeed-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\term\elements\failure\term-accel-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  let TermAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\term\attributes\failure\term-changeDirection-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\term\attributes\failure\term-changeSpeed-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\term\attributes\failure\term-accel-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "TermElementsCase">]
  let ``term-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "TermAttributesCase">]
  let ``term-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT wait (#PCDATA)>
  let WaitElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\wait\elements\success\wait-text-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\wait\elements\failure\wait-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  let WaitAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\wait\attributes\failure\wait-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "WaitElementsCase">]
  let ``wait-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "WaitAttributesCase">]
  let ``wait-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT action (changeDirection | accel | vanish | changeSpeed | repeat | wait | (fire | fireRef) | (action | actionRef))*>
  let ActionElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\action\elements\success\action-changeDirection.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-accel.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-vanish.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-changeSpeed.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-repeat.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-wait.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-fire.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-fireRef.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-action.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-actionRef.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\elements\success\action-multi.xml")
    // not support

  // <!ATTLIST action label CDATA #IMPLIED>
  let ActionAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\action\attributes\success\action-label-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\action\attributes\success\action-label-nothing.xml")
    // not support

  [<Test>]
  [<TestCaseSource "ActionElementsCase">]
  let ``action-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "ActionAttributesCase">]
  let ``action-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT vanish (#PCDATA)>
  let VanishElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\vanish\elements\success\vanish-text-nothing.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\vanish\elements\failure\vanish-text-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  let VanishAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\vanish\attributes\failure\vanish-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "VanishElementsCase">]
  let ``vanish-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "VanishAttributesCase">]
  let ``vanish-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT speed (#PCDATA)>
  let SpeedElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\speed\elements\success\speed-fire-text-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\elements\success\speed-bullet-text-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\elements\success\speed-changeSpeed-text-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\speed\elements\failure\speed-fire-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\speed\elements\failure\speed-bullet-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\speed\elements\failure\speed-changeSpeed-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  // <!ATTLIST speed type (absolute|relative|sequence) "absolute">
  let SpeedAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-fire-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-fire-type-absolute.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-fire-type-relative.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-fire-type-sequence.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-bullet-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-bullet-type-absolute.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-bullet-type-relative.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-bullet-type-sequence.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-changeSpeed-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-changeSpeed-type-absolute.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-changeSpeed-type-relative.xml")
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\success\speed-changeSpeed-type-sequence.xml")

    // not support
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\failure\speed-fire-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\failure\speed-bullet-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\speed\attributes\failure\speed-changeSpeed-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "SpeedElementsCase">]
  let ``speed-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "SpeedAttributesCase">]
  let ``speed-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT horizontal (#PCDATA)>
  let HorizontalElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\horizontal\elements\success\horizontal-text-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\horizontal\elements\failure\horizontal-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  // <!ATTLIST horizontal type (absolute|relative|sequence) "absolute">
  let HorizontalAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\horizontal\attributes\success\horizontal-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\horizontal\attributes\success\horizontal-type-absolute.xml")
          &> (singleParam @"..\..\..\TestData\xml\horizontal\attributes\success\horizontal-type-relative.xml")
          &> (singleParam @"..\..\..\TestData\xml\horizontal\attributes\success\horizontal-type-sequence.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\horizontal\attributes\failure\horizontal-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "HorizontalElementsCase">]
  let ``horizontal-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "HorizontalAttributesCase">]
  let ``horizontal-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT bullet (direction?, speed?, (action | actionRef)*)>
  let BulletElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\bullet\elements\success\bullet-direction-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\bullet\elements\success\bullet-direction-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\bullet\elements\success\bullet-speed-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\bullet\elements\success\bullet-speed-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\bullet\elements\success\bullet-action.xml")
          &> (singleParam @"..\..\..\TestData\xml\bullet\elements\success\bullet-actionRef.xml")
          &> (singleParam @"..\..\..\TestData\xml\bullet\elements\success\bullet-actionMulti.xml")
          &> (singleParam @"..\..\..\TestData\xml\bullet\elements\success\bullet-actionAndactionRef-nothing.xml")
    // not support

  // <!ATTLIST bullet label CDATA #IMPLIED>
  let BulletAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\bullet\attributes\success\bullet-label-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\bullet\attributes\success\bullet-label-nothing.xml")
    // not support

  [<Test>]
  [<TestCaseSource "BulletElementsCase">]
  let ``bullet-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "BulletAttributesCase">]
  let ``bullet-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT direction (#PCDATA)>
  let DirectionElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\direction\elements\success\direction-fire-text-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\elements\success\direction-bullet-text-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\elements\success\direction-changeDirection-text-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\direction\elements\failure\direction-fire-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\direction\elements\failure\direction-bullet-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\direction\elements\failure\direction-changeDirection-text-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  // <!ATTLIST direction type (aim|absolute|relative|sequence) "aim">
  let DirectionAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-fire-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-fire-type-aim.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-fire-type-absolute.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-fire-type-relative.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-fire-type-sequence.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-bullet-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-bullet-type-aim.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-bullet-type-absolute.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-bullet-type-relative.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-bullet-type-sequence.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-changeDirection-type-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-changeDirection-type-aim.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-changeDirection-type-absolute.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-changeDirection-type-relative.xml")
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\success\direction-changeDirection-type-sequence.xml")

    // not support
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\failure\direction-fire-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\failure\direction-bullet-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\direction\attributes\failure\direction-changeDirection-type-hoge.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "DirectionElementsCase">]
  let ``direction-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "DirectionAttributesCase">]
  let ``direction-属性`` (xmlFile) = 
    parse xmlFile

  //<!ELEMENT changeSpeed (speed, term)>
  let ChangeSpeedElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\changeSpeed\elements\success\changeSpeed-speed-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\changeSpeed\elements\success\changeSpeed-term-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\changeSpeed\elements\failure\changeSpeed-speed-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)
          &> (singleParam @"..\..\..\TestData\xml\changeSpeed\elements\failure\changeSpeed-term-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  let ChangeSpeedAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\xml\changeSpeed\attributes\failure\changeSpeed-attribute-exist.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "ChangeSpeedElementsCase">]
  let ``changeSpeed-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "ChangeSpeedAttributesCase">]
  let ``changeSpeed-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT fire (direction?, speed?, (bullet | bulletRef))>
  let FireElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\fire\elements\success\fire-direction-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\fire\elements\success\fire-direction-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\fire\elements\success\fire-speed-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\fire\elements\success\fire-speed-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\fire\elements\success\fire-bullet.xml")
          &> (singleParam @"..\..\..\TestData\xml\fire\elements\success\fire-bulletRef.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\fire\elements\failure\fire-bulletAndbulletRef-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  // <!ATTLIST fire label CDATA #IMPLIED>
  let FireAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\fire\attributes\success\fire-label-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\fire\attributes\success\fire-label-nothing.xml")
    // not support

  [<Test>]
  [<TestCaseSource "FireElementsCase">]
  let ``fire-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "FireAttributesCase">]
  let ``fire-属性`` (xmlFile) = 
    parse xmlFile

  // <!ELEMENT bulletRef (param*)>
  let BulletRefElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\bulletRef\elements\success\bulletRef-param-exist.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletRef\elements\success\bulletRef-param-nothing.xml")
          &> (singleParam @"..\..\..\TestData\xml\bulletRef\elements\success\bulletRef-param-multi.xml")
    // not support

  // <!ATTLIST bulletRef label CDATA #REQUIRED>
  let BulletRefAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\bulletRef\attributes\success\bulletRef-label-exist.xml")
    // not support
          &> (singleParam @"..\..\..\TestData\xml\bulletRef\attributes\failure\bulletRef-label-nothing.xml")
          &! (typeof<BulletmlDTDViolationException>)

  [<Test>]
  [<TestCaseSource "BulletRefElementsCase">]
  let ``bulletRef-要素`` (xmlFile) = 
    parse xmlFile

  [<Test>]
  [<TestCaseSource "BulletRefAttributesCase">]
  let ``bulletRef-属性`` (xmlFile) = 
    parse xmlFile


  //非決定値
  let InconclusiveElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\xml\inconclusive\elements\success\rand.xml")
          &> (singleParam @"..\..\..\TestData\xml\inconclusive\elements\success\rank.xml")

  [<Test>]
  [<TestCaseSource "InconclusiveElementsCase">]
  let ``非決定値 rand,rank`` (xmlFile) = 
    parse xmlFile

  [<TestCase(@"..\..\..\TestData\xml\sample\5way.xml")>]
  [<TestCase(@"..\..\..\TestData\xml\sample\readTest.xml")>]
  let ``その他-サンプル等`` (xmlFile) = 
    parse xmlFile

