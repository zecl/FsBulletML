namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Garegga
[<RequireQualifiedAccess>]
module Garegga =

  /// バトルガレッガのBlackHeartMk2のワインダー。by 白い弾幕くん
  /// [Garegga]_black_heart_mk2_winder.xml
  let black_heart_mk2_winder =
    createBulletmlInfo "バトルガレッガのBlackHeartMk2のワインダー。by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"135")),None,
               BulletRef ({bulletRefLabel = "winder";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"225")),None,
               BulletRef ({bulletRefLabel = "winder";},[])); Wait "220"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "winder";},None,Some (Speed (None,"2.3")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; ChangeSpeed (Speed (None,"0"),Term "1");
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"230")),None,
                   Bullet
                     ({bulletLabel = None;},None,None,
                      [Action ({actionLabel = None;},[Vanish])]));
                Action.ActionRef ({actionRefLabel = "move";},["0"; "40"]);
                Action.ActionRef ({actionRefLabel = "move";},["0.7+$rank"; "20"]);
                Action.ActionRef ({actionRefLabel = "move";},["-0.7-$rank"; "40"]);
                Action.ActionRef ({actionRefLabel = "move";},["0.7+$rank"; "20"]); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "move";},
           [Repeat
              (Times "$2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"$1-100")),
                      Some (Speed (None,"5")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"25")),
                             Some (Speed (None,"5")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "2"]))])])

