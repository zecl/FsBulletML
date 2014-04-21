namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// GWange
[<RequireQualifiedAccess>]
module GWange =

  /// G-わんげスレの957氏、回転ガラ by 白い弾幕くん
  /// [G-Wange]_roll_gara.xml
  let _roll_gara =
    createBulletmlInfo "G-わんげスレの957氏、回転ガラ by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Repeat
              (Times "600/(3-$rank*2)",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "line";},[]);
                   Wait "3-$rank*2+$rand"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "line";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-7")),
               Some (Speed (None,"0.6")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "5+$rank*5",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.3")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [ChangeDirection
                     (Direction
                        (Some {directionType = DirectionType.Sequence;},"-1+$rand*2"),
                      Term "30");
                   ChangeSpeed
                     (Speed
                        (Some {speedType = SpeedType.Absolute;},"(-1+$rand*2)*($rank*2+1)"),
                      Term "30"); Wait "30"]))])])

  /// G-わんげスレの966氏考案、往復ビット by 白い弾幕くん
  /// [G-Wange]_round_trip_bit.xml
  let round_trip_bit =
    createBulletmlInfo "G-わんげスレの966氏考案、往復ビット by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "src";},["5"; "91"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "src";},["4"; "-91"]));
            Wait "600"]);
        BulletmlElm.Action
          ({actionLabel = Some "Xway";},
           [Fire
              ({fireLabel = None;},
               Some
                 (Direction
                    (Some {directionType = Aim;},"-(5+$rank*5)*($1-1)-4+$rand*8")),
               Some (Speed (None,"1.6")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"5+$rank*5")),
                      Some (Speed (None,"1.6")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "fire";},
           [Action.ActionRef ({actionRefLabel = "Xway";},["3"]); Wait "15";
            Action.ActionRef ({actionRefLabel = "Xway";},["5"]); Wait "15"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "src";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"$2")),
           Some (Speed (None,"$1")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "5",
                   Action
                     ({actionLabel = None;},
                      [ChangeSpeed
                         (Speed (None,"0.01"),
                          Term "30");
                       Action.ActionRef ({actionRefLabel = "fire";},[]);
                       ChangeDirection
                         (Direction
                            (Some {directionType = DirectionType.Absolute;},"-$2"),
                          Term "1");
                       ChangeSpeed
                         (Speed (None,"$1"),Term "30");
                       Action.ActionRef ({actionRefLabel = "fire";},[]);
                       ChangeSpeed
                         (Speed (None,"0.01"),
                          Term "30");
                       Action.ActionRef ({actionRefLabel = "fire";},[]);
                       ChangeDirection
                         (Direction
                            (Some {directionType = DirectionType.Absolute;},"$2"),
                          Term "1");
                       ChangeSpeed
                         (Speed (None,"$1"),Term "30");
                       Action.ActionRef ({actionRefLabel = "fire";},[])]))])])])
