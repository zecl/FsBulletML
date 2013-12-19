namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// SilverGun
[<RequireQualifiedAccess>]
module SilverGun =

  /// レイディアントシルバーガン4Dボス、PENTA。by 白い弾幕くん
  /// [SilverGun]_4D_boss_PENTA.xml
  let ``4D_boss_PENTA`` =
    "レイディアントシルバーガン4Dボス、PENTA。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"100")),
               Some (Speed (None,"4")),BulletRef ({bulletRefLabel = "arm";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-100")),
               Some (Speed (None,"4")),BulletRef ({bulletRefLabel = "arm";},[]));
            Repeat
              (Times "400",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"7")),
                      Some (Speed (None,"1.5")),
                      Bullet ({bulletLabel = None;},None,None,[])); Wait "1"]));
            Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "arm";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "12"; ChangeSpeed (Speed (None,"0"),Term "1");
                Repeat
                  (Times "7",
                   Action
                     ({actionLabel = None;},
                      [Wait "60";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = Aim;},"-15")),
                          Some (Speed (None,"1.8")),
                          BulletRef ({bulletRefLabel = "homing";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),
                          Some (Speed (None,"1.8")),
                          BulletRef ({bulletRefLabel = "homing";},[])); Wait "2"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "homing";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "60";
                ChangeDirection
                  (Direction (Some {directionType = Aim;},"0"),Term "15-$rank*10");
                Wait "15-$rank*10";
                ChangeDirection
                  (Direction (Some {directionType = Aim;},"0"),Term "15-$rank*10")])])])