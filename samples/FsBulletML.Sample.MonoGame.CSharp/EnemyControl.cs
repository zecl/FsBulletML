using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    public static class EnemyControl
    {
        public static IEnumerable<Tuple<string,DTD.Bulletml>> Bullets()
        {
 
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Guwange.round_2_boss_circle_fire;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Psyvariar.b4_D_boss_MZIQ;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.SilverGun.b4D_boss_PENTA;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Strikers1999.hanabi;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.b10flower_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.dis_bee_1;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.dis_bee_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.dis_bee_3;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.roll_misago;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.slow_move;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.circle_fireworks2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.DragonBlaze.nebyurosu_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.GWange._roll_gara;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.knight_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.GWange.round_trip_bit;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.b88way;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.bit;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.rollbar;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Tenmado.b5_boss_1;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Tenmado.b5_boss_3;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Dodonpachi.hibachi;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Dodonpachi.kitiku_1; 
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Dodonpachi.kitiku_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.hibachi_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.hibachi_3;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_5_boss_gara_4;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_5_boss_gara_3;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_5_boss_gara_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_5_boss_gara_1_a;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_123_boss_izuna_hakkyou;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_1_boss;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_3_boss;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_3_boss_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_3_boss_last;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_4_boss;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.StormCalibar.last_boss_double_roll_bullets;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.acc_n_dec;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.circular_model;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.wind_cl;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.gnnnyari;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.mossari;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.double_w;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.star_in_the_sky;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.guruguru;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.backfire;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.yokokasoku;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.entangled_space;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.ellipse_bomb;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.gyakuhunsya;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.fujin_ranbu_true;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Bulletsmorph.double_seduction;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.accusation;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.XiiStag.b3b;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.KetuiLt.b2boss_winder_crash;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.ChaosSeed.big_monkey_boss;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Guwange.round_4_boss_eye_ball;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_4_boss_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_4_boss_4;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_4_boss_5;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_5_boss_1;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_5_boss_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_1;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_2;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_3;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_4;
            yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_5;           
        }

        public static IEnumerable<Tuple<string, Processable.BulletmlTask>> EnemyTasks() 
        {
            foreach (var item in EnemyControl.Bullets())
            {
                yield return  new Tuple<string, Processable.BulletmlTask>(item.Item1, BulletRunner.ConvertBulletmlTask(item.Item2));
            }
        }

        public static IEnumerable<Tuple<Vector2, Tuple<string,Processable.BulletmlTask>>> Enemys(Vector2 enemyDefaultPos, IEnumerable<Tuple<string, Processable.BulletmlTask>> tasks) 
        {
            foreach (var task in tasks)
            {
                yield return new Tuple<Vector2, Tuple<string, Processable.BulletmlTask>>(enemyDefaultPos, task);
            }
        }

  //let enemys enemyDefaultPos (tasks:(string * BulletmlTask) list) = 
  //  tasks |> List.map (fun task -> enemyDefaultPos, task)  

    }
}
