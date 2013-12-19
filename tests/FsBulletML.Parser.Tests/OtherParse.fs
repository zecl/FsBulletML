namespace FsBulletML.Parser.Tests

open System
open System.Xml 
open NUnit.Framework 
open FsBulletML
open FsBulletML.IntermediateParser
open FParsec
open FsUnit

module ``XML, SXML, FSBファイルパース結果比較`` = 
  let parse file =
    let xmlFile = String.Format( file, "xml")
    let sxmlFile = String.Format( file, "sxml")
    let fsbFile = String.Format( file, "fsb")
    let xml = XmlNode.ReadXml xmlFile 
    let sxml = match Sxml.parseFromFile sxmlFile with 
               | Success (r,_,_) -> r
               | Failure (_,_,_) -> failwith "sxml error"

    let fsb = match Offside.parseFromFile fsbFile with 
               | Success (r,_,_) -> r
               | Failure (_,_,_) -> failwith "fsb error"

    sxml |> should equal xml
    fsb |> should equal xml

  // <!ELEMENT accel (horizontal?, vertical?, term)>
  let AccelElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\accel\elements\success\accel-horizontal-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\accel\elements\success\accel-horizontal-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\accel\elements\success\accel-vertical-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\accel\elements\success\accel-vertical-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\accel\elements\success\accel-term.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\accel\elements\failure\accel-term-nothing.{0}")

  let AccelAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\accel\attributes\failure\accel-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "AccelElementsCase">]
  let ``accel-要素`` (format) = 
    parse format 

  [<Test>]
  [<TestCaseSource "AccelAttributesCase">]
  let ``accel-属性`` (format) = 
    parse format

  // <!ELEMENT action (changeDirection | accel | vanish | changeSpeed | repeat | wait | (fire | fireRef) | (action | actionRef))*>
  let ActionElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-changeDirection.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-accel.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-vanish.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-changeSpeed.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-repeat.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-wait.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-fire.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-fireRef.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-action.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-actionRef.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\elements\success\action-multi.{0}")
    // not support

  // <!ATTLIST action label CDATA #IMPLIED>
  let ActionAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\action\attributes\success\action-label-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\action\attributes\success\action-label-nothing.{0}")
    // not support

  [<Test>]
  [<TestCaseSource "ActionElementsCase">]
  let ``action-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "ActionAttributesCase">]
  let ``action-属性`` (format) = 
    parse format


  // <!ELEMENT actionRef (param*)>
  let ActionRefElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\actionRef\elements\success\actionRef-param-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\actionRef\elements\success\actionRef-param-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\actionRef\elements\success\actionRef-param-multi.{0}")
    // not support

  // <!ATTLIST actionRef label CDATA #REQUIRED>
  let ActionRefAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\actionRef\attributes\success\actionRef-label-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\actionRef\attributes\failure\actionRef-label-nothing.{0}")

  [<Test>]
  [<TestCaseSource "ActionRefElementsCase">]
  let ``actionRef-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "ActionRefAttributesCase">]
  let ``actionRef-属性`` (format) = 
    parse format

  // <!ELEMENT bullet (direction?, speed?, (action | actionRef)*)>
  let BulletElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\bullet\elements\success\bullet-direction-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bullet\elements\success\bullet-direction-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bullet\elements\success\bullet-speed-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bullet\elements\success\bullet-speed-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bullet\elements\success\bullet-action.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bullet\elements\success\bullet-actionRef.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bullet\elements\success\bullet-actionMulti.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bullet\elements\success\bullet-actionAndactionRef-nothing.{0}")
    // not support

  // <!ATTLIST bullet label CDATA #IMPLIED>
  let BulletAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\bullet\attributes\success\bullet-label-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bullet\attributes\success\bullet-label-nothing.{0}")
    // not support

  [<Test>]
  [<TestCaseSource "BulletElementsCase">]
  let ``bullet-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "BulletAttributesCase">]
  let ``bullet-属性`` (format) = 
    parse format

  // <!ELEMENT bulletml (bullet | fire | action)*>
  let BulletmlElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\bulletml\elements\success\bulletml-bullet.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\elements\success\bulletml-action.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\elements\success\bulletml-fire.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\elements\success\bulletml-multi.{0}")
    // not support

  // <!ATTLIST bulletml xmlns CDATA #IMPLIED>
  // <!ATTLIST bulletml type (none|vertical|horizontal) "none">
  let BulletmlAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\bulletml\attributes\success\bulletml-xmlns-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\attributes\success\bulletml-xmlns-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\attributes\success\bulletml-type-none.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\attributes\success\bulletml-type-vertical.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\attributes\success\bulletml-type-horizontal.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\attributes\success\bulletml-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\attributes\success\bulletml-xmlns-type-nothing.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\bulletml\attributes\failure\bulletml-type-hoge.{0}")

  [<Test>]
  [<TestCaseSource "BulletmlElementsCase">]
  let ``bulletml-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "BulletmlAttributesCase">]
  let ``bulletml-属性`` (format) = 
    parse format

  // <!ELEMENT bulletRef (param*)>
  let BulletRefElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\bulletRef\elements\success\bulletRef-param-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletRef\elements\success\bulletRef-param-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\bulletRef\elements\success\bulletRef-param-multi.{0}")
    // not support

  // <!ATTLIST bulletRef label CDATA #REQUIRED>
  let BulletRefAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\bulletRef\attributes\success\bulletRef-label-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\bulletRef\attributes\failure\bulletRef-label-nothing.{0}")

  [<Test>]
  [<TestCaseSource "BulletRefElementsCase">]
  let ``bulletRef-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "BulletRefAttributesCase">]
  let ``bulletRef-属性`` (format) = 
    parse format

  // <!ELEMENT changeDirection (direction, term)>
  let ChangeDirectionElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\changeDirection\elements\success\changeDirection-direction-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\changeDirection\elements\success\changeDirection-term-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\changeDirection\elements\failure\changeDirection-direction-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\changeDirection\elements\failure\changeDirection-term-nothing.{0}")

  let ChangeDirectionAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\{0}\changeDirection\attributes\failure\changeDirection-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "ChangeDirectionElementsCase">]
  let ``changeDirection-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "ChangeDirectionAttributesCase">]
  let ``changeDirection-属性`` (format) = 
    parse format

  //<!ELEMENT changeSpeed (speed, term)>
  let ChangeSpeedElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\changeSpeed\elements\success\changeSpeed-speed-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\changeSpeed\elements\success\changeSpeed-term-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\changeSpeed\elements\failure\changeSpeed-speed-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\changeSpeed\elements\failure\changeSpeed-term-nothing.{0}")

  let ChangeSpeedAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\{0}\changeSpeed\attributes\failure\changeSpeed-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "ChangeSpeedElementsCase">]
  let ``changeSpeed-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "ChangeSpeedAttributesCase">]
  let ``changeSpeed-属性`` (format) = 
    parse format

  // <!ELEMENT direction (#PCDATA)>
  let DirectionElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\direction\elements\success\direction-fire-text-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\elements\success\direction-bullet-text-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\elements\success\direction-changeDirection-text-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\direction\elements\failure\direction-fire-text-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\elements\failure\direction-bullet-text-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\elements\failure\direction-changeDirection-text-nothing.{0}")

  // <!ATTLIST direction type (aim|absolute|relative|sequence) "aim">
  let DirectionAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-fire-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-fire-type-aim.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-fire-type-absolute.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-fire-type-relative.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-fire-type-sequence.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-bullet-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-bullet-type-aim.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-bullet-type-absolute.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-bullet-type-relative.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-bullet-type-sequence.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-changeDirection-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-changeDirection-type-aim.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-changeDirection-type-absolute.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-changeDirection-type-relative.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\success\direction-changeDirection-type-sequence.{0}")

    // not support
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\failure\direction-fire-type-hoge.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\failure\direction-bullet-type-hoge.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\direction\attributes\failure\direction-changeDirection-type-hoge.{0}")

  [<Test>]
  [<TestCaseSource "DirectionElementsCase">]
  let ``direction-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "DirectionAttributesCase">]
  let ``direction-属性`` (format) = 
    parse format

  // <!ELEMENT fire (direction?, speed?, (bullet | bulletRef))>
  let FireElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\fire\elements\success\fire-direction-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\fire\elements\success\fire-direction-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\fire\elements\success\fire-speed-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\fire\elements\success\fire-speed-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\fire\elements\success\fire-bullet.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\fire\elements\success\fire-bulletRef.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\fire\elements\failure\fire-bulletAndbulletRef-nothing.{0}")

  // <!ATTLIST fire label CDATA #IMPLIED>
  let FireAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\fire\attributes\success\fire-label-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\fire\attributes\success\fire-label-nothing.{0}")
    // not support

  [<Test>]
  [<TestCaseSource "FireElementsCase">]
  let ``fire-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "FireAttributesCase">]
  let ``fire-属性`` (format) = 
    parse format

  // <!ELEMENT fireRef (param*)>
  let FireRefElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\fireRef\elements\success\fireRef-param-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\fireRef\elements\success\fireRef-param-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\fireRef\elements\success\fireRef-param-multi.{0}")
    // not support
  
  // <!ATTLIST fireRef label CDATA #REQUIRED>
  let FireRefAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\fireRef\attributes\success\fireRef-label-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\fireRef\attributes\failure\fireRef-label-nothing.{0}")

  [<Test>]
  [<TestCaseSource "FireRefElementsCase">]
  let ``fireRef-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "FireRefAttributesCase">]
  let ``fireRef-属性`` (format) = 
    parse format

  // <!ELEMENT horizontal (#PCDATA)>
  let HorizontalElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\horizontal\elements\success\horizontal-text-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\horizontal\elements\failure\horizontal-text-nothing.{0}")

  // <!ATTLIST horizontal type (absolute|relative|sequence) "absolute">
  let HorizontalAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\horizontal\attributes\success\horizontal-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\horizontal\attributes\success\horizontal-type-absolute.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\horizontal\attributes\success\horizontal-type-relative.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\horizontal\attributes\success\horizontal-type-sequence.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\horizontal\attributes\failure\horizontal-type-hoge.{0}")

  [<Test>]
  [<TestCaseSource "HorizontalElementsCase">]
  let ``horizontal-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "HorizontalAttributesCase">]
  let ``horizontal-属性`` (format) = 
    parse format

  // <!ELEMENT param (#PCDATA)>
  let ParamElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\param\elements\success\param-actionRef-text-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\param\elements\success\param-fireRef-text-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\param\elements\success\param-bulletRef-text-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\param\elements\failure\param-actionRef-text-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\param\elements\failure\param-fireRef-text-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\param\elements\failure\param-bulletRef-text-nothing.{0}")

  let ParamAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\{0}\param\attributes\failure\param-actionRef-attribute-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\param\attributes\failure\param-fireRef-attribute-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\param\attributes\failure\param-bulletRef-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "ParamElementsCase">]
  let ``param-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "ParamAttributesCase">]
  let ``param-属性`` (format) = 
    parse format

  // <!ELEMENT repeat (times, (action | actionRef))>
  let RepeatElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\repeat\elements\success\repeat-times-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\repeat\elements\success\repeat-action.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\repeat\elements\success\repeat-actionRef.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\repeat\elements\failure\repeat-times-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\repeat\elements\failure\repeat-actionMulti.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\repeat\elements\failure\repeat-actionAndactionRef-nothing.{0}")

  // <!ATTLIST bullet label CDATA #IMPLIED>
  let RepeatAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\{0}\repeat\attributes\failure\repeat-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "RepeatElementsCase">]
  let ``repeat-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "RepeatAttributesCase">]
  let ``repeat-属性`` (format) = 
    parse format

  // <!ELEMENT speed (#PCDATA)>
  let SpeedElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\speed\elements\success\speed-fire-text-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\elements\success\speed-bullet-text-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\elements\success\speed-changeSpeed-text-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\speed\elements\failure\speed-fire-text-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\elements\failure\speed-bullet-text-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\elements\failure\speed-changeSpeed-text-nothing.{0}")

  // <!ATTLIST speed type (absolute|relative|sequence) "absolute">
  let SpeedAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-fire-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-fire-type-absolute.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-fire-type-relative.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-fire-type-sequence.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-bullet-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-bullet-type-absolute.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-bullet-type-relative.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-bullet-type-sequence.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-changeSpeed-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-changeSpeed-type-absolute.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-changeSpeed-type-relative.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\success\speed-changeSpeed-type-sequence.{0}")

    // not support
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\failure\speed-fire-type-hoge.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\failure\speed-bullet-type-hoge.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\speed\attributes\failure\speed-changeSpeed-type-hoge.{0}")

  [<Test>]
  [<TestCaseSource "SpeedElementsCase">]
  let ``speed-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "SpeedAttributesCase">]
  let ``speed-属性`` (format) = 
    parse format

  // <!ELEMENT term (#PCDATA)>
  let TermElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\term\elements\success\term-changeDirection-text-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\term\elements\success\term-changeSpeed-text-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\term\elements\success\term-accel-text-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\term\elements\failure\term-changeDirection-text-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\term\elements\failure\term-changeSpeed-text-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\term\elements\failure\term-accel-text-nothing.{0}")

  let TermAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\{0}\term\attributes\failure\term-changeDirection-attribute-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\term\attributes\failure\term-changeSpeed-attribute-exist.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\term\attributes\failure\term-accel-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "TermElementsCase">]
  let ``term-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "TermAttributesCase">]
  let ``term-属性`` (format) = 
    parse format

  // <!ELEMENT times (#PCDATA)>
  let TimesElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\times\elements\success\times-repeat-text-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\times\elements\failure\times-repeat-text-nothing.{0}")

  let TimesAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\{0}\times\attributes\failure\times-repeat-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "TimesElementsCase">]
  let ``times-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "TimesAttributesCase">]
  let ``times-属性`` (format) = 
    parse format

  // <!ELEMENT vanish (#PCDATA)>
  let VanishElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\vanish\elements\success\vanish-text-nothing.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\vanish\elements\failure\vanish-text-exist.{0}")

  let VanishAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\{0}\vanish\attributes\failure\vanish-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "VanishElementsCase">]
  let ``vanish-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "VanishAttributesCase">]
  let ``vanish-属性`` (format) = 
    parse format

  // <!ELEMENT vertical (#PCDATA)>
  let VerticalElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\vertical\elements\success\vertical-text-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\vertical\elements\failure\vertical-text-nothing.{0}")

  // <!ATTLIST vertical type (absolute|relative|sequence) "absolute">
  let VerticalAttributesCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\vertical\attributes\success\vertical-type-nothing.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\vertical\attributes\success\vertical-type-absolute.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\vertical\attributes\success\vertical-type-relative.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\vertical\attributes\success\vertical-type-sequence.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\vertical\attributes\failure\vertical-type-hoge.{0}")

  [<Test>]
  [<TestCaseSource "VerticalElementsCase">]
  let ``vertical-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "VerticalAttributesCase">]
  let ``vertical-属性`` (format) = 
    parse format

  // <!ELEMENT wait (#PCDATA)>
  let WaitElementsCase =
    // support
    testCase (singleParam @"..\..\..\TestData\{0}\wait\elements\success\wait-text-exist.{0}")
    // not support
          &> (singleParam @"..\..\..\TestData\{0}\wait\elements\failure\wait-text-nothing.{0}")

  let WaitAttributesCase =
    // not support
    testCase (singleParam @"..\..\..\TestData\{0}\wait\attributes\failure\wait-attribute-exist.{0}")

  [<Test>]
  [<TestCaseSource "WaitElementsCase">]
  let ``wait-要素`` (format) = 
    parse format

  [<Test>]
  [<TestCaseSource "WaitAttributesCase">]
  let ``wait-属性`` (format) = 
    parse format

  // Sample
  let SampleCase =
    testCase (singleParam @"..\..\..\TestData\{0}\sample\5way.{0}")
          &> (singleParam @"..\..\..\TestData\{0}\sample\readTest.{0}")

  [<Test>]
  [<TestCaseSource "SampleCase">]
  let ``その他-サンプル等`` (format) = 
    parse format
