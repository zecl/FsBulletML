using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    class Particle
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 direction;
        public Vector2 origin;
        public float duration;
        public float scale;
        public float shrinkRate;
        public float speed;
        public bool isActive;
        public Color colour;

        public Particle(Texture2D tex, Vector2 pos, float speed, float angle, float scale, float shrinkRate, float duration, Color colour)
        {
            this.texture = tex;
            this.position = pos;
            this.scale = scale;
            this.shrinkRate = shrinkRate;
            this.isActive = true;
            this.duration = duration;
            this.colour = colour;
            this.speed = speed;

            this.origin = new Vector2(tex.Width / 2, tex.Height / 2);
            angle = MathHelper.ToRadians(angle);
            this.direction = Vector2.Transform(new Vector2(0, -1.0f), Matrix.CreateRotationZ(angle));
        }

        public void Update(float delta)
        {
            this.position += this.direction * this.speed * delta;
            this.scale -= this.shrinkRate * delta;

            this.duration -= delta;

            if (this.scale <= 0.0f || this.duration <= 0.0f)
            {
                this.isActive = false;
                this.position = new Vector2(-100, -100);
            }
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(this.texture, this.position, null, this.colour, 0, this.origin, this.scale, SpriteEffects.None, 0);
        }
    }
}
