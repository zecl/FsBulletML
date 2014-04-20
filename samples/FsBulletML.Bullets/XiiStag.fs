namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Xevious
[<RequireQualifiedAccess>]
module XiiStag =
  
  /// トゥエルブスタッグ３ボス by 白い弾幕くん
  /// [XII_STAG]_3b.xml
  let b3b = 
    "トゥエルブスタッグ３ボス by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "bara";},["1"]);
            Action.ActionRef ({actionRefLabel = "bara";},["-1"]);
            Action.ActionRef ({actionRefLabel = "3way";},["180-55"; "-5"]);
            Action.ActionRef ({actionRefLabel = "3way";},["180"; "0"]);
            Action.ActionRef ({actionRefLabel = "3way";},["180+55"; "5"]);
            Action.ActionRef ({actionRefLabel = "roll";},["180+45"; "1"]);
            Action.ActionRef ({actionRefLabel = "roll";},["180-45"; "-1"]);
            Action.ActionRef ({actionRefLabel = "straight";},["1"]);
            Action.ActionRef ({actionRefLabel = "straight";},["-1"]); Wait "50";
            Repeat
              (Times "3*$rank",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,
                      Some (Speed (Some {speedType = SpeedType.Absolute;},"0")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [ActionRef ({actionRefLabel = "fin1";},["1"])]));
                   Fire
                     ({fireLabel = None;},None,
                      Some (Speed (Some {speedType = SpeedType.Absolute;},"0")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [ActionRef ({actionRefLabel = "fin1";},["-1"])]));
                   Fire
                     ({fireLabel = None;},None,None,
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [ActionRef ({actionRefLabel = "white1";},[])]));
                   Wait "5*(6+(12*$rank))"])); Wait "110"]);
        BulletmlElm.Action
          ({actionLabel = Some "fin1";},
           [Action.ActionRef ({actionRefLabel = "fin2";},["$1"]); Vanish]);
        BulletmlElm.Action
          ({actionLabel = Some "fin2";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$1*90")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"1.7+(0.8*$rank)")),
               Bullet ({bulletLabel = None;},None,None,[])); Wait "2/($rank+0.2)";
            Repeat
              (Times "1+$rank*32",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},
                            "-1*$1*((2.3/($rank+0.01))+0.35)")),
                      Some (Speed (Some {speedType = SpeedType.Absolute;},"1.7+(0.8*$rank)")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Wait "2/($rank+0.2)"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "white1";},
           [FireRef ({fireRefLabel = "white2";},["0"; "0.00001"; "0"]);
            FireRef ({fireRefLabel = "white2";},["90"; "1"; "1.5"]);
            FireRef ({fireRefLabel = "white2";},["-90"; "1"; "-1.5"]); Vanish]);
        BulletmlElm.Fire
          ({fireLabel = Some "white2";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
           Some (Speed (Some {speedType = SpeedType.Absolute;},"$2")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "4";
                   ChangeSpeed
                     (Speed (Some {speedType = SpeedType.Absolute;},"0.00001"),Term "1");
                   Wait "83-(70*$rank)";
                   Repeat
                     (Times "6+(12*$rank)",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Relative;},"-1*$1+$3")),
                             Some (Speed (None,"2.9")),
                             Bullet ({bulletLabel = None;},None,None,[])); Wait "5"]));
                   Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "bara";},
           [FireRef ({fireRefLabel = "5c";},["44*$1"; "4.1"; "4"]);
            FireRef ({fireRefLabel = "5c";},["55.5*$1"; "3.45"; "3"]);
            FireRef ({fireRefLabel = "5c";},["55*$1"; "4.2"; "2"]);
            FireRef ({fireRefLabel = "5c";},["70*$1"; "3"; "0"]);
            FireRef ({fireRefLabel = "5c";},["68*$1"; "3.74"; "1"])]);
        BulletmlElm.Fire
          ({fireLabel = Some "5c";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180+$1")),
           Some (Speed (None,"$2/1.1")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "10";
                   ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"0"),Term "1");
                   Wait "5+($3*5)";
                   Repeat
                     (Times "10-(5/($rank+0.001))",
                      Action
                        ({actionLabel = None;},
                         [Repeat
                            (Times "3",ActionRef ({actionRefLabel = "almond1";},[]));
                          Wait "85-(40*$rank)"])); Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "almond1";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"3.5-(7*$rand)")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"0+(0.3*$rand)")),
               BulletRef ({bulletRefLabel = "almond2";},[])); Wait "3"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "almond2";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeSpeed
                  (Speed (Some {speedType = SpeedType.Relative;},"1.8+(0.8*$rank)"),Term "10")])]);
        BulletmlElm.Action
          ({actionLabel = Some "3way";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180+$2")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"3.5")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Wait "10";
                       ChangeSpeed
                         (Speed (Some {speedType = SpeedType.Absolute;},"0"),Term "1");
                       Wait "1";
                       Repeat
                         (Times "7+(10*$rank)",
                          Action
                            ({actionLabel = None;},
                             [FireRef ({fireRefLabel = "9way";},["$1+16"]);
                              FireRef ({fireRefLabel = "9way";},["$1"]);
                              FireRef ({fireRefLabel = "9way";},["$1-16"]);
                              Wait "25"])); Vanish])]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "9way";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
           Some (Speed (Some {speedType = SpeedType.Absolute;},"1.5")),
           Bullet ({bulletLabel = None;},None,None,[]));
        BulletmlElm.Action
          ({actionLabel = Some "roll";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180+(11*$2)")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"10")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Wait "5";
                       ChangeSpeed
                         (Speed (Some {speedType = SpeedType.Absolute;},"0"),Term "1");
                       Wait "1";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                          Some (Speed (Some {speedType = SpeedType.Absolute;},"1.5")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"$1+(30*$2)")),
                          Some (Speed (Some {speedType = SpeedType.Absolute;},"1.5")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "15";
                       Repeat
                         (Times "11+(17*$rank)",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"-35*$2")),
                                 Some (Speed (Some {speedType = SpeedType.Absolute;},"1.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"30*$2")),
                                 Some (Speed (Some {speedType = SpeedType.Absolute;},"1.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Wait "15"])); Vanish])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "straight";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180+(82*$1)")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"2.7")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Wait "13";
                       ChangeSpeed
                         (Speed (Some {speedType = SpeedType.Absolute;},"0"),Term "1");
                       Wait "1";
                       Repeat
                         (Times "3+(5*$rank)",
                          ActionRef ({actionRefLabel = "fall";},[])); Vanish])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "fall";},
           [Repeat
              (Times "7",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                      Some (Speed (Some {speedType = SpeedType.Absolute;},"2.9")),
                      Bullet ({bulletLabel = None;},None,None,[])); Wait "5"]));
            Wait "15"])])
