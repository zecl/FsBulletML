namespace FsBulletML.Bullets.PlayerBullet
open FsBulletML

/// その他
[<RequireQualifiedAccess>]
module PlayerBullet = 

  /// 2Way Left
  let b2wayLeftBullet = 
    Bulletml.Bulletml ({bulletmlXmlns = None; bulletmlType = Some ShootingDirection.BulletVertical;},
      [BulletmlElm.Action ({actionLabel = Some "top";},
        [Action.Fire ({fireLabel = None;}, Some (Direction (Some {directionType = DirectionType.Absolute;},"-10")), Some (Speed (None,"20")), BulletElm.Bullet ({bulletLabel = None;}, None, None, []));
         Action.Fire ({fireLabel = None;},  Some (Direction (Some {directionType = DirectionType.Absolute;},"0")), Some (Speed (None,"20")), BulletElm.Bullet ({bulletLabel = None;}, None, None, []))])])

  /// 2Way Right
  let b2wayRightBullet = 
    Bulletml.Bulletml ({bulletmlXmlns = None; bulletmlType = Some ShootingDirection.BulletVertical;},
      [BulletmlElm.Action ({actionLabel = Some "top";}, 
        [Action.Fire ({fireLabel = None;}, Some (Direction (Some {directionType = DirectionType.Absolute;},"10")), Some (Speed (None,"20")), BulletElm.Bullet ({bulletLabel = None;}, None, None, []));
         Action.Fire ({fireLabel = None;}, Some (Direction (Some {directionType = DirectionType.Absolute;},"0")), Some (Speed (None,"20")), BulletElm.Bullet ({bulletLabel = None;}, None, None, []))])])

  /// ホーミング弾
  let homing = 
    Bulletml.Bulletml ({bulletmlXmlns = None; bulletmlType = Some ShootingDirection.BulletHorizontal;},
      [BulletmlElm.Action ({actionLabel = Some "top";},
        [Action.Repeat (Times "2", ActionElm.Action ({actionLabel = None;}, 
                         [Action.Fire ({fireLabel = None;}, Some (Direction (None,"(-30+$rand*120)")), None, BulletElm.BulletRef ({bulletRefLabel = "hmgLsr";}, [])); 
                          Action.Repeat (Times "5", ActionElm.Action ({actionLabel = None;},
                                                       [Action.Wait "1";
                                                        Action.Fire ({fireLabel = None;}, Some (Direction (Some {directionType = DirectionType.Sequence;},"0")), None, BulletElm.BulletRef ({bulletRefLabel = "hmgLsr";}, []))]));
                                                        Action.Wait "10"]))]);
       BulletmlElm.Bullet ({bulletLabel = Some "hmgLsr";}, None, Some (Speed (None,"2")), 
                            [ActionElm.Action ({actionLabel = None;},
                              [Action.ChangeSpeed (Speed (None,"0.3"), Term "40"); Action.Wait "100";
                               Action.ChangeSpeed (Speed (None,"5"), Term "90")]); 
                             ActionElm.Action ({actionLabel = None;},
                              [Action.Repeat (Times "9999", ActionElm.Action ({actionLabel = None;},
                                                              [Action.ChangeDirection (Direction (Some {directionType = DirectionType.Aim;},"0"), Term "40-$rank*20");
                                                               Action.Wait "5"]))])])])

