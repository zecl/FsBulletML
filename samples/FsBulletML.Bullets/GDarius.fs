namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// GDarius
[<RequireQualifiedAccess>]
module GDarius =

  /// Gダライアス中のホーミングレーザー by 白い弾幕くん
  /// [G_DARIUS]_homing_laser.xml
  let ``homing_laser`` =
    "Gダライアス中のホーミングレーザー by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},Some (Direction (None,"-60+$rand*120")),
                      None,BulletRef ({bulletRefLabel = "hmgLsr";},[]));
                   Repeat
                     (Times "8",
                      Action
                        ({actionLabel = None;},
                         [Wait "1";
                          Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                             None,BulletRef ({bulletRefLabel = "hmgLsr";},[]))]));
                   Wait "10"])); Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "hmgLsr";},None,Some (Speed (None,"2")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0.3"),Term "30"); Wait "100";
                ChangeSpeed (Speed (None,"5"),Term "100")]);
            Action
              ({actionLabel = None;},
               [Repeat
                  (Times "12",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = Aim;},"0"),
                          Term "45-$rank*30"); Wait "5"]))])])])
