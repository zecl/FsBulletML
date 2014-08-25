namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Strikers1999
[<RequireQualifiedAccess>]
module Strikers1999 =

  /// ストライカーズ1999の花火かも。by 白い弾幕くん
  /// [Strikers1999]_hanabi.xml
  let hanabi =
    createBulletmlInfo <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;
        bulletmlName = Some "ストライカーズ1999の花火かも。by 白い弾幕くん";
        bulletmlDescription = None},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                      Some (Speed (None,"3")),
                      BulletRef ({bulletRefLabel = "fastHanabi";},[]));
                   Wait "110-$rank*60"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "fastFour";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"5")),
               Some (Speed (None,"2+$rank")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-10")),
               Some (Speed (None,"2+$rank")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
               Some (Speed (None,"1.5+$rank")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-20")),
               Some (Speed (None,"1.5+$rank")),
               Bullet ({bulletLabel = None;},None,None,[]))]);
        BulletmlElm.Action
          ({actionLabel = Some "slowFour";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"5")),
               Some (Speed (None,"1+$rank*0.8")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-10")),
               Some (Speed (None,"1+$rank*0.8")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
               Some (Speed (None,"0.7+$rank*0.8")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-20")),
               Some (Speed (None,"0.7+$rank*0.8")),
               Bullet ({bulletLabel = None;},None,None,[]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "fastHanabi";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "15";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),
                   Some (Speed (None,"2.5+$rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Action.ActionRef ({actionRefLabel = "fastFour";},[]);
                Repeat
                  (Times "16",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"32.5")),
                          Some (Speed (None,"2.5+$rank")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef ({actionRefLabel = "fastFour";},[])]));
                FireRef ({fireRefLabel = "slowHanabi";},[]); Vanish])]);
        BulletmlElm.Fire
          ({fireLabel = Some "slowHanabi";},None,None,
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
                      Some (Speed (None,"1.3+$rank*0.8")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "slowFour";},[]);
                   Repeat
                     (Times "16",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"32.5")),
                             Some (Speed (None,"1.3+$rank*0.8")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Action.ActionRef ({actionRefLabel = "slowFour";},[])])); Vanish])]))])
