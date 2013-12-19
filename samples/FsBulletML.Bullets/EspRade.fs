namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// EspRade
[<RequireQualifiedAccess>]
module EspRade =

  /// エスプレイド、最終面後半「アリスクローン」by 白い弾幕くん
  /// [ESP_RADE]_round_5_alice_clone.xml
  let ``round_5_alice_clone`` =
    "エスプレイド、最終面後半「アリスクローン」by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Fire
          ({fireLabel = Some "alice";},Some (Direction (None,"$rand*360")),
           Some (Speed (None,"8")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "10*$rand";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"$rand*30-15")),
                      None,Bullet ({bulletLabel = None;},None,None,[])); Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "600",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "alice";},[]); Wait "$rank+1+$rand"]));
            Wait "100"])])

  /// エスプレイド、無敵の軍神アレス第二形態 by 白い弾幕くん
  /// [ESP_RADE]_round_5_boss_ares_2.xml
  let ``round_5_boss_ares_2`` = 
    "エスプレイド、無敵の軍神アレス第二形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Action
          ({actionLabel = Some "XWay";},
           [Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$2")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aim3";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10";
                Fire
                  ({fireLabel = None;},None,Some (Speed (None,"0")),
                   BulletRef ({bulletRefLabel = "aim3Impl";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aim3Impl";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "7",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-33+$rand*6")),
                          Some (Speed (None,"1.5")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef
                         ({actionRefLabel = "XWay";},
                          ["3"; "30"]);
                       Repeat
                         (Times "2+$rank*3",
                          Action
                            ({actionLabel = None;},
                             [Wait "3";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"-60")),
                                 Some (Speed (None,"1.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Action.ActionRef
                                ({actionRefLabel = "XWay";},
                                 ["3"; "30"])]));
                       Wait "54-$rank*9"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aim";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10";
                Fire
                  ({fireLabel = None;},None,Some (Speed (None,"0")),
                   BulletRef ({bulletRefLabel = "aimImpl";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aimImpl";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "7",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-3+$rand*6")),
                          Some (Speed (None,"1.5")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "2+$rank*3",
                          Action
                            ({actionLabel = None;},
                             [Wait "3";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some (Speed (None,"1.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]));
                       Wait "54-$rank*9"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "fan";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "3+$rank*4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"$1-$2*3")),
                          Some (Speed (None,"$3")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef
                         ({actionRefLabel = "XWay";},
                          ["7"; "10"]);
                       Wait "420/(3+$rank*4)"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"110")),
               Some (Speed (None,"4")),
               BulletRef ({bulletRefLabel = "aim3";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-110")),
               Some (Speed (None,"4")),
               BulletRef ({bulletRefLabel = "aim3";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"125")),
               Some (Speed (None,"5")),
               BulletRef ({bulletRefLabel = "aim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-125")),
               Some (Speed (None,"5")),
               BulletRef ({bulletRefLabel = "aim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"150")),
               Some (Speed (None,"7")),
               BulletRef ({bulletRefLabel = "aim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-150")),
               Some (Speed (None,"7")),
               BulletRef ({bulletRefLabel = "aim";},[])); Wait "10";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"6")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["-135"; "10"; "1.3"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               Some (Speed (None,"6")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["135"; "10"; "1.3"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"110")),
               Some (Speed (None,"4")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["-164"; "8"; "1.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-110")),
               Some (Speed (None,"4")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["156"; "8"; "1.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"130")),
               Some (Speed (None,"2")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["180"; "8"; "1.1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-130")),
               Some (Speed (None,"2")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["180"; "5"; "1.1"]));
            Wait "430"])])

  /// エスプレイド、ガラ婦人第一形態の片方 by 白い弾幕くん
  /// [ESP_RADE]_round_5_boss_gara_1_a.xml
  let ``round_5_boss_gara_1_a`` =
    "エスプレイド、ガラ婦人第一形態の片方 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "sequenceThree";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"12")),
               Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Action.ActionRef ({actionRefLabel = "sequenceTwo";},[])]);
        BulletmlElm.Action
          ({actionLabel = Some "sequenceTwo";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"3")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "oogi";},
           [Fire
              ({fireLabel = None;},Some (Direction (None,"-90")),
               Some (Speed (None,"1.5")),Bullet ({bulletLabel = None;},None,None,[]));
            Action.ActionRef ({actionRefLabel = "sequenceTwo";},[]);
            Repeat (Times "11",ActionRef ({actionRefLabel = "sequenceThree";},[]));
            Wait "10"]);
        BulletmlElm.Action
          ({actionLabel = Some "oogiOuHuku";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-213")),
               Some (Speed (None,"1.5")),Bullet ({bulletLabel = None;},None,None,[]));
            Action.ActionRef ({actionRefLabel = "sequenceTwo";},[]);
            Repeat (Times "11",ActionRef ({actionRefLabel = "sequenceThree";},[]));
            Wait "10"]);
        BulletmlElm.Action
          ({actionLabel = Some "gara1a";},
           [Repeat
              (Times "5",
               Action
                 ({actionLabel = None;},
                  [ChangeDirection (Direction (None,"360*$rand"),Term "1");
                   ChangeSpeed (Speed (None,"0.5*$rand+0.5"),Term "1");
                   Action.ActionRef ({actionRefLabel = "oogi";},[]);
                   Repeat
                     (Times "$rand*(3+$rank*2)+1+$rank*2",
                      Action
                        ({actionLabel = None;},
                         [Action.ActionRef ({actionRefLabel = "oogiOuHuku";},[])]));
                   ChangeSpeed (Speed (None,"0"),Term "1"); Wait "50"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},[Action.ActionRef ({actionRefLabel = "gara1a";},[])])])

  /// エスプレイド、ガラ第一形態のもう一方 by 白い弾幕くん
  /// [ESP_RADE]_round_5_boss_gara_1_b.xml
  let ``round_5_boss_gara_1_b`` =
    "エスプレイド、ガラ第一形態のもう一方 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "8way2";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180-75")),
               Some (Speed (None,"4")),Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "7",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.25")),
                      Bullet ({bulletLabel = None;},None,None,[]))]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180+75")),
               Some (Speed (None,"4")),Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "7",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"-9")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.25")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "downShot";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-60+$rand*120")),
               Some (Speed (None,"4*$rand")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Wait "20";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),

                          Some (Speed (None,"1.2")),
                          Bullet ({bulletLabel = None;},None,None,[])); Vanish])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "gara";},
           [ChangeDirection
              (Direction (Some {directionType = Aim;},"10+$rand*340"),Term "1");
            ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"0.3"),Term "1");
            Repeat
              (Times "3+$rank*4",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "8",
                      Action
                        ({actionLabel = None;},
                         [Action.ActionRef ({actionRefLabel = "downShot";},[]);
                          Wait "3*(3-$rank*2)*$rand"]));
                   Action.ActionRef ({actionRefLabel = "8way2";},[])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat (Times "5",ActionRef ({actionRefLabel = "gara";},[]));
            ChangeSpeed (Speed (None,"0"),Term "1"); Wait "30"])])

  /// エスプレイド、ガラ婦人第二形態 by 白い弾幕くん
  /// [ESP_RADE]_round_5_boss_gara_2.xml
  let ``round_5_boss_gara_2`` =
    "エスプレイド、ガラ婦人第二形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "featherShot";},None,Some (Speed (None,"6")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "20"); Wait "20";
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "featherAim";},[]));
                Repeat
                  (Times "150+$rank*100",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},Some (Direction (None,"90*$rand-45")),

                          None,Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "3-$rank*2"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "featherAim";},None,Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "7",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},None,Some (Speed (None,"3")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "20",
                          Action
                            ({actionLabel = None;},
                             [Wait "2";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some (Speed (None,"3")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]));
                       Wait "30"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
               BulletRef ({bulletRefLabel = "featherShot";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),None,
               BulletRef ({bulletRefLabel = "featherShot";},[])); Wait "550"])])

  /// エスプレイド、ガラ第三形態 by 白い弾幕くん
  /// [ESP_RADE]_round_5_boss_gara_3.xml
  let ``round_5_boss_gara_3`` =
    "エスプレイド、ガラ第三形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "stop";},
           [Wait "15"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "1"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "featherAllWay";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some
                     (Direction
                        (Some {directionType = DirectionType.Relative;},"$2*(180-(10-$1)*60)")),
                   Some (Speed (None,"0")),
                   Bullet
                     ({bulletLabel = None;},None,None,
                      [Action ({actionLabel = None;},[Vanish])]));
                Action.ActionRef ({actionRefLabel = "stop";},[]);
                Repeat
                  (Times "40",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"-2*(7-$1)*$2")),
                          Some (Speed (None,"0.9+0.2*(6-$1)")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "$1-1",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"-2*$2")),
                                 Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]));
                       Wait "15"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "featherAim";},None,None,
           [Action
              ({actionLabel = None;},
               [Action.ActionRef ({actionRefLabel = "stop";},[]);
                Repeat
                  (Times "10+$rank*20",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = Aim;},"-3")),
                          Some (Speed (None,"1.2")),
                          Bullet
                            ({bulletLabel = None;},None,None,
                             [Action ({actionLabel = None;},[])]));
                       Repeat
                         (Times "2",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"3")),
                                 Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                                 Bullet
                                   ({bulletLabel = None;},None,None,
                                    [Action ({actionLabel = None;},[])]))]));
                       Wait "40-$rank*20"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"1")),
               BulletRef ({bulletRefLabel = "featherAim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               Some (Speed (None,"1")),
               BulletRef ({bulletRefLabel = "featherAim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"70")),
               Some (Speed (None,"2")),
               BulletRef ({bulletRefLabel = "featherAim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-70")),
               Some (Speed (None,"2")),
               BulletRef ({bulletRefLabel = "featherAim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"100")),
               Some (Speed (None,"1.8")),
               BulletRef ({bulletRefLabel = "featherAllWay";},["3"; "1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-100")),
               Some (Speed (None,"1.8")),
               BulletRef ({bulletRefLabel = "featherAllWay";},["3"; "-1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"3")),
               BulletRef ({bulletRefLabel = "featherAllWay";},["4"; "1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               Some (Speed (None,"3")),
               BulletRef ({bulletRefLabel = "featherAllWay";},["4"; "-1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"85")),
               Some (Speed (None,"4")),
               BulletRef ({bulletRefLabel = "featherAllWay";},["5"; "1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-85")),
               Some (Speed (None,"4")),
               BulletRef ({bulletRefLabel = "featherAllWay";},["5"; "-1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"72")),
               Some (Speed (None,"5")),
               BulletRef ({bulletRefLabel = "featherAllWay";},["6"; "1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-72")),
               Some (Speed (None,"5")),
               BulletRef ({bulletRefLabel = "featherAllWay";},["6"; "-1"]));
            Wait "700"])])

  /// エスプレイド、ガラ婦人第四形態 by 白い弾幕くん
  /// [ESP_RADE]_round_5_boss_gara_4.xml
  let ``round_5_boss_gara_4`` =
    "エスプレイド、ガラ婦人第四形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "featherShot";},None,Some (Speed (None,"7")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "20"); Wait "20";
                Repeat
                  (Times "50",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},Some (Direction (None,"20*$rand-10")),

                          Some (Speed (None,"2*$rand+0.7")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,

                      BulletRef ({bulletRefLabel = "featherShot";},[]));
                   Wait "30+$rank*30";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),None,
                      BulletRef ({bulletRefLabel = "featherShot";},[]));
                   Wait "30+$rank*30"])); Wait "120";
            Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,

                      BulletRef ({bulletRefLabel = "featherShot";},[]));
                   Wait "40-$rank*20";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),None,
                      BulletRef ({bulletRefLabel = "featherShot";},[]));
                   Wait "40-$rank*20"])); Wait "60"])])

  /// エスプレイド、ガラ婦人最終形態 by 白い弾幕くん
  /// [ESP_RADE]_round_5_boss_gara_5.xml
  let ``round_5_boss_gara_5`` =
    "エスプレイド、ガラ婦人最終形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "accel";},
           [ChangeDirection (Direction (None,"360*$rand"),Term "1");
            ChangeSpeed (Speed (None,"0.5+$rand*0.5"),Term "1")]);
        BulletmlElm.Action
          ({actionLabel = Some "stop";},[ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Action
          ({actionLabel = Some "stopAndWait";},
           [Action.ActionRef ({actionRefLabel = "stop";},[]); Wait "70-$rank*50"]);
        BulletmlElm.Action
          ({actionLabel = Some "ippon";},
           [Repeat
              (Times "26",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.12")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "murasaki";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$2")),
               Some (Speed (None,"0.8")),Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "3+$rand*17",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "ippon";},[]); Wait "6";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"$1")),
                      Some (Speed (None,"0.8")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "ao";},
           [Repeat
              (Times "3+$rand*17",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,Some (Speed (None,"0.8")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "ippon";},[]); Wait "6"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "gara5";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "accel";},[]);
                   Action.ActionRef ({actionRefLabel = "murasaki";},["5"; "180-$rand*90"]);

                   Action.ActionRef ({actionRefLabel = "stopAndWait";},[]);
                   Action.ActionRef ({actionRefLabel = "accel";},[]);
                   Action.ActionRef ({actionRefLabel = "ao";},[]);
                   Action.ActionRef ({actionRefLabel = "stopAndWait";},[]);
                   Action.ActionRef ({actionRefLabel = "accel";},[]);
                   Action.ActionRef ({actionRefLabel = "murasaki";},["-5"; "180+$rand*90"]);
                   Action.ActionRef ({actionRefLabel = "stopAndWait";},[]);
                   Action.ActionRef ({actionRefLabel = "accel";},[]);
                   Action.ActionRef ({actionRefLabel = "ao";},[]);
                   Action.ActionRef ({actionRefLabel = "stopAndWait";},[])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},[Action.ActionRef ({actionRefLabel = "gara5";},[])])
    ])

  /// エスプレイド、五行覚師、発狂。by 白い弾幕くん
  /// [ESP_RADE]_round_5_boss_kakusi_hakkyou.xml
  let ``round_5_boss_kakusi_hakkyou`` =
    "エスプレイド、五行覚師、発狂。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "6shots";},None,Some (Speed (None,"3")),
           [Action
              ({actionLabel = None;},
               [Wait "3";
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = Aim;},"-15+30*$rand")),
                          Some (Speed (None,"0.8+$rank+$rand")),
                          Bullet ({bulletLabel = None;},None,None,[]))]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = Aim;},"-45+30*$rand")),
                          Some (Speed (None,"0.8+$rank+$rand")),
                          Bullet ({bulletLabel = None;},None,None,[]))]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = Aim;},"15+30*$rand")),

                          Some (Speed (None,"0.8+$rank+$rand")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "kakusi";},None,Some (Speed (None,"6")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "10"); Wait "10";
                Repeat
                  (Times "4+$rank*6",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = Aim;},"90")),None,
                          BulletRef ({bulletRefLabel = "6shots";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = Aim;},"-90")),None,

                          BulletRef ({bulletRefLabel = "6shots";},[]));
                       Wait "200/(4+$rank*6)"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
               BulletRef ({bulletRefLabel = "kakusi";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),None,
               BulletRef ({bulletRefLabel = "kakusi";},[])); Wait "200"])])

  /// エスプレイド、1-3面のボスとなる、IZUNA発狂 by 白い弾幕くん
  /// [ESP_RADE]_round_123_boss_izuna_hakkyou.xml
  let ``round_123_boss_izuna_hakkyou`` =
    "エスプレイド、1-3面のボスとなる、IZUNA発狂 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "Red";},None,None,[Action ({actionLabel = None;},[])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Action
          ({actionLabel = Some "XWay";},
           [Action.ActionRef
              ({actionRefLabel = "XWayFan";},
               ["$1"; "$2"; "0"])]);
        BulletmlElm.Action
          ({actionLabel = Some "XWayFan";},
           [Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$2")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"$3")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "roll";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"90*$1")),
           Some (Speed (None,"3")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"1.5+$rank")),
                   BulletRef ({bulletRefLabel = "Dummy";},[]));
                Repeat
                  (Times "10",
                   Action
                     ({actionLabel = None;},
                      [Wait "8";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"5.3*$1")),
                          Some
                            (Speed
                               (Some {speedType = SpeedType.Sequence;},"-0.3-$rank*0.5")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef
                         ({actionRefLabel = "XWay";},
                          ["8"; "45"]);
                       Repeat
                         (Times "5",
                          Action
                            ({actionLabel = None;},
                             [Wait "8";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"3*$1")),
                                 Some
                                   (Speed
                                      (Some {speedType = SpeedType.Sequence;},"0.06+$rank*0.1")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Action.ActionRef
                                ({actionRefLabel = "XWay";},
                                 ["8"; "45"])]))]));
                Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "roll";},["1"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "roll";},["-1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
               Some (Speed (None,"2")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Wait "10";
                       Action.ActionRef ({actionRefLabel = "Stop";},[]);
                       Action.ActionRef ({actionRefLabel = "aim";},[]); Vanish])]));
            Wait "500"]);
        BulletmlElm.Action
          ({actionLabel = Some "aim";},
           [Repeat
              (Times "10",
               Action
                 ({actionLabel = None;},
                  [Wait "50";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = Aim;},"-1")),
                      Some (Speed (None,"1.7")),
                      BulletRef ({bulletRefLabel = "Red";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"2")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      BulletRef ({bulletRefLabel = "Red";},[]));
                   Repeat
                     (Times "2+$rank*6",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"-2")),
                             Some
                               (Speed
                                  (Some {speedType = SpeedType.Sequence;},"0.1")),
                             BulletRef ({bulletRefLabel = "Red";},[]));
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"2")),
                             Some
                               (Speed
                                  (Some {speedType = SpeedType.Sequence;},"0")),
                             BulletRef ({bulletRefLabel = "Red";},[]))]))]))])])

  /// エスプレイド、1-3面のボスとなる、ペラボーイ発狂 by 白い弾幕くん
  /// [ESP_RADE]_round_123_boss_pelaboy_hakkyou.xml
  let ``round_123_boss_pelaboy_hakkyou`` =
    "エスプレイド、1-3面のボスとなる、ペラボーイ発狂 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "Red";},None,None,[Action ({actionLabel = None;},[])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Action
          ({actionLabel = Some "XWay";},
           [Action.ActionRef
              ({actionRefLabel = "XWayFan";},
               ["$1"; "$2"; "0"])]);
        BulletmlElm.Action
          ({actionLabel = Some "XWayFan";},
           [Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$2")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"$3")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "subBatteryFan";},None,
           Some (Speed (None,"4")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Wait "250";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"-45")),
                   Some (Speed (None,"1.6")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Action.ActionRef
                  ({actionRefLabel = "XWay";},
                   ["10+$rank*10"; "90/(10+$rank*10)"]);
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Wait "5";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},
                                "-90")),
                          Some (Speed (None,"1.6")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef
                         ({actionRefLabel = "XWay";},
                          ["11+$rank*10";"90/(10+$rank*10)"])])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aimFan";},None,Some (Speed (None,"4")),
           [Action
              ({actionLabel = None;},
               [Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction
                        (Some {directionType = Aim;},"-4-$rank*8")),
                   Some (Speed (None,"1.6")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Action.ActionRef
                  ({actionRefLabel = "XWay";},
                   ["5+$rank*8"; "2"]); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "subBatteryAim";},None,
           Some (Speed (None,"1")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Wait "100+$rand*50";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"90")),
                          None,BulletRef ({bulletRefLabel = "aimFan";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"-90")),None,
                          BulletRef ({bulletRefLabel = "aimFan";},[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "soldier";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"90*$1")),
           Some (Speed (None,"2")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction (Some {directionType = DirectionType.Absolute;},"$2")),
                   Some (Speed (None,"1.3")),
                   BulletRef ({bulletRefLabel = "Dummy";},[]));
                Repeat
                  (Times "120+$rank*200",
                   Action
                     ({actionLabel = None;},
                      [Wait "440/(120+$rank*200)+$rand";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"17*$1")),
                          Some
                            (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                          Bullet ({bulletLabel = None;},None,None,[]))]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "soldier";},["1"; "90"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "soldier";},
                  ["-1"; "-80"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
               None,BulletRef ({bulletRefLabel = "subBatteryAim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               None,BulletRef ({bulletRefLabel = "subBatteryFan";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               None,BulletRef ({bulletRefLabel = "subBatteryFan";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
               Some (Speed (None,"2")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Wait "10";
                       Fire
                         ({fireLabel = None;},None,
                          Some (Speed (None,"0")),
                          Bullet
                            ({bulletLabel = None;},None,None,
                             [Action
                                ({actionLabel = None;},
                                 [Action.ActionRef ({actionRefLabel = "mainBattery";},[]);
                                  Vanish])])); Vanish])])); Wait "500"]);
        BulletmlElm.Action
          ({actionLabel = Some "mainBattery";},
           [Repeat
              (Times "15",
               Action
                 ({actionLabel = None;},
                  [Wait "8";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = Aim;},"0")),
                      Some (Speed (None,"1.6")),
                      Bullet ({bulletLabel = None;},None,None,[]))]));
            Wait "195";
            Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Wait "20";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"88+$rand*4")),
                      Some (Speed (None,"1.6")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWay";},
                      ["12+$rank*16";"180/(12+$rank*16)"]); Wait "20";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"93+$rand*4")),
                      Some (Speed (None,"1.6")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWay";},
                      ["11+$rank*16";"170/(11+$rank*16)"])])); Wait "40"])])

  /// エスプレイド、1-3面のボスとなる、近江悟君 by 白い弾幕くん
  /// [ESP_RADE]_round_123_boss_satoru_5way.xml
  let ``round_123_boss_satoru_5way`` =
    "エスプレイド、1-3面のボスとなる、近江悟君 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "1way";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$2+$1*$rand*2-$1")),
               Some (Speed (None,"1")),Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = Aim;},"$2+$1*$rand*2-$1")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.1")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "5way";},
           [Action.ActionRef ({actionRefLabel = "1way";},["$1"; "-30"]);
            Action.ActionRef ({actionRefLabel = "1way";},["$1"; "-15"]);
            Action.ActionRef ({actionRefLabel = "1way";},["$1"; "0"]);
            Action.ActionRef ({actionRefLabel = "1way";},["$1"; "15"]);
            Action.ActionRef ({actionRefLabel = "1way";},["$1"; "30"])]);
        BulletmlElm.Action
          ({actionLabel = Some "idousite5way";},
           [ChangeDirection (Direction (None,"$rand*360"),Term "1");
            ChangeSpeed (Speed (None,"2"),Term "1"); Wait "30";
            Action.ActionRef ({actionRefLabel = "5way";},["$1"]);
            ChangeSpeed (Speed (None,"0"),Term "1"); Wait "90-$rank*60"]);
        BulletmlElm.Action
          ({actionLabel = Some "satoru";},
           [Action.ActionRef ({actionRefLabel = "idousite5way";},["1"]);
            Action.ActionRef ({actionRefLabel = "idousite5way";},["2"]);
            Action.ActionRef ({actionRefLabel = "idousite5way";},["3"]);
            Action.ActionRef ({actionRefLabel = "idousite5way";},["4"]);
            Action.ActionRef ({actionRefLabel = "idousite5way";},["5"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "satoru";},[]); Wait "30"])])
