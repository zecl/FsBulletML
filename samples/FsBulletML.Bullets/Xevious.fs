namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Xevious
[<RequireQualifiedAccess>]
module Xevious =

  /// ゼビウス、らしい。 by 白い弾幕くん
  /// [XEVIOUS]_garu_zakato.xml
  let garu_zakato =
    "ゼビウス、らしい。 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                      Some (Speed (None,"3")),
                      BulletRef ({bulletRefLabel = "gzc";},[]));
                   Wait "20-$rank*10+$rand*10"])); Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "gzc";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10+$rand*10";
                Repeat
                  (Times "16",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"360/16")),
                          None,BulletRef ({bulletRefLabel = "spr";},[]))]));
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),
                          None,BulletRef ({bulletRefLabel = "hrmSpr";},[]))]));
                Vanish])]);
        BulletmlElm.Bullet ({bulletLabel = Some "spr";},None,Some (Speed (None,"2")),[]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "hrmSpr";},None,Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},[ChangeSpeed (Speed (None,"2"),Term "60")]);
            Action
              ({actionLabel = None;},
               [Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = Aim;},"0"),Term "40");
                       Wait "1"]))])])])
