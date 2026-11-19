using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public class SpriteRenderer : Component, IRendererComponent
    {
        internal Texture2D currentTexture;
        public Texture2D Sprite => currentTexture;
        public Vector2 SpriteSize => new Vector2(currentTexture.Width, currentTexture.Height);

        public void SetSprite(string key)
        {
            currentTexture = ContentEngineManager.Instance.GetTexture2D(key);
        }

        internal override void componentAdded(GameObject owner)
        {
        }

        public void SetSprite(object obj)
        {
            if (obj is Texture2D texture)
            {
                currentTexture = texture;
            }
            else if (obj is string key)
            {
                SetSprite(key);
            }
        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentTexture != null)
            {
                spriteBatch.Draw(currentTexture, Owner.Transform.Position, null, Color.White, 0f, SpriteSize / 2, Owner.Transform.Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
