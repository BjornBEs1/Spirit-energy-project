/*
 * File: SpriteRenderer.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spirit_energy_project
{
    public class SpriteRenderer : Component, IRendererComponent
    {
        internal Rectangle srcRectangle;
        internal Texture2D currentTexture;
        public Texture2D Sprite => currentTexture;
        public Vector2 SpriteSize => new Vector2(currentTexture.Width, currentTexture.Height);
        public Vector2 Origin { get; set; }

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
            else if (obj is TextureRegion textureRegion)
            {
                srcRectangle = textureRegion.SourceRectangle;
                SetSprite(textureRegion.Texture);
            }
        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentTexture != null)
            {
                spriteBatch.Draw(currentTexture, Owner.Transform.Position, srcRectangle, Color.White, Owner.Transform.Rotation, Origin, Owner.Transform.Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
