using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    public class Background
    {
        Texture2D texture; 
        Vector2 position;
        Vector2 textureOrigin;
        float scrollSpeed;
        int screenHeight;

        public Background(Texture2D texture, int screenHeight, float scrollSpeed)
        {
            this.scrollSpeed = scrollSpeed;
            this.screenHeight = screenHeight;
            this.texture = texture;
            this.textureOrigin = new Vector2(texture.Width / 2, 0);
            this.position = new Vector2(textureOrigin.X, screenHeight / 2);
        }

        public void Update(float delta)
        {
            this.position.Y += this.scrollSpeed * delta;
            this.position.Y = this.position.Y % this.texture.Height;
        }

        public void Draw(SpriteBatch sp)
        {
            if (this.position.Y < this.screenHeight)
                sp.Draw(this.texture, this.position, null, Color.White, 0.0f, this.textureOrigin, 1.0f, SpriteEffects.None, 0.0f);
            sp.Draw(this.texture, this.position - new Vector2(0, this.texture.Height), null, Color.White, 0.0f, this.textureOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
