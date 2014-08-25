namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// DragonBlaze
[<RequireQualifiedAccess>]
module DragonBlaze =

  /// ドラゴンブレイズのネビュロス第二形態かも。by 白い弾幕くん
  /// [DragonBlaze]_nebyurosu_2.xml
  let nebyurosu_2 =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "ドラゴンブレイズのネビュロス第二形態かも。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "add3";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Repeat
              (Times "150",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"4")),
                      Some (Speed (None,"1+$rank")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "add3";},[]); Wait "2"]));
            Wait "60-$rank*30"]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Repeat
              (Times "150",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"-5")),
                      Some (Speed (None,"1+$rank")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "add3";},[]); Wait "2"]));
            Wait "60-$rank*30"])])
