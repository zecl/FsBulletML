using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    class ParticleEmitter
    {
        private List<Particle> particles;
        private static Random Rand = new Random();
 
        public ParticleEmitter()
        {
            this.particles = new List<Particle>();
        }
        
        public void Emmit(Texture2D tex, Vector2 pos, float scale, float shrinkRate,float duration, int amount, int maxSpeed, Color colour)
        {
            for (int i = 0; i < amount; i++)
            {
                int angle = ParticleEmitter.Rand.Next(0, 360);
                float speed = ParticleEmitter.Rand.Next(1, maxSpeed);
                var p = new Particle(tex, pos, speed, angle, scale, shrinkRate, duration, colour);
                this.particles.Add(p);
            }
        }

        public void Clear()
        {
            this.particles.Clear();
        }

        public void Update(double delta)
        {
            List<Particle> toRemove = new List<Particle>();
            for (int i = 0; i < this.particles.Count; i++)
            {
                this.particles[i].Update(delta);
 
                if (!this.particles[i].IsActive)
                    toRemove.Add(this.particles[i]);
            }
 
            for (int i = 0; i < toRemove.Count; i++)
                this.particles.Remove(toRemove[i]);
        }

        public void Draw(SpriteBatch sp)
        {
            for (int i = 0; i < this.particles.Count; i++)
                this.particles[i].Draw(sp);
        }
    }
}
