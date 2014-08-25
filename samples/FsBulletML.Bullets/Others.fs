namespace FsBulletML.Bullets.EnemyBullet
open FsBulletML

/// その他
[<RequireQualifiedAccess>]
module Others = 

  /// 全方位弾
  let AllWay = 
    createBulletmlInfo <|
    Bulletml({ bulletmlXmlns = None; bulletmlType = Some ShootingDirection.BulletVertical; bulletmlName = Some "全方位弾"; bulletmlDescription = None},
        [BulletmlElm.Action ({actionLabel = Some "circle";},
            [Action.Repeat
                (Times "$1",
                Action ({actionLabel = None;},
                    [Fire ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"360/$1")),
                      None,
                      Bullet ({bulletLabel = None;}, None, None,[]))]))]);
          BulletmlElm.Action ({actionLabel = Some "top";},
            [Action.Repeat
                (Times "30",
                ActionElm.Action ({actionLabel = None;},
                    [Action.ActionRef ({actionRefLabel = "circle";}, ["20"]); Wait "20"]))])])

  /// 前方5way弾
  let b5way = 
    createBulletmlInfo <|
    Bulletml ({bulletmlXmlns = None; bulletmlType = Some BulletVertical; bulletmlName = Some "前方5way弾"; bulletmlDescription = None},
        [BulletmlElm.Action ({actionLabel = Some "top";},
            [Fire ({fireLabel = None;},
                Some (Direction (Some {directionType = DirectionType.Relative;},"-20+180")),
                None,
                Bullet ({bulletLabel = None;},None,None,[]));
             Repeat
                (Times "4",
                 Action ({actionLabel = None;},
                    [Fire ({fireLabel = None;},
                        Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                        None,
                        Bullet ({bulletLabel = None;},None,None,[]))]))])])

  /// 初期方向Aim弾１発
  let homingOne = 
    createBulletmlInfo <|
    Bulletml({bulletmlXmlns = None; bulletmlType = None; bulletmlName = Some "初期方向Aim弾１発"; bulletmlDescription = None},
        [BulletmlElm.Action ({actionLabel = Some "top";},
           [Fire ({fireLabel = None;},None,None,
                Bullet ({bulletLabel = None;},None,None,[]))])])
