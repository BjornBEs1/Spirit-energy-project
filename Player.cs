using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spirit_energy_project.InputSubsystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Spirit_energy_project
{
    public class Player : GameObject
    {
        private Texture2D ghostSprite;
        private Rectangle currentFrame;
        private Rectangle right = new Rectangle(0,0,32,32);
        private Rectangle left = new Rectangle(0, 32, 32, 32);
        private Rectangle down = new Rectangle(0, 64, 32, 32);
        private Rectangle up = new Rectangle(0, 96, 32, 32);
        private float speed = 200;
        private float rotation;
        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine("test");
            HandleInput();
            Transform.Position += velocity * speed*Time.DeltaTime;
            if (velocity.X == 0)
            {
                NormalizeRotation();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ghostSprite, Transform.Position, currentFrame, Color.White, MathHelper.ToRadians(rotation), new Vector2(16, 8), 1f, SpriteEffects.None, 0f);
        }
        public override void Initialize()
        {
            currentFrame = down;
        }
        public override void LoadContents()
        {
            ContentEngineManager.Instance.LoadTexture2D("GhostSprite", "GhostSpriteSheet");
            ghostSprite = ContentEngineManager.Instance.GetTexture2D("GhostSprite");
        }
        protected void HandleInput()
        {
            velocity = Vector2.Zero;

            if (Input.IsKeyDown(KeyCode.Up))
            {
                currentFrame = up;
                velocity += new Vector2(0, -1);
            }
            if (Input.IsKeyDown(KeyCode.Down))
            {
                currentFrame = down;
                velocity += new Vector2(0, 1);
            }
            if (Input.IsKeyDown(KeyCode.Left))
            {
                currentFrame = left;
                velocity += new Vector2(-1, 0);
                if (rotation > -20f)
                    rotation -= 2f;
            }
            if (Input.IsKeyDown(KeyCode.Right))
            {
                currentFrame = right;
                velocity += new Vector2(1, 0);
                if (rotation < 20)
                    rotation += 2f;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        }
        private void NormalizeRotation()
        {
            if (rotation > 0)
            {
                rotation -= 1f;
            }
            if (rotation < 0)
            {
                rotation += 1f;
            }
        }
        public override void LoadContents()
        {
            
        }
    }
}
