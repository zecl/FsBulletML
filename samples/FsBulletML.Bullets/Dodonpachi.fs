namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Dodonpachi
[<RequireQualifiedAccess>]
module Dodonpachi =

  /// 怒首領蜂、火蜂。by 白い弾幕くん
  /// [Dodonpachi]_hibachi.xml
  let hibachi =
    createBulletmlInfo "怒首領蜂、火蜂。by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "allWay";},
           [Fire
              ({fireLabel = None;},Some (Direction (None,"-50+$rand*20")),
               Some (Speed (None,"1+$rank")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "15+16*$rank*$rank",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Sequence;},"24-$rank*12")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "right";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"90"),Term "1");
            ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"1"),Term "1");
            Repeat
              (Times "25",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "allWay";},[]); Wait "3"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "left";},
           [ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"-90"),Term "1");
            ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"1"),Term "1");
            Repeat
              (Times "25",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "allWay";},[]); Wait "3"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Action.ActionRef ({actionRefLabel = "right";},[]);
                   Action.ActionRef ({actionRefLabel = "left";},[]);
                   Action.ActionRef ({actionRefLabel = "left";},[]);
                   Action.ActionRef ({actionRefLabel = "right";},[])]));
            ChangeSpeed (Speed (None,"0"),Term "1"); Wait "1"])])
  
  /// 怒首領蜂、最終鬼畜兵器その一。 by 白い弾幕くん
  /// [Dodonpachi]_kitiku_1.xml
  let kitiku_1 =
    createBulletmlInfo "怒首領蜂、最終鬼畜兵器その一。 by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "fast";},None,Some (Speed (None,"10")),
           [Action
              ({actionLabel = None;},
               [Wait "6"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "20";
                Repeat
                  (Times "10+$rank*18",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"-11-$rand*2")),
                          Some (Speed (None,"1.5")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef ({actionRefLabel = "add3";},[]);
                       Repeat
                         (Times "4",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some
                                   (Speed
                                      (Some {speedType = SpeedType.Sequence;},"0.1+$rank*0.2")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Action.ActionRef ({actionRefLabel = "add3";},[])]));
                       Wait "336/(10+$rank*18)"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "add3";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "slowColorChange";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180+45*$1")),
           Some (Speed (None,"7")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "6"; ChangeSpeed (Speed (None,"0"),Term "1");
                   Repeat
                     (Times "50+$rank*50",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"(8-$rank*4)*$1")),
                             Some (Speed (None,"1.2")),
                             Bullet ({bulletLabel = None;},None,None,[]));
                          Action.ActionRef ({actionRefLabel = "add3";},[]);
                          Wait "8-$rank*4+$rand"])); Vanish])]));
        BulletmlElm.Fire
          ({fireLabel = Some "slow";},None,None,
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "slowColorChange";},["$1"]); Vanish])]));
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-85")),None,
               BulletRef ({bulletRefLabel = "fast";},[])); Wait "1";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"85")),None,
               BulletRef ({bulletRefLabel = "fast";},[])); Wait "1";
            FireRef ({fireRefLabel = "slow";},["1"]); Wait "1";
            FireRef ({fireRefLabel = "slow";},["-1"]); Wait "430"])])

  /// 怒首領蜂、最終鬼畜兵器その二。 by 白い弾幕くん
  /// [Dodonpachi]_kitiku_2.xml
  let kitiku_2 =
    createBulletmlInfo "怒首領蜂、最終鬼畜兵器その二。 by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "feather";},None,Some (Speed (None,"4")),
           [Action
              ({actionLabel = None;},
               [Wait "6";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),None,
                   Bullet
                     ({bulletLabel = None;},None,None,
                      [Action ({actionLabel = None;},[Vanish])]));
                ChangeSpeed (Speed (None,"0"),Term "1"); Wait "1";
                Repeat
                  (Times "100",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"10-(3+$rank*6)*3")),
                          Some (Speed (None,"1")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "3+$rank*6",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction (Some {directionType = DirectionType.Sequence;},"3")),
                                 Some (Speed (Some {speedType = SpeedType.Sequence;},"0.15")),

                                 Bullet ({bulletLabel = None;},None,None,[]))]));
                       Wait "4"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),None,
               BulletRef ({bulletRefLabel = "feather";},[])); Wait "1";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
               BulletRef ({bulletRefLabel = "feather";},[])); Wait "430"])])

  /// 怒首領蜂、最終鬼畜兵器その三。 by 白い弾幕くん
  /// [Dodonpachi]_kitiku_3.xml
  let kitiku_3 =
    createBulletmlInfo "怒首領蜂、最終鬼畜兵器その三。 by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Repeat
              (Times "200+$rank*200",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"-50+$rand*100")),
                      Some (Speed (None,"1.6")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Wait "2-$rank+$rand"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "kobati";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "$1";
                Repeat
                  (Times "20",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = Aim;},"0")),
                          Some (Speed (None,"1.6")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "(16-$rank*8)*3"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Repeat
              (Times "8+$rank*8",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"80")),
                      Some (Speed (None,"1.5")),
                      BulletRef ({bulletRefLabel = "kobati";},["(16-$rank*8)*3"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"-80")),
                      Some (Speed (None,"1.5")),
                      BulletRef ({bulletRefLabel = "kobati";},["(16+$rank*8)*3"]));
                   Wait "16-$rank*8";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"80")),
                      Some (Speed (None,"1.5")),
                      BulletRef ({bulletRefLabel = "kobati";},["(16-$rank*8)*2"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"-80")),
                      Some (Speed (None,"1.5")),
                      BulletRef ({bulletRefLabel = "kobati";},["(16-$rank*8)*2"]));
                   Wait "16-$rank*8";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"80")),
                      Some (Speed (None,"1.5")),
                      BulletRef ({bulletRefLabel = "kobati";},["16-$rank*8"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"-80")),
                      Some (Speed (None,"1.5")),
                      BulletRef ({bulletRefLabel = "kobati";},["16-$rank*8"]));
                   Wait "16-$rank*8"])); Wait "120"])])

  /// 怒首領蜂、最終鬼畜兵器その五。by 白い弾幕くん
  /// [Dodonpachi]_kitiku_5.xml
  let kitiku_5 =
    createBulletmlInfo "怒首領蜂、最終鬼畜兵器その五。by 白い弾幕くん" <|
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Repeat
              (Times "30+$rank*30",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction (Some {directionType = DirectionType.Sequence;},"220+$rand*2")),
                      Some (Speed (None,"1.2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                      Some (Speed (None,"1.2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"120")),
                      Some (Speed (None,"1.2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                      Some (Speed (None,"1.2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Wait "20-$rank*10"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Repeat
              (Times "30+$rank*30",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"-30+$rand*60")),
                      Some (Speed (None,"1.3")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action ({actionLabel = None;},[])])); Wait "20-$rank*10"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "kobati";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "5";
                ChangeDirection
                  (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
                ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"1.2"),Term "1");
                Wait "1";
                Repeat
                  (Times "9999",
                   Action
                     ({actionLabel = None;},
                      [Wait "30-$rank*10";
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"45")),
                          Some (Speed (None,"0.4+$rank*0.2")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"90")),
                                 Some (Speed (None,"0.4+$rank*0.2")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]))]))])]);
        BulletmlElm.Action
          ({actionLabel = Some "top3";},
           [Repeat
              (Times "5+$rank*5",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
                      Some (Speed (None,"10")),
                      BulletRef ({bulletRefLabel = "kobati";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
                      Some (Speed (None,"5")),
                      BulletRef ({bulletRefLabel = "kobati";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
                      Some (Speed (None,"10")),
                      BulletRef ({bulletRefLabel = "kobati";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
                      Some (Speed (None,"5")),
                      BulletRef ({bulletRefLabel = "kobati";},[]));
                   Wait "120-$rank*60"])); Wait "120"])])
