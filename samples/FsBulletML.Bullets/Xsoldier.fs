namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Xsoldier
[<RequireQualifiedAccess>]
module Xsoldier =

  /// XSoldierの8面ボスの主砲 by 白い弾幕くん
  /// [xsoldier]_8_boss_main.xml
  let b8_boss_main =
    createBulletmlInfo "XSoldierの8面ボスの主砲 by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0")),
               BulletRef ({bulletRefLabel = "dummy";},["90"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0")),
               BulletRef ({bulletRefLabel = "dummy";},["270"]));
            Wait "100 - $rank * 90";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0")),
               BulletRef ({bulletRefLabel = "allway";},["0"; "1.5"])); Wait "5";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0")),
               BulletRef ({bulletRefLabel = "allway";},["2.5"; "1.8"])); Wait "20"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"0")),BulletRef ({bulletRefLabel = "bit";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"0.5")),
                   BulletRef ({bulletRefLabel = "bit";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"1.0")),
                   BulletRef ({bulletRefLabel = "bit";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"1.5")),
                   BulletRef ({bulletRefLabel = "bit";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "allway";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "71",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"5")),
                          Some (Speed (None,"$2")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "20"; ChangeSpeed (Speed (None,"0"),Term "1");
                Wait "105 - $rank * 90";
                Repeat
                  (Times "20",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                          Some (Speed (None,"3")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                          Some (Speed (None,"3.5")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                          Some (Speed (None,"4")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1"]));
                Vanish])])])



