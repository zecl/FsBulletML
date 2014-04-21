using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FsBulletML;
using FsBulletML.MonoGame;
using Settings = FsBulletML.MonoGame.Settings;
using unit = Microsoft.FSharp.Core.Unit;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    public class FsBulletMLSampleGame : Game
    {
        private static Vector2 EnemyDefaultPos = new Vector2(Settings.Enemy.X, Settings.Enemy.Y);
        private static IEnumerable<BulletmlInfo> EnemyBullets = null;

        private GraphicsDeviceManager gmanager;
        private SpriteBatch spriteBatch;
        private Texture2D EnemyTexture { get; set; }
        private Texture2D PlayerBulletTexture { get; set; }
        private Texture2D EnemyBulletTexture { get; set; }
        private Texture2D BackgroundTexture { get; set; }
        private Texture2D ParticleTexture { get; set; }
        private string BulletName { get; set; }
        private IEnemy Boss { get; set; }
        private Background Background { get; set; }
        private ParticleEmitter Emitter { get; set; }
        private SpriteFont Font { get; set; }
        private Fps Fps { get; set; }
        private KeyboardState CurrentKeyState { get; set; }
        private KeyboardState PrevKeyState { get; set; }
        private int EnemyIndex { get; set; }
        public static Player Player { get; set; }

        public FsBulletMLSampleGame()
        {
            gmanager = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";

            gmanager.PreferredBackBufferWidth = (int)Settings.Display.Width;
            gmanager.PreferredBackBufferHeight = (int)Settings.Display.Height;
            Player = new Player();
            Player.Init();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            base.Window.Title = "FsBulletML.Sample.MonoGame.CSharp";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Font = this.Content.Load<SpriteFont>(@"..\..\Content\font\SpriteFont2");
            FsBulletMLSampleGame.EnemyBullets = EnemyControl.Bullets();

            this.Fps = new Fps();

            var enemyInfo = GetEnemyInfo(this.EnemyIndex);
            var pos = enemyInfo.Item1;
            var bullet = enemyInfo.Item2;
            this.BulletName = bullet.Name;
            this.Boss = CreateEnemy(pos, bullet); 

            Player.Texture = this.Content.Load<Texture2D>(@"..\..\Content\Sprites\player");

            this.EnemyTexture = this.Content.Load<Texture2D>(@"..\..\Content\Sprites\enemy1");
            this.PlayerBulletTexture = this.Content.Load<Texture2D>(@"..\..\Content\Sprites\p_bullet_s");
            this.EnemyBulletTexture = this.Content.Load<Texture2D>(@"..\..\Content\Sprites\g_bullet_s");
            this.BackgroundTexture = this.Content.Load<Texture2D>(@"..\..\Content\Sprites\background");
            this.ParticleTexture = this.Content.Load<Texture2D>(@"..\..\Content\Sprites\particle");

            this.Emitter = new ParticleEmitter();
            this.Background = new Background(this.BackgroundTexture, gmanager.PreferredBackBufferHeight, 64);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.Background.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            this.Fps.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            this.PrevKeyState = this.CurrentKeyState;
            this.CurrentKeyState = Keyboard.GetState(); 

            if (this.Boss.Life == 0 || (this.IsPressed(Keys.Enter)))
            {
                Manager.RemoveAll();
                this.EnemyIndex += 1;
                var enemyInfo = GetEnemyInfo(this.EnemyIndex);
                var pos = enemyInfo.Item1;
                var bullet = enemyInfo.Item2;
                ((IBullet)this.Boss).Used = false;
                this.BulletName = bullet.Name;
                this.Boss = CreateEnemy(pos, bullet);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
              this.Exit();

            Player.Update();
            Manager.Update();
            Manager.Free();
            Manager.UpdateSpace();

            Action cont1 = () => { CreateParticle(Player.Pos); Player.DamageCounter += 1; };
            Action cont2 = () => { CreateParticle(this.Boss.Pos); if (this.Boss.Life > 0) this.Boss.Life -= 1; };

            Manager.CheckPlayerCollision(Player.Pos, Player.Radius, cont1.ToFSharpFunc());
            Manager.CheckEnemyCollision(this.Boss.Pos, this.Boss.Radius, cont2.ToFSharpFunc());
            this.Emitter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private void CreateParticle(Vector2 pos)
        {
            var rand = new Random ();
            var v = new Vector2 (rand.Next(0, 10), rand.Next(0, 10));
            this.Emitter.Emmit(this.ParticleTexture, pos + v, 1.4f, 1.2f, 1.0f, 2, 45, Color.Gold);
            this.Emitter.Emmit(this.ParticleTexture, pos + v, 1.2f, 1.5f, 1.2f, 1, 40, Color.OrangeRed);
            this.Emitter.Emmit(this.ParticleTexture, pos + v, 1.2f, 1.2f, 0.9f, 1, 45, Color.LightGoldenrodYellow);
            this.Emitter.Emmit(this.ParticleTexture, pos + v, 1.6f, 2.0f, 1.8f, 2, 40, Color.Orange);
            this.Emitter.Emmit(this.ParticleTexture, pos + v, 1.0f, 1.5f, 1.5f, 2, 40, Color.DarkRed);
            this.Emitter.Emmit(this.ParticleTexture, pos + v, 1.3f, 1.2f, 1.4f, 2, 40, Color.Tomato);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);
            this.Background.Draw(spriteBatch);

            DrawText ((String.Format("FPS: {0:F5}", this.Fps.Value)), new Vector2(3, 3), Color.White);

            foreach (var bullet in Manager.PlayerBullets)
            {
		        var  textureCenter = new Vector2(this.PlayerBulletTexture.Width / 2, this.PlayerBulletTexture.Height / 2);
                var position = Manager.GetDrawPos(bullet.Pos, this.PlayerBulletTexture) + textureCenter;
                spriteBatch.Draw(this.PlayerBulletTexture, position, null, Color.White,bullet.Dir, textureCenter, 1.0f, SpriteEffects.None, 0);
            }

            foreach (var bullet in Manager.EnemyBullets)
            {
                var textureCenter = new Vector2(this.EnemyBulletTexture.Width / 2, this.EnemyBulletTexture.Height / 2);
                var position = Manager.GetDrawPos(bullet.Pos, this.EnemyBulletTexture) + textureCenter;
                spriteBatch.Draw(this.EnemyBulletTexture, position, null, Color.White, bullet.Dir, textureCenter, 1.0f, SpriteEffects.None, 0);
            }

            foreach (var enemy in Manager.Enemys)
            {
                spriteBatch.Draw(this.EnemyTexture, Manager.GetDrawPos(enemy.Pos, this.EnemyTexture), Color.AntiqueWhite);
            }

            spriteBatch.Draw(Player.Texture, Manager.GetDrawPos(Player.Pos, Player.Texture), Color.AntiqueWhite);

            DrawText(string.Format("Name :{0} ",this.BulletName), new Vector2(3, 18), Color.White);
            DrawText(string.Format("Boss Life : {0}", this.Boss.Life.ToString()), new Vector2(3, 33), Color.White);
            DrawText(string.Format("Player Damage : {0}", Player.DamageCounter.ToString()), new Vector2(3, 48), Color.White);

            this.Emitter.Draw(spriteBatch);
            spriteBatch.End();
        }

        private bool IsPressed(Keys key)
        {
            return this.CurrentKeyState.IsKeyDown(key) && this.PrevKeyState.IsKeyUp(key);
        }

        private Enemy CreateEnemy(Vector2 pos, BulletmlInfo bulletMove) 
        {
            var enemy = new Enemy();
            Manager.AddEnemy(enemy);
            enemy.pos = pos;
            var name = bulletMove.Name;
            enemy.SetBulletTask(name, bulletMove.BulletmlTaskOption);
            return enemy;
        }

        private Tuple<Vector2, BulletmlInfo> GetEnemyInfo(int index) 
        {
            var len = FsBulletMLSampleGame.EnemyBullets.Count();
            if (len <= (index))
                this.EnemyIndex = 0;
            var bullet = FsBulletMLSampleGame.EnemyBullets.ElementAt(this.EnemyIndex);
            return new Tuple<Vector2, BulletmlInfo>(EnemyDefaultPos, bullet);
        }

        private void DrawText(string msg, Vector2 v, Color c)
        {
            spriteBatch.DrawString(this.Font, msg, new Vector2(v.X + 2, v.Y + 2), Color.Gray);
            spriteBatch.DrawString(this.Font, msg, v, c);
        }
    }
}
