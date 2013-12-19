namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Original
[<RequireQualifiedAccess>]
module Original =

  /// 大原さんのオリジナル、断罪 by 白い弾幕くん
  /// [Original]_accusation.xml
  let ``accusation`` = 
    "大原さんのオリジナル、断罪 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
               None,BulletRef ({bulletRefLabel = "centerbit";},[])); Wait "800"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "centerbit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
           Some (Speed (None,"0.9")),
           [Action
              ({actionLabel = None;},
               [Wait "40"; ChangeSpeed (Speed (None,"0.0"),Term "1"); Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                   None,BulletRef ({bulletRefLabel = "pillarbit";},[]));
                Repeat
                  (Times "17",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"5")),
                          None,BulletRef ({bulletRefLabel = "dummybit";},[]))]));
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"5")),
                          None,BulletRef ({bulletRefLabel = "pillarbit";},[]));
                       Repeat
                         (Times "17",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"5")),
                                 None,BulletRef ({bulletRefLabel = "dummybit";},[]))]))]));
                Wait "120";
                Repeat
                  (Times "140",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                          Some (Speed (None,"0.2")),
                          BulletRef ({bulletRefLabel = "weak";},["240"])); Wait "2"]));
                Repeat
                  (Times "70",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                          Some (Speed (None,"2.0")),
                          BulletRef ({bulletRefLabel = "weak";},["24"]));
                       Repeat
                         (Times "4",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.2")),
                                 BulletRef ({bulletRefLabel = "weak";},["24"]))]));
                       Wait "2"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "pillarbit";},None,Some (Speed (None,"0.6")),
           [Action
              ({actionLabel = None;},
               [Wait "120"; ChangeSpeed (Speed (None,"0.001"),Term "1"); Wait "120";
                Repeat
                  (Times "300 / (35 - 33 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Repeat
                         (Times "10",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Absolute;},
                                       "360 * $rand")),Some (Speed (None,"2.0")),
                                 BulletRef ({bulletRefLabel = "weak";},["15"]))]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Relative;},"-45 + 90 * $rand")),
                          Some
                            (Speed
                               (None,"(2.5 + 1.0 * $rand) * (0.25 + 0.75 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "35 - 33 * $rank"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummybit";},None,Some (Speed (None,"0.6")),
           [Action
              ({actionLabel = None;},
               [Wait "120"; ChangeSpeed (Speed (None,"0.001"),Term "1"); Wait "120";
                Repeat
                  (Times "300 / (35 - 33 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Relative;},"-45 + 90 * $rand")),
                          Some
                            (Speed
                               (None,"(2.5 + 1.0 * $rand) * (0.25 + 0.75 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "35 - 33 * $rank"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "weak";},None,None,
           [Action ({actionLabel = None;},[Wait "$1"; Vanish])])])

  /// 大原さんのオリジナル、風の精 by 白い弾幕くん
  /// [Original]_air_elemental.xml
  let ``air_elemental`` =
    "大原さんのオリジナル、風の精 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "parentbit";},[])); Wait "650"]);
        BulletmlElm.Action
          ({actionLabel = Some "slash";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
               Some (Speed (None,"0.22")),
               BulletRef ({bulletRefLabel = "spiralbit";},["150"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
               Some (Speed (None,"0.22")),
               BulletRef ({bulletRefLabel = "spiralbit";},["150"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
               Some (Speed (None,"0.22")),
               BulletRef ({bulletRefLabel = "spiralbit";},["150"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"180")),
               Some (Speed (None,"0.2")),
               BulletRef ({bulletRefLabel = "spiralbit";},["120"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
               Some (Speed (None,"0.2")),
               BulletRef ({bulletRefLabel = "spiralbit";},["120"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
               Some (Speed (None,"0.2")),
               BulletRef ({bulletRefLabel = "spiralbit";},["120"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"150")),
               Some (Speed (None,"0.17")),
               BulletRef ({bulletRefLabel = "spiralbit";},["-150"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
               Some (Speed (None,"0.17")),
               BulletRef ({bulletRefLabel = "spiralbit";},["-150"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
               Some (Speed (None,"0.17")),
               BulletRef ({bulletRefLabel = "spiralbit";},["-150"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"180")),
               Some (Speed (None,"0.25")),
               BulletRef ({bulletRefLabel = "spiralbit";},["-120"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
               Some (Speed (None,"0.25")),
               BulletRef ({bulletRefLabel = "spiralbit";},["-120"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
               Some (Speed (None,"0.25")),
               BulletRef ({bulletRefLabel = "spiralbit";},["-120"]));
            Repeat
              (Times "18",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                      Some (Speed (Some {speedType = SpeedType.Absolute;},"1.0")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                      Some (Speed (Some {speedType = SpeedType.Absolute;},"1.0")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.3")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "parentbit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"170 + 20 * $rand")),
           Some (Speed (None,"1.8")),
           [Action
              ({actionLabel = None;},
               [Wait "40"; ChangeSpeed (Speed (None,"0.0001"),Term "1"); Wait "5";
                Action.ActionRef ({actionRefLabel = "arrow";},[]);
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Wait "5"; ChangeSpeed (Speed (None,"1.8"),Term "1");
                       ChangeDirection
                         (Direction
                            (Some {directionType = DirectionType.Relative;},"170 + 20 * $rand"),
                          Term "1"); Wait "40";
                       ChangeSpeed (Speed (None,"0.0001"),Term "1"); Wait "5";
                       Action.ActionRef ({actionRefLabel = "arrow";},[])])); Wait "80";
                ChangeSpeed (Speed (None,"1.8"),Term "1");
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"170 + 20 * $rand"),
                   Term "1"); Wait "40";
                ChangeSpeed (Speed (None,"0.0001"),Term "1"); Wait "5";
                Action.ActionRef ({actionRefLabel = "slash";},[]); Wait "150";
                ChangeSpeed (Speed (None,"1.8"),Term "1");
                ChangeDirection
                  (Direction (Some {directionType = Aim;},"-30 + 60 * $rand"),
                   Term "1"); Wait "15";
                ChangeSpeed (Speed (None,"0.0001"),Term "1"); Wait "5";
                Action.ActionRef ({actionRefLabel = "slash";},[]); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "arrow";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"0")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"1.3")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
               Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-3")),
               Some (Speed (Some {speedType = SpeedType.Sequence;},"0.0")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"6")),
               Some (Speed (Some {speedType = SpeedType.Sequence;},"0.0")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-3")),
               Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
               Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
               Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.1")),
               Bullet ({bulletLabel = None;},None,None,[]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "spiralbit";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"$1"),Term "90");
                Repeat
                  (Times "2 + 6 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                          None,BulletRef ({bulletRefLabel = "spiral";},["-$1"]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"90")),
                          Some (Speed (Some {speedType = SpeedType.Relative;},"0.6")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "10 - 7 * $rank";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                          None,BulletRef ({bulletRefLabel = "spiral";},["$1"]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-90")),
                          Some (Speed (Some {speedType = SpeedType.Relative;},"0.6")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "10 - 7 * $rank"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "spiral";},None,
           Some (Speed (Some {speedType = SpeedType.Relative;},"0.8")),
           [Action
              ({actionLabel = None;},
               [Wait "20";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"$1"),Term "90")])])])
  
  /// オリジナル。後ろに弾を撃つ人々。 by 白い弾幕くん
  /// [Original]_backfire.xml
  let ``backfire`` =
    "オリジナル。後ろに弾を撃つ人々。 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "5+$rank*10",
               Action
                 ({actionLabel = None;},[FireRef ({fireRefLabel = "backFire";},[])]));
            Wait "300"]);
        BulletmlElm.Fire
          ({fireLabel = Some "backFire";},
           Some (Direction (None,"50-$rand*100")),
           Some (Speed (None,"1.2")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "10",
                      Action
                        ({actionLabel = None;},
                         [ChangeDirection
                            (Direction (None,"150-$rand*300"),
                             Term "30");
                          Repeat
                            (Times "5",
                             Action
                               ({actionLabel = None;},
                                [Wait "6";
                                 Fire
                                   ({fireLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Relative;},"180")),
                                    Some (Speed (None,"1.2")),
                                    Bullet ({bulletLabel = None;},None,None,[]))]))]));
                   Repeat
                     (Times "999",
                      Action
                        ({actionLabel = None;},
                         [Wait "6";
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Relative;},"180")),
                             Some (Speed (None,"1.2")),
                             Bullet ({bulletLabel = None;},None,None,[]))]))])]))])

  /// 大原さんのオリジナル、ふきだしボム by 白い弾幕くん
  /// [Original]_balloon_bomb.xml
  let ``balloon_bomb`` = 
    "大原さんのオリジナル、ふきだしボム by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "3 + 17 * $rank",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"120 + 120 * $rand")),
                      Some (Speed (None,"1.0 + 0.3 * $rand")),
                      BulletRef
                        ({bulletRefLabel = "balloon";},["0.6 + 1.2 * $rank"]));
                   Wait "43 - 30 * $rank"])); Wait "200 - 100 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "balloon";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "20"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                   Some (Speed (None,"$1 * 0.88")),
                   BulletRef ({bulletRefLabel = "balloonbit";},["$1 * 0.88"]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
                          Some (Speed (None,"$1 * 0.88")),
                          BulletRef ({bulletRefLabel = "balloonbit";},["$1 * 0.88"]))]));
                Repeat
                  (Times "24",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                          Some (Speed (None,"$1")),
                          BulletRef
                            ({bulletRefLabel = "curvebit";},["10"; "40"; "$1"]))]));
                Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                   Some (Speed (None,"$1")),
                   BulletRef ({bulletRefLabel = "curvebit";},["10"; "40"; "$1"]));
                Repeat
                  (Times "23",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                          Some (Speed (None,"$1")),
                          BulletRef
                            ({bulletRefLabel = "curvebit";},["10"; "40"; "$1"]))]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-3")),
                   Some (Speed (None,"$1 * 0.88")),
                   BulletRef ({bulletRefLabel = "balloonbit";},["$1 * 0.88"]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
                          Some (Speed (None,"$1 * 0.88")),
                          BulletRef ({bulletRefLabel = "balloonbit";},["$1 * 0.88"]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "balloonbit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "4";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"-60")),
                   Some (Speed (None,"$1")),
                   BulletRef ({bulletRefLabel = "curvebit";},["5"; "-50"; "$1"]));
                Repeat
                  (Times "9",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"13")),
                          Some (Speed (None,"$1")),
                          BulletRef
                            ({bulletRefLabel = "curvebit";},["5"; "-50"; "$1"]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "curvebit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "$1";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"$2")),
                   Some (Speed (None,"$3")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])

  /// 大原さんのオリジナル、原点回帰その一 by 白い弾幕くん
  /// [Original]_btb_1.xml
  let ``btb_1`` = 
    "大原さんのオリジナル、原点回帰その一 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nway";},
                         ["(8 + 28 * $rank * $rank)"; "0.4"; "0"])); Wait "20";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nway";},
                         ["(8 + 28 * $rank * $rank)"; "0.4";
                          "(360 / (8 + 28 * $rank * $rank)) * (3/4)"])); Wait "20";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nway";},
                         ["(8 + 28 * $rank * $rank)"; "0.4";
                          "(360 / (8 + 28 * $rank * $rank)) * (1/2)"])); Wait "20";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nway";},
                         ["(8 + 28 * $rank * $rank)"; "0.4";
                          "(360 / (8 + 28 * $rank * $rank)) * (3/4)"])); Wait "20";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nway";},
                         ["(8 + 28 * $rank * $rank)"; "0.4";
                          "(360 / (8 + 28 * $rank * $rank))"])); Wait "20";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nway";},
                         ["(8 + 28 * $rank * $rank)"; "0.4";
                          "(360 / (8 + 28 * $rank * $rank)) * (1/4)"])); Wait "20";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nway";},
                         ["(8 + 28 * $rank * $rank)"; "0.4";
                          "(360 / (8 + 28 * $rank * $rank)) * (1/2)"])); Wait "20";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nway";},
                         ["(8 + 28 * $rank * $rank)"; "0.4";
                          "(360 / (8 + 28 * $rank * $rank)) * (1/4)"])); Wait "20"]));
            Wait "80";
            Repeat
              (Times "10",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nwayaim";},
                         ["(8 + 28 * $rank * $rank)"; "0.8 + 0.6 * $rank";
                          "(360 / (8 + 28 * $rank * $rank)) / 2"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nwayaim";},
                         ["(8 + 28 * $rank * $rank)"; "1.0 + 0.6 * $rank";
                          "(360 / (8 + 28 * $rank * $rank)) / 4"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "nwayaim";},
                         ["(8 + 28 * $rank * $rank)"; "1.2 + 0.6 * $rank"; "0"]));
                   Wait "30"])); Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "nway";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$3")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "$1",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"360 / $1")),
                          Some (Speed (None,"$2")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "nwayaim";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"$3")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "$1",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"360 / $1")),
                          Some (Speed (None,"$2")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// 大原さんのオリジナル、原点回帰その二 by 白い弾幕くん
  /// [Original]_btb_2.xml
  let ``btb_2`` = 
    "大原さんのオリジナル、原点回帰その二 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "280 / ((50 - 43 * $rank) * 4)",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vaim";},["0"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vaim";},["18"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vaim";},["-18"; "0.7 + 0.9 * $rank"]));
                   Wait "(50 - 43 * $rank)";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},["0"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},["60"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},
                         ["120"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},
                         ["180"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},
                         ["240"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},
                         ["300"; "0.7 + 0.9 * $rank"])); Wait "(50 - 43 * $rank)";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vaimrev";},["0"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vaimrev";},["18"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vaimrev";},["-18"; "0.7 + 0.9 * $rank"]));
                   Wait "(50 - 43 * $rank)";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},["30"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},["90"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},
                         ["150"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},
                         ["210"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},
                         ["270"; "0.7 + 0.9 * $rank"]));
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef
                        ({bulletRefLabel = "vabsolute";},
                         ["330"; "0.7 + 0.9 * $rank"])); Wait "(50 - 43 * $rank)"]));
            Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "vaim";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"$1")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"3")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-6")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-12")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-18")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "7 - 4 * $rank"; Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "vaimrev";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"$1")),
                   Some (Speed (None,"$2")),BulletRef ({bulletRefLabel = "red";},[]));
                Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"3")),
                   Some (Speed (None,"$2 * 1.1")),
                   BulletRef ({bulletRefLabel = "red";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-6")),
                   Some (Speed (None,"$2 * 1.1")),
                   BulletRef ({bulletRefLabel = "red";},[])); Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                   Some (Speed (None,"$2 * 1.21")),
                   BulletRef ({bulletRefLabel = "red";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-12")),
                   Some (Speed (None,"$2 * 1.21")),
                   BulletRef ({bulletRefLabel = "red";},[])); Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                   Some (Speed (None,"$2 * 1.331")),
                   BulletRef ({bulletRefLabel = "red";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-18")),
                   Some (Speed (None,"$2 * 1.331")),
                   BulletRef ({bulletRefLabel = "red";},[])); Wait "7 - 4 * $rank";
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "vabsolute";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"3")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-6")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-12")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "7 - 4 * $rank";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-18")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "7 - 4 * $rank"; Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "red";},None,None,
           [Action ({actionLabel = None;},[Wait "1000"; Vanish])])])

  /// 大原さんのオリジナル、原点回帰その三 by 白い弾幕くん
  /// [Original]_btb_3.xml
  let ``btb_3`` =
    "大原さんのオリジナル、原点回帰その三 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "half";},["1"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "half";},["-1"]));
            Wait "90 + 100 / (5 - 4 * $rank)"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "half";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some
                     (Direction
                        (Some {directionType = DirectionType.Absolute;},"4 * (5 - 4 * $rank) * $1")),
                   None,BulletRef ({bulletRefLabel = "laser";},["0.8"])); Wait "1";
                Repeat
                  (Times "30 / (5 - 4 * $rank)",
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
                                      (Some {directionType = DirectionType.Sequence;},
                                       "6 * (5 - 4 * $rank) * $1")),None,
                                 BulletRef ({bulletRefLabel = "laser";},["0.8"]))]));
                       Wait "1"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),None,
                   BulletRef ({bulletRefLabel = "laser";},["0.9"])); Wait "1";
                Repeat
                  (Times "30 / (5 - 4 * $rank)",
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
                                      (Some {directionType = DirectionType.Sequence;},
                                       "6 * (5 - 4 * $rank) * $1")),None,
                                 BulletRef ({bulletRefLabel = "laser";},["0.9"]))]));
                       Wait "1"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"11 * $1")),
                   None,BulletRef ({bulletRefLabel = "laser";},["1.2"])); Wait "1";
                Repeat
                  (Times "30 / (5 - 4 * $rank)",
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
                                      (Some {directionType = DirectionType.Sequence;},
                                       "6 * (5 - 4 * $rank) * $1")),None,
                                 BulletRef ({bulletRefLabel = "laser";},["1.2"]))]));
                       Wait "1"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "laser";},None,Some (Speed (None,"0.01")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "3";
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"2.5 - 2.0 * $rank")),
                          Some (Speed (None,"$1")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "3"]));
                Vanish])])])

  /// 大原さんのオリジナル、原点回帰その四 by 白い弾幕くん
  /// [Original]_btb_4.xml
  let ``btb_4`` = 
    "大原さんのオリジナル、原点回帰その四 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "dummy";},
                  ["8 + 28 * $rank"; "0.6 + 0.9 * $rank"])); Wait "440 - 90 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "12",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},None,None,
                          BulletRef
                            ({bulletRefLabel = "nwayabsolute";},["$1"; "$2"; "0"]));
                       Wait "3";
                       Repeat
                         (Times "2 + 3 * $rank",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},None,None,
                                 BulletRef
                                   ({bulletRefLabel = "nwayabsolute";},
                                    ["$1 / 3"; "$2 * (0.9 + 0.4 * $rand)";
                                     "(360 / 12)"]))])); Wait "3";
                       Fire
                         ({fireLabel = None;},None,None,
                          BulletRef
                            ({bulletRefLabel = "nwayabsolute";},
                             ["$1"; "$2"; "(360 / 36) * (1/4)"])); Wait "3";
                       Repeat
                         (Times "2 + 3 * $rank",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},None,None,
                                 BulletRef
                                   ({bulletRefLabel = "nwayabsolute";},
                                    ["$1 / 3"; "$2 * (0.9 + 0.4 * $rand)";
                                     "(360 / 12) * (3/4)"]))])); Wait "3";
                       Fire
                         ({fireLabel = None;},None,None,
                          BulletRef
                            ({bulletRefLabel = "nwayabsolute";},
                             ["$1"; "$2"; "(360 / 36) * (1/2)"])); Wait "3";
                       Repeat
                         (Times "2 + 3 * $rank",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},None,None,
                                 BulletRef
                                   ({bulletRefLabel = "nwayabsolute";},
                                    ["$1 / 3"; "$2 * (0.9 + 0.4 * $rand)";
                                     "(360 / 12) * (1/2)"]))])); Wait "3";
                       Fire
                         ({fireLabel = None;},None,None,
                          BulletRef
                            ({bulletRefLabel = "nwayabsolute";},
                             ["$1"; "$2"; "(360 / 36) * (3/4)"])); Wait "3";
                       Repeat
                         (Times "2 + 3 * $rank",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},None,None,
                                 BulletRef
                                   ({bulletRefLabel = "nwayabsolute";},
                                    ["$1 / 3"; "$2 * (0.9 + 0.4 * $rand)";
                                     "(360 / 12) * (1/4)"]))])); Wait "3"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "nwayabsolute";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$3")),
                   Some (Speed (None,"$2")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "$1",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"(360 / $1) + (3 + 12 * (1 - $rank) * (1 - $rank)) * (-1 + 2 * $rand)")),
                          Some (Speed (None,"$2")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// 大原さんのオリジナル、原点回帰その五 by 白い弾幕くん
  /// [Original]_btb_5.xml
  let ``btb_5`` = 
    "大原さんのオリジナル、原点回帰その五 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "dummy";},["72 * $rand"])); Wait "880"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),None,
                   BulletRef ({bulletRefLabel = "winder";},["2.0"]));
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"72")),
                          None,BulletRef ({bulletRefLabel = "winder";},["2.0"]))]));
                Wait "420";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1 + 96")),
                   None,
                   BulletRef ({bulletRefLabel = "shotgun";},["1.0 + 1.0 * $rank"]));
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"72")),
                          None,
                          BulletRef
                            ({bulletRefLabel = "shotgun";},["1.0 + 1.0 * $rank"]))]));
                Wait "350";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1 - 24")),
                   None,
                   BulletRef ({bulletRefLabel = "shotgun";},["1.0 + 1.0 * $rank"]));
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"72")),
                          None,
                          BulletRef
                            ({bulletRefLabel = "shotgun";},["1.0 + 1.0 * $rank"]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "shotgun";},None,Some (Speed (None,"0.001")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"-34")),
                   Some (Speed (None,"$1 * 0.93")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "68 / (12 - 10 * $rank * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},
                                "12 - 10 * $rank * $rank")),
                          Some (Speed (None,"$1 * 0.93")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-1")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "66 / (12 - 10 * $rank * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},
                                "-(12 - 10 * $rank * $rank)")),
                          Some (Speed (None,"$1")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "winder";},None,Some (Speed (None,"0.001")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "10",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                          None,BulletRef ({bulletRefLabel = "laser";},["$1"]));
                       Wait "14"]));
                Repeat
                  (Times "15",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"4")),
                          None,BulletRef ({bulletRefLabel = "laser";},["$1"]));
                       Wait "14"]));
                Repeat
                  (Times "10",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          None,BulletRef ({bulletRefLabel = "laser";},["$1"]));
                       Wait "14"]));
                Repeat
                  (Times "15",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"-8")),
                          None,BulletRef ({bulletRefLabel = "laser";},["$1"]));
                       Wait "14"]));
                Repeat
                  (Times "10",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          None,BulletRef ({bulletRefLabel = "laser";},["$1"]));
                       Wait "14"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "laser";},None,Some (Speed (None,"0.01")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"1")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "1";
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"2")),
                          Some (Speed (None,"$1")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"-2")),
                          Some (Speed (None,"$1")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"-2")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "1";
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"-2")),
                          Some (Speed (None,"$1")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"2")),
                          Some (Speed (None,"$1")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1"]));
                Vanish])])])

  /// 大原さんのオリジナル、原点回帰その六 by 白い弾幕くん
  /// [Original]_btb_6.xml
  let ``btb_6`` = 
    "大原さんのオリジナル、原点回帰その六 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "360"; "-7"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "330"; "-6"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "300"; "-5"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "270"; "-4"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "240"; "-3"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "210"; "-2"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "180"; "-1"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "180"; "1"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "150"; "2"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "120"; "3"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "90"; "4"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "60"; "5"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "30"; "6"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "halfwinder";},["1.8"; "2"; "0"; "7"]));
            Wait "700"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "halfwinder";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "$3"; "$4"]));
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef
                     ({bulletRefLabel = "changecolor";},["$1"; "$2"; "$3"; "-$4"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "changecolor";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Wait "$2 * 2";
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "$3"; "$4"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$3")),None,
                   BulletRef ({bulletRefLabel = "laser";},["$1"; "$2"]));
                Wait "$2 * (15 - 9 * $rank)";
                Repeat
                  (Times "300 / (15 - 9 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$4")),
                          None,BulletRef ({bulletRefLabel = "laser";},["$1"; "$2"]));
                       Wait "$2 * (15 - 9 * $rank)"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "laser";},None,Some (Speed (None,"0.01")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "1 + 3 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                          Some (Speed (None,"$1 * (0.5 + 0.5 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "$2"]));
                Vanish])])])

  /// 大原さんのオリジナル、検閲済 by 白い弾幕くん
  /// [Original]_censored.xml
  let ``censored`` =
    "大原さんのオリジナル、検閲済 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
               Some (Speed (None,"3.0")),
               BulletRef ({bulletRefLabel = "center";},["0"]));
            Wait "800 - 50 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "center";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "15";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"-10 + $1")),
                   Some (Speed (None,"2.0")),
                   BulletRef ({bulletRefLabel = "arm";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"80 + $1")),
                   Some (Speed (None,"2.0")),
                   BulletRef ({bulletRefLabel = "arm";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"170 + $1")),
                   Some (Speed (None,"2.0")),
                   BulletRef ({bulletRefLabel = "arm";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"260 + $1")),
                   Some (Speed (None,"2.0")),
                   BulletRef ({bulletRefLabel = "arm";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "arm";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "25"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "5";
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "halfwinder";},["330"; "-8"]));
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "halfwinder";},["270"; "-5"]));
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "halfwinder";},["210"; "-2"]));
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "halfwinder";},["150"; "2"]));
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "halfwinder";},["90"; "5"]));
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "halfwinder";},["30"; "8"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "halfwinder";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"]));
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "changecolor";},["$1"; "-$2"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "changecolor";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Wait "(62 - 50 * $rank)/3";
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"0.7 + 1.1 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "62 - 50 * $rank";
                Repeat
                  (Times "600 / (62 - 50 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$2")),
                          Some (Speed (None,"0.7 + 1.1 * $rank")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "62 - 50 * $rank"])); Vanish])])])

  /// 大原さんのオリジナル、キメラ by 白い弾幕くん
  /// [Original]_chimera.xml
  let ``chimera`` =
    "大原さんのオリジナル、キメラ by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"0")),None,
               BulletRef ({bulletRefLabel = "centerbit";},[])); Wait "450"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "centerbit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
           Some (Speed (None,"3.0")),
           [Action
              ({actionLabel = None;},
               [Wait "40"; ChangeSpeed (Speed (None,"0.001"),Term "1"); Wait "5";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"160 + 40 * $rand"),
                   Term "90");
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"60")),None,
                   BulletRef ({bulletRefLabel = "sidebit";},["-40"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"-60")),None,
                   BulletRef ({bulletRefLabel = "sidebit";},["40"])); Wait "90";
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = Aim;},"170 + 20 * $rand"),
                          Term "1"); ChangeSpeed (Speed (None,"0.85"),Term "1");
                       Wait "40"; ChangeSpeed (Speed (None,"0.001"),Term "1");
                       Wait "5";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = Aim;},"60")),None,
                          BulletRef ({bulletRefLabel = "sidebit";},["-40"]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = Aim;},"-60")),None,
                          BulletRef ({bulletRefLabel = "sidebit";},["40"]));
                       Wait "45"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "sidebit";},None,Some (Speed (None,"0.5")),
           [Action
              ({actionLabel = None;},
               [Wait "30"; ChangeSpeed (Speed (None,"0.001"),Term "1"); Wait "5";
                Repeat
                  (Times "30 + 220 * $rank * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Relative;},
                                "$1 - 45 + 90 * $rand")),
                          Some (Speed (None,"3.5 + 1.0 * $rand")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// オリジナル、円を描きながらの自機狙い3way by 白い弾幕くん
  /// [Original]_circle.xml
  let ``circle`` = 
    "オリジナル、円を描きながらの自機狙い3way by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "5sp";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$1")),
               Some (Speed (None,"1")),Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.4")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "maru";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
           Some (Speed (None,"4")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [ChangeDirection
                     (Direction (Some {directionType = DirectionType.Sequence;},"4"),Term "1000");
                   Repeat
                     (Times "90",
                      Action
                        ({actionLabel = None;},
                         [Action.ActionRef ({actionRefLabel = "5sp";},["0"]);
                          Action.ActionRef ({actionRefLabel = "5sp";},["70-$rank*40"]);
                          Action.ActionRef ({actionRefLabel = "5sp";},["-70+$rank*40"]);
                          Wait "3"])); Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [FireRef ({fireRefLabel = "maru";},[]); Wait "320"])])

  /// オリジナル、どかんと一発 by 白い弾幕くん         
  /// [Original]_dokkaan.xml
  let ``dokkaan`` =
    "オリジナル、どかんと一発 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "200+200*$rank",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},Some (Direction (None,"120*$rand-60")),
                      Some (Speed (None,"0.5+$rand*2")),
                      Bullet ({bulletLabel = None;},None,None,[]))])); Wait "150"])])



  /// 大原さんのオリジナル、楕円ボム by 白い弾幕くん
  /// [Original]_ellipse_bomb.xml
  let ``ellipse_bomb`` =
    "大原さんのオリジナル、楕円ボム by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some
                 (Direction (Some {directionType = DirectionType.Absolute;},"215 + 20 * $rand")),
               Some (Speed (None,"2.0")),
               BulletRef
                 ({bulletRefLabel = "bit";},
                  ["0.8"; "1.4 * (0.5 + 0.5 * $rank)"; "30 - 20 * $rank"]));
            Wait "40";
            Fire
              ({fireLabel = None;},
               Some
                 (Direction (Some {directionType = DirectionType.Absolute;},"145 - 20 * $rand")),
               Some (Speed (None,"2.0")),
               BulletRef
                 ({bulletRefLabel = "bit";},
                  ["0.8"; "1.4 * (0.5 + 0.5 * $rank)"; "30 - 20 * $rank"]));
            Wait "40";
            Fire
              ({fireLabel = None;},
               Some
                 (Direction (Some {directionType = DirectionType.Absolute;},"170 + 20 * $rand")),
               Some (Speed (None,"2.0")),
               BulletRef
                 ({bulletRefLabel = "bit";},
                  ["0.8"; "1.4 * (0.5 + 0.5 * $rank)"; "30 - 20 * $rank"]));
            Wait "600 - 250 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "15"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "15";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                   None,BulletRef ({bulletRefLabel = "ellipse";},["$1"; "$2"; "$3"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "ellipse";},None,Some (Speed (None,"0.001")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"5")),
                   Some (Speed (None,"$1")),
                   BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]));
                Repeat
                  (Times "6",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"-($1 * 0.04)")),
                          BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]))]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"-($1 * 0.01)")),
                          BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]))]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                   Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                   BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"($1 * 0.01)")),
                          BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]))]));
                Repeat
                  (Times "6",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"($1 * 0.04)")),
                          BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]))]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                   Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                   BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]));
                Repeat
                  (Times "6",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"-($1 * 0.04)")),
                          BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]))]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"-($1 * 0.01)")),
                          BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]))]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                   Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                   BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]));
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"($1 * 0.01)")),
                          BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]))]));
                Repeat
                  (Times "6",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"10")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"($1 * 0.04)")),
                          BulletRef ({bulletRefLabel = "red";},["$2"; "$3"]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "red";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "35"; ChangeSpeed (Speed (None,"0.001"),Term "1"); Wait "5";
                Wait "$2";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"140")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "$2";
                Repeat
                  (Times "12",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"7")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"$1 * 0.03")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "$2"]));
                Vanish])])])

  /// 大原さんのオリジナル、EntangledSpace by 白い弾幕くん
  /// [Original]_entangled_space.xml
  let ``entangled_space`` = 
    "大原さんのオリジナル、EntangledSpace by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "dummy";},
                  ["2.5"; "1.2 * (0.5 + 0.5 * $rank)"; "11 * (3.5 - 2.5 * $rank)";
                   "0"; "10"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "dummy";},
                  ["1.5"; "1.5 * (0.5 + 0.5 * $rank)"; "7 * (3.5 - 2.5 * $rank)";
                   "0"; "-8"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "dummy";},
                  ["0.5"; "1.8 * (0.5 + 0.5 * $rank)"; "5 * (3.5 - 2.5 * $rank)";
                   "0"; "6"])); Wait "1050 - 150 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
           Some (Speed (None,"2.0")),
           [Action
              ({actionLabel = None;},
               [Wait "15"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "15";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                   Some (Speed (None,"$1")),
                   BulletRef ({bulletRefLabel = "bit";},["$2"; "$3"; "$4"; "$5"]));
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),
                          Some (Speed (None,"$1")),
                          BulletRef
                            ({bulletRefLabel = "bit";},["$2"; "$3"; "$4"; "$5"]))]));
                Wait "20";
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),
                          Some (Speed (None,"$1")),
                          BulletRef
                            ({bulletRefLabel = "bit";},["$2"; "$3"; "$4"; "$5"]));
                       Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"90")),
                                 Some (Speed (None,"$1")),
                                 BulletRef
                                   ({bulletRefLabel = "bit";},
                                    ["$2"; "$3"; "$4"; "$5"]))])); Wait "20"]));
                Repeat
                  (Times "200 / $3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"$5 * 2.5")),
                          Some (Speed (None,"$2 * 0.6")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some
                                   (Speed (Some {speedType = SpeedType.Sequence;},"$2 * 0.05")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]));
                       Repeat
                         (Times "2",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"120")),
                                 Some (Speed (None,"$2 * 0.6")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Repeat
                                (Times "3",
                                 Action
                                   ({actionLabel = None;},
                                    [Fire
                                       ({fireLabel = None;},
                                        Some
                                          (Direction
                                             (Some {directionType = DirectionType.Sequence;},"0")),
                                        Some
                                          (Speed
                                             (Some {speedType = SpeedType.Sequence;},
                                              "$2 * 0.05")),
                                        Bullet ({bulletLabel = None;},None,None,[]))]))]));
                       Wait "$3 * 3"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "15"; ChangeSpeed (Speed (None,"0.001"),Term "1"); Wait "1";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-(17 * $4)"),
                   Term "1"); Wait "1"; Wait "200 - 100 * $rank";
                ChangeSpeed (Speed (None,"0.13"),Term "1"); Wait "1";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"$3")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "$2";
                Repeat
                  (Times "96 / $2",
                   Action
                     ({actionLabel = None;},
                      [Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"$4")),
                                 Some
                                   (Speed
                                      (Some {speedType = SpeedType.Sequence;},"-($1 * 0.1)")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Wait "$2"]));
                       Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"$4")),
                                 Some
                                   (Speed
                                      (Some {speedType = SpeedType.Sequence;},"($1 * 0.1)")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Wait "$2"]))])); Vanish])])])

  /// 大原さんのオリジナル、邪眼 by 白い弾幕くん
  /// [Original]_evil_eye.xml
  let ``evil_eye`` =
    "大原さんのオリジナル、邪眼 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"0.02")),
               BulletRef ({bulletRefLabel = "cannonbit";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"0.06")),
               BulletRef ({bulletRefLabel = "cannonbit";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"0.10")),
               BulletRef ({bulletRefLabel = "cannonbit";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               Some (Speed (None,"0.02")),
               BulletRef ({bulletRefLabel = "cannonbit";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               Some (Speed (None,"0.06")),
               BulletRef ({bulletRefLabel = "cannonbit";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               Some (Speed (None,"0.10")),
               BulletRef ({bulletRefLabel = "cannonbit";},[])); Wait "120";
            Repeat
              (Times "5 + 10 * $rank",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "5way";},["30"]);
                   Wait "27 - 20 * $rank";
                   Action.ActionRef ({actionRefLabel = "5way";},["20"]);
                   Wait "27 - 20 * $rank"])); Wait "60"]);
        BulletmlElm.Action
          ({actionLabel = Some "5way";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$1 * (-2)")),
               Some (Speed (None,"1.3")),
               BulletRef ({bulletRefLabel = "bit";},["$1 * 2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$1 * (-1)")),
               Some (Speed (None,"1.3")),
               BulletRef ({bulletRefLabel = "bit";},["$1 * 1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"0")),
               Some (Speed (None,"1.3")),BulletRef ({bulletRefLabel = "bit";},["0"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$1 * 1")),
               Some (Speed (None,"1.3")),
               BulletRef ({bulletRefLabel = "bit";},["$1 * (-1)"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$1 * 2")),
               Some (Speed (None,"1.3")),
               BulletRef ({bulletRefLabel = "bit";},["$1 * (-2)"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "30";
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction
                        (Some {directionType = DirectionType.Relative;},"$1 - 15 + 30 * $rand")),
                   Some (Speed (None,"1.3 + 1.0 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "2 + 3 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.1")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "cannonbit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "60";
                Repeat
                  (Times "80",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                          Some (Speed (None,"0.0001 + 12.0 * $rand")),
                          BulletRef ({bulletRefLabel = "weak";},[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "weak";},None,None,
           [Action ({actionLabel = None;},[Wait "10"; Vanish])])])

  /// 大原さんのオリジナル、偽婦人乱舞 by 白い弾幕くん
  /// [Original]_fujin_ranbu_fake.xml
  let ``fujin_ranbu_fake`` = 
    "大原さんのオリジナル、偽婦人乱舞 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "dummy";},
                  ["0.6 + 0.6 * $rank"; "48 - 41 * $rank"; "8 * $rank"]));
            Wait "400 - 100 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
                ChangeSpeed (Speed (None,"2"),Term "20");
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 + 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "$3"]));
                       Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 - 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "-$3"]));
                       Wait "10"])); ChangeSpeed (Speed (None,"0"),Term "20");
                Repeat
                  (Times "1",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 + 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "$3"]));
                       Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 - 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "-$3"]));
                       Wait "10"]));
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1");
                ChangeSpeed (Speed (None,"2"),Term "30");
                Repeat
                  (Times "999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 + 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "$3"]));
                       Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 - 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "-$3"]));
                       Wait "10"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),None,
                   BulletRef ({bulletRefLabel = "shotgun";},["$1"])); Wait "$2";
                Repeat
                  (Times "200",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$3")),
                          None,BulletRef ({bulletRefLabel = "shotgun";},["$1"]));
                       Wait "$2"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "shotgun";},None,Some (Speed (None,"0.001")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.21")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.331")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.4641")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.610510")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])

  /// 大原さんのオリジナル、真婦人乱舞 by 白い弾幕くん
  /// [Original]_fujin_ranbu_true.xml
  let ``fujin_ranbu_true`` =
    "大原さんのオリジナル、真婦人乱舞 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef
                 ({bulletRefLabel = "dummy";},
                  ["0.6 + 0.6 * $rank"; "48 - 41 * $rank"; "8 * $rank"]));
            Wait "400 - 100 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
                ChangeSpeed (Speed (None,"2"),Term "20");
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 + 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "-$3"]));
                       Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 - 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "$3"]));
                       Wait "10"])); ChangeSpeed (Speed (None,"0"),Term "20");
                Repeat
                  (Times "1",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 + 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "-$3"]));
                       Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 - 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "$3"]));
                       Wait "10"]));
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1");
                ChangeSpeed (Speed (None,"2"),Term "30");
                Repeat
                  (Times "999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 + 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "-$3"]));
                       Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Absolute;},"180 - 60")),
                          None,
                          BulletRef ({bulletRefLabel = "bit";},["$1"; "$2"; "$3"]));
                       Wait "10"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),None,
                   BulletRef ({bulletRefLabel = "shotgun";},["$1"])); Wait "$2";
                Repeat
                  (Times "200",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$3")),
                          None,BulletRef ({bulletRefLabel = "shotgun";},["$1"]));
                       Wait "$2"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "shotgun";},None,Some (Speed (None,"0.001")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.21")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.331")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.4641")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"$1 * 1.610510")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])

  /// オリジナル、ぐるぐる by 白い弾幕くん
  /// [Original]_guruguru.xml
  let ``guruguru`` = 
    "オリジナル、ぐるぐる by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "issyuu";},
           [Repeat
              (Times "360/(20-$rank*10)+1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Sequence;},"18-$rank*6")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "guruguru";},None,Some (Speed (None,"0.3")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},None,Some (Speed (None,"0.7")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "issyuu";},[]);
                   Repeat
                     (Times "30",
                      Action
                        ({actionLabel = None;},
                         [Wait "12";
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"-356")),
                             Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Action.ActionRef ({actionRefLabel = "issyuu";},[])])); Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [FireRef ({fireRefLabel = "guruguru";},[]); Wait "500"])])

  /// オリジナル。ぐるちょ。 by 白い弾幕くん
  /// [Original]_gurutyo.xml
  let ``gurutyo`` = 
    "オリジナル。ぐるちょ。 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Fire
          ({fireLabel = Some "gurutyo";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
           Some (Speed (None,"$1*(3+$rank*4)")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [ChangeDirection
                     (Direction (Some {directionType = DirectionType.Sequence;},"$1*6"),
                      Term "1000");
                   Repeat
                     (Times "500",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                             None,Bullet ({bulletLabel = None;},None,None,[]));
                          Wait "1"])); Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [FireRef ({fireRefLabel = "gurutyo";},["1"]);
            FireRef ({fireRefLabel = "gurutyo";},["-1"]); Wait "550"])])

  /// オリジナル。逆噴射。by 白い弾幕くん
  /// [Original]_gyakuhunsya.xml
  let ``gyakuhunsya`` =
    "オリジナル。逆噴射。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "gyakuhunsya";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
            ChangeSpeed (Speed (None,"2"),Term "20"); Wait "60";
            ChangeSpeed (Speed (None,"0"),Term "20"); Wait "20";
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1");
            ChangeSpeed (Speed (None,"2"),Term "30");
            Fire
              ({fireLabel = None;},None,Some (Speed (None,"0.6")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "80",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "2+$rank*2",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (None,"$rand*120-60")),
                             Some (Speed (Some {speedType = SpeedType.Sequence;},"0.005")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "1"])); ChangeSpeed (Speed (None,"0"),Term "1"); Wait "10"]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "gyakuhunsya";},[])])])

  /// 大原さんのオリジナル、ハジケリスト by 白い弾幕くん
  /// [Original]_hajike.xml
  let ``hajike`` = 
    "大原さんのオリジナル、ハジケリスト by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180 - 60")),
                      None,BulletRef ({bulletRefLabel = "wave";},["5"; "42"]));
                   Wait "30 - 15 * $rank";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180 + 60")),
                      None,BulletRef ({bulletRefLabel = "wave";},["-5"; "-42"]));
                   Wait "30 - 15 * $rank";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180 - 62")),
                      None,BulletRef ({bulletRefLabel = "wave";},["5"; "40"]));
                   Wait "30 - 15 * $rank";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180 + 62")),
                      None,BulletRef ({bulletRefLabel = "wave";},["-5"; "-40"]));
                   Wait "30 - 15 * $rank";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180 - 58")),
                      None,BulletRef ({bulletRefLabel = "wave";},["5"; "40"]));
                   Wait "30 - 15 * $rank";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180 + 58")),
                      None,BulletRef ({bulletRefLabel = "wave";},["-5"; "-40"]));
                   Wait "30 - 15 * $rank"])); Wait "300 - 50 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "wave";},None,Some (Speed (None,"1.0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"$2")),
                   Some (Speed (None,"0.3")),
                   BulletRef ({bulletRefLabel = "cross";},[])); Wait "3";
                Repeat
                  (Times "10 + 20 * $rank * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$1")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.05")),
                          BulletRef ({bulletRefLabel = "cross";},[])); Wait "3"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "cross";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "100";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (Some {speedType = SpeedType.Relative;},"0.6 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"90")),
                   Some (Speed (Some {speedType = SpeedType.Relative;},"0.6 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"180")),
                   Some (Speed (Some {speedType = SpeedType.Relative;},"0.6 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"270")),
                   Some (Speed (Some {speedType = SpeedType.Relative;},"0.6 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])

  /// オリジナル。はさみ。by 白い弾幕くん
  /// [Original]_hasami.xml
  let ``hasami`` = 
    "オリジナル。はさみ。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "curve";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$1")),
               Some (Speed (None,"1.2+$rank*0.6")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction
                            (Some {directionType = Aim;},"$1*-1.5"),
                          Term "100")])]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-$1")),
               Some (Speed (None,"1.3+$rank*0.4")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction
                            (Some {directionType = Aim;},"$1*1.5"),
                          Term "100")])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "200",
               Action
                 ({actionLabel = None;},
                  [Wait "2";
                   Action.ActionRef ({actionRefLabel = "curve";},["$rand*90"])]));
            Wait "50"])])
                        
  /// オリジナル。ひらひら。by 白い弾幕くん  
  /// [Original]_hirahira.xml
  let ``hirahira`` = 
    "オリジナル。ひらひら。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "5way";},
           [Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"20")),None,
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "hira";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-40+$1*60")),None,
               Bullet ({bulletLabel = None;},None,None,[]));
            Action.ActionRef ({actionRefLabel = "5way";},[]);
            Repeat
              (Times "60",
               Action
                 ({actionLabel = None;},
                  [Wait "8-$rank*2";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"-80-$1*2+$rand-0.5")),
                      None,Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "5way";},[])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Action.ActionRef ({actionRefLabel = "hira";},["-1"]); Wait "60"]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Action.ActionRef ({actionRefLabel = "hira";},["1"]); Wait "60"])])

  /// オリジナル。放水っぽい感じ。by 白い弾幕くん
  /// [Original]_housya.xml
  let ``housya`` = 
    "オリジナル。放水っぽい感じ。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},Some (Direction (None,"-80")),None,
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "70",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "3*($rank+0.5)",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},
                                   "($rand*20-9)/($rank+0.5)")),
                             Some
                               (Speed
                                  (Some {speedType = SpeedType.Sequence;},"0.01/($rank+0.5)")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "1"]))])])

  /// 大原さんのオリジナル、カゴメ by 白い弾幕くん
  /// [Original]_kagome.xml
  let ``kagome`` = 
    "大原さんのオリジナル、カゴメ by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10 + 10 * $rank",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"75")),
                      Some (Speed (None,"1.1")),
                      BulletRef ({bulletRefLabel = "matrixbit";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"255")),
                      Some (Speed (None,"1.1")),
                      BulletRef ({bulletRefLabel = "matrixbit";},[]));
                   Wait "30 * (2.0 - 1.0 * $rank)"])); Wait "150"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "matrixbit";},None,Some (Speed (None,"0.5")),
           [Action
              ({actionLabel = None;},
               [Wait "30 * (2.0 - 1.0 * $rank)";
                Repeat
                  (Times "999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"90")),
                          Some (Speed (None,"1.1")),
                          BulletRef ({bulletRefLabel = "finalbit";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-90")),
                          Some (Speed (None,"1.1")),
                          BulletRef ({bulletRefLabel = "finalbit";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-10 + 20 * $rand")),
                          Some (Speed (None,"1.1")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "60 * (2.0 - 1.0 * $rank)"]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "finalbit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "30 * (2.0 - 1.0 * $rank)";
                Repeat
                  (Times "999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"30")),
                          Some (Speed (None,"1.1 * (2 / 1.7320508)")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-30")),
                          Some (Speed (None,"1.1 * (2 / 1.7320508)")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "60 * (2.0 - 1.0 * $rank)"]))])])])

  /// 大原さんのオリジナル、毛玉 by 白い弾幕くん
  /// [Original]_kedama.xml
  let ``kedama`` = 
    "大原さんのオリジナル、毛玉 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"90")),None,
                      BulletRef ({bulletRefLabel = "bit";},["1"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"-90")),None,
                      BulletRef ({bulletRefLabel = "bit";},["-1"])); Wait "90"]));
            Wait "250"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,Some (Speed (None,"3.0")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; ChangeSpeed (Speed (None,"0.6"),Term "1"); Wait "5";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-105 * $1"),Term "1");
                Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"60 * $1")),
                   Some (Speed (None,"0.6 + 0.7 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "12 - 10 * $rank";
                Repeat
                  (Times "999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"113 * $1")),
                          Some (Speed (None,"0.6 + 0.7 * $rank")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "12 - 10 * $rank"]))])])])

  /// 大原さんのオリジナル、弾幕の騎士その一 by 白い弾幕くん
  /// [Original]_knight_1.xml
  let ``knight_1`` =
    "大原さんのオリジナル、弾幕の騎士その一 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},[])); Wait "450"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},
           Some (Direction (Some {directionType = Aim;},"0")),
           Some (Speed (None,"0.5")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"1.1"),Term "120");
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Repeat
                         (Times "6",
                          Action
                            ({actionLabel = None;},
                             [ChangeDirection
                                (Direction (Some {directionType = Aim;},"0"),
                                 Term "10"); Wait "10"]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-70 + 20 * $rand")),
                          None,BulletRef ({bulletRefLabel = "kick";},[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "kick";},None,Some (Speed (None,"4.0")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0.001"),Term "30"); Wait "30";
                ChangeDirection
                  (Direction (Some {directionType = Aim;},"0"),Term "1"); Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"-20")),
                   Some (Speed (None,"0.7")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "4 + 25 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"10 - 8 * $rank")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.05")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Wait "10";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"0.7")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "4 + 25 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"10 - 8 * $rank")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.05")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Wait "10";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"20")),
                   Some (Speed (None,"0.7")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "4 + 25 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"10 - 8 * $rank")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.05")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// 大原さんのオリジナル、弾幕の騎士その二 by 白い弾幕くん
  /// [Original]_knight_2.xml
  let ``knight_2`` = 
    "大原さんのオリジナル、弾幕の騎士その二 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"120")),None,
               BulletRef ({bulletRefLabel = "bit";},["90"; "1.0"])); Wait "10";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"150")),None,
               BulletRef ({bulletRefLabel = "bit";},["70"; "1.2"])); Wait "10";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),None,
               BulletRef ({bulletRefLabel = "bit";},["50"; "1.4"])); Wait "10";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"210")),None,
               BulletRef ({bulletRefLabel = "bit";},["30"; "1.6"])); Wait "10";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"240")),None,
               BulletRef ({bulletRefLabel = "bit";},["10"; "1.8"]));
            Wait "300 - 100 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,Some (Speed (None,"2.5")),
           [Action
              ({actionLabel = None;},
               [Wait "30"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "$1";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),
                   Some (Speed (None,"$2 * (0.5 + 0.5 * $rank)")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "19 + 100 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},
                                "360 / (19 + 100 * $rank)")),
                          Some (Speed (None,"$2 * (0.5 + 0.5 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// 大原さんのオリジナル、弾幕の騎士その三 by 白い弾幕くん
  /// [Original]_knight_3.xml
  let ``knight_3`` = 
    "大原さんのオリジナル、弾幕の騎士その三 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"60")),None,
               BulletRef ({bulletRefLabel = "bit";},[])); Wait "300"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,Some (Speed (None,"3.0")),
           [Action
              ({actionLabel = None;},
               [Wait "20"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "2";
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "groundbit";},[])); Wait "30";
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "skybit";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "skybit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"210")),
           Some (Speed (None,"3.0")),
           [Action
              ({actionLabel = None;},
               [Wait "30";
                Repeat
                  (Times "4",
                   Action
                     ({actionLabel = None;},
                      [Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"110 + 20 * $rand")),
                          None,BulletRef ({bulletRefLabel = "dummy";},[]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "groundbit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
           Some (Speed (None,"3.0")),
           [Action
              ({actionLabel = None;},
               [Wait "20";
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"230 + 20 * $rand")),
                          None,BulletRef ({bulletRefLabel = "dummy";},[]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},None,Some (Speed (None,"0.001")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "5 + 15 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Relative;},"-10 + 20 * $rand")),
                          Some (Speed (None,"1.0 + 1.4 * $rank")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "5",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some (Speed (Some {speedType = SpeedType.Sequence;},"0.1")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]));
                       Wait "12 - 10 * $rank"])); Vanish])])])

  /// 大原さんのオリジナル、弾幕の騎士その四 by 白い弾幕くん
  /// [Original]_knight_4.xml
  let ``knight_4`` = 
    "大原さんのオリジナル、弾幕の騎士その四 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"120")),None,
               BulletRef ({bulletRefLabel = "parentbit";},["1.6"]));
            Wait "300 - 100 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "parentbit";},
           Some (Direction (Some {directionType = Aim;},"0")),
           Some (Speed (None,"1.5")),
           [Action
              ({actionLabel = None;},
               [Wait "20 + 10 * $rand";
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"60")),
                          None,BulletRef ({bulletRefLabel = "bit";},["-120"; "$1"]));
                       Wait "10";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-60")),
                          None,BulletRef ({bulletRefLabel = "bit";},["120"; "$1"]));
                       Wait "10"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},None,Some (Speed (None,"2.5")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; ChangeSpeed (Speed (None,"0.0001"),Term "1"); Wait "5";
                Repeat
                  (Times "4 + 4 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"$1")),
                          Some (Speed (None,"$2 * (0.5 + 0.5 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "5"]));
                Vanish])])])

  /// オリジナル。固体。 by 白い弾幕くん
  /// [Original]_kotai.xml
  let ``kotai`` = 
    "オリジナル。固体。 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "src";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10";
                Repeat
                  (Times "25",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"$rand*360")),
                          Some (Speed (None,"$rand*10")),
                          Bullet
                            ({bulletLabel = None;},None,None,
                             [Action
                                ({actionLabel = None;},
                                 [Wait "1";
                                  Fire
                                    ({fireLabel = None;},
                                     Some
                                       (Direction
                                          (Some {directionType = DirectionType.Absolute;},"180")),
                                     Some (Speed (None,"1.8")),
                                     Bullet ({bulletLabel = None;},None,None,[]));
                                  Vanish])]))]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "350/(10-$rank*6)",
               Action
                 ({actionLabel = None;},
                  [Wait "10-$rank*6";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"90")),
                      Some (Speed (None,"-10+$rand*20")),
                      BulletRef ({bulletRefLabel = "src";},[]))])); Wait "30"])])

  /// 大原さんのオリジナル、鯨幕砲 by 白い弾幕くん
  /// [Original]_kujira.xml
  let ``kujira`` = 
    "大原さんのオリジナル、鯨幕砲 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},["2"; "1.0"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},["-2"; "1.0"])); Wait "10";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},["2.5"; "1.05"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},["-2.5"; "1.05"])); Wait "10";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},["3"; "1.1"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},["-3"; "1.1"])); Wait "10";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},["3.5"; "1.15"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "bit";},["-3.5"; "1.15"])); Wait "160";
            Repeat
              (Times "5",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"30")),
                      Some (Speed (None,"1.55")),
                      BulletRef ({bulletRefLabel = "kujira";},["179"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"40")),
                      Some (Speed (None,"1.4")),
                      BulletRef ({bulletRefLabel = "kujira";},["170"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"50")),
                      Some (Speed (None,"1.25")),
                      BulletRef ({bulletRefLabel = "kujira";},["160"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"60")),
                      Some (Speed (None,"1.1")),
                      BulletRef ({bulletRefLabel = "kujira";},["150"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"70")),
                      Some (Speed (None,"0.95")),
                      BulletRef ({bulletRefLabel = "kujira";},["140"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"80")),
                      Some (Speed (None,"0.8")),
                      BulletRef ({bulletRefLabel = "kujira";},["130"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-30")),
                      Some (Speed (None,"1.55")),
                      BulletRef ({bulletRefLabel = "kujira";},["-179"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-40")),
                      Some (Speed (None,"1.4")),
                      BulletRef ({bulletRefLabel = "kujira";},["-170"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-50")),
                      Some (Speed (None,"1.25")),
                      BulletRef ({bulletRefLabel = "kujira";},["-160"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-60")),
                      Some (Speed (None,"1.1")),
                      BulletRef ({bulletRefLabel = "kujira";},["-150"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-70")),
                      Some (Speed (None,"0.95")),
                      BulletRef ({bulletRefLabel = "kujira";},["-140"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-80")),
                      Some (Speed (None,"0.8")),
                      BulletRef ({bulletRefLabel = "kujira";},["-130"])); Wait "2"]));
            Wait "250 - 100 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "kujira";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "5";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"$1"),Term "60");
                Wait "60 + 5";
                ChangeSpeed (Speed (None,"3.5 * (0.75 + 0.25 * $rank)"),Term "30");
                Repeat
                  (Times "10 / (2.0 - 1.0 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = Aim;},"0"),
                          Term "8 * (2.0 - 1.0 * $rank)");
                       Wait "8 * (2.0 - 1.0 * $rank)"]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0.0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
                   Some (Speed (None,"$2 * (0.5 + 0.5 * $rank)")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Wait "2 * (3.5 - 2.5 * $rank)";
                Repeat
                  (Times "100 / (3.5 - 2.5 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},
                                "$1 * (3.5 - 2.5 * $rank)")),
                          Some (Speed (None,"$2 * (0.5 + 0.5 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "2 * (3.5 - 2.5 * $rank)"])); Vanish])])])

  /// オリジナル。くねくね。by 白い弾幕くん
  /// [Original]_kunekune.xml
  let ``kunekune`` = 
    "オリジナル。くねくね。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "fire";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
               Some (Speed (Some {speedType = SpeedType.Relative;},"-0.5")),
               Bullet ({bulletLabel = None;},None,None,[]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "src";},Some (Direction (None,"(30+$rank*20)*$1")),
           Some (Speed (None,"2")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "10",
                      Action
                        ({actionLabel = None;},
                         [Accel
                            (Some
                               (Horizontal
                                  (Some {horizontalType = Relative;},"$1*4")),None,
                             Term "20");
                          Repeat
                            (Times "10",
                             Action
                               ({actionLabel = None;},
                                [Action.ActionRef ({actionRefLabel = "fire";},["$1"]);
                                 Wait "2"]));
                          Accel
                            (Some
                               (Horizontal
                                  (Some {horizontalType = Relative;},"-$1*4")),None,
                             Term "20");
                          Repeat
                            (Times "10",
                             Action
                               ({actionLabel = None;},
                                [Action.ActionRef ({actionRefLabel = "fire";},["$1"]);
                                 Wait "2"]))]))])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "src";},["1"]);
                   FireRef ({fireRefLabel = "src";},["-1"]); Wait "80"])); Wait "60"])])

  /// オリジナル。扇状弾二つ。by 白い弾幕くん
  /// [Original]_oogi_hutatsu.xml
  let ``oogi_hutatsu`` = 
    "オリジナル。扇状弾二つ。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "oogiSeq";},
           [Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"8")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "oogi";},
           [Fire
              ({fireLabel = None;},Some (Direction (None,"-80")),
               Some (Speed (None,"$2")),Bullet ({bulletLabel = None;},None,None,[]));
            Action.ActionRef ({actionRefLabel = "oogiSeq";},[]);
            Repeat
              (Times "10+$rank*10",
               Action
                 ({actionLabel = None;},
                  [Wait "2";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"-$1")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.04")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef ({actionRefLabel = "oogiSeq";},[])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "oogi";},["161"; "0.8+$rank*0.4"]);
            Wait "30"; Action.ActionRef ({actionRefLabel = "oogi";},["159"; "1+$rank*0.6"]);
            Wait "150"])])

  /// 大原さんのオリジナル、光学探査兵器 by 白い弾幕くん
  /// [Original]_optic_seeker.xml
  let ``optic_seeker`` = 
    "大原さんのオリジナル、光学探査兵器 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "2 + 8 * $rank",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = Aim;},"-30 + 60 * $rand")),
                      Some (Speed (None,"3.0 + 2.0 * $rank")),
                      BulletRef ({bulletRefLabel = "seal";},[]));
                   Wait "25 - 20 * $rank";
                   Repeat
                     (Times "3",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = Aim;},"-60 + 120 * $rand")),
                             Some (Speed (None,"3.0 + 2.0 * $rank")),
                             BulletRef ({bulletRefLabel = "reflect";},[]));
                          Wait "25 - 20 * $rank"]))])); Wait "100"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "reflect";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "(30 + 20 * $rand) * (1.0 - 0.5 * $rank)";
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction
                            (Some {directionType = DirectionType.Relative;},"60 + 240 * $rand"),
                          Term "1"); Wait "(10 + 10 * $rand) * (1.0 - 0.5 * $rank)"]));
                ChangeDirection
                  (Direction (Some {directionType = Aim;},"0"),Term "1")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seal";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10 + 10 * $rand"; ChangeSpeed (Speed (None,"0"),Term "1");
                Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),
                   Some (Speed (None,"3.0 + 2.0 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "5";
                Repeat
                  (Times "7",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"45")),
                          Some (Speed (None,"3.0 + 2.0 * $rank")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "5"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = Aim;},"0")),
                   Some (Speed (None,"3.0 + 2.0 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])
  
  /// オリジナル。ぱん。by 白い弾幕くん
  /// [Original]_pan.xml
  let ``pan`` =
    "オリジナル。ぱん。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "pan";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10";
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction
                        (Some {directionType = DirectionType.Relative;},"95*$1")),
                   Some (Speed (None,"1.8+$rank")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-90")),
               Some (Speed (None,"0.001")),
               BulletRef ({bulletRefLabel = "pan";},["1"]));
            Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"3")),
                      Some
                        (Speed
                           (Some {speedType = SpeedType.Sequence;},"0.2+$rank*0.4")),
                      BulletRef ({bulletRefLabel = "pan";},["1"]))]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"90")),
               Some (Speed (None,"0.001")),
               BulletRef ({bulletRefLabel = "pan";},["-1"]));
            Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"-3")),
                      Some
                        (Speed
                           (Some {speedType = SpeedType.Sequence;},"0.2+$rank*0.4")),
                      BulletRef ({bulletRefLabel = "pan";},["-1"]))]));
            Wait "40"])])

  /// オリジナル。弱誘導弾から左右に弾幕。by 白い弾幕くん
  /// [Original]_progear_cheap_fake.xml
  let ``progear_cheap_fake`` = 
    "オリジナル。弱誘導弾から左右に弾幕。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Fire
          ({fireLabel = Some "weekHoming";},
           Some (Direction (Some {directionType = Aim;},"0")),
           Some (Speed (None,"0.1")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "3",
                      Action
                        ({actionLabel = None;},
                         [ChangeSpeed (Speed (None,"1.5"),Term "30"); Wait "30";
                          Repeat
                            (Times "2+$rank*4",
                             Action
                               ({actionLabel = None;},
                                [Fire
                                   ({fireLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Relative;},"90")),
                                    Some (Speed (None,"1.3")),
                                    Bullet ({bulletLabel = None;},None,None,[]));
                                 Fire
                                   ({fireLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Relative;},"-90")),
                                    Some (Speed (None,"1.3")),
                                    Bullet ({bulletLabel = None;},None,None,[]));
                                 Wait "2"]));
                          ChangeDirection
                            (Direction (Some {directionType = Aim;},"60-120*$rand"),
                             Term "20"); ChangeSpeed (Speed (None,"0.1"),Term "20");
                          Wait "20"])); ChangeSpeed (Speed (None,"1.5"),Term "30");
                   Wait "30"])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "weekHoming";},[]); Wait "60-$rank*30"]));
            Wait "180"])])

  /// オリジナル。炸裂弾。by 白い弾幕くん
  /// [Original]_sakuretudan.xml
  let ``sakuretudan`` = 
    "オリジナル。炸裂弾。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},Some (Direction (None,"$rand*360")),
                      Some (Speed (None,"2")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action
                            ({actionLabel = None;},
                             [ChangeDirection (Direction (None,"0"),Term "60");
                              Wait "60";
                              Repeat
                                (Times "15+$rank*20",
                                 Action
                                   ({actionLabel = None;},
                                    [Repeat
                                       (Times "2",
                                        Action
                                          ({actionLabel = None;},
                                           [Fire
                                              ({fireLabel = None;},
                                               Some (Direction (None,"360*$rand")),
                                               Some (Speed (None,"5")),
                                               Bullet
                                                 ({bulletLabel = None;},None,None,
                                                  [Action
                                                     ({actionLabel = None;},
                                                      [Wait "$rand*5";
                                                       Fire
                                                         ({fireLabel = None;},None,
                                                          Some
                                                            (Speed
                                                               (None,"$rand*4+0.5")),
                                                          Bullet
                                                            ({bulletLabel = None;},
                                                             None,None,[])); Vanish])]))]));
                                     Wait "1"])); Vanish])])); Wait "60"]));
            Wait "100"])])

  /// 大原さんのオリジナル、シューティングスター by 白い弾幕くん
  /// [Original]_shooting_star.xml
  let ``shooting_star`` = 
    "大原さんのオリジナル、シューティングスター by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"75 + 10 * $rand")),
               Some (Speed (None,"1.5")),
               BulletRef ({bulletRefLabel = "star";},["215 + 10 * $rand"]));
            Wait "50";
            Fire
              ({fireLabel = None;},
               Some
                 (Direction (Some {directionType = DirectionType.Absolute;},"-(75 + 10 * $rand)")),
               Some (Speed (None,"1.5")),
               BulletRef ({bulletRefLabel = "star";},["-(215 + 10 * $rand)"]));
            Wait "30";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"75 + 10 * $rand")),
               Some (Speed (None,"1.5")),
               BulletRef ({bulletRefLabel = "star";},["215 + 10 * $rand"]));
            Wait "10";
            Fire
              ({fireLabel = None;},
               Some
                 (Direction (Some {directionType = DirectionType.Absolute;},"-(75 + 10 * $rand)")),
               Some (Speed (None,"1.5")),
               BulletRef ({bulletRefLabel = "star";},["-(215 + 10 * $rand)"]));
            Wait "650 - 150 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "star";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "45"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "15";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"$1"),Term "1");
                ChangeSpeed (Speed (None,"2.0"),Term "180"); Wait "1";
                Repeat
                  (Times "180",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Relative;},"150 + 60 * $rand")),
                          None,
                          BulletRef
                            ({bulletRefLabel = "tail";},
                             ["1.5 * (0.25 + 0.75 * $rank)"])); Wait "1"]));
                Fire
                  ({fireLabel = None;},None,None,
                   BulletRef ({bulletRefLabel = "head";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "head";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
           Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "20 * (0.25 + 0.75 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                          Some
                            (Speed
                               (None,"(0.3 + 0.3 * $rand) * (0.25 + 0.75 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]))]));
                Repeat
                  (Times "30 * (0.25 + 0.75 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                          Some
                            (Speed
                               (None,"(0.5 + 0.5 * $rand) * (0.25 + 0.75 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]))]));
                Repeat
                  (Times "50 * (0.25 + 0.75 * $rank)",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                          Some
                            (Speed
                               (None,"(0.8 + 0.8 * $rand) * (0.25 + 0.75 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]))]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                   Some (Speed (None,"1.6 * (0.25 + 0.75 * $rank)")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "12 + 24 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},
                                "360 / (12 + 24 * $rank)")),
                          Some (Speed (None,"1.6 * (0.25 + 0.75 * $rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "tail";},None,Some (Speed (None,"0.001")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "1 + 3 * $rank * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Relative;},"-3 + 6 * $rand")),
                          Some
                            (Speed (None,"$1 * (1.0 + (0.1 + 0.2 * $rank) * $rand)")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1"]));
                Vanish])])])

  /// 大原さんのオリジナル、あの空に星を by 白い弾幕くん
  /// [Original]_star_in_the_sky.xml
  let ``star_in_the_sky`` = 
    "大原さんのオリジナル、あの空に星を by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
               None,BulletRef ({bulletRefLabel = "dummy";},[]));
            Repeat
              (Times "11",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),None,
                      BulletRef ({bulletRefLabel = "dummy";},[]))]));
            Wait "450 - 200 * $rank"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "dummy";},None,Some (Speed (None,"0.0001")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),
                   Some (Speed (None,"0.6")),
                   BulletRef ({bulletRefLabel = "star";},[])); Wait "2";
                Repeat
                  (Times "7",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"-7")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.05")),
                          BulletRef ({bulletRefLabel = "star";},[])); Wait "2"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "star";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "55"; ChangeSpeed (Speed (None,"0.0001"),Term "1"); Wait "5";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"-170")),
                   Some (Speed (None,"0.6 + 0.7 * $rank")),
                   Bullet ({bulletLabel = None;},None,None,[])); Wait "5";
                Repeat
                  (Times "3 * 4 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"11")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.05")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "5"]));
                Vanish])])])

  /// オリジナル。池に落ちた石六個。 by 白い弾幕くん
  /// [Original]_stone6.xml
  let ``stone6`` = 
    "オリジナル。池に落ちた石六個。 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"150")),
               Some (Speed (None,"4")),BulletRef ({bulletRefLabel = "roll";},[]));
            Wait "2";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"210")),
               Some (Speed (None,"4")),BulletRef ({bulletRefLabel = "roll";},[]));
            Wait "2";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"135")),
               Some (Speed (None,"3")),BulletRef ({bulletRefLabel = "roll";},[]));
            Wait "2";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"225")),
               Some (Speed (None,"3")),BulletRef ({bulletRefLabel = "roll";},[]));
            Wait "2";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"135")),
               Some (Speed (None,"2")),BulletRef ({bulletRefLabel = "roll";},[]));
            Wait "2";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"225")),
               Some (Speed (None,"2")),BulletRef ({bulletRefLabel = "roll";},[]));
            Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "roll";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10"; ChangeSpeed (Speed (None,"0"),Term "1");
                Repeat
                  (Times "45",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"8")),
                          Some (Speed (None,"1.3+$rank")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// オリジナル。道を探せ。by 白い弾幕くん
  /// [Original]_stop_and_run.xml
  let ``stop_and_run`` = 
    "オリジナル。道を探せ。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "stopAndRun";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "60"); Wait "180-$rank*120";
                Accel (None,Some (Vertical (None,"3")),Term "120"); Wait "120"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "200",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},Some (Direction (None,"$rand*360")),
                      Some (Speed (None,"3*$rand+0.5")),
                      BulletRef ({bulletRefLabel = "stopAndRun";},[]))]));
            Wait "260-$rank*120"])])

  /// 大原さんのオリジナル、時空転換 by 白い弾幕くん
  /// [Original]_time_twist.xml
  let ``time_twist`` = 
    "大原さんのオリジナル、時空転換 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "ancient";},[]); Wait "60 - 40 * $rank";
            Action.ActionRef ({actionRefLabel = "future";},[]); Wait "60 - 40 * $rank";
            Action.ActionRef ({actionRefLabel = "modern";},[]); Wait "60 - 40 * $rank";
            Action.ActionRef ({actionRefLabel = "medieval";},[]); Wait "60 - 40 * $rank";
            Action.ActionRef ({actionRefLabel = "primal";},[]); Wait "450"]);
        BulletmlElm.Action
          ({actionLabel = Some "ancient";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"1.5")),
               BulletRef ({bulletRefLabel = "ancientBit";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               Some (Speed (None,"1.5")),
               BulletRef ({bulletRefLabel = "ancientBit";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"0.0")),
               BulletRef ({bulletRefLabel = "ancientBit";},[]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "ancientBit";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "30"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "5";
                Repeat
                  (Times "40",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                          Some (Speed (None,"2.0")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "5"]));
                Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "modern";},
           [Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"-45 + 90 * $rand")),
                      None,BulletRef ({bulletRefLabel = "modernBit";},[])); Wait "6"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "modernBit";},None,Some (Speed (None,"0.5")),
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"160 + 40 * $rand"),
                   Term "90"); Wait "30";
                Repeat
                  (Times "5 + 5 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"-45 + 90 * $rand")),
                          None,BulletRef ({bulletRefLabel = "modernScore";},[]));
                       Wait "3"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "modernScore";},None,Some (Speed (None,"0.5")),
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"160 + 40 * $rand"),
                   Term "90");
                ChangeSpeed (Speed (None,"1.0 + 2.0 * $rank"),Term "300")])]);
        BulletmlElm.Action
          ({actionLabel = Some "future";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "futureBit";},[]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "futureBit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
           Some (Speed (None,"1.5")),
           [Action
              ({actionLabel = None;},
               [Wait "30";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                   None,BulletRef ({bulletRefLabel = "futureTriangle";},[]));
                Repeat
                  (Times "10",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
                          None,BulletRef ({bulletRefLabel = "futureTriangle";},[]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "futureTriangle";},None,Some (Speed (None,"1.1")),
           [Action
              ({actionLabel = None;},
               [Wait "30";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"120"),Term "1");
                Wait "5";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"150"),Term "60");
                Repeat
                  (Times "20",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"45")),
                          Some (Speed (None,"0.8")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-45")),
                          Some (Speed (None,"1.0")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-60")),
                          Some (Speed (None,"1.2")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "1"]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "medieval";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "medievalBit";},[]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "medievalBit";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
           Some (Speed (None,"1.5")),
           [Action
              ({actionLabel = None;},
               [Wait "60"; ChangeSpeed (Speed (None,"0.0"),Term "1"); Wait "5";
                Repeat
                  (Times "50",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                          None,BulletRef ({bulletRefLabel = "medievalStar";},["90"]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                          None,
                          BulletRef ({bulletRefLabel = "medievalStar";},["-90"]));
                       Wait "2"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "medievalStar";},None,Some (Speed (None,"1.1")),
           [Action
              ({actionLabel = None;},
               [Wait "60";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                   Some (Speed (None,"1.1")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "primal";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "primalBit";},[]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "primalBit";},
           Some (Direction (Some {directionType = Aim;},"0")),
           Some (Speed (None,"0.5")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"1.1"),Term "120");
                Repeat
                  (Times "24",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = Aim;},"0"),Term "5");
                       Wait "5"])); ChangeSpeed (Speed (None,"0.0"),Term "120");
                Repeat
                  (Times "24",
                   Action
                     ({actionLabel = None;},
                      [ChangeDirection
                         (Direction (Some {directionType = Aim;},"0"),Term "5");
                       Wait "5"])); Wait "60";
                Repeat
                  (Times "80 + 220 * $rank",
                   Action
                     ({actionLabel = None;},
                      [Action.ActionRef
                         ({actionRefLabel = "primalRock";},["180 * $rand"; "1"]);
                       Action.ActionRef
                         ({actionRefLabel = "primalRock";},["180 * $rand"; "-1"])]));
                Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "primalRock";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$1 * $2")),
               Some
                 (Speed
                    (None,
                     "
          (0.8 + 1.1*$1*(180-$1)/(90*90)) * (0.5+0.5*$rand) * (0.5+0.5*$rank)
        ")),
               Bullet ({bulletLabel = None;},None,None,[]))])])

  /// 大原さんのオリジナル、津波。by 白い弾幕くん
  /// [Original]_tsunami.xml
  let ``tsunami`` = 
    "大原さんのオリジナル、津波。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "80+$rank*120",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Absolute;},"360 * $rand")),
                      Some (Speed (None,"0.6 - 0.5 * $rank + 0.15 * $rand")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"60 + 240 * $rand")),
                      Some (Speed (None,"0.6 - 0.5 * $rank + 0.15 * $rand")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"120 + 120 * $rand")),
                      Some (Speed (None,"0.6 - 0.5 * $rank + 0.15 * $rand")),
                      Bullet ({bulletLabel = None;},None,None,[])); Wait "2"]));
            Wait "10 + 210 * $rank * $rank";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"70 + 40 * $rand")),
               Some (Speed (None,"0.6 - 0.3 * $rank")),
               BulletRef ({bulletRefLabel = "layer1";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"180")),
               Some (Speed (None,"0.6 - 0.3 * $rank")),
               BulletRef ({bulletRefLabel = "layer1";},[])); Wait "600"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "layer1";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "15";
                Repeat
                  (Times "100",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"90")),
                          Some (Speed (None,"0.8 - 0.4 * $rank")),
                          BulletRef ({bulletRefLabel = "layer2";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-90")),
                          Some (Speed (None,"0.8 - 0.4 * $rank")),
                          BulletRef ({bulletRefLabel = "layer2";},[])); Wait "60"]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "layer2";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "15";
                Repeat
                  (Times "100",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"45")),
                          Some (Speed (None,"1.0 - 0.5 * $rank")),
                          BulletRef ({bulletRefLabel = "layer3";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-45")),
                          Some (Speed (None,"1.0 - 0.5 * $rank")),
                          BulletRef ({bulletRefLabel = "layer3";},[])); Wait "60"]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "layer3";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "15";
                Repeat
                  (Times "100",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"30")),
                          Some (Speed (None,"0.2 + 0.3 * $rank * $rand")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Relative;},"-30")),
                          Some (Speed (None,"0.2 + 0.3 * $rank * $rand")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "60"]))])])])

  /// オリジナル。二つ十字。by 白い弾幕くん
  /// [Original]_two_cross.xml
  let ``two_cross`` = 
    "オリジナル。二つ十字。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "add3";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),None,
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "slow";},Some (Direction (None,"(50-$rank*20)*$1")),
           Some (Speed (None,"2")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "5";
                   Repeat
                     (Times "100",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"4*$1")),
                             None,Bullet ({bulletLabel = None;},None,None,[]));
                          Action.ActionRef ({actionRefLabel = "add3";},[]); Wait "4"]))])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "slow";},["1"]);
                   FireRef ({fireRefLabel = "slow";},["-1"]); Wait "80"]));
            Wait "60"])])    
    
  /// オリジナル。うねりを作る。by 白い弾幕くん
  /// [Original]_uneri.xml
  let ``uneri`` = 
    "オリジナル。うねりを作る。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "src";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"$1")),
               Some (Speed (None,"1.5")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "450/(4-$rank*2)",
               Action
                 ({actionLabel = None;},
                  [Wait "4.5-$rank*3+$rand";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$rand*10-5")),
                      Some (Speed (None,"1.5")),
                      Bullet ({bulletLabel = None;},None,None,[]))])); Vanish]);
        BulletmlElm.Fire
          ({fireLabel = Some "srcFire";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
           Some (Speed (None,"$1")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "5";
                   ChangeSpeed (Speed (None,"0"),Term "1");
                   Action.ActionRef ({actionRefLabel = "src";},["$2"])])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [FireRef ({fireRefLabel = "srcFire";},["0"; "0"]);
            FireRef ({fireRefLabel = "srcFire";},["4"; "10"]);
            FireRef ({fireRefLabel = "srcFire";},["8"; "20"]);
            FireRef ({fireRefLabel = "srcFire";},["-4"; "-10"]);
            FireRef ({fireRefLabel = "srcFire";},["-8"; "-20"]);
            Wait "500"])])    
        
  /// オリジナル。ワナ。by 白い弾幕くん
  /// [Original]_wana.xml
  let ``wana`` = 
    "オリジナル。ワナ。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
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
          ({actionLabel = Some "top2";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-10")),
               Some (Speed (None,"0")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[Vanish])]));
            Action.ActionRef
              ({actionRefLabel = "main";},
               ["20"; "5"; "1.10"; "-0.04"; "1.10"; "-0.04"]);
            Action.ActionRef
              ({actionRefLabel = "main";},
               ["15-$rank*6"; "5"; "1.06"; "0.04"; "1.06"; "0.04"]);
            Action.ActionRef
              ({actionRefLabel = "main";},
               ["5+$rank*20"; "5"; "1.10";"-0.04"; "1.10"; "-0.04"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"215")),
               Some (Speed (None,"0")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[Vanish])]));
            Action.ActionRef
              ({actionRefLabel = "main";},
               ["20"; "-5"; "1.10"; "-0.04"; "1.10"; "-0.04"]);
            Action.ActionRef
              ({actionRefLabel = "main";},
               ["15-$rank*6"; "-5"; "1.06"; "0.04"; "1.06"; "0.04"]);
            Action.ActionRef
              ({actionRefLabel = "main";},
               ["5+$rank*20"; "-5"; "1.10"; "-0.04"; "1.06"; "0.04"])]);
        BulletmlElm.Action
          ({actionLabel = Some "main";},
           [Repeat
              (Times "$1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"36+$2")),
                      Some (Speed (None,"$3")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWayFan";},
                      ["5"; "1"; "$4"]);
                   Repeat
                     (Times "4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"36")),
                             Some (Speed (None,"$3")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Action.ActionRef
                            ({actionRefLabel = "XWayFan";},
                             ["5"; "1"; "$4"])]));
                   Repeat
                     (Times "4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"36")),
                             Some (Speed (None,"$5")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Action.ActionRef
                            ({actionRefLabel = "XWayFan";},
                             ["5"; "1"; "$6"])])); Wait "15"]))])])

  /// オリジナル。横加速。by 白い弾幕くん
  /// [Original]_yokokasoku.xml
  let ``yokokasoku`` = 
    "オリジナル。横加速。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Fire
          ({fireLabel = Some "accelShot";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
           Some (Speed (None,"0")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Accel (Some (Horizontal (None,"3*$1")),None,Term "80")])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"$rand*90-45")),
                      Some (Speed (None,"1")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action
                            ({actionLabel = None;},
                             [Repeat
                                (Times "9999",
                                 Action
                                   ({actionLabel = None;},
                                    [Wait "$rand*20+30-$rank*20";
                                     FireRef ({fireRefLabel = "accelShot";},["1"]);
                                     FireRef ({fireRefLabel = "accelShot";},["-1"])]))])]));
                   Wait "10"])); Wait "120"])])

  /// オリジナル。雑魚で突撃。by 白い弾幕くん
  /// [Original]_zako_atack.xml
  let ``zako_atack`` = 
    "オリジナル。雑魚で突撃。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "40+$rank*20",
               Action
                 ({actionLabel = None;},
                  [Wait "4";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = Aim;},"$rand*180-90")),
                      Some (Speed (None,"1.5")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action
                            ({actionLabel = None;},
                             [Repeat
                                (Times "3",
                                 Action
                                   ({actionLabel = None;},
                                    [Fire
                                       ({fireLabel = None;},
                                        Some
                                          (Direction
                                             (Some {directionType = Aim;},"(0.5-$rand)*$rank*10")),
                                        Some
                                          (Speed (None,"1.5")),
                                        Bullet ({bulletLabel = None;},None,None,[]));
                                     Wait "20+$rand*$rank*40"]))])]))]))])])                        
                            
