namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// OtakuTwo
[<RequireQualifiedAccess>]
module OtakuTwo =

  /// おたくツーさん作、円形発射弾・花火型 by 白い弾幕くん
  /// [OtakuTwo]_circle_fireworks.xml
  let circle_fireworks =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;
        bulletmlName = Some "おたくツーさん作、円形発射弾・花火型 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
            ChangeSpeed (Speed (None,"1"),Term "1"); Wait "25";
            ChangeSpeed (Speed (None,"0"),Term "1");
            Repeat
              (Times "20+$rank*40",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"0")),
                      Some (Speed (None,"0.4+$rank*0.6")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "59",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"6")),
                             Some (Speed (None,"0.4+$rank*0.6")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "30-$rank*20";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                      Some (Speed (None,"1.2+$rank*1.8")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "59",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"6")),
                             Some (Speed (None,"1.2+$rank*1.8")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "30-$rank*20"]));
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1");
            ChangeSpeed (Speed (None,"1"),Term "1"); Wait "25";
            ChangeSpeed (Speed (None,"0"),Term "1")])])

  /// おたくツーさん作、円形発射弾・花火型弐式 by 白い弾幕くん
  /// [OtakuTwo]_circle_fireworks2.xml
  let circle_fireworks2 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;
        bulletmlName = Some "おたくツーさん作、円形発射弾・花火型弐式 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
            ChangeSpeed (Speed (None,"1"),Term "1"); Wait "25";
            ChangeSpeed (Speed (None,"0"),Term "1");
            Repeat
              (Times "20+$rank*40",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"0")),
                      Some (Speed (None,"1.2+$rank*1.8")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "59",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"6")),
                             Some (Speed (None,"1.2+$rank*1.8")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "30-$rank*20";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                      Some (Speed (None,"0.4+$rank*0.6")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "59",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"6")),
                             Some (Speed (None,"0.4+$rank*0.6")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "30-$rank*20"]));
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1");
            ChangeSpeed (Speed (None,"1"),Term "1"); Wait "25";
            ChangeSpeed (Speed (None,"0"),Term "1")])])

  /// おたくツーさん作、円形発射弾・速度変化型 by 白い弾幕くん
  /// [OtakuTwo]_circle_trap.xml
  let circle_trap =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;
        bulletmlName = Some "おたくツーさん作、円形発射弾・速度変化型 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "main";},["$rand"; "$rand"])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "main";},
           [Action.ActionRef ({actionRefLabel = "move";},["100+$1*160"; "$2"]);
            Wait "40-$rank*20"; 
            Action.ActionRef ({actionRefLabel = "round";},[]);
            Action.ActionRef ({actionRefLabel = "move";},["280+$1*160"; "$2"]); Wait "25"]);
        BulletmlElm.Action
          ({actionLabel = Some "move";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$1"),Term "1");
            ChangeSpeed (Speed (None,"$2*1.5+$rank*1.5"),Term "1");
            Wait "40-$rank*20"; ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Action
          ({actionLabel = Some "round";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$rand*360")),
               Some (Speed (None,"0")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[Vanish])])); Wait "1";
            Repeat
              (Times "6",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "30",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"1")),
                             Some (Speed (None,"0.8+$rank*0.4")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Repeat
                     (Times "30",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"1")),
                             Some (Speed (None,"0.6+$rank*0.3")),
                             BulletRef ({bulletRefLabel = "speed";},[]))]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "speed";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "100-$rank*50";
                ChangeSpeed (Speed (None,"1.2+$rank*0.6"),Term "1");
                Wait "(100-$rank*50)/2";
                ChangeSpeed (Speed (None,"0.8+$rank*0.4"),Term "1")])])])

  /// 最臭鬼畜兵器「非蜂」１：ニオイ波動 by 白い弾幕くん
  /// [OtakuTwo]_dis_bee_1.xml
  let dis_bee_1 = 
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "最臭鬼畜兵器「非蜂」１：ニオイ波動 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef
              ({actionRefLabel = "position";},["$rand"; "$rand"; "10"; "6-$rank*4"]);
            Action.ActionRef
              ({actionRefLabel = "position";},["$rand"; "$rand"; "15"; "6-$rank*4"]);
            Action.ActionRef
              ({actionRefLabel = "position";},["$rand"; "$rand"; "20"; "6-$rank*4"]);
            Action.ActionRef
              ({actionRefLabel = "position";},["$rand"; "$rand"; "25"; "6-$rank*4"]);
            Action.ActionRef
              ({actionRefLabel = "position";},["$rand"; "$rand"; "30"; "6-$rank*4"])]);
        BulletmlElm.Action
          ({actionLabel = Some "position";},
           [Action.ActionRef ({actionRefLabel = "move";},["100+$1*160"; "$2"]);
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$rand*360"),Term "1");
            Wait "1"; Action.ActionRef ({actionRefLabel = "wave";},["$3"; "$4"]);
            Action.ActionRef ({actionRefLabel = "move";},["(100+$1*160)+180"; "$2"])]);
        BulletmlElm.Action
          ({actionLabel = Some "move";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$1"),Term "1");
            ChangeSpeed (Speed (None,"$2*6+$rank*6"),Term "1"); Wait "10-$rank*5";
            ChangeSpeed (Speed (None,"0"),Term "1"); Wait "1"]);
        BulletmlElm.Action
          ({actionLabel = Some "wave";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "allrange";},[" 0.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},[" 3.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},[" 5.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},[" 6.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},[" 6.5"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},[" 6.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},[" 5.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},[" 3.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},[" 0.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},["-3.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},["-5.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},["-6.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},["-6.5"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},["-6.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},["-5.0"; "$1"]);
                   Wait "$2";
                   Action.ActionRef ({actionRefLabel = "allrange";},["-3.0"; "$1"]);
                   Wait "$2"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "allrange";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
               Some (Speed (None,"0.8+$rank*1.6")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "$2-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"360/$2")),
                      Some (Speed (None,"0.8+$rank*1.6")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))])])

  /// 最臭鬼畜兵器「非蜂」２：壁花火 by 白い弾幕くん
  /// [OtakuTwo]_dis_bee_2.xml
  let dis_bee_2 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "最臭鬼畜兵器「非蜂」２：壁花火 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "move";},["180"]);
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$rand*360"),Term "1");
            Wait "5";
            Repeat
              (Times "20+$rank*20",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "wall";},["15"]);
                   Wait "25-$rank*$rank*12";
                   Action.ActionRef ({actionRefLabel = "wall";},[" 0"]);
                   Wait "25-$rank*$rank*12"]));
            Action.ActionRef ({actionRefLabel = "move";},["0"])]);
        BulletmlElm.Action
          ({actionLabel = Some "move";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$1"),Term "5"); Wait "6";
            ChangeSpeed (Speed (None,"1"),Term "50"); Wait "55";
            ChangeSpeed (Speed (None,"0"),Term "50"); Wait "55"]);
        BulletmlElm.Action
          ({actionLabel = Some "wall";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
               Some (Speed (None,"1+$rank*1.2")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Action.ActionRef ({actionRefLabel = "wallbody";},[]);
            Repeat
              (Times "11",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                      Some (Speed (None,"1+$rank*1.2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "wallbody";},[])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "wallbody";},
           [Repeat
              (Times "15",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"1")),
                      Some (Speed (None,"1+$rank*1.2")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))])])

  /// 最臭鬼畜兵器「非蜂」３：ぐるぐる風車 by 白い弾幕くん
  /// [OtakuTwo]_dis_bee_3.xml
  let dis_bee_3 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "最臭鬼畜兵器「非蜂」３：ぐるぐる風車 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0")),BulletRef ({bulletRefLabel = "top";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),None,
               BulletRef ({bulletRefLabel = "byakko";},[]));
            Action.ActionRef ({actionRefLabel = "byakko";},[]);
            Repeat
              (Times "120+$rank*$rank*$rank*120",
               Action
                 ({actionLabel = None;},
                  [Wait "8-$rank*$rank*$rank*4";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"128+$rand*0.5")),None,
                      BulletRef ({bulletRefLabel = "byakko";},[]));
                   Action.ActionRef ({actionRefLabel = "byakko";},[])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "byakko";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),None,
                      BulletRef ({bulletRefLabel = "byakko";},[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "byakko";},None,Some (Speed (None,"6")),
           [Action
              ({actionLabel = None;},
               [FireRef ({fireRefLabel = "byakkoway";},[" 48.4-$rand*0.8"]);
                FireRef ({fireRefLabel = "byakkoway";},[" 32.4-$rand*0.8"]);
                FireRef ({fireRefLabel = "byakkoway";},[" 16.4-$rand*0.8"]);
                FireRef ({fireRefLabel = "byakkoway";},["  0.4-$rand*0.8"]);
                FireRef ({fireRefLabel = "byakkoway";},["-16.4+$rand*0.8"]);
                FireRef ({fireRefLabel = "byakkoway";},["-32.4+$rand*0.8"]);
                FireRef ({fireRefLabel = "byakkoway";},["-48.4+$rand*0.8"]); Vanish])]);
        BulletmlElm.Fire
          ({fireLabel = Some "byakkoway";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
           Some (Speed (None,"0.8+$rank*$rank*1")),
           Bullet ({bulletLabel = None;},None,None,[]));
        BulletmlElm.Bullet
          ({bulletLabel = Some "top";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),None,
                   BulletRef ({bulletRefLabel = "backfire";},[]));
                Action.ActionRef ({actionRefLabel = "backfire";},[]);
                Repeat
                  (Times "120+$rank*$rank*$rank*120",
                   Action
                     ({actionLabel = None;},
                      [Wait "8-$rank*$rank*$rank*4";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},
                                "112+$rand*0.5-$rank*$rank*$rank*$rank*$rank*9.5")),
                          None,BulletRef ({bulletRefLabel = "backfire";},[]));
                       Action.ActionRef ({actionRefLabel = "backfire";},[])]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "backfire";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),None,
                      BulletRef ({bulletRefLabel = "backfire";},[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "backfire";},None,Some (Speed (None,"10")),
           [Action
              ({actionLabel = None;},
               [Wait "4"; FireRef ({fireRefLabel = "backfire";},["100.5"]);
                FireRef ({fireRefLabel = "backfire";},["110.5"]);
                FireRef ({fireRefLabel = "backfire";},["120.5"]);
                FireRef ({fireRefLabel = "backfire";},["130.5"]);
                FireRef ({fireRefLabel = "backfire";},["140.5"]); Vanish])]);
        BulletmlElm.Fire
          ({fireLabel = Some "backfire";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"$1-$rand")),
           Some (Speed (None,"1+$rank*$rank")),
           Bullet ({bulletLabel = None;},None,None,[]))])

  /// おたくツーさん作、回転砲台・鶚型 by 白い弾幕くん
  /// [OtakuTwo]_roll_misago.xml
  let roll_misago = 
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "おたくツーさん作、回転砲台・鶚型 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
            ChangeSpeed (Speed (None,"1"),Term "1"); Wait "25";
            ChangeSpeed (Speed (None,"0"),Term "1"); Wait "10";
            Repeat
              (Times "6",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),
                      Some (Speed (None,"0.5")),
                      BulletRef ({bulletRefLabel = "white";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),
                      Some (Speed (None,"0.5")),
                      BulletRef ({bulletRefLabel = "black";},[]))]));
            Repeat
              (Times "120",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"3")),
                      Some (Speed (None,"0.48")),
                      BulletRef ({bulletRefLabel = "normal";},[]))])); Wait "1700";
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1");
            ChangeSpeed (Speed (None,"1"),Term "1"); Wait "25";
            ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "white";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "80";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-90"),Term "4");
                Wait "4";
                Repeat
                  (Times "8",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = DirectionType.Relative;},"-135"),
                          Term "195");
                       Repeat
                         (Times "30+$rank*15",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Relative;},"90")),
                                 Some (Speed (None,"0.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Wait "6-$rank*2"]))]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "black";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"-90")),
                   Some (Speed (None,"0.8")),
                   BulletRef ({bulletRefLabel = "direction";},[])); Wait "80";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-90"),Term "4");
                Wait "4";
                Repeat
                  (Times "8",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = DirectionType.Relative;},"-135"),
                          Term "195");
                       Repeat
                         (Times "30+$rank*30",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},
                                       "3.92307692307692307692307692307833*(2-$rank)")),
                                 Some (Speed (None,"0.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Wait "6-$rank*3"]))]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "normal";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "80";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-90"),Term "4");
                Wait "4";
                Repeat
                  (Times "8",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = DirectionType.Relative;},"-135"),
                          Term "195"); Wait "180"]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "direction";},None,None,
           [Action ({actionLabel = None;},[Vanish])])])

  /// 回転発射弾・四段風車形 by 白い弾幕くん
  /// [OtakuTwo]_self-0012.xml
  let self_0012 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;
        bulletmlName = Some "回転発射弾・四段風車形 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0")),
               BulletRef ({bulletRefLabel = "4way";},["$rand"; "$rand"]));
            Action.ActionRef ({actionRefLabel = "3way";},["$rand"; "$rand"]); Wait "100"]);
        BulletmlElm.Action
          ({actionLabel = Some "3way";},
           [Repeat
              (Times "200",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"123.7+$1")),
                      Some (Speed (None,"(1+$2*0.5)*(1+$rank*$rank*$rank*$rank)")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "2",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"120")),
                             Some
                               (Speed
                                  (None,"(1+$2*0.5)*(1+$rank*$rank*$rank*$rank)")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "8-$rank*4"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "4way";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "200",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"92.1+$1")),
                          Some
                            (Speed (None,"(0.8+$2*0.8)*(1+$rank*$rank*$rank*$rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"90")),
                                 Some
                                   (Speed
                                      (None,
                                       "(0.8+$2*0.8)*(1+$rank*$rank*$rank*$rank)")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]));
                       Wait "8-$rank*4"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top5";},
           [Action.ActionRef ({actionRefLabel = "5way";},["$rand"; "$rand"])]);
        BulletmlElm.Action
          ({actionLabel = Some "5way";},
           [Repeat
              (Times "200",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"78.7+$1")),
                      Some (Speed (None,"(0.6+$2*1.6)*(1+$rank*$rank*$rank*$rank)")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action
                            ({actionLabel = None;},
                             [ChangeSpeed
                                (Speed (Some {speedType = SpeedType.Relative;},"0"),
                                 Term "9999")])]));
                   Repeat
                     (Times "4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"72")),
                             Some
                               (Speed
                                  (None,"(0.6+$2*1.6)*(1+$rank*$rank*$rank*$rank)")),
                             Bullet
                               ({bulletLabel = None;},None,None,
                                [Action
                                   ({actionLabel = None;},
                                    [ChangeSpeed
                                       (Speed (Some {speedType = SpeedType.Relative;},"0"),
                                        Term "9999")])]))])); Wait "8-$rank*4"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top6";},
           [Action.ActionRef ({actionRefLabel = "6way";},["$rand"; "$rand"])]);
        BulletmlElm.Action
          ({actionLabel = Some "6way";},
           [Repeat
              (Times "200",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"63.3+$1")),
                      Some (Speed (None,"(0.6+$2*0.9)*(1+$rank*$rank*$rank*$rank)")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action
                            ({actionLabel = None;},
                             [Wait "9999";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Relative;},"0")),
                                 Some (Speed (Some {speedType = SpeedType.Relative;},"0")),
                                 Bullet
                                   ({bulletLabel = None;},None,None,
                                    [Action ({actionLabel = None;},[Vanish])]))])]));
                   Repeat
                     (Times "5",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"60")),
                             Some
                               (Speed
                                  (None,"(0.6+$2*0.9)*(1+$rank*$rank*$rank*$rank)")),
                             Bullet
                               ({bulletLabel = None;},None,None,
                                [Action
                                   ({actionLabel = None;},
                                    [Wait "9999";
                                     Fire
                                       ({fireLabel = None;},
                                        Some
                                          (Direction
                                             (Some {directionType = DirectionType.Relative;},"0")),
                                        Some
                                          (Speed (Some {speedType = SpeedType.Relative;},"0")),
                                        Bullet
                                          ({bulletLabel = None;},None,None,
                                           [Action ({actionLabel = None;},[Vanish])]))])]))]));
                   Wait "8-$rank*4"]))])])

  /// 自機拘束弾・不規則回転型 by 白い弾幕くん
  /// [OtakuTwo]_self-0062.xml
  let self_0062 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;
        bulletmlName = Some "自機拘束弾・不規則回転型 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "move";},["180"]);
            ChangeDirection (Direction (Some {directionType = Aim;},"0"),Term "1");
            Wait "1";
            Repeat
              (Times "50",
               Action
                 ({actionLabel = None;},
                  [Wait "1";
                   FireRef ({fireRefLabel = "winder";},[" 22.5-$rank*12.5"; "7"]);
                   FireRef ({fireRefLabel = "winder";},["-22.5+$rank*12.5"; "7"]);
                   FireRef ({fireRefLabel = "winder";},[" 22.5-$rank*12.5"; "7.1"]);
                   FireRef ({fireRefLabel = "winder";},["-22.5+$rank*12.5"; "7.1"])]));
            Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [ChangeDirection
                     (Direction (Some {directionType = DirectionType.Relative;},"(-60+$rand*120)"),
                      Term "100+$rank*50");
                   Repeat
                     (Times "100-$rand*50",
                      Action
                        ({actionLabel = None;},
                         [Wait "1";
                          FireRef
                            ({fireRefLabel = "winder";},[" 22.5-$rank*12.5"; "7"]);
                          FireRef
                            ({fireRefLabel = "winder";},["-22.5+$rank*12.5"; "7"]);
                          FireRef
                            ({fireRefLabel = "winder";},[" 22.5-$rank*12.5"; "7.1"]);
                          FireRef
                            ({fireRefLabel = "winder";},["-22.5+$rank*12.5"; "7.1"])]))]));
            Wait "50"; Action.ActionRef ({actionRefLabel = "move";},["0"])]);
        BulletmlElm.Action
          ({actionLabel = Some "move";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$1"),Term "1");
            ChangeSpeed (Speed (None,"2"),Term "1"); Wait "18";
            ChangeSpeed (Speed (None,"0"),Term "1"); Wait "5"]);
        BulletmlElm.Fire
          ({fireLabel = Some "winder";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
           Some (Speed (None,"$2")),Bullet ({bulletLabel = None;},None,None,[]))])
  
  /// 自機拘束弾・不規則回転型改 by 白い弾幕くん
  /// [OtakuTwo]_self-0063.xml
  let self_0063 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;
        bulletmlName = Some "自機拘束弾・不規則回転型改 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "move";},["180"]);
            ChangeDirection (Direction (Some {directionType = Aim;},"0"),Term "1");
            Wait "1";
            Repeat
              (Times "50",
               Action
                 ({actionLabel = None;},
                  [Wait "1";
                   FireRef ({fireRefLabel = "winder";},[" 22.5-$rank*12.5"; "7"]);
                   FireRef ({fireRefLabel = "winder";},["-22.5+$rank*12.5"; "7"]);
                   FireRef ({fireRefLabel = "winder";},[" 22.5-$rank*12.5"; "7.1"]);
                   FireRef ({fireRefLabel = "winder";},["-22.5+$rank*12.5"; "7.1"])]));
            Repeat
              (Times "5",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "wall";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
                      Some (Speed (None,"0")),
                      BulletRef ({bulletRefLabel = "round";},[]));
                   Action.ActionRef ({actionRefLabel = "wall";},[]);
                   Action.ActionRef ({actionRefLabel = "wall";},[]);
                   Action.ActionRef ({actionRefLabel = "wall";},[])])); Wait "50";
            Action.ActionRef ({actionRefLabel = "move";},["0"])]);
        BulletmlElm.Action
          ({actionLabel = Some "move";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$1"),Term "1");
            ChangeSpeed (Speed (None,"2"),Term "1"); Wait "18";
            ChangeSpeed (Speed (None,"0"),Term "1"); Wait "5"]);
        BulletmlElm.Action
          ({actionLabel = Some "wall";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Relative;},"-60+$rand*120"),
               Term "100+$rank*50");
            Repeat
              (Times "100-$rand*50",
               Action
                 ({actionLabel = None;},
                  [Wait "1";
                   FireRef ({fireRefLabel = "winder";},[" 22.5-$rank*12.5"; "7"]);
                   FireRef ({fireRefLabel = "winder";},["-22.5+$rank*12.5"; "7"]);
                   FireRef ({fireRefLabel = "winder";},[" 22.5-$rank*12.5"; "7.1"]);
                   FireRef ({fireRefLabel = "winder";},["-22.5+$rank*12.5"; "7.1"])]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "winder";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
           Some (Speed (None,"$2")),Bullet ({bulletLabel = None;},None,None,[]));
        BulletmlElm.Bullet
          ({bulletLabel = Some "round";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "100*$rand";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$rand*360")),
                   Some (Speed (None,"1.0+$rank*1.0")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"        4")),
                   Some (Speed (None,"0.8+$rank*0.8")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "44",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"4")),
                          Some (Speed (None,"1.0+$rank*1.0")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"4")),
                          Some (Speed (None,"0.8+$rank*0.8")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// 回転砲台・鶚型副産物 by 白い弾幕くん
  /// [OtakuTwo]_self-0071.xml
  let self_0071 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "回転砲台・鶚型副産物 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "move";},["180"]);
            Action.ActionRef ({actionRefLabel = "way";},["6"; "$rand"]);
            Action.ActionRef ({actionRefLabel = "way";},["8"; "$rand"]);
            Action.ActionRef ({actionRefLabel = "way";},["10"; "$rand"]);
            Action.ActionRef ({actionRefLabel = "way";},["12"; "$rand"]); Wait "50";
            Action.ActionRef ({actionRefLabel = "move";},["0"])]);
        BulletmlElm.Action
          ({actionLabel = Some "move";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$1"),Term "2"); Wait "3";
            ChangeSpeed (Speed (None,"2"),Term "25"); Wait "27";
            ChangeSpeed (Speed (None,"0"),Term "25"); Wait "27"]);
        BulletmlElm.Action
          ({actionLabel = Some "way";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$rand*360")),
               Some (Speed (None,"1")),BulletRef ({bulletRefLabel = "turn";},["$2"]));
            Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"360/$1")),
                      Some (Speed (None,"1")),
                      BulletRef ({bulletRefLabel = "turn";},["$2"]))])); Wait "500"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "turn";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "100"); Wait "90";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"180")),
                   Some (Speed (None,"0.1")),
                   BulletRef ({bulletRefLabel = "bit";},["$1"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0.5"),Term "50");
                ChangeDirection
                  (Direction
                     (Some {directionType = DirectionType.Relative;},"0.1+$rank*$rank*$rank*90"),
                   Term "1000-$rank*$rank*$rank*500");
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"$1*360")),
                   Some (Speed (None,"0")),
                   Bullet
                     ({bulletLabel = None;},None,None,
                      [Action ({actionLabel = None;},[Vanish])]));
                Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"4.6")),
                          Some (Speed (None,"0.8+$rank*$rank*0.4")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "10-$rank*5"]))])])])

  /// 加速弾・巨大弾落下型 by 白い弾幕くん
  /// [OtakuTwo]_self-0081.xml
  let self_0081 = 
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "加速弾・巨大弾落下型 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "50",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "seed";},[" 1"; "$rand"; "$rand"]);
                   Wait "15";
                   FireRef ({fireRefLabel = "seed";},["-1"; "$rand"; "$rand"]);
                   Wait "15"]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "seed";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"90*$1")),
           Some (Speed (None,"$rand*3")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "20";
                   Repeat
                     (Times "40",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                             Some (Speed (None,"0.5+$rank")),
                             BulletRef
                               ({bulletRefLabel = "roundbase";},["$2"; "$3"]))]));
                   Vanish])]));
        BulletmlElm.Bullet
          ({bulletLabel = Some "roundbase";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
                   Some (Speed (None,"$1")),
                   BulletRef ({bulletRefLabel = "round";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),
                   Some (Speed (None,"$2")),
                   BulletRef ({bulletRefLabel = "round";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "round";},None,None,
           [Action
              ({actionLabel = None;},
               [Accel (None,Some (Vertical (None,"10")),Term "250")])])])

  /// 「緋蜂のような物体」超速青弾part1 by 白い弾幕くん
  /// [OtakuTwo]_self-1020.xml
  let self_1020 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "「緋蜂のような物体」超速青弾part1 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "50",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"30+1.1*5")),
                      Some (Speed (None,"2+$rank*$rank*2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "11",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"30")),
                             Some (Speed (None,"2+$rank*$rank*2")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Repeat
                     (Times "4+$rank*10",
                      Action
                        ({actionLabel = None;},
                         [Wait "1";
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"31.1")),
                             Some (Speed (None,"2+$rank*$rank*2")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Repeat
                            (Times "11",
                             Action
                               ({actionLabel = None;},
                                [Fire
                                   ({fireLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Sequence;},"30")),
                                    Some (Speed (None,"2+$rank*$rank*2")),
                                    Bullet ({bulletLabel = None;},None,None,[]))]))]));
                   Wait "10"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Repeat
              (Times "50",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Sequence;},"-30-0.7*5")),
                      Some (Speed (None,"2+$rank*$rank*2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "11",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"-30")),
                             Some (Speed (None,"2+$rank*$rank*2")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Repeat
                     (Times "4+$rank*10",
                      Action
                        ({actionLabel = None;},
                         [Wait "1";
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"-30.7")),
                             Some (Speed (None,"2+$rank*$rank*2")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Repeat
                            (Times "11",
                             Action
                               ({actionLabel = None;},
                                [Fire
                                   ({fireLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Sequence;},"-30")),
                                    Some (Speed (None,"2+$rank*$rank*2")),
                                    Bullet ({bulletLabel = None;},None,None,[]))]))]));
                   Wait "10"]))])])

  /// 「緋蜂のような物体」超速青弾part2 by 白い弾幕くん
  /// [OtakuTwo]_self-1021.xml
  let self_1021 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "「緋蜂のような物体」超速青弾part2 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Wait "10"; Action.ActionRef ({actionRefLabel = "cyclone";},["0.1+$rand"; " 1"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Action.ActionRef ({actionRefLabel = "cyclone";},["0.1+$rand"; "-1"]); Wait "10"]);
        BulletmlElm.Action
          ({actionLabel = Some "cyclone";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"($rand*360)*$2")),
               None,BulletRef ({bulletRefLabel = "speed";},[]));
            Repeat
              (Times "11",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"(30)*$2")),
                      None,BulletRef ({bulletRefLabel = "speed";},[]))])); Wait "1";
            Repeat
              (Times "9",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Sequence;},"(30+$1)*$2")),
                      None,BulletRef ({bulletRefLabel = "speed";},[]));
                   Repeat
                     (Times "11",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"(30)*$2")),None,
                             BulletRef ({bulletRefLabel = "speed";},[]))]));
                   Wait "1"])); Wait "10";
            Repeat
              (Times "39",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"(30+$1*10)*$2")),None,
                      BulletRef ({bulletRefLabel = "speed";},[]));
                   Repeat
                     (Times "11",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"(30)*$2")),None,
                             BulletRef ({bulletRefLabel = "speed";},[]))]));
                   Wait "1";
                   Repeat
                     (Times "9",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"(30+$1)*$2")),
                             None,BulletRef ({bulletRefLabel = "speed";},[]));
                          Repeat
                            (Times "11",
                             Action
                               ({actionLabel = None;},
                                [Fire
                                   ({fireLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Sequence;},"(30)*$2")),
                                    None,BulletRef ({bulletRefLabel = "speed";},[]))]));
                          Wait "1"])); Wait "10"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "speed";},None,Some (Speed (None,"1+$rank*2")),[])])

  /// rRootageより妄想　Part01 by 白い弾幕くん
  /// [OtakuTwo]_self-2010.xml
  let self_2010 = 
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "rRootageより妄想　Part01 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "main";},["$rand"; " 1"; " 1"; " 1"; " 1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; "-1"; "-1"; "-1"; "-1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; "-1"; "-1"; " 1"; " 1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; " 1"; " 1"; "-1"; "-1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; " 1"; "-1"; "-1"; " 1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; "-1"; " 1"; " 1"; "-1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; "-1"; " 1"; "-1"; " 1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; " 1"; "-1"; " 1"; "-1"])]);
        BulletmlElm.Action
          ({actionLabel = Some "main";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"45")),None,
               BulletRef ({bulletRefLabel = "cross";},["$1"; "$2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),None,
               BulletRef ({bulletRefLabel = "cross";},["$1"; "$3"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),None,
               BulletRef ({bulletRefLabel = "cross";},["$1"; "$4"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),None,
               BulletRef ({bulletRefLabel = "cross";},["$1"; "$5"]));
            Wait "200-$rank*100"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "cross";},None,Some (Speed (None,"1")),
           [Action
              ({actionLabel = None;},
               [Wait "15"; ChangeSpeed (Speed (None,"0"),Term "1");
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360*$1*$2")),
                   Some (Speed (None,"1+$rank*$rank*1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"180*$2")),
                   Some (Speed (None,"1+$rank*$rank*1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "49",
                   Action
                     ({actionLabel = None;},
                      [Wait "4-$rank*2";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"187.7*$2")),
                          Some (Speed (None,"1+$rank*$rank*1")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"180*$2")),
                          Some (Speed (None,"1+$rank*$rank*1")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// rRootageより妄想　Part01-ANOTHER by 白い弾幕くん
  /// [OtakuTwo]_self-2011.xml
  let self_2011 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "rRootageより妄想　Part01-ANOTHER by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "main";},["$rand"; " 1"; " 1"; " 1"; " 1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; "-1"; "-1"; "-1"; "-1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; "-1"; "-1"; " 1"; " 1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; " 1"; " 1"; "-1"; "-1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; " 1"; "-1"; "-1"; " 1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; "-1"; " 1"; " 1"; "-1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; "-1"; " 1"; "-1"; " 1"]);
            Action.ActionRef ({actionRefLabel = "main";},["$rand"; " 1"; "-1"; " 1"; "-1"])]);
        BulletmlElm.Action
          ({actionLabel = Some "main";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"45")),
               Some (Speed (None,"1")),
               BulletRef ({bulletRefLabel = "cross";},["$1"; "$2"]));
            Repeat
              (Times "1+$rank*2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.2")),
                      BulletRef ({bulletRefLabel = "cross";},["$1"; "$2"]))]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),
               Some (Speed (None,"1")),
               BulletRef ({bulletRefLabel = "cross";},["$1"; "$3"]));
            Repeat
              (Times "1+$rank*2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.2")),
                      BulletRef ({bulletRefLabel = "cross";},["$1"; "$3"]))]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),
               Some (Speed (None,"1")),
               BulletRef ({bulletRefLabel = "cross";},["$1"; "$4"]));
            Repeat
              (Times "1+$rank*2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.2")),
                      BulletRef ({bulletRefLabel = "cross";},["$1"; "$4"]))]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),
               Some (Speed (None,"1")),
               BulletRef ({bulletRefLabel = "cross";},["$1"; "$5"]));
            Repeat
              (Times "1+$rank*2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.2")),
                      BulletRef ({bulletRefLabel = "cross";},["$1"; "$5"]))]));
            Wait "200-$rank*100"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "cross";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "15"; ChangeSpeed (Speed (None,"0"),Term "1");
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360*$1*$2")),
                   Some (Speed (None,"1+$rank*$rank*1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"180*$2")),
                   Some (Speed (None,"1+$rank*$rank*1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "49",
                   Action
                     ({actionLabel = None;},
                      [Wait "4-$rank*2";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"187.7*$2")),
                          Some (Speed (None,"1+$rank*$rank*1")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"180*$2")),
                          Some (Speed (None,"1+$rank*$rank*1")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// rRootageより妄想　Part02 by 白い弾幕くん
  /// [OtakuTwo]_self-2020.xml
  let self_2020 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "rRootageより妄想　Part02 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1"); Wait "1";
            ChangeSpeed (Speed (None,"5"),Term "1"); Wait "15";
            ChangeSpeed (Speed (None,"0"),Term "1");
            Repeat
              (Times "45",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "seed";},[" 1"]);
                   FireRef ({fireRefLabel = "seed";},["-1"]); Wait "30"]));
            Wait "450"]);
        BulletmlElm.Fire
          ({fireLabel = Some "seed";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"90*$1")),
           Some (Speed (None,"(1.5-$rank*0.5)")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "$rand*(30-$rank*15)";
                   Repeat
                     (Times "9999",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                             Some (Speed (None,"1")),
                             BulletRef ({bulletRefLabel = "bomb";},[]));
                          Wait "30-$rank*15"]))])]));
        BulletmlElm.Bullet
          ({bulletLabel = Some "bomb";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "225";
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction
                        (Some {directionType = Aim;},
                         "-20+($rand-0.5)*$rank*$rank*$rank*$rank*$rank*$rank*$rank*$rank*20")),
                   Some (Speed (None,"1.5+$rank*$rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"20")),
                          Some (Speed (None,"1.5+$rank*$rank")),
                          Bullet ({bulletLabel = None;},None,None,[]))]))])])])

  /// おたくツーさん作、自機拘束弾・低速移動型 by 白い弾幕くん
  /// [OtakuTwo]_slow_move.xml
  let slow_move = 
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "おたくツーさん作、自機拘束弾・低速移動型 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [FireRef ({fireRefLabel = "lr";},[" 90"; "1.5"]);
            FireRef ({fireRefLabel = "lr";},["-90"; "1.5"]);
            Action.ActionRef ({actionRefLabel = "move";},["  0"; "0.9"]); Wait "150";
            Repeat
              (Times "100+100*$rank",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "bara";},[" 90"]);
                   FireRef ({fireRefLabel = "bara";},["-90"]); Wait "10-$rank*5"]));
            Wait "30"; Action.ActionRef ({actionRefLabel = "move";},["180"; "0.9"])]);
        BulletmlElm.Action
          ({actionLabel = Some "move";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"$1"),Term "10");
            Wait "12"; ChangeSpeed (Speed (None,"$2"),Term "50"); Wait "55";
            ChangeSpeed (Speed (None," 0"),Term "50"); Wait "55"]);
        BulletmlElm.Fire
          ({fireLabel = Some "bara";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
           Some (Speed (None,"1+$rank*5")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "10";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"30-$rand*60")),
                      Some (Speed (None,"1")),
                      Bullet ({bulletLabel = None;},None,None,[])); Vanish])]));
        BulletmlElm.Fire
          ({fireLabel = Some "lr";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "move";},["$1"; "$2"]);
                   FireRef ({fireRefLabel = "tb";},["  0"; "0.9"]);
                   FireRef ({fireRefLabel = "tb";},["180"; "3.0"]); Vanish])]));
        BulletmlElm.Fire
          ({fireLabel = Some "tb";},None,Some (Speed (None,"0")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "move";},["$1"; "$2"]); Wait "20";
                   ChangeDirection
                     (Direction (Some {directionType = Aim;},"0"),Term "5");
                   Wait "10"; Action.ActionRef ({actionRefLabel = "shot";},[]); Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "shot";},
           [Repeat
              (Times "500",
               Action
                 ({actionLabel = None;},
                  [ChangeDirection
                     (Direction (Some {directionType = Aim;},"0"),Term "10+$rank*10");
                   FireRef ({fireRefLabel = "winder";},[" 20-$rank*10"]);
                   FireRef ({fireRefLabel = "winder";},["-20+$rank*10"]); Wait "2"]));
            Vanish]);
        BulletmlElm.Fire
          ({fireLabel = Some "winder";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
           Some (Speed (None,"8")),Bullet ({bulletLabel = None;},None,None,[]))])