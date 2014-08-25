namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Bulletsmorph
[<RequireQualifiedAccess>]
module Bulletsmorph =

  /// Bulletsmorphで生成。紋章遺伝学その二。by 白い弾幕くん
  /// [Bulletsmorph]_aba_2.xml
  let aba_2 =  
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "Bulletsmorphで生成。紋章遺伝学その二。by 白い弾幕くん";
        bulletmlDescription = None},
        [BulletmlElm.Action
          ({actionLabel = Some "top";},
            [Repeat
              (Times "8",
                Action
                  ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "center";},["90 * $rand"; "1"]);
                    Wait "12";
                    Action.ActionRef ({actionRefLabel = "center";},["90 * $rand"; "-1"]);
                    Wait "12";
                    Action.ActionRef ({actionRefLabel = "center";},["30 * $rand"; "1"]);
                    Wait "12";
                    Action.ActionRef ({actionRefLabel = "center";},["30 * $rand"; "-1"]);
                    Wait "12"])); Wait "150"]);
        BulletmlElm.Action
          ({actionLabel = Some "center";},
            [Fire
              ({fireLabel = None;},
                Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                None,BulletRef ({bulletRefLabel = "circle";},["$1"; "$2"]));
            Repeat
              (Times "(4 + 8 * $rank) - 1",
                Action
                  ({actionLabel = None;},
                  [Fire
                      ({fireLabel = None;},
                      Some
                        (Direction
                            (Some {directionType = DirectionType.Sequence;},"360 / (4 + 8 * $rank)")),
                      None,BulletRef ({bulletRefLabel = "circle";},["$1"; "$2"]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "circle";},None,Some (Speed (None,"1.3")),
            [Action
              ({actionLabel = None;},
                [Wait "20";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"180 + $1 * $2"),
                    Term "1"); Wait "125 - $1";
                Fire
                  ({fireLabel = None;},
                    Some (Direction (Some {directionType = Aim;},"0")),None,
                    BulletRef ({bulletRefLabel = "red";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "red";},None,Some (Speed (None,"0.1")),
            [Action
              ({actionLabel = None;},[ChangeSpeed (Speed (None,"4.0"),Term "300")])])])

  /// Bulletsmorphで生成。紋章遺伝学その三。by 白い弾幕くん
  /// [Bulletsmorph]_aba_3.xml
  let aba_3 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "Bulletsmorphで生成。紋章遺伝学その三。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "4 + 16 * $rank",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"120 + 120 * $rand")),
                      None,BulletRef ({bulletRefLabel = "bomb";},[]));
                   Wait "60 - 30 * $rank"])); Wait "180"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bomb";},None,Some (Speed (None,"0.5 + 1.9 * $rand")),
           [Action
              ({actionLabel = None;},
               [Wait "50";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                   None,BulletRef ({bulletRefLabel = "bombbit";},[]));
                Repeat
                  (Times "(4 + 8 * $rank) - 1",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},
                                "360 / (4 + 8 * $rank)")),None,
                          BulletRef ({bulletRefLabel = "bombbit";},[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bombbit";},None,Some (Speed (None,"0.8")),
           [Action
              ({actionLabel = None;},
               [Wait "120";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"120")),
                   Some (Speed (None,"1.3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"240")),
                   Some (Speed (None,"1.3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),
                   Some (Speed (None,"1.3")),
                   BulletRef ({bulletRefLabel = "changecolor";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "changecolor";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (Some {speedType = SpeedType.Relative;},"0")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])

  /// Bulletsmorphで生成。紋章遺伝学その四。by 白い弾幕くん
  /// [Bulletsmorph]_aba_4.xml
  let aba_4 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "Bulletsmorphで生成。紋章遺伝学その四。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,Some (Speed (None,"0.1")),
               BulletRef ({bulletRefLabel = "cross";},[])); Wait "5";
            Repeat
              (Times "40 + 60 * $rank",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.04")),
                      BulletRef ({bulletRefLabel = "cross";},[]));
                   Wait "20 - 10 * $rank"])); Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "cross";},
           Some (Direction (Some {directionType = Aim;},"0")),None,
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (Some {speedType = SpeedType.Relative;},"4.0"),Term "300");

                Wait "45";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
                   Some (Speed (None,"1.3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
                   Some (Speed (None,"1.3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
                   Some (Speed (None,"1.3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),
                   Some (Speed (None,"1.3")),
                   Bullet ({bulletLabel = None;},None,None,[]))])])])

  /// Bulletsmorphで生成。紋章遺伝学その五。by 白い弾幕くん
  /// [Bulletsmorph]_aba_5.xml
  let aba_5 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "Bulletsmorphで生成。紋章遺伝学その五。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
               BulletRef ({bulletRefLabel = "bit";},["1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
               BulletRef ({bulletRefLabel = "bit";},["-1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),None,
               BulletRef ({bulletRefLabel = "bit";},["1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),None,
               BulletRef ({bulletRefLabel = "bit";},["-1"]));
            Repeat
              (Times "300",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"-(120 + 45 * $rank) + (240 + 90 * $rank) * $rand")),
                      Some (Speed (None,"1.6")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "5",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                             Some (Speed (Some {speedType = SpeedType.Sequence;},"0.2")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "2"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,Some (Speed (None,"0.2")),
           [Action
              ({actionLabel = None;},
               [Wait "60"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction
                        (Some {directionType = Aim;},"(45 - 25 * $rank) * $1")),None,
                   BulletRef ({bulletRefLabel = "backstab";},[])); Wait "3";
                Repeat
                  (Times "29",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"-0.5 * $1")),None,
                          BulletRef ({bulletRefLabel = "backstab";},[])); Wait "3"]));
                Repeat
                  (Times "30",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"0.5 * $1")),
                          None,BulletRef ({bulletRefLabel = "backstab";},[]));
                       Wait "3"]));
                Repeat
                  (Times "30",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"-0.5 * $1")),None,
                          BulletRef ({bulletRefLabel = "backstab";},[])); Wait "3"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "backstab";},None,Some (Speed (None,"1.6")),
           [Action
              ({actionLabel = None;},
               [Wait "70 + 20 * $rand";
                ChangeDirection
                  (Direction (Some {directionType = Aim;},"0"),Term "1")])])])

  /// Bulletsmorphで生成。紋章遺伝学その六。by 白い弾幕くん
  /// [Bulletsmorph]_aba_6.xml
  let aba_6 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "Bulletsmorphで生成。紋章遺伝学その六。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "allway";},[]);
            Action.ActionRef ({actionRefLabel = "bar";},[]); Wait "200"]);
        BulletmlElm.Action
          ({actionLabel = Some "allway";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"15")),None,
               BulletRef ({bulletRefLabel = "allwaybit";},[]));
            Repeat
              (Times "11",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),None,
                      BulletRef ({bulletRefLabel = "allwaybit";},[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "allwaybit";},None,Some (Speed (None,"6.0")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"90")),
                          None,BulletRef ({bulletRefLabel = "stopandgo";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-90")),
                          None,BulletRef ({bulletRefLabel = "stopandgo";},[]));
                       Wait "6 - 4 * $rank"]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "stopandgo";},None,Some (Speed (None,"1.0")),
           [Action
              ({actionLabel = None;},
               [Wait "20"; ChangeSpeed (Speed (None,"0.0001"),Term "1"); Wait "40";
                ChangeSpeed (Speed (None,"4.0"),Term "300")])]);
        BulletmlElm.Action
          ({actionLabel = Some "bar";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "barhand";},["1"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "barhand";},["-1"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "barhand";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0.0001")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
                   Some (Speed (None,"4.0 - 2.0 * $rank")),
                   BulletRef ({bulletRefLabel = "barbit";},["1"]));
                Repeat
                  (Times "2 + 3 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          Some
                            (Speed
                               (Some {speedType = SpeedType.Sequence;},"4.0 - 2.0 * $rank")),
                          BulletRef ({bulletRefLabel = "barbit";},["1"]))]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"180")),
                   Some (Speed (None,"4.0 - 2.0 * $rank")),
                   BulletRef ({bulletRefLabel = "barbit";},["-1"]));
                Repeat
                  (Times "2 + 3 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          Some
                            (Speed
                               (Some {speedType = SpeedType.Sequence;},"4.0 - 2.0 * $rank")),
                          BulletRef ({bulletRefLabel = "barbit";},["-1"]))]));
                Wait "5";
                Repeat
                  (Times "20",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"180 + 10 * $1")),
                          Some (Speed (None,"4.0 - 2.0 * $rank")),
                          BulletRef ({bulletRefLabel = "barbit";},["1"]));
                       Repeat
                         (Times "2 + 3 * $rank",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some
                                   (Speed
                                      (Some {speedType = SpeedType.Sequence;},
                                       "4.0 - 2.0 * $rank")),
                                 BulletRef ({bulletRefLabel = "barbit";},["1"]))]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"180")),
                          Some (Speed (None,"4.0 - 2.0 * $rank")),
                          BulletRef ({bulletRefLabel = "barbit";},["-1"]));
                       Repeat
                         (Times "2 + 3 * $rank",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some
                                   (Speed
                                      (Some {speedType = SpeedType.Sequence;},
                                       "4.0 - 2.0 * $rank")),
                                 BulletRef ({bulletRefLabel = "barbit";},["-1"]))]));
                       Wait "5"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "barbit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "5"; ChangeSpeed (Speed (None,"0.0001"),Term "1"); Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"90 * $1")),
                   Some (Speed (None,"1.3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.1")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// Bulletsmorphで生成。紋章遺伝学その七。by 白い弾幕くん
  /// [Bulletsmorph]_aba_7.xml
  let aba_7 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "Bulletsmorphで生成。紋章遺伝学その七。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
                      Some (Speed (None,"1.1")),
                      BulletRef ({bulletRefLabel = "dummy";},[])); Wait "60";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
                      Some (Speed (None,"1.1")),
                      BulletRef ({bulletRefLabel = "dummy";},[])); Wait "60"]));
            Wait "250 - 50 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "60";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"-32")),
                   Some (Speed (None,"1.1")),
                   BulletRef ({bulletRefLabel = "bit";},[]));
                Repeat
                  (Times "8",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"8")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                          BulletRef ({bulletRefLabel = "bit";},[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "20";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (Some {speedType = SpeedType.Relative;},"0.3")),
                   BulletRef ({bulletRefLabel = "slowdown";},[]));
                Repeat
                  (Times "2 + 4 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.3")),
                          BulletRef ({bulletRefLabel = "slowdown";},[]))]));
                Wait "20";
                ChangeDirection
                  (Direction
                     (Some {directionType = Aim;},
                      "(30 - 20 * $rank) * (-1.0 + 2.0 * $rand)"),Term "1");
                ChangeSpeed
                  (Speed (Some {speedType = SpeedType.Relative;},"2.0 + 2.0 * $rank"),
                   Term "300")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "slowdown";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "20"; ChangeSpeed (Speed (None,"0.3"),Term "60")])])])

  /// Bulletsmorphで生成。収束全方位弾。by 白い弾幕くん
  /// [Bulletsmorph]_convergent.xml
  let convergent = 
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "Bulletsmorphで生成。収束全方位弾。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
               Some (Speed (None,"1.0")),
               BulletRef
                 ({bulletRefLabel = "nwaybit";},
                  ["90"; "1.5 * (0.5 + 0.5 * $rank)"; "3"]));
            Repeat
              (Times "35",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                      Some (Speed (None,"1.0")),
                      BulletRef
                        ({bulletRefLabel = "nwaybit";},
                         ["90"; "1.5 * (0.5 + 0.5 * $rank)"; "3"]))]));
            Repeat
              (Times "36",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                      Some (Speed (None,"1.0")),
                      BulletRef
                        ({bulletRefLabel = "nwaybit";},
                         ["-90"; "1.5 * (0.5 + 0.5 * $rank)"; "-3"]))])); Wait "150"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "nwaybit";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "4";
                Repeat
                  (Times "2 + 4 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$3")),
                          Some (Speed (None,"$2")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "4"]));
                Vanish])])])
  
  /// Bulletsmorphで生成。ダブルいろじかけ。by 白い弾幕くん
  /// [Bulletsmorph]_double_seduction.xml
  let double_seduction =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "Bulletsmorphで生成。ダブルいろじかけ。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"30")),None,
               BulletRef ({bulletRefLabel = "parentbit";},["1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-30")),None,
               BulletRef ({bulletRefLabel = "parentbit";},["-1"])); Wait "300"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "parentbit";},None,Some (Speed (None,"2.0")),
           [Action
              ({actionLabel = None;},
               [Action.ActionRef ({actionRefLabel = "cross";},["75"; "0"]);
                Action.ActionRef ({actionRefLabel = "cross";},["70"; "0"]);
                Action.ActionRef ({actionRefLabel = "cross";},["65"; "0"]);
                Action.ActionRef ({actionRefLabel = "cross";},["60"; "0"]);
                Action.ActionRef ({actionRefLabel = "cross";},["55"; "0"]);
                Action.ActionRef ({actionRefLabel = "cross";},["50"; "0"]);
                Action.ActionRef ({actionRefLabel = "cross";},["80"; "15 * $1"]);
                Action.ActionRef ({actionRefLabel = "cross";},["75"; "10 * $1"]);
                Action.ActionRef ({actionRefLabel = "cross";},["70"; "6 * $1"]);
                Action.ActionRef ({actionRefLabel = "cross";},["65"; "3 * $1"]);
                Action.ActionRef ({actionRefLabel = "cross";},["60"; "1 * $1"]);
                Action.ActionRef ({actionRefLabel = "cross";},["55"; "0"]); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "cross";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),None,
               BulletRef ({bulletRefLabel = "aimbit";},["$1"; "$2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
               BulletRef ({bulletRefLabel = "aimbit";},["$1"; "$2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),None,
               BulletRef ({bulletRefLabel = "aimbit";},["$1"; "$2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),None,
               BulletRef ({bulletRefLabel = "aimbit";},["$1"; "$2"])); Wait "5"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aimbit";},None,Some (Speed (None,"0.6")),
           [Action
              ({actionLabel = None;},
               [Wait "$1";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"$2")),
                   Some (Speed (None,"1.6 * (0.5 + 0.5 * $rank)")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "2 + 5 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.1")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// Bulletsmorphで生成。落下するひも。by 白い弾幕くん
  /// [Bulletsmorph]_fallen_string.xml
  let fallen_string = 
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;
        bulletmlName = Some "Bulletsmorphで生成。落下するひも。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "5",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "impl:48";},[]); Wait "50"]));
            Wait "50"]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:48";},
           [Wait "20";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Relative;},"-90")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"0.6")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Action.ActionRef ({actionRefLabel = "impl:60";},[]);
                       Action.ActionRef ({actionRefLabel = "impl:38";},[])])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:60";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"0")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"1")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"1.8")),
               Bullet ({bulletLabel = None;},None,None,[])); Wait "3"]);
        BulletmlElm.Bullet
          ({bulletLabel =
             Some "bulletmls/[Progear]_round_4_boss_fast_rocket.xml:_:downAccel";},
           None,None,
           [Action
              ({actionLabel = None;},
               [Accel
                  (None,Some (Vertical (Some {verticalType = VerticalType.Absolute;},"2.7")),
                   Term "120")])]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:38";},
           [FireRef
              ({fireRefLabel =
                 "bulletmls/[Progear]_round_5_middle_boss_rockets.xml:_:udBlt";},
               ["90"]); Wait "24-$rank*8";
            FireRef
              ({fireRefLabel =
                 "bulletmls/[Progear]_round_5_middle_boss_rockets.xml:_:udBlt";},
               ["-90"]); Wait "24-$rank*8"]);
        BulletmlElm.Fire
          ({fireLabel =
             Some "bulletmls/[Progear]_round_5_middle_boss_rockets.xml:_:udBlt";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"$1-25+$rand*50")),None,
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "impl:59";},[])])]));
        BulletmlElm.Action
          ({actionLabel = Some "impl:59";},
           [Repeat
              (Times "9999",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Absolute;},"1")),
                      BulletRef
                        ({bulletRefLabel =
                           "bulletmls/[Progear]_round_4_boss_fast_rocket.xml:_:downAccel";},
                         []));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"60")),
                      Some (Speed (Some {speedType = SpeedType.Absolute;},"1.8")),
                      BulletRef
                        ({bulletRefLabel =
                           "bulletmls/[Progear]_round_4_boss_fast_rocket.xml:_:downAccel";},
                         [])); Wait "3"]))])])

  /// Bulletsmorphで生成。くねくねと誘導弾。 by 白い弾幕くん                         
  /// [Bulletsmorph]_kunekune_plus_homing.xml
  let kunekune_plus_homing =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;
        bulletmlName = Some "Bulletsmorphで生成。くねくねと誘導弾。 by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "impl:259";},[]); Wait "50"]));
            Wait "60"]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:259";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"15+30*$rand")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"1.8-$rank+$rand")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Action.ActionRef ({actionRefLabel = "impl:30";},[])])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:30";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$2")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"$1")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Action.ActionRef ({actionRefLabel = "impl:156";},[]); Vanish])]));
            Repeat
              (Times "10+$rank*10",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "impl:12";},[])])); Vanish]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:156";},
           [Wait "1";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),None,
               BulletRef
                 ({bulletRefLabel =
                    "bulletmls/[G_DARIUS]_homing_laser.xml:_:hmgLsr";},[]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bulletmls/[G_DARIUS]_homing_laser.xml:_:hmgLsr";},
           None,Some (Speed (Some {speedType = SpeedType.Absolute;},"2")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"0.3"),Term "30");
                Wait "100";
                ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"5"),Term "100")]);
            Action
              ({actionLabel = None;},
               [Repeat
                  (Times "12",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = Aim;},"0"),
                          Term "45-$rank*30"); Wait "5"]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:12";},
           [Repeat
              (Times "9999",
               Action
                 ({actionLabel = None;},
                  [Wait "2";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),None,
                      Bullet ({bulletLabel = None;},None,None,[]))]))])])

  /// Bulletsmorphで生成。悟君が4人。by 白い弾幕くん
  /// [Bulletsmorph]_satoru4.xml
  let satoru4 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;
        bulletmlName = Some "Bulletsmorphで生成。悟君が4人。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "impl:100";},[]); Wait "80"]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:100";},
           [Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"$rand*16-8")),
                      Some
                        (Speed
                           (Some {speedType = SpeedType.Absolute;},
                            "($1+$rand*$1)*($rank/2+0.65)")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action
                            ({actionLabel = None;},
                             [Action.ActionRef ({actionRefLabel = "impl:205";},[])])]));
                   Wait "1"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "impl:205";},
           [Action.ActionRef
              ({actionRefLabel =
                 "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:idousite5way";},
               ["$rank*3+$rand"])]);
        BulletmlElm.Action
          ({actionLabel =
             Some
               "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:idousite5way";},
           [ChangeDirection
              (Direction (Some {directionType = Aim;},"$rand*360"),Term "1");
            ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"2"),Term "1");
            Wait "30";
            Action.ActionRef
              ({actionRefLabel =
                 "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:5way";},
               ["$1"]);
            ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"0"),Term "1"); Vanish]);
        BulletmlElm.Action
          ({actionLabel =
             Some "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:5way";},
           [Action.ActionRef
              ({actionRefLabel =
                 "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:1way";},
               ["$1"; "-30"]);
            Action.ActionRef
              ({actionRefLabel =
                 "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:1way";},
               ["$1"; "-15"]);
            Action.ActionRef
              ({actionRefLabel =
                 "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:1way";},
               ["$1"; "0"]);
            Action.ActionRef
              ({actionRefLabel =
                 "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:1way";},
               ["$1"; "15"]);
            Action.ActionRef
              ({actionRefLabel =
                 "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:1way";},
               ["$1"; "30"])]);
        BulletmlElm.Action
          ({actionLabel =
             Some "bulletmls/[ESP_RADE]_round_123_boss_satoru_5way.xml:_:1way";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$2+$1*$rand*2-$1")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"1")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = Aim;},"$2+$1*$rand*2-$1")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.1")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))])])
