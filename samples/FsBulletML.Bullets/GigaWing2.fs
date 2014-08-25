namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// GigaWing2
[<RequireQualifiedAccess>]
module GigaWing2 =

  /// ギガウィング2のアークリミかも。by 白い弾幕くん
  /// [GigaWing2]_akurimi.xml
  let akurimi =
    createBulletmlInfo <| 
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "ギガウィング2のアークリミかも。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "add2";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"9")),None,
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[Vanish])]));
            Repeat
              (Times "150",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"7-18")),
                      Some (Speed (None,"1.8")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "add2";},[]); Wait "4-$rank*2+$rand"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"9")),None,
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[Vanish])]));
            Repeat
              (Times "150",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"-7-18")),
                      Some (Speed (None,"1.8")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "add2";},[]); Wait "4-$rank*2+$rand"]))])])
