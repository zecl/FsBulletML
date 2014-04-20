using System;
using System.Collections.Generic;
using System.Linq;
using FsBulletML;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            FsBulletML.Processable.BulletMLManager.Init(new BulletFunctions());
            using (var game = new FsBulletMLSampleGame())
                game.Run();
        }
    }
}
