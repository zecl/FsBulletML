namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Progear
[<RequireQualifiedAccess>]
module Progear =

  /// CAVEのプロギアの嵐、一面ボス。by 白い弾幕くん
  /// [Progear]_round_1_boss_grow_bullets.xml
  let ``round_1_boss_grow_bullets`` =
    "CAVEのプロギアの嵐、一面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "oogi";},
           [Fire
              ({fireLabel = None;},
               Some
                 (Direction
                    (Some {directionType = DirectionType.Absolute;},"270-(4+$rank*6)*15/2")),None,
               BulletRef ({bulletRefLabel = "seed";},[]));
            Repeat
              (Times "4+$rank*6",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),None,
                      BulletRef ({bulletRefLabel = "seed";},[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "oogi";},[]); Wait "40"]));
            Wait "40";
            Repeat
              (Times "8",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "oogi";},[]); Wait "20"]));
            Wait "30"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed";},None,Some (Speed (None,"1.5")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "60"); Wait "60";
                Fire
                  ({fireLabel = None;},None,Some (Speed (None,"0.75")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "4+$rank*4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},None,
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.3")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// CAVEのプロギアの嵐、二面ボス、発狂モード。by 白い弾幕くん
  /// [Progear]_round_2_boss_struggling.xml
  let ``round_2_boss_struggling`` =
    "CAVEのプロギアの嵐、二面ボス、発狂モード。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "1000",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"180")),None,
                      BulletRef ({bulletRefLabel = "changeStraight";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"159")),None,
                      BulletRef ({bulletRefLabel = "changeStraight";},[]));
                   Wait "1+(1-$rank)*3*$rand"])); Wait "180"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "changeStraight";},None,Some (Speed (None,"0.8")),
           [Action
              ({actionLabel = None;},
               [Wait "20+$rand*100";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"270"),Term "60");
                ChangeSpeed (Speed (None,"0"),Term "40"); Wait "40";
                ChangeSpeed (Speed (None,"0.5+$rand*0.7"),Term "20")])])])
    
  /// CAVEのプロギアの嵐、三面ボス。by 白い弾幕くん
  /// [Progear]_round_3_boss_back_burst.xml
  let ``round_3_boss_back_burst`` =
    "CAVEのプロギアの嵐、三面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "200",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Absolute;},"220+$rand*100")),None,
                      BulletRef ({bulletRefLabel = "backBurst";},[]));
                   Wait "4-$rank*2"])); Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "backBurst";},None,Some (Speed (None,"1.2")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "80"); Wait "60+$rand*20";
                Repeat
                  (Times "2",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"60+$rand*60")),
                          None,BulletRef ({bulletRefLabel = "downAccel";},[]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "downAccel";},None,Some (Speed (None,"1.8")),
           [Action
              ({actionLabel = None;},
               [Accel
                  (Some (Horizontal (Some {horizontalType = Relative;},"-7")),None,
                   Term "250")])])])

  /// CAVEのプロギアの嵐、三面ボス。by 白い弾幕くん
  /// [Progear]_round_3_boss_wave_bullets.xml
  let ``round_3_boss_wave_bullets`` =
    "CAVEのプロギアの嵐、三面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"310")),None,
                      BulletRef ({bulletRefLabel = "wave";},["-3"])); Wait "30";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"230")),None,
                      BulletRef ({bulletRefLabel = "wave";},["3"])); Wait "30"]));
            Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "wave";},None,Some (Speed (None,"1.5")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},Some (Direction (None,"0")),None,
                   BulletRef ({bulletRefLabel = "nrm";},[]));
                Repeat
                  (Times "12+$rank*12",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$1")),
                          None,BulletRef ({bulletRefLabel = "nrm";},[])); Wait "3"]));
                Vanish])]);
        BulletmlElm.Bullet ({bulletLabel = Some "nrm";},None,Some (Speed (None,"1")),[])])

  /// CAVEのプロギアの嵐、四面ボス。by 白い弾幕くん
  /// [Progear]_round_4_boss_fast_rocket.xml
  let ``round_4_boss_fast_rocket`` =
    "CAVEのプロギアの嵐、四面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "fireRoot";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
               Some (Speed (None,"0.2")),BulletRef ({bulletRefLabel = "rootBl";},[]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.5")),
                      BulletRef ({bulletRefLabel = "rootBl";},[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Action.ActionRef ({actionRefLabel = "fireRoot";},["$rand*16"]);
            Action.ActionRef ({actionRefLabel = "fireRoot";},["180+$rand*16"]); Wait "120"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "rootBl";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "40";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"274+$rand*4")),
                   None,BulletRef ({bulletRefLabel = "rocket";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "rocket";},None,Some (Speed (None,"5+$rand")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
                          Some (Speed (None,"1")),
                          BulletRef ({bulletRefLabel = "downAccel";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Absolute;},"60")),
                          Some (Speed (None,"1.8")),
                          BulletRef ({bulletRefLabel = "downAccel";},[]));
                       Wait "5-$rank*4"]))])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "downAccel";},None,None,
           [Action
              ({actionLabel = None;},
               [Accel (None,Some (Vertical (None,"2.7")),Term "120")])])])

  /// CAVEのプロギアの嵐、五面ボス。by 白い弾幕くん
  /// [Progear]_round_5_boss_last_round_wave.xml
  let ``round_5_boss_last_round_wave`` =
    "CAVEのプロギアの嵐、五面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "2+$rank*1.5",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},None,None,
                             BulletRef ({bulletRefLabel = "rfRkt";},[])); Wait "45"]));
                   Wait "100"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "rfRkt";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [Wait "2";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                          None,Bullet ({bulletLabel = None;},None,None,[]))]))])])])

  /// CAVEのプロギアの嵐、五面ボス。by 白い弾幕くん
  /// [Progear]_round_5_middle_boss_rockets.xml
  let ``round_5_middle_boss_rockets`` =
    "CAVEのプロギアの嵐、五面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "50",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),None,
                      BulletRef ({bulletRefLabel = "rocket";},[])); Wait "10"]));
            Wait "120"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "rocket";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [FireRef ({fireRefLabel = "udBlt";},["90"]); Wait "20-$rank*8";
                       FireRef ({fireRefLabel = "udBlt";},["-90"]);
                       Wait "$rand*10+15-$rank*8"]))])]);
        BulletmlElm.Fire
          ({fireLabel = Some "udBlt";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"$1-25+$rand*50")),None,
           Bullet ({bulletLabel = None;},None,None,[]))])

  /// CAVEのプロギアの嵐、二周目一面ボス(嘘) by 白い弾幕くん
  /// [Progear]_round_6_boss_parabola_shot.xml
  let ``round_6_boss_parabola_shot`` =
    "CAVEのプロギアの嵐、二周目一面ボス(嘘) by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletHorizontal;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "25",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Absolute;},"190+$rand*30")),
                      None,BulletRef ({bulletRefLabel = "seed";},["1"]));
                   Wait "15-$rank*5";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Absolute;},"350-$rand*30")),
                      None,BulletRef ({bulletRefLabel = "seed";},["-1"]));
                   Wait "15-$rank*5"])); Wait "60"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed";},None,Some (Speed (None,"1")),
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "60"); Wait "60";
                Fire
                  ({fireLabel = None;},None,None,
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some
                     (Direction
                        (Some {directionType = DirectionType.Absolute;},"270+30*$1+$rand*50*$1")),
                   None,BulletRef ({bulletRefLabel = "downAccel";},["$1"]));
                Repeat
                  (Times "3",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"-0.4")),
                          BulletRef ({bulletRefLabel = "downAccel";},["$1"]))]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "downAccel";},None,Some (Speed (None,"2.5")),
           [Action
              ({actionLabel = None;},
               [Accel (None,Some (Vertical (None,"4*$1")),Term "120")])])])

  /// CAVEのプロギアの嵐、二周目四面ボス。by 白い弾幕くん
  /// [Progear]_round_9_boss.xml
  let ``round_9_boss`` =
    "CAVEのプロギアの嵐、二周目四面ボス。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "accel";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeSpeed
                  (Speed (Some {speedType = SpeedType.Sequence;},"0.03"),Term "9999");
                Wait "9999"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"80")),None,
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[Wait "20"; Vanish])]));
            Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"40")),
                      Some (Speed (None,"5")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action
                            ({actionLabel = None;},
                             [Repeat
                                (Times "9999",
                                 Action
                                   ({actionLabel = None;},
                                    [Fire
                                       ({fireLabel = None;},
                                        Some
                                          (Direction
                                             (Some {directionType = DirectionType.Absolute;},"0")),
                                        Some (Speed (None,"0.5")),
                                        BulletRef ({bulletRefLabel = "accel";},[]));
                                     Fire
                                       ({fireLabel = None;},
                                        Some
                                          (Direction
                                             (Some {directionType = DirectionType.Absolute;},"180")),
                                        Some (Speed (None,"0.5")),
                                        BulletRef ({bulletRefLabel = "accel";},[]));
                                     Wait "4-$rank*2+$rand"]))])]))])); Wait "120"])])

  /// CAVEのプロギアの嵐、ラスボスの雰囲気。by 白い弾幕くん
  /// [Progear]_round_10_boss_before_final.xml
  let ``round_10_boss_before_final`` =
    "CAVEのプロギアの嵐、ラスボスの雰囲気。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Fire
          ({fireLabel = Some "rollOut";},
           Some (Direction (Some {directionType = DirectionType.Relative;},"90")),
           Some (Speed (None,"0.0001")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "350"; ChangeSpeed (Speed (None,"1"),Term "100");
                   ChangeDirection
                     (Direction (Some {directionType = DirectionType.Relative;},"50-$rank*40"),
                      Term "100"); Wait "1000"])]));
        BulletmlElm.Bullet
          ({bulletLabel = Some "setter";},None,Some (Speed (None,"3")),
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "999",
                   Action
                     ({actionLabel = None;},
                      [Wait "5"; FireRef ({fireRefLabel = "rollOut";},[])]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$rand*10")),None,
               BulletRef ({bulletRefLabel = "setter";},[]));
            Repeat
              (Times "45/(2-$rank)",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Sequence;},"16-$rank*8")),
                      None,BulletRef ({bulletRefLabel = "setter";},[])); Wait "1"]));
            Wait "40";
            Repeat
              (Times "125+$rank*125",
               Action
                 ({actionLabel = None;},
                  [Wait "1.5-$rank/2+$rand";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"45-$rand*90")),
                      Some (Speed (None,"1.2")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Wait "80"; ChangeSpeed (Speed (None,"0.7"),Term "1");
            ChangeDirection (Direction (Some {directionType = Aim;},"0"),Term "1");
            Wait "1";
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Sequence;},"1.44444"),Term "250");
            Wait "250"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "20";
            ChangeSpeed (Speed (None,"0.7"),Term "1");
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Sequence;},"30"),Term "12");
            Wait "12"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "200-$rank*60"])])
