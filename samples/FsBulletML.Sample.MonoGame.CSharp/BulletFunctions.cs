using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsBulletML;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    class BulletFunctions : FsBulletML.Processable.IBulletMLManager 
    {
        private static Random rand = new Random();
        public BulletFunctions ()
        {

        }

        public float GetPlayerPosX()
        {
            return FsBulletMLSampleGame.Player.Pos.X;
        }

        public float GetPlayerPosY()
        {
            return FsBulletMLSampleGame.Player.Pos.Y;
        }

        public float GetRandom()
        {
            return Convert.ToSingle(Math.Round(rand.NextDouble() * 10000) / 10000);
        }

        public float GetRank()
        {
            return 0;
        }
    }
}
