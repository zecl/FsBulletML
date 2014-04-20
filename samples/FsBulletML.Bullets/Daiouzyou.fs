namespace FsBulletML.Bullets.EnemyBullet.Sdmkun
open FsBulletML

/// 白い弾幕くんより
/// Daiouzyou
[<RequireQualifiedAccess>]
module Daiouzyou =

  /// 怒首領蜂大往生「緋蜂」開幕攻撃 by 白い弾幕くん
  /// [Daiouzyou]_hibachi_1.xml
  let hibachi_1 =
    "怒首領蜂大往生「緋蜂」開幕攻撃 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "10+$rank*70",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = Aim;},"$rand*30-74+$rank*2")),
                      Some (Speed (None,"0.5+$rank*2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]);
                   FireRef ({fireRefLabel = "n";},[]); Wait "14-$rank*10"]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "n";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"$rand*2+7-$rank*2")),
           Some (Speed (None,"0.5+$rank*2")),
           Bullet ({bulletLabel = None;},None,None,[]))])

  /// 怒首領蜂大往生「緋蜂」超速青弾 by 白い弾幕くん
  /// [Daiouzyou]_hibachi_2.xml
  let hibachi_2 =
    "怒首領蜂大往生「緋蜂」超速青弾 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"175")),
               Some (Speed (None,"1+$rank*4")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "30",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]); Wait "1";
                   Action.ActionRef ({actionRefLabel = "rights";},[]);
                   Action.ActionRef ({actionRefLabel = "rights";},[]);
                   Action.ActionRef ({actionRefLabel = "rights";},[]);
                   Action.ActionRef ({actionRefLabel = "rights";},[]);
                   Action.ActionRef ({actionRefLabel = "rights";},[]);
                   Action.ActionRef ({actionRefLabel = "rights";},[]);
                   Action.ActionRef ({actionRefLabel = "rights";},[]);
                   Action.ActionRef ({actionRefLabel = "rights";},[]); Wait "15-$rank*10";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"4")),
                      Some (Speed (None,"1+$rank*4")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "tops";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"185")),
               Some (Speed (None,"1+$rank*4")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "30",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]);
                   FireRef ({fireRefLabel = "allway";},[]); Wait "1";
                   Action.ActionRef ({actionRefLabel = "lefts";},[]);
                   Action.ActionRef ({actionRefLabel = "lefts";},[]);
                   Action.ActionRef ({actionRefLabel = "lefts";},[]);
                   Action.ActionRef ({actionRefLabel = "lefts";},[]);
                   Action.ActionRef ({actionRefLabel = "lefts";},[]);
                   Action.ActionRef ({actionRefLabel = "lefts";},[]);
                   Action.ActionRef ({actionRefLabel = "lefts";},[]);
                   Action.ActionRef ({actionRefLabel = "lefts";},[]); Wait "15-$rank*10";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"-4")),
                      Some (Speed (None,"1+$rank*4")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "lefts";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"-0.7")),
               Some (Speed (None,"1+$rank*4")),
               Bullet ({bulletLabel = None;},None,None,[]));
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]); Wait "1"]);
        BulletmlElm.Action
          ({actionLabel = Some "rights";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"0.7")),
               Some (Speed (None,"1+$rank*4")),
               Bullet ({bulletLabel = None;},None,None,[]));
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]);
            FireRef ({fireRefLabel = "allway";},[]); Wait "1"]);
        BulletmlElm.Fire
          ({fireLabel = Some "allway";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"45")),
           Some (Speed (None,"1+$rank*4")),
           Bullet ({bulletLabel = None;},None,None,[]))])

  /// 怒首領蜂大往生「緋蜂」第三攻撃 by 白い弾幕くん
  /// [Daiouzyou]_hibachi_3.xml
  let hibachi_3 =
    "怒首領蜂大往生「緋蜂」第三攻撃 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"2"),Term "1");
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
            Wait "10";
            ChangeSpeed (Speed (Some {speedType = SpeedType.Absolute;},"0"),Term "1");
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"1.5")),BulletRef ({bulletRefLabel = "blue";},[]));

            Repeat
              (Times "60+$rank*60",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]);
                   FireRef ({fireRefLabel = "red";},[]); Wait "20-$rank*14";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"-31.5")),
                      Some (Speed (None,"1.5")),
                      BulletRef ({bulletRefLabel = "blue";},[]))]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "red";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"30")),
           Some (Speed (None,"1.5")),BulletRef ({bulletRefLabel = "blue";},[]));
        BulletmlElm.Bullet
          ({bulletLabel = Some "blue";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "30";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"-110")),
                   Some (Speed (None,"1.5")),
                   Bullet ({bulletLabel = None;},None,None,[]))])])])

  /// 怒首領蜂大往生「緋蜂」発狂攻撃 by 白い弾幕くん
  /// [Daiouzyou]_hibachi_4.xml
  let hibachi_4 =
    "怒首領蜂大往生「緋蜂」発狂攻撃 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = None;
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"45")),
               Some (Speed (None,"1+$rank*0.5")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "113+900/(16-$rank*10)",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "four";},[]);
                   FireRef ({fireRefLabel = "four";},[]);
                   FireRef ({fireRefLabel = "four";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"86")),
                      Some (Speed (None,"1+$rank*0.5")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Wait "16-$rank*10"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "tops";},
           [Wait "(16-$rank*10)*22.5";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"45")),
               Some (Speed (None,"1+$rank*0.5")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "91+900/(16-$rank*10)",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "four";},[]);
                   FireRef ({fireRefLabel = "four";},[]);
                   FireRef ({fireRefLabel = "four";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"94")),
                      Some (Speed (None,"1+$rank*0.5")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Wait "16-$rank*10"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "topt";},
           [Wait "(16-$rank*10)*45";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "gurugurup";},[]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "gurugurup";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeSpeed (Speed (None,"0"),Term "1");
                Action.ActionRef ({actionRefLabel = "guru2";},[]); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "guru2";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"0")),
               Some (Speed (None,"1+$rank")),
               BulletRef ({bulletRefLabel = "guruc";},[]));
            Repeat
              (Times "450/(16-$rank*10)",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   FireRef ({fireRefLabel = "guru";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"20.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   FireRef ({fireRefLabel = "guru2";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"20.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc2";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   FireRef ({fireRefLabel = "guru3";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"20.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc3";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   FireRef ({fireRefLabel = "guru4";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"20.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc4";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   FireRef ({fireRefLabel = "guru5";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"20.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc5";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   FireRef ({fireRefLabel = "guru6";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"20.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc6";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   FireRef ({fireRefLabel = "guru7";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"21")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc7";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "6",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   FireRef ({fireRefLabel = "guru8";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"21.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc8";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "7",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   FireRef ({fireRefLabel = "guru9";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"22")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc9";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   FireRef ({fireRefLabel = "guru10";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"22.7")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc10";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "7",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   FireRef ({fireRefLabel = "guru11";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"23")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc11";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "6",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   FireRef ({fireRefLabel = "guru12";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"23.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc12";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   FireRef ({fireRefLabel = "guru13";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"24")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc13";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   FireRef ({fireRefLabel = "guru14";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"24.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc14";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   FireRef ({fireRefLabel = "guru15";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"24.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc15";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   FireRef ({fireRefLabel = "guru16";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"24.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc16";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   FireRef ({fireRefLabel = "guru17";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"24.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc17";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   FireRef ({fireRefLabel = "guru18";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"24.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc18";},[]));
                   Wait "16-$rank*10"]));
            Repeat
              (Times "450/(16-$rank*10)",
               Action
                 ({actionLabel = None;},
                  [FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   FireRef ({fireRefLabel = "guru19";},[]);
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"24.5")),
                      Some (Speed (None,"1+$rank")),
                      BulletRef ({bulletRefLabel = "guruc19";},[]));
                   Wait "16-$rank*10"]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "guru";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru2";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc2";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru3";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc3";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru4";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc4";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru5";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc5";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru6";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc6";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru7";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc7";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru8";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc8";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru9";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),BulletRef ({bulletRefLabel = "guruc9";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru10";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.3+$rand*0.4")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc10";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru11";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc11";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru12";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc12";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru13";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc13";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru14";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc14";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru15";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc15";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru16";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc16";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru17";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc17";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru18";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc18";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "guru19";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
           Some (Speed (None,"1+$rank")),
           BulletRef ({bulletRefLabel = "guruc19";},[]));
        BulletmlElm.Fire
          ({fireLabel = Some "four";},
           Some (Direction (Some {directionType = DirectionType.Sequence;},"90")),
           Some (Speed (None,"1+$rank*0.5")),
           Bullet ({bulletLabel = None;},None,None,[]));
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "90")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc2";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "170")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc3";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "260")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc4";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "300")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc5";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "450")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc6";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "600")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc7";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "700")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc8";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "800")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc9";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"-270"),Term "900")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc10";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"0"),Term "90")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc11";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "900")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc12";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "800")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc13";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "700")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc14";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "600")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc15";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "450")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc16";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "300")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc17";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "280")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc18";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "230")])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "guruc19";},None,None,
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Relative;},"270"),Term "90")])])])

  /// 怒首領蜂大往生「緋蜂」最終形態を妄想してみた by 白い弾幕くん
  /// [Daiouzyou]_hibachi_image.xml
  let hibachi_image = 
    "怒首領蜂大往生「緋蜂」最終形態を妄想してみた by 白い弾幕くん",
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
          ({actionLabel = Some "spiral";},
           [Fire
              ({fireLabel = None;},None,Some (Speed (None,"0")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action
                     ({actionLabel = None;},
                      [Repeat
                         (Times "30+$rank*45",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"$1")),
                                 Some (Speed (None,"1+$rank*2")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Action.ActionRef
                                ({actionRefLabel = "XWay";},
                                 ["10"; "36"]);
                              Wait "6-$rank*3"])); Vanish])]));
            Wait "225"]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Action.ActionRef ({actionRefLabel = "spiral";},["7"]);
            Action.ActionRef ({actionRefLabel = "spiral";},["-7"])]);
        BulletmlElm.Action
          ({actionLabel = Some "fan4";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-$1")),
               None,BulletRef ({bulletRefLabel = "Dummy";},[]));
            Repeat
              (Times "60+$rank*90",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$1")),
                      Some (Speed (None,"1+$rank*2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWay";},
                      ["4"; "90"]);
                   Wait "6-$rank*3"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Action.ActionRef ({actionRefLabel = "fan4";},["4"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top3";},
           [Action.ActionRef ({actionRefLabel = "fan4";},["-4"])])])

  /// 怒首領蜂大往生「緋蜂」最終形態に多分似たもの by 白い弾幕くん
  /// [Daiouzyou]_hibachi_maybe.xml
  let hibachi_maybe =
    "怒首領蜂大往生「緋蜂」最終形態に多分似たもの by 白い弾幕くん",
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
           [Repeat
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
                                  (Some {directionType = DirectionType.Sequence;},"$1")),
                             Some
                               (Speed (None,"1.5+$rank*$rank*1.5")),
                             BulletRef
                               ({bulletRefLabel = "curve";},
                                ["$1"]));
                          Repeat
                            (Times "10+$rank*10-1",
                             Action
                               ({actionLabel = None;},
                                [Fire
                                   ({fireLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Sequence;},"36/($rank+1)")),
                                    Some
                                      (Speed
                                         (Some {speedType = SpeedType.Sequence;},"0")),
                                    BulletRef
                                      ({bulletRefLabel = "curve";},
                                       ["$1"]))]));
                          Wait "6-$rank*3"]));
                   Wait "6-$rank*3";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$1")),
                      Some (Speed (None,"1+$rank*2")),
                      BulletRef ({bulletRefLabel = "Dummy";},[]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Action.ActionRef ({actionRefLabel = "spiral";},["-2"])]);
        BulletmlElm.Action
          ({actionLabel = Some "fan4";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-$1")),
               None,BulletRef ({bulletRefLabel = "Dummy";},[]));
            Repeat
              (Times "30+$rank*45",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$1")),
                      Some (Speed (None,"1+$rank*$rank*2")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWay";},
                      ["4"; "90"]);
                   Wait "6-$rank*3"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Action.ActionRef ({actionRefLabel = "fan4";},["4"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top3";},
           [Action.ActionRef ({actionRefLabel = "fan4";},["-4"])])])

  /// 怒首領蜂大往生一面ボス by 白い弾幕くん
  /// [Daiouzyou]_round_1_boss.xml
  let round_1_boss = 
    "怒首領蜂大往生一面ボス by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = None;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                      Some (Speed (None,"4")),
                      BulletRef ({bulletRefLabel = "seed";},[])); Wait "500"]));
            Wait "100"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "9";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),None,
                   BulletRef ({bulletRefLabel = "seed2";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"180")),None,
                   BulletRef ({bulletRefLabel = "seed2";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed2";},None,Some (Speed (None,"18")),
           [Action
              ({actionLabel = None;},
               [Wait "1";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"90")),None,
                   BulletRef ({bulletRefLabel = "seed3";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed3";},None,Some (Speed (None,"0.8")),
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Sequence;},"1.2"),Term "9999");
                Repeat
                  (Times "100+200*$rank",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"180-12")),

                          None,Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"180")),

                          None,Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "3-$rank*2*$rand"]))]);
            Action
              ({actionLabel = None;},
               [Repeat
                  (Times "6",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},None,None,
                          Bullet
                            ({bulletLabel = None;},Some (Direction (None,"-8")),None
    ,
                             []));
                       Repeat
                         (Times "4",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},None,None,
                                 Bullet
                                   ({bulletLabel = None;},
                                    Some
                                      (Direction
                                         (Some {directionType = DirectionType.Sequence;},"4")),
                                    None,[Action ({actionLabel = None;},[])]))]));
                       Wait "80"])); Vanish])])])

  /// 怒首領蜂大往生一面ボス、発狂。by 白い弾幕くん
  /// [Daiouzyou]_round_1_boss_hakkyou.xml
  let round_1_boss_hakkyou =
    "怒首領蜂大往生一面ボス、発狂。by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Repeat
              (Times "128",
               Action
                 ({actionLabel = None;},
                  [Wait "4"; Action.ActionRef ({actionRefLabel = "four";},["$rand*90+135"])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "four";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"6")),BulletRef ({bulletRefLabel = "rb";},["$1"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"60")),
                      Some (Speed (None,"6")),
                      BulletRef ({bulletRefLabel = "rb";},["$1"]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "rb";},None,None,
           [ActionRef ({actionRefLabel = "red";},["$1+$rand*20-10"])]);
        BulletmlElm.Action
          ({actionLabel = Some "red";},
           [Wait "1";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
               Some (Speed (None,"1+$rank")),
               Bullet ({bulletLabel = None;},None,None,[])); Vanish]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Wait "160";
                   Fire
                     ({fireLabel = None;},None,None,
                      BulletRef ({bulletRefLabel = "sht";},["1.2"])); Wait "80"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "sht";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "16",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},Some (Direction (None,"$rand*16-8")),
                          Some (Speed (None,"($1+$rand*$1)*($rank/2+0.65)")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top3";},
           [Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
                      BulletRef ({bulletRefLabel = "rd_seed";},["-5"; "-5"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),None,
                      BulletRef ({bulletRefLabel = "rd_seed";},["5"; "5"]));
                   Wait "240"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "rd_seed";},None,Some (Speed (None,"3")),
           [Action
              ({actionLabel = None;},
               [Wait "1";
                Fire
                  ({fireLabel = None;},None,Some (Speed (None,"0")),
                   BulletRef ({bulletRefLabel = "rd_seed2";},[]));
                Fire
                  ({fireLabel = None;},None,Some (Speed (None,"0")),
                   BulletRef ({bulletRefLabel = "bd_seed";},["0"; "$2"]));
                Fire
                  ({fireLabel = None;},None,Some (Speed (None,"0")),
                   BulletRef ({bulletRefLabel = "bd_seed";},["$1"; "$2"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "rd_seed2";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "5",
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
                                      (Some {directionType = DirectionType.Absolute;},"180")),
                                 Some (Speed (None,"1.2")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Wait "4"])); Wait "12"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bd_seed";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},Some (Direction (None,"$2")),
                   Some (Speed (None,"0.6")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "11",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"$1")),
                          Some (Speed (Some {speedType = SpeedType.Sequence;},"0.2")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "4"]));
                Vanish])])])

  /// 怒首領蜂大往生三面ボス「厳武」第二形態 by 白い弾幕くん
  /// [Daiouzyou]_round_3_boss.xml
  let round_3_boss =
    "怒首領蜂大往生三面ボス「厳武」第二形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"155")),
               Some (Speed (None,"3.3")),
               BulletRef ({bulletRefLabel = "roll";},["1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"205")),
               Some (Speed (None,"3.3")),
               BulletRef ({bulletRefLabel = "roll";},["-1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"135")),
               Some (Speed (None,"3.2")),
               BulletRef ({bulletRefLabel = "roll";},["1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"225")),
               Some (Speed (None,"3.2")),
               BulletRef ({bulletRefLabel = "roll";},["-1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"135")),
               Some (Speed (None,"2")),BulletRef ({bulletRefLabel = "roll";},["1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"225")),
               Some (Speed (None,"2")),BulletRef ({bulletRefLabel = "roll";},["-1"]));
            Wait "400"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "roll";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "12"; ChangeSpeed (Speed (None,"0"),Term "1");
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"180+90*$1")),
                   None,
                   Bullet
                     ({bulletLabel = None;},None,None,
                      [Action ({actionLabel = None;},[Vanish])]));
                Repeat
                  (Times "200",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"9")),
                          Some (Speed (None,"1+$rank")),
                          Bullet ({bulletLabel = None;},None,None,[])); Wait "2"]));
                Vanish])])])

  /// 怒首領蜂大往生三面ボス「厳武」第三形態 by 白い弾幕くん
  /// [Daiouzyou]_round_3_boss_2.xml
  let round_3_boss_2 =
    "怒首領蜂大往生三面ボス「厳武」第三形態 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "Red";},None,None,[Action ({actionLabel = None;},[])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
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
          ({actionLabel = Some "top1";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               None,BulletRef ({bulletRefLabel = "aim2";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               None,BulletRef ({bulletRefLabel = "aim2";},[]));
            Fire
              ({fireLabel = None;},None,Some (Speed (None,"0")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [ActionRef ({actionRefLabel = "fanRoll";},["7"])]));
            Fire
              ({fireLabel = None;},None,Some (Speed (None,"0")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [ActionRef ({actionRefLabel = "fanRoll";},["-7"])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Action.ActionRef ({actionRefLabel = "3wayRoll";},["13"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top3";},
           [Action.ActionRef ({actionRefLabel = "3wayRoll";},["-13"])]);
        BulletmlElm.Action
          ({actionLabel = Some "3wayRoll";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
               None,BulletRef ({bulletRefLabel = "Dummy";},[]));
            Repeat
              (Times "14",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"-1.3*$1")),
                      Some (Speed (None,"1.4+$rank*0.8")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWay";},
                      ["3"; "$1"]);
                   Wait "10"]));
            Repeat
              (Times "20",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"1.3*$1")),
                      Some (Speed (None,"1.4+$rank*0.8")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWay";},
                      ["3"; "-$1"]);
                   Wait "10"]))]);
        BulletmlElm.Action
          ({actionLabel = Some "fanRoll";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$1*8")),
               None,BulletRef ({bulletRefLabel = "Dummy";},[]));
            Repeat
              (Times "32",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"-$1*2.1")),
                      Some (Speed (None,"1.2+$rank*0.4")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWayFan";},
                      ["4"; "$1"; "0.3"]);
                   Wait "10"])); Vanish]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aim2";},None,Some (Speed (None,"1")),
           [Action
              ({actionLabel = None;},
               [Wait "8"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "14+$rank*12",
                   Action
                     ({actionLabel = None;},
                      [Wait "320/(14+$rank*12)+$rand";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"0")),
                          Some (Speed (None,"1.4+$rank*0.8")),
                          BulletRef ({bulletRefLabel = "Red";},[]))])); Vanish])])])

  /// 怒首領蜂大往生三面ボス「厳武」発狂 by 白い弾幕くん
  /// [Daiouzyou]_round_3_boss_last.xml
  let round_3_boss_last =
    "怒首領蜂大往生三面ボス「厳武」発狂 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"100")),
               Some (Speed (None,"3")),
               BulletRef ({bulletRefLabel = "armSrc";},["1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-100")),
               Some (Speed (None,"3")),
               BulletRef ({bulletRefLabel = "armSrc";},["0"]));
            FireRef ({fireRefLabel = "center";},[]); Wait "500"]);
        BulletmlElm.Action
          ({actionLabel = Some "center3";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-10.5*$1")),None,
               BulletRef ({bulletRefLabel = "Dummy";},[]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "6",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"$1")),
                             Some (Speed (None,"1+$rank")),
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
                                    Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                                    Bullet ({bulletLabel = None;},None,None,[]))]));
                          Wait "5"]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"$1")),None,
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action ({actionLabel = None;},[Vanish])])); Wait "5"]))]);
        BulletmlElm.Fire
          ({fireLabel = Some "center";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
           Some (Speed (None,"5")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Wait "10"; ChangeSpeed (Speed (None,"0"),Term "1");
                   Repeat
                     (Times "2",
                      Action
                        ({actionLabel = None;},
                         [Action.ActionRef ({actionRefLabel = "center3";},["-4"]);
                          Wait "30"; Action.ActionRef ({actionRefLabel = "center3";},["4"]);
                          Wait "30"])); Vanish])]));
        BulletmlElm.Bullet
          ({bulletLabel = Some "armSrc";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "12"; ChangeSpeed (Speed (None,"0"),Term "1"); Wait "1";
                FireRef ({fireRefLabel = "arm";},["8-16*$1"; "0"]); Wait "2";
                FireRef ({fireRefLabel = "arm";},["8-16*$1"; "90"]); Wait "2";
                FireRef ({fireRefLabel = "arm";},["8-16*$1"; "180"]); Wait "2";
                FireRef ({fireRefLabel = "arm";},["8-16*$1"; "270"]); Vanish])]);
        BulletmlElm.Fire
          ({fireLabel = Some "arm";},None,Some (Speed (None,"0")),
           Bullet
             ({bulletLabel = None;},None,None,
              [Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"$2")),
                      Some (Speed (None,"1.5")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "80+$rank*80",
                      Action
                        ({actionLabel = None;},
                         [Wait "480/(80+$rank*80)";
                          Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"$1")),
                             Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                             Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]))])

  /// 怒首領蜂大往生四面ボス「逝流」第一形態その三 by 白い弾幕くん
  /// [Daiouzyou]_round_4_boss.xml
  let round_4_boss =
    "怒首領蜂大往生四面ボス「逝流」第一形態その三 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"110")),
               Some (Speed (None,"3")),
               BulletRef ({bulletRefLabel = "armSrc";},["1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-110")),
               Some (Speed (None,"3")),
               BulletRef ({bulletRefLabel = "armSrc";},["0"])); Wait "400"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "armSrc";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "12";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                   Some (Speed (None,"1")),
                   BulletRef ({bulletRefLabel = "arm";},["$1"; "1"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"60")),
                   Some (Speed (None,"1")),
                   BulletRef ({bulletRefLabel = "arm";},["$1"; "1"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"-60")),
                   Some (Speed (None,"1")),
                   BulletRef ({bulletRefLabel = "arm";},["$1"; "1"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                   Some (Speed (None,"1")),
                   BulletRef ({bulletRefLabel = "arm";},["$1"; "-1"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"60")),
                   Some (Speed (None,"1")),
                   BulletRef ({bulletRefLabel = "arm";},["$1"; "-1"]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"-60")),
                   Some (Speed (None,"1")),
                   BulletRef ({bulletRefLabel = "arm";},["$1"; "-1"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "arm";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "12";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"180*$1")),None,
                   Bullet
                     ({bulletLabel = None;},None,None,
                      [Action ({actionLabel = None;},[Vanish])]));
                ChangeSpeed (Speed (None,"0"),Term "1");
                Repeat
                  (Times "400/(6-$rank*2)",
                   Action
                     ({actionLabel = None;},
                      [Wait "6-$rank*2+$rand";
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"11*$2")),
                          Some (Speed (None,"1.5+$rank*0.5")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// 怒首領蜂大往生四面ボス「逝流」第一形態その一 by 白い弾幕くん
  /// [Daiouzyou]_round_4_boss_1.xml
  let round_4_boss_1 =
    "怒首領蜂大往生四面ボス「逝流」第一形態その一 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed";},None,Some (Speed (None,"4")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "20",
                   Action
                     ({actionLabel = None;},
                      [Wait "20";
                       Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"116+$rand*6-$rank*15")),
                                 Some (Speed (None,"1.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Repeat
                                (Times "3.5+$rank*5",
                                 Action
                                   ({actionLabel = None;},
                                    [Fire
                                       ({fireLabel = None;},
                                        Some
                                          (Direction
                                             (Some {directionType = DirectionType.Sequence;},"3")),
                                        Some
                                          (Speed
                                             (None,"1.5")),
                                        Bullet ({bulletLabel = None;},None,None,[]))]))]))]));
                Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "xway";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-7*$1-7")),
               None,BulletRef ({bulletRefLabel = "Dummy";},[]));
            Repeat
              (Times "$1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"15")),
                      Some (Speed (None,"1.3")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action ({actionLabel = None;},[])]));
                   Repeat
                     (Times "4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"0")),
                             Some
                               (Speed
                                  (Some {speedType = SpeedType.Sequence;},"0.1")),
                             Bullet
                               ({bulletLabel = None;},None,None,
                                [Action ({actionLabel = None;},[])]))]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"110")),
               None,BulletRef ({bulletRefLabel = "seed";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-110")),
               None,BulletRef ({bulletRefLabel = "seed";},[])); Wait "400"])])

  /// 怒首領蜂大往生四面ボス「逝流」第一形態その二 by 白い弾幕くん
  /// [Daiouzyou]_round_4_boss_2.xml
  let round_4_boss_2 =
    "怒首領蜂大往生四面ボス「逝流」第一形態その二 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "blue";},None,Some (Speed (None,"3")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "16+$rank*16",
                   Action
                     ({actionLabel = None;},
                      [Wait "10-$rank*4+$rand";
                       Repeat
                         (Times "3",
                          Action
                            ({actionLabel = None;},
                             [Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"95")),
                                 Some (Speed (None,"1.4")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Repeat
                                (Times "3",
                                 Action
                                   ({actionLabel = None;},
                                    [Fire
                                       ({fireLabel = None;},
                                        Some
                                          (Direction
                                             (Some {directionType = DirectionType.Sequence;},"10")),
                                        Some
                                          (Speed
                                             (None,"1.4")),
                                        Bullet ({bulletLabel = None;},None,None,[]))]))]))]));
                Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "xway";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-7*$1-7")),
               None,BulletRef ({bulletRefLabel = "Dummy";},[]));
            Repeat
              (Times "$1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"15")),
                      Some (Speed (None,"1.3")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action ({actionLabel = None;},[])]));
                   Repeat
                     (Times "4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction
                                  (Some {directionType = DirectionType.Sequence;},"0")),
                             Some
                               (Speed
                                  (Some {speedType = SpeedType.Sequence;},"0.08+$rank*0.08")),
                             Bullet
                               ({bulletLabel = None;},None,None,
                                [Action ({actionLabel = None;},[])]))]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "red";},None,Some (Speed (None,"3")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "5",
                   Action
                     ({actionLabel = None;},
                      [Action.ActionRef
                         ({actionRefLabel = "xway";},
                          ["$rand*3+$rank*2"]);
                       Wait "40"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"120")),
               None,BulletRef ({bulletRefLabel = "blue";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-120")),
               None,BulletRef ({bulletRefLabel = "red";},[])); Wait "200";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-120")),
               None,BulletRef ({bulletRefLabel = "blue";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"120")),
               None,BulletRef ({bulletRefLabel = "red";},[])); Wait "200"])])

  /// 怒首領蜂大往生四面ボス「逝流」第一形態その四 by 白い弾幕くん
  /// [Daiouzyou]_round_4_boss_4.xml
  let round_4_boss_4 =
    "怒首領蜂大往生四面ボス「逝流」第一形態その四 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
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
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "fan";},
           [Wait "30";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
               Some (Speed (None,"1.2+$rank")),
               BulletRef ({bulletRefLabel = "Dummy";},[]));
            Action.ActionRef ({actionRefLabel = "XWay";},["$2"; "$3"]);
            Repeat
              (Times "6",
               Action
                 ({actionLabel = None;},
                  [Wait "30";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$4")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Action.ActionRef
                     ({actionRefLabel = "XWay";},
                      ["$2"; "$3"])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "top1";},
           [Action.ActionRef
              ({actionRefLabel = "fan";},
               ["220"; "8"; "5"; "-42.5"]);
            Action.ActionRef
              ({actionRefLabel = "fan";},
               ["150"; "8"; "-5"; "42.5"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top2";},
           [Action.ActionRef
              ({actionRefLabel = "fan";},
               ["200"; "7"; "2.5"; "-22.5"]);
            Action.ActionRef
              ({actionRefLabel = "fan";},
               ["170"; "7"; "-2.5"; "22.5"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top3";},
           [Action.ActionRef
              ({actionRefLabel = "fan";},
               ["160"; "8"; "5"; "-42.5"]);
            Action.ActionRef
              ({actionRefLabel = "fan";},
               ["210"; "8"; "-5"; "42.5"])]);
        BulletmlElm.Action
          ({actionLabel = Some "top4";},
           [Wait "20";
            Repeat
              (Times "2",
               Action
                 ({actionLabel = None;},
                  [Repeat
                     (Times "36+$rank*20",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some (Direction (None,"$rand*360")),
                             Some (Speed (None,"2")),
                             Bullet
                               ({bulletLabel = None;},None,None,
                                [Action
                                   ({actionLabel = None;},
                                    [Wait "10*$rand";
                                     Action.ActionRef ({actionRefLabel = "Stop";},[]);
                                     Wait "60";
                                     ChangeDirection
                                       (Direction
                                          (Some {directionType = Aim;},"0"),
                                        Term "1");
                                     ChangeSpeed
                                       (Speed (None,"2.4"),
                                        Term "1")])]));
                          Wait "3"]));
                   Wait "60-$rank*60"]))])])

  /// 怒首領蜂大往生四面ボス「逝流」第二形態その一 by 白い弾幕くん
  /// [Daiouzyou]_round_4_boss_5.xml
  let round_4_boss_5 =
    "怒首領蜂大往生四面ボス「逝流」第二形態その一 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Action
          ({actionLabel = Some "XWay";},
           [Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$2")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "blueFan";},None,Some (Speed (None,"3")),
           [Action
              ({actionLabel = None;},
               [Wait "20"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "6",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"120+$1*2")),
                          Some (Speed (None,"1.6")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef
                         ({actionRefLabel = "XWay";},
                          ["3"; "120"]);
                       Repeat
                         (Times "6+$rank*6",
                          Action
                            ({actionLabel = None;},
                             [Wait "56/(6+$rank*6)";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"120+$1")),
                                 Some (Speed (None,"1.6")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Action.ActionRef
                                ({actionRefLabel = "XWay";},
                                 ["3"; "120"])]));
                       Wait "14"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "singleRedAim";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"0")),
               Some (Speed (None,"2")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[])]));
            Repeat
              (Times "15",
               Action
                 ({actionLabel = None;},
                  [Wait "4";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"0")),
                      Some (Speed (None,"2")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action ({actionLabel = None;},[])]))]))]);
        BulletmlElm.Action
          ({actionLabel = Some "doubleRedAim";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-5*$1")),
               Some (Speed (None,"2")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[])]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Sequence;},"20*$1")),
               Some (Speed (None,"2")),
               Bullet
                 ({bulletLabel = None;},None,None,
                  [Action ({actionLabel = None;},[])]));
            Repeat
              (Times "15",
               Action
                 ({actionLabel = None;},
                  [Wait "4";
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"-20*$1")),
                      Some (Speed (None,"2")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action ({actionLabel = None;},[])]));
                   Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"20*$1")),
                      Some (Speed (None,"2")),
                      Bullet
                        ({bulletLabel = None;},None,None,
                         [Action ({actionLabel = None;},[])]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "redAim2";},None,Some (Speed (None,"1")),
           [Action
              ({actionLabel = None;},
               [Wait "20"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Wait "100";
                Action.ActionRef ({actionRefLabel = "singleRedAim";},[]);
                Wait "60";
                Action.ActionRef ({actionRefLabel = "doubleRedAim";},["-1"]);
                Wait "20";
                Action.ActionRef ({actionRefLabel = "doubleRedAim";},["-1"]);
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "redAim1";},None,Some (Speed (None,"1")),
           [Action
              ({actionLabel = None;},
               [Wait "20"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Wait "40";
                Action.ActionRef ({actionRefLabel = "singleRedAim";},[]);
                Wait "60";
                Action.ActionRef ({actionRefLabel = "doubleRedAim";},["1"]);
                Wait "80";
                Action.ActionRef ({actionRefLabel = "doubleRedAim";},["1"]);
                Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               None,BulletRef ({bulletRefLabel = "blueFan";},["4"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               None,BulletRef ({bulletRefLabel = "blueFan";},["-4"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               None,BulletRef ({bulletRefLabel = "redAim2";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               None,BulletRef ({bulletRefLabel = "redAim1";},[]));
            Wait "400"])])

  /// 怒首領蜂大往生五面ボス「黄流」第一形態その一 by 白い弾幕くん
  /// [Daiouzyou]_round_5_boss_1.xml
  let round_5_boss_1 =
    "怒首領蜂大往生五面ボス「黄流」第一形態その一 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Action
          ({actionLabel = Some "XWay";},
           [Repeat
              (Times "$1-1",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some
                        (Direction
                           (Some {directionType = DirectionType.Sequence;},"$2")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aim3";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10";
                Fire
                  ({fireLabel = None;},None,Some (Speed (None,"0")),
                   BulletRef ({bulletRefLabel = "aim3Impl";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aim3Impl";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "7",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},
                                "-33+$rand*6")),
                          Some (Speed (None,"1.5")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef
                         ({actionRefLabel = "XWay";},
                          ["3"; "30"]);
                       Repeat
                         (Times "2+$rank*3",
                          Action
                            ({actionLabel = None;},
                             [Wait "3";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"-60")),
                                 Some (Speed (None,"1.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]));
                              Action.ActionRef
                                ({actionRefLabel = "XWay";},
                                 ["3"; "30"])]));
                       Wait "54-$rank*9"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aim";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10";
                Fire
                  ({fireLabel = None;},None,Some (Speed (None,"0")),
                   BulletRef ({bulletRefLabel = "aimImpl";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "aimImpl";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "7",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},
                                "-3+$rand*6")),
                          Some (Speed (None,"1.5")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Repeat
                         (Times "2+$rank*3",
                          Action
                            ({actionLabel = None;},
                             [Wait "3";
                              Fire
                                ({fireLabel = None;},
                                 Some
                                   (Direction
                                      (Some {directionType = DirectionType.Sequence;},"0")),
                                 Some (Speed (None,"1.5")),
                                 Bullet ({bulletLabel = None;},None,None,[]))]));
                       Wait "54-$rank*9"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "fan";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "3+$rank*4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Absolute;},"$1-$2*3")),
                          Some (Speed (None,"$3")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Action.ActionRef
                         ({actionRefLabel = "XWay";},
                          ["7"; "10"]);
                       Wait "420/(3+$rank*4)"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"110")),
               Some (Speed (None,"4")),
               BulletRef ({bulletRefLabel = "aim3";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-110")),
               Some (Speed (None,"4")),
               BulletRef ({bulletRefLabel = "aim3";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"125")),
               Some (Speed (None,"5")),
               BulletRef ({bulletRefLabel = "aim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-125")),
               Some (Speed (None,"5")),
               BulletRef ({bulletRefLabel = "aim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"150")),
               Some (Speed (None,"7")),
               BulletRef ({bulletRefLabel = "aim";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-150")),
               Some (Speed (None,"7")),
               BulletRef ({bulletRefLabel = "aim";},[])); Wait "10";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"6")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["-135"; "10"; "1.3"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-90")),
               Some (Speed (None,"6")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["135"; "10"; "1.3"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"110")),
               Some (Speed (None,"4")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["-164"; "8"; "1.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-110")),
               Some (Speed (None,"4")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["156"; "8"; "1.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"130")),
               Some (Speed (None,"2")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["180"; "8"; "1.1"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-130")),
               Some (Speed (None,"2")),
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["180"; "5"; "1.1"]));
            Wait "430"])])
  
  /// 怒首領蜂大往生五面ボス「黄流」第一形態その二 by 白い弾幕くん
  /// [Daiouzyou]_round_5_boss_2.xml
  let round_5_boss_2 =
    "怒首領蜂大往生五面ボス「黄流」第一形態その二 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Bullet
          ({bulletLabel = Some "Red";},None,None,[Action ({actionLabel = None;},[])]);
        BulletmlElm.Action
          ({actionLabel = Some "Stop";},
           [ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "Dummy";},None,None,
           [Action ({actionLabel = None;},[Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seven";},
           Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
           Some (Speed (None,"4")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Repeat
                  (Times "5+$rank*4",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-10")),
                          Some (Speed (None,"1.5")),
                          BulletRef ({bulletRefLabel = "Red";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"10")),
                          Some (Speed (None,"1.5")),
                          BulletRef ({bulletRefLabel = "Red";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-5")),
                          Some (Speed (None,"1.3")),
                          BulletRef ({bulletRefLabel = "Red";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"5")),
                          Some (Speed (None,"1.3")),
                          BulletRef ({bulletRefLabel = "Red";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"-5")),
                          Some (Speed (None,"1.7")),
                          BulletRef ({bulletRefLabel = "Red";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"5")),
                          Some (Speed (None,"1.7")),
                          BulletRef ({bulletRefLabel = "Red";},[]));
                       Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = Aim;},"0")),
                          Some (Speed (None,"1.5")),
                          BulletRef ({bulletRefLabel = "Red";},[]));
                       Wait "360/(5+$rank*4)"])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "fan";},None,Some (Speed (None,"4")),
           [Action
              ({actionLabel = None;},
               [Wait "10"; Action.ActionRef ({actionRefLabel = "Stop";},[]);
                Fire
                  ({fireLabel = None;},Some (Direction (None,"$1")),None,
                   BulletRef ({bulletRefLabel = "Dummy";},[]));
                Repeat
                  (Times "35+$rank*35",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"$2")),
                          Some (Speed (None,"$3")),
                          Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "10/(1+$rank)+$rand"])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "seven";},[]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"170")),
               None,
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["55"; "10"; "1.8+$rank*0.4"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"170")),
               None,
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["60"; "10"; "1+$rank*0.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"170")),
               None,
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["225"; "10"; "1.4+$rank*0.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"170")),
               None,
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["250"; "10"; "1.3+$rank*0.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-170")),
               None,
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["55"; "-10"; "1.8+$rank*0.4"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-170")),
               None,
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["60"; "-10"; "1+$rank*0.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-170")),
               None,
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["225"; "-10"; "1.4+$rank*0.2"]));
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"-170")),
               None,
               BulletRef
                 ({bulletRefLabel = "fan";},
                  ["250"; "-10"; "1.3+$rank*0.2"]));
            Wait "360"])])
  
  /// 怒首領蜂大往生二周目一面ボス、その一 by 白い弾幕くん
  /// [Daiouzyou]_round_6_boss_1.xml
  let round_6_boss_1 =
    "怒首領蜂大往生二周目一面ボス、その一 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Repeat
              (Times "64",
               Action
                 ({actionLabel = None;},
                  [Wait "2"; Action.ActionRef ({actionRefLabel = "four";},["$rand*90+135"])]))]);
        BulletmlElm.Action
          ({actionLabel = Some "four";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),
               Some (Speed (None,"6")),BulletRef ({bulletRefLabel = "rb";},["$1"]));
            Repeat
              (Times "3",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"60")),
                      Some (Speed (None,"6")),
                      BulletRef ({bulletRefLabel = "rb";},["$1"]))]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "rb";},None,None,
           [ActionRef ({actionRefLabel = "red";},["$1+$rand*20-10"])]);
        BulletmlElm.Action
          ({actionLabel = Some "red";},
           [Wait "1";
            Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = DirectionType.Absolute;},"$1")),
               Some (Speed (None,"1.2+$rank")),
               Bullet ({bulletLabel = None;},None,None,[])); Vanish])])
  
  /// 怒首領蜂大往生二周目一面ボス、その二 by 白い弾幕くん
  /// [Daiouzyou]_round_6_boss_2.xml
  let round_6_boss_2 =
    "怒首領蜂大往生二周目一面ボス、その二 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [ChangeSpeed (Speed (None,"4"),Term "1");
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"180"),Term "1");
            Wait "10"; ChangeSpeed (Speed (None,"0"),Term "1");
            Repeat
              (Times "5",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
                      BulletRef ({bulletRefLabel = "bl_seed";},[]));
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),None,
                      BulletRef ({bulletRefLabel = "bl_seed";},[])); Wait "80"]));
            ChangeSpeed (Speed (None,"4"),Term "1");
            ChangeDirection
              (Direction (Some {directionType = DirectionType.Absolute;},"0"),Term "1"); Wait "10";
            ChangeSpeed (Speed (None,"0"),Term "1")]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bl_seed";},None,Some (Speed (None,"24")),
           [Action
              ({actionLabel = None;},
               [Wait "1";
                Fire
                  ({fireLabel = None;},None,None,
                   Bullet
                     ({bulletLabel = None;},None,Some (Speed (None,"0")),
                      [ActionRef ({actionRefLabel = "bl";},[])])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "bl";},
           [Fire
              ({fireLabel = None;},
               Some (Direction (Some {directionType = Aim;},"-30")),
               Some (Speed (Some {speedType = SpeedType.Absolute;},"1")),
               Bullet ({bulletLabel = None;},None,None,[]));
            Repeat
              (Times "4",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                      Bullet ({bulletLabel = None;},None,None,[]))])); Wait "4";
            Repeat
              (Times "3+$rank*6",
               Action
                 ({actionLabel = None;},
                  [Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = Aim;},"-30")),
                      Some (Speed (Some {speedType = SpeedType.Sequence;},"0.4")),
                      Bullet ({bulletLabel = None;},None,None,[]));
                   Repeat
                     (Times "4",
                      Action
                        ({actionLabel = None;},
                         [Fire
                            ({fireLabel = None;},
                             Some
                               (Direction (Some {directionType = DirectionType.Sequence;},"15")),
                             Some (Speed (Some {speedType = SpeedType.Sequence;},"0")),
                             Bullet ({bulletLabel = None;},None,None,[]))]));
                   Wait "4"])); Vanish])])

  /// 怒首領蜂大往生二周目一面ボス、その三 by 白い弾幕くん
  /// [Daiouzyou]_round_6_boss_3.xml
  let round_6_boss_3 =
    "怒首領蜂大往生二周目一面ボス、その三 by 白い弾幕くん",
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
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"270")),None,
                      BulletRef ({bulletRefLabel = "bm_seed";},["-25"])); Wait "20";
                   Fire
                     ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"90")),None,
                      BulletRef ({bulletRefLabel = "bm_seed";},["25"])); Wait "100"]))]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "bm_seed";},None,Some (Speed (None,"24")),
           [Action
              ({actionLabel = None;},
               [Wait "1";
                Fire
                  ({fireLabel = None;},None,None,
                   Bullet
                     ({bulletLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                      Some (Speed (None,"3")),
                      [ActionRef ({actionRefLabel = "bm";},[])]));
                Fire
                  ({fireLabel = None;},None,None,
                   Bullet
                     ({bulletLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"$1")),
                      Some (Speed (None,"2")),
                      [ActionRef ({actionRefLabel = "bm";},[])])); Vanish])]);
        BulletmlElm.Action
          ({actionLabel = Some "bm";},
           [ChangeSpeed (Speed (None,"0"),Term "50"); Wait "45";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round";},["1.5"; "0"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round";},["1.25"; "7"]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round";},["1"; "14"])); Vanish]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "round";},None,Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Absolute;},"$2")),
                   Some (Speed (None,"$1")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Repeat
                  (Times "10+$rank*10",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction
                               (Some {directionType = DirectionType.Sequence;},"360/(10+$rank*10)")),
                          Some (Speed (None,"$1")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])])])

  /// 怒首領蜂大往生二周目一面ボス、その四 by 白い弾幕くん
  /// [Daiouzyou]_round_6_boss_4.xml
  let round_6_boss_4 =
    "怒首領蜂大往生二周目一面ボス、その四 by 白い弾幕くん",
    Bulletml
      ({bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml";
        bulletmlType = Some BulletVertical;},
       [BulletmlElm.Action
          ({actionLabel = Some "top";},
           [Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round_seed";},[]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "sht";},["0.8"])); Wait "20";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round_seed";},[])); Wait "100";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round_seed";},[]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "sht";},["1"])); Wait "20";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round_seed";},[])); Wait "100";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round_seed";},[]));
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "sht";},["1.2"])); Wait "20";
            Fire
              ({fireLabel = None;},None,None,
               BulletRef ({bulletRefLabel = "round_seed";},[])); Wait "25"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "sht";},None,None,
           [Action
              ({actionLabel = None;},
               [Repeat
                  (Times "16",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},Some (Direction (None,"$rand*16-8")),
                          Some (Speed (None,"($1+$rand*$1)*(1+$rank*$rank)")),
                          Bullet ({bulletLabel = None;},None,None,[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "round_seed";},None,Some (Speed (None,"0")),
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},Some (Direction (None,"0")),None,
                   BulletRef ({bulletRefLabel = "two";},[]));
                Repeat
                  (Times "15",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"22.5")),
                          None,BulletRef ({bulletRefLabel = "two";},[]))])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "two";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"-4")),
                   Some (Speed (None,"1+$rank")),
                   Bullet ({bulletLabel = None;},None,None,[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"4")),
                   Some (Speed (None,"1+$rank")),
                   Bullet ({bulletLabel = None;},None,None,[])); Vanish])])])

  /// 怒首領蜂大往生二周目一面ボス、その五 by 白い弾幕くん
  /// [Daiouzyou]_round_6_boss_5.xml
  let round_6_boss_5 =
    "怒首領蜂大往生二周目一面ボス、その五 by 白い弾幕くん",
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
                      Some (Direction (Some {directionType = DirectionType.Absolute;},"180")),
                      Some (Speed (None,"4")),
                      BulletRef ({bulletRefLabel = "seed";},[])); Wait "500"]));
            Wait "200"]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed";},None,None,
           [Action
              ({actionLabel = None;},
               [Wait "9";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"0")),None,
                   BulletRef ({bulletRefLabel = "seed2";},[]));
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"180")),None,
                   BulletRef ({bulletRefLabel = "seed2";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed2";},None,Some (Speed (None,"18")),
           [Action
              ({actionLabel = None;},
               [Wait "1";
                Fire
                  ({fireLabel = None;},
                   Some (Direction (Some {directionType = DirectionType.Relative;},"90")),None,
                   BulletRef ({bulletRefLabel = "seed3";},[])); Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "seed3";},None,Some (Speed (None,"0.8")),
           [Action
              ({actionLabel = None;},
               [ChangeDirection
                  (Direction (Some {directionType = DirectionType.Sequence;},"1.2"),Term "9999")]);
            Action
              ({actionLabel = None;},
               [Repeat
                  (Times "62+$rank*100",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},
                          Some
                            (Direction (Some {directionType = DirectionType.Sequence;},"40-10")),
                          None,Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"140")),

                          None,Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"40")),
                          None,Bullet ({bulletLabel = None;},None,None,[]));
                       Fire
                         ({fireLabel = None;},
                          Some (Direction (Some {directionType = DirectionType.Sequence;},"140")),

                          None,Bullet ({bulletLabel = None;},None,None,[]));
                       Wait "8-$rank*6"]))]);
            Action
              ({actionLabel = None;},
               [Repeat
                  (Times "5",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},None,None,
                          BulletRef ({bulletRefLabel = "tw";},[])); Wait "138"]));
                Vanish])]);
        BulletmlElm.Bullet
          ({bulletLabel = Some "tw";},None,None,
           [Action
              ({actionLabel = None;},
               [Fire
                  ({fireLabel = None;},None,None,
                   Bullet
                     ({bulletLabel = None;},Some (Direction (None,"-12")),None,
                      [Action ({actionLabel = None;},[])]));
                Repeat
                  (Times "3.5+$rank*5+$rand",
                   Action
                     ({actionLabel = None;},
                      [Fire
                         ({fireLabel = None;},None,None,
                          Bullet
                            ({bulletLabel = None;},
                             Some (Direction (Some {directionType = DirectionType.Sequence;},"4")),
                             None,[Action ({actionLabel = None;},[])]))])); Vanish])])])
