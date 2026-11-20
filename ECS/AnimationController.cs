/*
 * File: AnimationController.cs
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
using Microsoft.Xna.Framework.Input;

namespace Spirit_energy_project
{
    public class AnimationController : Component, IRendererComponent
    {
        internal Dictionary<string, Tileset> spriteSheetTexture;
        internal TextureRegion textureRegion;
        internal Texture2D spriteSheetTexture2D;
        internal Tileset currentSheet;
        internal List<string> frames;
        internal List<Texture2D> textureFrames;
        internal IRendererComponent renderer;

        int animCount;
        float animTime = 0;
        public float AnimationSpeed { get; set; }

        public AnimationController()
        {
            AnimationSpeed = 1;
            frames = new List<string>();
        }
        public AnimationController(IRendererComponent _renderer)
        {
            AnimationSpeed = 1;
            frames = new List<string>();
            renderer = _renderer;
        }
        public AnimationController(GameObject gameObject)
        {
            AnimationSpeed = 1;
            frames = new List<string>();
        }

        internal override void componentAdded(GameObject owner)
        {
        }
        public void SetSpriteSheet(string sprite, string path, string tileSetKey, Rectangle sourceRectangle, int spriteWidth, int spriteHeight)
        {
            if (spriteSheetTexture2D == null)
            {
                spriteSheetTexture = new Dictionary<string, Tileset>();
                ContentEngineManager.Instance.LoadTexture2D(sprite, path);
                spriteSheetTexture2D = ContentEngineManager.Instance.GetTexture2D(sprite);
            }
            textureRegion = new TextureRegion(spriteSheetTexture2D, sourceRectangle);
            currentSheet = new Tileset(textureRegion, spriteWidth, spriteHeight);
            spriteSheetTexture.Add(tileSetKey, currentSheet);
            animCount = currentSheet.Count;
        }
        public void SetCurrentSheet(string tileSetKey)
        {
            currentSheet = spriteSheetTexture[tileSetKey];
            animCount = currentSheet.Count;
        }
        public void SetFrames(string[] _frames, string[] _paths)
        {
            frames.AddRange(_frames);
            animCount = frames.Count;
            textureFrames = new List<Texture2D>(frames.Count);
            for (int i = 0; i < frames.Count; i++)
            {
                textureFrames.Add(null);
            }
        }
        public void SetFrame(string frame, string path)
        {
            ContentEngineManager.Instance.LoadTexture2D(frame, path);
            frames.Add(frame);
            animCount = frames.Count;
            textureFrames = new List<Texture2D>(frames.Count);
            for (int i = 0; i < frames.Count; i++)
            {
                textureFrames.Add(null);
            }
        }
        public void SetSprite(object obj)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            animTime += Time.DeltaTime * AnimationSpeed;
            if (animTime > animCount)
            {
                animTime = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int animIndex = (int)Math.Clamp(animTime, 0, animCount);

            if (spriteSheetTexture != null)
            {
                TextureRegion region = currentSheet.GetTile(animIndex);
                renderer.SetSprite(region);
                renderer.Draw(spriteBatch);
            }
            else if (textureFrames.Count > 0)
            {
                if (textureFrames[animIndex] == null)
                {
                    textureFrames[animIndex] = ContentEngineManager.Instance.GetTexture2D(frames[animIndex]);
                }
                renderer.SetSprite(textureFrames[animIndex]);
                renderer.Draw(spriteBatch);
            }
        }
    }
}
