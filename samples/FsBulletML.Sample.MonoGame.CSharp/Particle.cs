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
        private Texture2D texture;
        private Vector2 position;
        private Vector2 direction;
        private Vector2 origin;
        private float duration;
        private float scale;
        private float shrinkRate;
        private float speed;
        private Color colour;
        public bool IsActive { get; set; }

        public Particle(Texture2D tex, Vector2 pos, float speed, float angle, float scale, float shrinkRate, float duration, Color colour)
        {
            this.texture = tex;
            this.position = pos;
            this.scale = scale;
            this.shrinkRate = shrinkRate;
            this.duration = duration;
            this.colour = colour;
            this.speed = speed;
            this.IsActive = true;

            this.origin = new Vector2(tex.Width / 2, tex.Height / 2);
            angle = MathHelper.ToRadians(angle);
            this.direction = Vector2.Transform(new Vector2(0, -1.0f), Matrix.CreateRotationZ(angle));
        }

        public void Update(double delta)
        {
            var d = (float)delta;
            this.position += this.direction * this.speed * d;
            this.scale -= this.shrinkRate * d;
            this.duration -= d;

            if (this.scale <= 0.0f || this.duration <= 0.0f)
            {
                this.IsActive = false;
                this.position = new Vector2(-100, -100);
            }
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(this.texture, this.position, null, this.colour, 0, this.origin, this.scale, SpriteEffects.None, 0);
        }
    }
}
