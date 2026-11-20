/*
 * File: Player.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spirit_energy_project.InputSubsystem;

namespace Spirit_energy_project
{
    public class Player : GameObject
    {
        private Texture2D ghostSprite;
        private Rectangle currentFrame;
        private Rectangle right = new Rectangle(0, 0, 32, 32);
        private Rectangle left = new Rectangle(0, 32, 32, 32);
        private Rectangle down = new Rectangle(0, 64, 32, 32);
        private Rectangle up = new Rectangle(0, 96, 32, 32);
        private float speed = 200;
        private float rotation;
        AnimationController controller;
        SpriteRenderer renderer;
        public Player()
        {
            renderer = new SpriteRenderer();
            controller = new AnimationController(renderer);
            addComponent(renderer);
            addComponent(controller);
        }
        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Transform.Position += velocity * speed * Time.DeltaTime;
            if (velocity.X == 0)
            {
                NormalizeRotation();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            controller.Draw(spriteBatch);
            // spriteBatch.Draw(ghostSprite, Transform.Position, null, Color.White, MathHelper.ToRadians(rotation), new Vector2(16, 8), 1f, SpriteEffects.None, 0f);
        }
        public override void Initialize()
        {
            currentFrame = down;
            Transform.Position = new Vector2(100, 100);
        }
        public override void LoadContents()
        {
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_right", new Rectangle(0, 0, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_left", new Rectangle(0, 32, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_down", new Rectangle(0, 64, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_up", new Rectangle(0, 96, 128, 32), 32, 32);
        }
        protected void HandleInput()
        {
            velocity = Vector2.Zero;

            if (Input.IsKeyDown(KeyCode.Up))
            {
                currentFrame = up;
                controller.SetCurrentSheet("ghost_up");
                velocity += new Vector2(0, -1);
            }
            if (Input.IsKeyDown(KeyCode.Down))
            {
                currentFrame = down;
                controller.SetCurrentSheet("ghost_down");
                velocity += new Vector2(0, 1);
            }
            if (Input.IsKeyDown(KeyCode.Left))
            {
                currentFrame = left;
                controller.SetCurrentSheet("ghost_left");
                velocity += new Vector2(-1, 0);
                if (Transform.Rotation > -20f)
                    Transform.Rotation -= 2f;
            }
            if (Input.IsKeyDown(KeyCode.Right))
            {
                controller.SetCurrentSheet("ghost_right");
                currentFrame = right;
                velocity += new Vector2(1, 0);
                if (Transform.Rotation < 20)
                    Transform.Rotation += 2f;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        }
        private void NormalizeRotation()
        {
            if (Transform.Rotation > 0)
            {
                Transform.Rotation -= 1f;
            }
            if (Transform.Rotation < 0)
            {
                Transform.Rotation += 1f;
            }
        }
    }
}
