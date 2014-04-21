namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// KetuiLt
[<RequireQualifiedAccess>]
module KetuiLt =

  /// ケツイロケテより、一面ボスのビット攻撃 by 白い弾幕くん
  /// [Ketui_LT]_1boss_bit.xml
  let b1boss_bit =
    createBulletmlInfo "ケツイロケテより、一面ボスのビット攻撃 by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "XWay";},
           [Action.ActionRef
              ({actionRefLabel = "XWayFan";},
               ["$1"; "$2"; "0"])]);
        BulletmlElm.Action
          ({actionLabel = Some "XWayFan";},
           [Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$2")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"$3")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "3way";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Wait "30";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = Aim;},"-3")),
                      Some (Speed (None,"1.4")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWay";},
                      ["3"; "2"])]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Accel
                         (Some
                            (Horizontal
                               (Some {horizontalType = Absolute;},"0")),
                          Some
                            (Vertical
                               (Some {verticalType = VerticalType.Absolute;},"1")),
                          Term "60");
                       Action.ActionRef ({actionRefLabel = "3way";},[]);
                       Accel
                         (Some
                            (Horizontal
                               (Some {horizontalType = Absolute;},"-2")),
                          Some
                            (Vertical
                               (Some {verticalType = VerticalType.Absolute;},"0")),
                          Term "60");
                       Action.ActionRef ({actionRefLabel = "3way";},[]);
                       Accel
                         (Some
                            (Horizontal
                               (Some {horizontalType = Absolute;},"0")),
                          Some
                            (Vertical
                               (Some {verticalType = VerticalType.Absolute;},"-1")),
                          Term "60");
                       Action.ActionRef ({actionRefLabel = "3way";},[]);
                       Accel
                         (Some
                            (Horizontal
                               (Some {horizontalType = Absolute;},"2")),
                          Some
                            (Vertical
                               (Some {verticalType = VerticalType.Absolute;},"0")),
                          Term "60");
                       Action.ActionRef ({actionRefLabel = "3way";},[])]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "4+$rank*6",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"90")),
                      Some (Speed (None,"2")),
                      BulletRef ({bulletRefLabel = "bit";},[]));
                   Wait "245/(4+$rank*6)"])); Wait "550"])])

  /// ケツイロケテより、三ボスのくねくね by 白い弾幕くん
  /// [Ketui_LT]_3boss_kunekune.xml
  let b3boss_kunekune =
    createBulletmlInfo "ケツイロケテより、三ボスのくねくね by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "aimSrc";},None,Some (Speed (None,"3")),
           [Action
              ({actionLabel = None;},
               [Wait "10";
                ChangeSpeed (Speed (None,"0"),Term "1");
                Repeat
                  (Times "5+$rank*10",
                   Action
                     ({actionLabel = None;},
                      [Wait "340/(5+$rank*10)";
                       Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Wait "2";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = Aim;},"0")),
                                 Some (Speed (None,"2")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "circleSrc";},None,Some (Speed (None,"4")),
           [Action
              ({actionLabel = None;},
               [Wait "10";
                ChangeSpeed
                  (Speed (None,"0.5+$rank"),Term "1");
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Sequence;},"5"),
                   Term "9999");
                Repeat
                  (Times "200",
                   Action
                     ({actionLabel = None;},
                      [Wait "2";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"180")),
                          Some (Speed (None,"3+$rand*0.02")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               None,BulletRef ({bulletRefLabel = "circleSrc";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               None,BulletRef ({bulletRefLabel = "circleSrc";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"135")),
               None,BulletRef ({bulletRefLabel = "aimSrc";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-135")),
               None,BulletRef ({bulletRefLabel = "aimSrc";},[]));
            Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Wait "12-$rank*8";
                   Repeat
                     (Times "4+$rank*4",
                      Action
                        ({actionLabel = None;},
                         [Wait "2";
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Absolute;},"180")),
                             Some (Speed (None,"4")),
                             Bullet
                               ({bulletLabel = None;},None,None,
                                [Action
                                   ({actionLabel = None;},
                                    [Wait "6";
                                     ChangeSpeed
                                       (Speed (None,"1"),
                                        Term "5");
                                     Wait "20";
                                     ChangeDirection
                                       (Direction
                                          (Some {directionType = Aim;},"0"),
                                        Term "1");
                                     ChangeSpeed
                                       (Speed (None,"2.2"),
                                        Term "1")])]))]))]))])])

  /// ケツイロケテより、三ボスの自機狙い弾と横殴り弾 by 白い弾幕くん
  /// [Ketui_LT]_3boss_roll_and_aim.xml
  let b3boss_roll_and_aim =
    createBulletmlInfo "ケツイロケテより、三ボスの自機狙い弾と横殴り弾 by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "XWay";},
           [Action.ActionRef
              ({actionRefLabel = "XWayFan";},
               ["$1"; "$2"; "0"])]);
        BulletmlElm.Action
          ({actionLabel = Some "XWayFan";},
           [Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$2")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"$3")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "curve";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction
                            (Some {directionType = DirectionType.Relative;},"-$1*(4+$rank*$rank*4)"),
                          Term "10"); Wait "10"]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "spiral";},
           [Wait "$rand * 30";
            Repeat
              (Times "10+$rank*15",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "2",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"$1*5")),
                             Some
                               (Speed (None,"1.5+$rank*$rank*1.5")),
                             BulletRef
                               ({bulletRefLabel = "curve";},
                                ["$1"]));
                          Repeat
                            (Times "4",
                             Action
                               ({actionLabel = None;},
                                [Fire
                                   ({fireLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Sequence;},"90")),
                                    Some
                                      (Speed
                                         (Some {speedType = SpeedType.Sequence;},"0")),
                                    BulletRef
                                      ({bulletRefLabel = "curve";},
                                       ["$1"]))]));
                          Wait "6 + $rand * 3"]));
                   Wait "6";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$1")),
                      Some (Speed (None,"1+$rank*2")),
                      BulletRef ({bulletRefLabel = "Dummy";},[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "twoWay";},
           [Repeat
              (Times "5+$rank*4",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "3+$rank*4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = Aim;},"3")),
                             Some (Speed (None,"1.8")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = Aim;},"-3")),
                             Some (Speed (None,"1.8")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Wait "5"])); 
                   Wait "20"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Action.ActionRef ({actionRefLabel = "spiral";},["-2"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Action.ActionRef ({actionRefLabel = "spiral";},["2"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top3";},
           [Action.ActionRef ({actionRefLabel = "twoWay";},[])])])


  /// ケツイロケテより、二面ボスのワインダー？ by 白い弾幕くん
  /// [Ketui_LT]_2boss_winder_crash.xml
  let b2boss_winder_crash =
    createBulletmlInfo "ケツイロケテより、二面ボスのワインダー？ by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "pre";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-20")),
               Some (Speed (None,"2")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"40")),
                      Some (Speed (None,"4")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"-40")),
                      Some (Speed (None,"4")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Wait "2"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "missile";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [Wait "5-$rank*2+$rand";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"0")),
                          Some (Speed (None,"0.0000001")),
                          Bullet
                            ({bulletLabel = None;},None,None,
                             [Action
                                ({actionLabel = None;},
                                 [Wait "60";
                                  ChangeSpeed
                                    (Speed (None,"3"),
                                     Term "30")])]))]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "missiles";},
           [Fire
              ({fireLabel = None;},
               Some
                 (Direction
                    (Some {directionType = DirectionType.Sequence;},"-($1-1)*1.5")),
               Some (Speed (None,"4")),
               BulletRef ({bulletRefLabel = "missile";},[]));
            Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"3")),
                      Some (Speed (None,"4")),
                      BulletRef ({bulletRefLabel = "missile";},[]))]));
            Fire
              ({fireLabel = None;},
               Some
                 (Direction
                    (Some {directionType = DirectionType.Sequence;},"40-($1-1)*3")),
               Some (Speed (None,"4")),
               BulletRef ({bulletRefLabel = "missile";},[]));
            Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"3")),
                      Some (Speed (None,"4")),
                      BulletRef ({bulletRefLabel = "missile";},[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "pre";},[]);
            Action.ActionRef ({actionRefLabel = "missiles";},["3+$rank*4"]);
            Wait "160"; 
            Action.ActionRef ({actionRefLabel = "pre";},[]);
            Action.ActionRef ({actionRefLabel = "missiles";},["4+$rank*6"]);
            Wait "160"])])
