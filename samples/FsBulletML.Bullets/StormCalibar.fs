namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// StormCalibar
[<RequireQualifiedAccess>]
module StormCalibar =

  /// ストームキャリバーのラスボス、回転二つ。by 白い弾幕くん
  /// [STORM_CALIBAR]_last_boss_double_roll_bullets.xml
  let last_boss_double_roll_bullets =
    createBulletmlInfo "ストームキャリバーのラスボス、回転二つ。by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "rollShots";},
           [Repeat
              (Times "200",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"11*$1")),
                      Some (Speed (None,"1")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "3+$rank*4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                             Some (Speed (Some {speedType = SpeedType.Sequence;},"0.3")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "2"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "right";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"90"),Term "1");
            ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"1.5"),Term "1");
            Wait "50"]);
        BulletmlElm.Action
          ({actionLabel = Some "left";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"-90"),Term "1");
            ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"1.5"),Term "1");
            Wait "50"]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "right";},[]);
                   Action.ActionRef ({actionRefLabel = "left";},[]);
                   Action.ActionRef ({actionRefLabel = "left";},[]);
                   Action.ActionRef ({actionRefLabel = "right";},[])]));
            ChangeSpeed (Speed (None,"0"),Term "1"); Wait "1"]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Action.ActionRef ({actionRefLabel = "rollShots";},["-1"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top3";},
           [Action.ActionRef ({actionRefLabel = "rollShots";},["1"])])])
