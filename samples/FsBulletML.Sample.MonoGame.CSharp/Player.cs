using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FsBulletML;
using FsBulletML.MonoGame;
using Bulletml = FsBulletML.DTD.Bulletml;
using Settings = FsBulletML.MonoGame.Settings;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    public class Player
    {
        private static Bulletml b2wayLeftBullet = Xml.readXml(@"..\..\Content\xml\PlayerBullet\2wayLeft.xml");
        private static Bulletml b2wayRightBullet = Xml.readXml(@"..\..\Content\xml\PlayerBullet\2wayRight.xml");
        private static Bulletml homing = Xml.readXml(@"..\..\Content\xml\PlayerBullet\homing.xml");

        private int Timer { get; set; }
        public Vector2 Pos { get; set; }
        public float Speed { get; set; }
        public float Radius { get; set; }
        public int DamageCounter { get; set; }
        public Texture2D Texture { get; set; }
        public Player() {}

        public void Init() 
        {
            this.Pos = new Vector2(Settings.Player.X, Settings.Player.Y);
            this.Speed = 3.5f;
            this.Radius = 3.5f;
        }

        public void Update()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left)) 
            {
                if (this.Pos.X - this.Speed  >= 0)
                    this.Pos = new Vector2(this.Pos.X - this.Speed, this.Pos.Y);
            }
            if (keyboardState.IsKeyDown(Keys.Right)) 
            {
                if (this.Pos.X - this.Speed <= Settings.Display.Width)
                    this.Pos = new Vector2(this.Pos.X + this.Speed, this.Pos.Y);
            }
            if (keyboardState.IsKeyDown(Keys.Up)) 
            {
                if (this.Pos.Y - this.Speed >= 0)
                    this.Pos = new Vector2(this.Pos.X, this.Pos.Y - this.Speed);
            }
            if (keyboardState.IsKeyDown(Keys.Down)) 
            {
                if (this.Pos.Y - this.Speed <= Settings.Display.Height)
                    this.Pos = new Vector2(this.Pos.X, this.Pos.Y + this.Speed);
            }

            this.Timer += 1;
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                Shoot2WayLeftBullet();
                Shoot2WayRightBullet();
                ShootHomingBullet();
            }
 
            if (this.Timer > 60)
                this.Timer = 0;
        }

        private void Shoot2WayLeftBullet () 
        {
            var task = BulletRunner.ConvertBulletmlTaskOption(Player.b2wayLeftBullet);

            if (this.Timer > 0)
            {
                var bullet = new PlayerBullet();
                Manager.AddPlayerBulletPos(bullet, new Vector2(this.Pos.X - 10, this.Pos.Y + 1));
                bullet.SetTask(task);
            }
        }

        private void Shoot2WayRightBullet()
        {
            var task = BulletRunner.ConvertBulletmlTaskOption(Player.b2wayRightBullet);

            if (this.Timer > 0)
            {
                var bullet = new PlayerBullet();
                Manager.AddPlayerBulletPos(bullet, new Vector2(this.Pos.X + 10, this.Pos.Y + 1));
                bullet.SetTask(task);
            }
        }

        private void ShootHomingBullet()
        {
            var task = BulletRunner.ConvertBulletmlTaskOption(Player.homing);

            if (this.Timer > 60)
            {
                var bullet = new PlayerBullet();
                Manager.AddPlayerBulletPos(bullet, this.Pos);
                bullet.SetTask(task);
            }
        }
    }
}
