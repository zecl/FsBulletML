namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Psyvariar
[<RequireQualifiedAccess>]
module Psyvariar =

  /// サイヴァリア4-Dボス、MZIQかも。by 白い弾幕くん
  /// [Psyvariar]_4-D_boss_MZIQ.xml
  let ``4_D_boss_MZIQ`` =
    "サイヴァリア4-Dボス、MZIQかも。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "add11";},
           [Repeat
              (Times "11",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "30",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"-11")),
                      Some (Speed (None,"1+$rank")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "add11";},[]);
                   Repeat
                     (Times "3",
                      Action
                        ({actionLabel = None;},
                         [Wait "4-$rank*2+$rand";
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"-5+30")),
                             Some (Speed (None,"1+$rank")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Action.ActionRef ({actionRefLabel = "add11";},[])]));
                   Wait "4-$rank*2+$rand"])); Wait "30-$rank*30"])])

  /// サイヴァリア、多分最終面ボス。by 白い弾幕くん
  /// [Psyvariar]_X-A_boss_opening.xml
  let ``X_A_boss_opening`` =
    "サイヴァリア、多分最終面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "600",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},Some (Direction (None,"-45+$rand*90")),
                      Some (Speed (None,"(0.3+$rand*0.5)*($rank+1)")),
                      Bullet ({bulletLabel = None;},None,None,[])); Wait "1"]));
            Wait "100"])])

  /// サイヴァリア、多分最終面ボス。by 白い弾幕くん
  /// [Psyvariar]_X-A_boss_winder.xml
  let ``X_A_boss_winder`` =
    "サイヴァリア、多分最終面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "winderBullet";},None,Some (Speed (None,"3")),[]);
        BulletmlElm.Fire
          ({fireLabel = Some "fireWinder";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"$1")),None,
           BulletRef ({bulletRefLabel = "winderBullet";},[]));
        BulletmlElm.Action
          ({actionLabel = Some "roundWinder";},
           [FireRef ({fireRefLabel = "fireWinder";},["$1"]);
            Repeat
              (Times "11",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "fireWinder";},["30"])])); Wait "5"]);
        BulletmlElm.Action
          ({actionLabel = Some "winderSequence";},
           [Repeat (Times "12",ActionRef ({actionRefLabel = "roundWinder";},["30"]));
            Repeat (Times "12",ActionRef ({actionRefLabel = "roundWinder";},["$1"]));
            Repeat (Times "12",ActionRef ({actionRefLabel = "roundWinder";},["30"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"2")),None,
               BulletRef ({bulletRefLabel = "winderBullet";},[]));
            Action.ActionRef ({actionRefLabel = "winderSequence";},["30.9+0.1*$rank"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-2")),None,
               BulletRef ({bulletRefLabel = "winderBullet";},[]));
            Action.ActionRef ({actionRefLabel = "winderSequence";},["29.1-0.1*$rank"])])])
