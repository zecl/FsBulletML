using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    class Fps
    {
        public float Value {get; private set;}
        private float interval;
        private float updateTimer;
        private int frameCount;

        public Fps()
        {
            this.Value = 0.0f;
            this.interval = 1.0f;
            this.updateTimer = 0.0f;
            this.frameCount = 0;
        }

        public void Update(float delta)
        {
            this.frameCount++;

            this.updateTimer += delta;

            if (this.updateTimer > this.interval)
            {
                this.Value = this.frameCount / this.updateTimer;
                this.frameCount = 0;
                this.updateTimer -= this.interval;
            }
        }
    }
}
