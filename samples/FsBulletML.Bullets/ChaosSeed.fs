namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// ChaosSeed
[<RequireQualifiedAccess>]
module ChaosSeed =

  /// カオスシード、大猿ボス。by 白い弾幕くん
  /// [ChaosSeed]_big_monkey_boss.xml
  let big_monkey_boss =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;
        bulletmlName = Some "カオスシード、大猿ボス。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "roll";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Sequence;},"3"),Term "10000");
                ChangeSpeed (Speed (None,"2"),Term "60"); Wait "60";
                ChangeSpeed (Speed (None,"1.8"),Term "40"); Wait "40";
                ChangeSpeed (Speed (None,"2"),Term "30"); Wait "30";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Sequence;},"2"),Term "10000");
                ChangeSpeed
                  (Speed (Some {speedType = SpeedType.Sequence;},"0.01"),Term "100000")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "explosionBullet";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "30";
                Repeat
                  (Times "12",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),
                          Some (Speed (None,"1.2")),
                          BulletRef ({bulletRefLabel = "roll";},[]))])); Vanish])]);

        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "3+$rank*6",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"-90+180*$rand")),
                      Some (Speed (None,"$rand*3+1")),
                      BulletRef ({bulletRefLabel = "explosionBullet";},[]));
                   Wait "90-$rank*60"]))])])
