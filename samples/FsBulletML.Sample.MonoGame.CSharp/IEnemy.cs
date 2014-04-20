using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsBulletML.MonoGame;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    interface IEnemy : IBullet
    {
        int Life { get; set; }
        void Shoot();
    }
}
