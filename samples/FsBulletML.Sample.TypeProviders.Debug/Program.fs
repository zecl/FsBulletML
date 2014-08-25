module Hoge
open FsBulletML.TypeProviders

type XML1 = FsBulletML.TypeProviders.Xml.BulletML<"5way.xml;2wayRight.xml">
type SXML1 = FsBulletML.TypeProviders.Sxml.BulletML<"5way.sxml">
type FSB1 = FsBulletML.TypeProviders.Fsb.BulletML<"5way.fsb">

[<Literal>]
let bullets = """
5way.xml;
<bulletml type="vertical" name="前方5way弾">
    <action label='top'>
        <changeDirection>
            <direction>3</direction>
            <term>5</term>
        </changeDirection>
        <fire>
            <direction type="absolute">-10</direction>
            <speed>20</speed>
            <bullet/>
        </fire>
        <fire>
            <direction type="absolute">0</direction>
            <speed>20</speed>
            <bullet/>
        </fire>
    </action>
</bulletml>; 
2wayRight.xml; 
<bulletml type='horizontal' name='弾の名前ほげもげ'>
    <action label='top'>
        <fire>
            <bullet/>
        </fire>
    </action>
</bulletml>;
<bulletml type='horizontal'>
    <action label='top'>
        <fire>
            <bullet/>
        </fire>
    </action>
</bulletml>;
 homing.xml"""
type XML2 = FsBulletML.TypeProviders.BulletML<bullets>

type SXML2 = FsBulletML.TypeProviders.BulletML<"5way.sxml", Style.Sxml>
type FSB2 = FsBulletML.TypeProviders.BulletML<"5way.fsb", Style.Fsb>

[<EntryPoint>]
let main argv = 
  let a = FsBulletML.TypeProviders.Style.Xml
  
  let bulletml1 = new XML1()
  bulletml1 |> printfn "%A"
  let hoge = bulletml1.``5way``
  hoge |> printfn "%A"

  let bulletml2 = new SXML1()
  bulletml2.``5way`` |> printfn "%A"
  let bulletml3 = new FSB1()
  bulletml3.``5way`` |> printfn "%A"

  let bulletml4 = new XML2()
  let hoge = bulletml4.Bullet1
  hoge |> printfn "%A"
  let fuga = bulletml4.Bullet3
  fuga.ToString() |> printfn "%s"
  let piyo = bulletml4.Bullet4
  piyo.ToString() |> printfn "%s"

  let bulletml5 = new SXML2()
  bulletml5.``5way`` |> printfn "%A"
  let bulletml6 = new FSB2()
  bulletml6.``5way`` |> printfn "%A"


  System.Console.ReadKey () |> ignore

  printfn "%A" argv
  0 // 整数の終了コードを返します
