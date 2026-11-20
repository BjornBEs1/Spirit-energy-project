using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public class AnimationController : Component, IRendererComponent
    {
        internal string spriteSheet;
        internal List<string> frames;
        internal List<Texture2D> textureFrames;
        internal IRendererComponent renderer;

        int animCount => frames.Count;
        float animTime = 0;
        public float AnimationSpeed { get; set; }

        public AnimationController()
        {
            frames = new List<string>();
        }
        public AnimationController(IRendererComponent _renderer)
        {
            frames = new List<string>();
            renderer = _renderer;
        }
        public AnimationController(GameObject gameObject)
        {
            frames = new List<string>();
        }

        internal override void componentAdded(GameObject owner)
        {
        }
        public void SetSpriteSheet(string sprite, string path)
        {
            ContentEngineManager.Instance.LoadTexture2D(sprite, path);
            spriteSheet = sprite;
        }
        public void SetFrames(string[] _frames, string[] _paths)
        {
            frames.AddRange(_frames);
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

            if (string.IsNullOrEmpty(spriteSheet))
            {

            }

            if (textureFrames.Count > 0)
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
