namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Tenmado
[<RequireQualifiedAccess>]
module Tenmado =

  /// tenmadoより、三面ボス「Disconnection」by 白い弾幕くん
  /// [tenmado]_3_boss_2.xml
  let ``3_boss_2`` =
    "tenmadoより、三面ボス「Disconnection」by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"240")),
               Some (Speed (None,"0.6")),
               BulletRef ({bulletRefLabel = "bitlaser";},["60"; "10"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-240")),
               Some (Speed (None,"0.6")),
               BulletRef ({bulletRefLabel = "bitlaser";},["-60"; "-10"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"240")),
               Some (Speed (None,"0.6")),
               BulletRef ({bulletRefLabel = "bitaim";},["60"; "10"; "35"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-240")),
               Some (Speed (None,"0.6")),
               BulletRef ({bulletRefLabel = "bitaim";},["-60"; "-10"; "5"]));
            Wait "60";
            Repeat
              (Times "600 / (6.0 - 4.0 * $rank)",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = Aim;},"-30 + 60 * $rand")),
                      Some (Speed (None,"1.3+$rank*0.7")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Wait "6.0 - 4.0 * $rank"])); Wait "90"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bitlaser";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "120"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "30";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
                ChangeSpeed (Speed (None,"0.6"),Term "1"); Wait "90";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"0.1")),
                   BulletRef ({bulletRefLabel = "laser";},["0.3"]));
                Repeat
                  (Times "6",
                   Action
                     ({actionLabel = None;},
                      [Wait "20";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$2")),
                          Some (Speed (None,"0.1")),
                          BulletRef ({bulletRefLabel = "laser";},["0.3"]))]));
                ChangeSpeed (Speed (None,"0"),Term "1"); Wait "30";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1");
                ChangeSpeed (Speed (None,"0.8"),Term "1"); Wait "10";
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction (Some {directionType = DirectionType.Absolute;},"$1 + 3.5 * $2")),
                   Some (Speed (None,"0.1")),
                   BulletRef ({bulletRefLabel = "laser";},["1.5"]));
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Wait "20";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"-$2")),
                          Some (Speed (None,"0.1")),
                          BulletRef ({bulletRefLabel = "laser";},["1.5"]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bitaim";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "120"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "30";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
                ChangeSpeed (Speed (None,"0.6"),Term "1"); Wait "40 - $3";
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Wait "70";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-10 + 20 * $rand")),
                          Some (Speed (None,"0.6")),
                          Bullet ({bulletLabel = None;},None,None,[]))]));
                Wait "30 + $3"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "30";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1");
                ChangeSpeed (Speed (None,"0.8"),Term "1"); Wait "40 - $3";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"-10 + 20 * $rand")),
                   Some (Speed (None,"0.6")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "50 + $3";
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "laser";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 + 0.01")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 + 0.02")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 + 0.03")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 + 0.04")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 + 0.05")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])

  /// tenmadoより、最終ボス「L」第一形態 by 白い弾幕くん
  /// [tenmado]_5_boss_1.xml
  let ``5_boss_1`` =
    "tenmadoより、最終ボス「L」第一形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0")),BulletRef ({bulletRefLabel = "random";},[]));
            Repeat
              (Times "8",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
                      Some (Speed (None,"0.5")),
                      BulletRef ({bulletRefLabel = "surprise";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),
                      Some (Speed (None,"0.5")),
                      BulletRef ({bulletRefLabel = "surprise";},[])); Wait "100"]));
            Wait "20"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "surprise";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "100"; ChangeSpeed (Speed (None,"0"),Term "1");
                Repeat
                  (Times "5",
                   Action
                     ({actionLabel = None;},
                      [Repeat
                         (Times "30",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = Aim;},"3.5")),
                                 Some (Speed (None,"15+$rand*15")),
                                 Bullet
                                   ({bulletLabel = None;},None,None,
                                    [Action ({actionLabel = None;},[])]));
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = Aim;},"-3.5")),
                                 Some (Speed (None,"15+$rand*15")),
                                 Bullet
                                   ({bulletLabel = None;},None,None,
                                    [Action ({actionLabel = None;},[])]))]));
                       Wait "1"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "random";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "200";
                Repeat
                  (Times "6000/(130 - 100 * $rank) ",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-22 + 44 * $rand")),
                          Some (Speed (None,"1.6 + 1.0 * $rand")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "0.1 * (130 - 100 * $rank)"])); Vanish])])])

  /// tenmadoより、最終ボス「L」第三形態 by 白い弾幕くん
  /// [tenmado]_5_boss_3.xml
  let ``5_boss_3`` =
    "tenmadoより、最終ボス「L」第三形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0")),BulletRef ({bulletRefLabel = "stardust";},[]));
            Wait "120";
            Repeat
              (Times "840/(120 - 100 * $rank)",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"0")),
                      Some (Speed (None,"1")),
                      BulletRef ({bulletRefLabel = "laser";},["2"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"0")),
                      Some (Speed (None,"1")),
                      BulletRef ({bulletRefLabel = "laser";},["2.05"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"0")),
                      Some (Speed (None,"1")),
                      BulletRef ({bulletRefLabel = "laser";},["2.1"]));
                   Wait "0.5 * (120 - 100 * $rank)"])); Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "stardust";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "5+$rank*10",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"135 + 90 * $rand")),
                          Some (Speed (None,"0.3 + 1.7 * $rand")),
                          BulletRef
                            ({bulletRefLabel = "stardust2";},["60"; "1.2"; "0.8"]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"135 + 90 * $rand")),
                          Some (Speed (None,"0.3 + 1.7 * $rand")),
                          BulletRef
                            ({bulletRefLabel = "stardust2";},["68"; "0.8"; "1.2"]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"135 + 90 * $rand")),
                          Some (Speed (None,"0.3 + 1.7 * $rand")),
                          BulletRef
                            ({bulletRefLabel = "stardust2";},["76"; "1.2"; "0.8"]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"135 + 90 * $rand")),
                          Some (Speed (None,"0.3 + 1.7 * $rand")),
                          BulletRef
                            ({bulletRefLabel = "stardust2";},["84"; "0.8"; "1.2"]));
                       Wait "960/(10+$rank*20)"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "stardust2";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "$1";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"30")),
                   Some (Speed (None,"$3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"60")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
                   Some (Speed (None,"$3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"120")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"150")),
                   Some (Speed (None,"$3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"210")),
                   Some (Speed (None,"$3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"240")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),
                   Some (Speed (None,"$3")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"300")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"330")),
                   Some (Speed (None,"$3")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "laser";},None,None,
           [Action
              ({actionLabel = None;},[ChangeSpeed (Speed (None,"$1"),Term "1")])])])
