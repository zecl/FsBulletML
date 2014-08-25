namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Guwange
[<RequireQualifiedAccess>]
module Guwange =

  /// ぐわんげ、二面ボス by 白い弾幕くん
  /// [Guwange]_round_2_boss_circle_fire.xml
  let round_2_boss_circle_fire = 
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "ぐわんげ、二面ボス by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Fire
          ({fireLabel = Some "circle";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"$1")),
           Some (Speed (None,"6")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "3";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"$2")),
                      Some (Speed (None,"1.5+$rank")),
                      Bullet ({bulletLabel = None;},None,None,[])); Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "fireCircle";},
           [Repeat
              (Times "18",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "circle";},["20"; "$1"])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "fireCircle";},["180-45+90*$rand"]);
            Wait "10"])])

  /// ぐわんげ、三面ボス by 白い弾幕くん
  /// [Guwange]_round_3_boss_fast_3way.xml
  let round_3_boss_fast_3way =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "ぐわんげ、三面ボス by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10+$rank*50",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},Some (Direction (None,"$rand*360")),
                      Some (Speed (None,"5")),
                      BulletRef ({bulletRefLabel = "seed";},["5+$rand*10"]));
                   Wait "20-$rank*10"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "$1"); Wait "$1";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"-20")),None,
                   BulletRef ({bulletRefLabel = "3way";},[]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"20")),
                          None,BulletRef ({bulletRefLabel = "3way";},[]))]));
                Wait "6";
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
                          BulletRef ({bulletRefLabel = "3way";},[]));
                       Repeat
                         (Times "2",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"-20")),
                                 Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                                 BulletRef ({bulletRefLabel = "3way";},[]))]));
                       Wait "6";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
                          BulletRef ({bulletRefLabel = "3way";},[]));
                       Repeat
                         (Times "2",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"20")),
                                 Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                                 BulletRef ({bulletRefLabel = "3way";},[]))]));
                       Wait "6"])); Vanish])]);
        BulletmlElm.Bullet ({bulletLabel = Some "3way";},None,Some (Speed (None,"3")),[])])

  /// ぐわんげ、四面ボス by 白い弾幕くん
  /// [Guwange]_round_4_boss_eye_ball.xml
  let round_4_boss_eye_ball =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "ぐわんげ、四面ボス by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10+$rank*10",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},Some (Direction (None,"$rand*360")),None,
                      BulletRef ({bulletRefLabel = "eye";},[])); Wait "30"]));
            Wait "120"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "eye";},None,Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"10"),Term "400");
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Sequence;},"$rand*5-2"),
                   Term "9999");
                Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                          None,BulletRef ({bulletRefLabel = "shadow";},[]));
                       Wait "4"]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "shadow";},None,Some (Speed (None,"0.1")),
           [Action
              ({actionLabel = None;},
               [Wait "20";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"90")),
                   Some (Speed (None,"0.6")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"-90")),
                   Some (Speed (None,"0.6")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])
