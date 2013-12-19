namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Noiz2sa
[<RequireQualifiedAccess>]
module Noiz2sa =

  /// Noiz2saより、88way。 by 白い弾幕くん
  /// [Noiz2sa]_88way.xml
  let ``88way`` =
    "Noiz2saより、88way。 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
               Some (Speed (None,"0.7")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [ActionRef ({actionRefLabel = "main";},[])])); Wait "200"]);
        BulletmlElm.Action
          ({actionLabel = Some "main";},
           [Repeat
              (Times "6+$rank*10",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"360/(6+$rank*10)")),
                      None,BulletRef ({bulletRefLabel = "16way";},[]));
                   Wait "100/(6+$rank*10)"])); Vanish]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "16way";},None,Some (Speed (None,"$rand+1")),
           [Action
              ({actionLabel = None;},
               [Wait "20+$rand*40";
                Repeat
                  (Times "16",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
                          None,
                          Bullet
                            ({bulletLabel = None;},None,Some (Speed (None,"1.7")),[]))]));
                Vanish])])])

  /// Noiz2saより、ビットから自機狙い弾。 by 白い弾幕くん
  /// [Noiz2sa]_bit.xml
  let ``bit`` =
    "Noiz2saより、ビットから自機狙い弾。 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "4+$rank*10",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,None,
                      BulletRef ({bulletRefLabel = "bit";},[]))])); Wait "180"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = DirectionType.Absolute;},"$rand*360"),
                          Term "20"); ChangeSpeed (Speed (None,"2"),Term "20");
                       Wait "20"; ChangeSpeed (Speed (None,"0"),Term "20");
                       Wait "20";
                       Fire
                         ({fireLabel = None;},None,None,
                          BulletRef ({bulletRefLabel = "seed";},[])); Wait "0"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed";},None,Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"$rand*10-5")),None,
                   BulletRef ({bulletRefLabel = "nrm";},[]));
                Repeat
                  (Times "5",
                   Action
                     ({actionLabel = None;},
                      [Wait "6";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          None,BulletRef ({bulletRefLabel = "nrm";},[]))])); Vanish])]);
        BulletmlElm.Bullet ({bulletLabel = Some "nrm";},None,Some (Speed (None,"2")),[])])

  /// Noiz2saより、回る棒。by 白い弾幕くん
  /// [Noiz2sa]_rollbar.xml
  let ``rollbar`` = 
    "Noiz2saより、回る棒。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$rand*50")),
               Some (Speed (None,"0")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[Vanish])]));
            Action.ActionRef ({actionRefLabel = "main";},[])]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"180+$rank*50")),
               Some (Speed (None,"0")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[Vanish])]));
            Action.ActionRef ({actionRefLabel = "main";},[])]);
        BulletmlElm.Action
          ({actionLabel = Some "main";},
           [Repeat
              (Times "15+$rank*10",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"180")),None,
                      BulletRef ({bulletRefLabel = "firebar";},["90"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"160")),None,
                      BulletRef ({bulletRefLabel = "firebar";},["-90"]));
                   Wait "200/(15+$rank*10)"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "firebar";},None,Some (Speed (None,"10")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "5",
                   Action
                     ({actionLabel = None;},
                      [Wait "1";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
                          None,
                          Bullet
                            ({bulletLabel = None;},None,Some (Speed (None,"1.5")),[]))]));
                Vanish])])])
