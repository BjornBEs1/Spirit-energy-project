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
        private float speed = 200;
        AnimationController controller;
        SpriteRenderer renderer;
        public Player()
        {
            renderer = new SpriteRenderer();
            controller = new AnimationController(renderer);
            addComponent(renderer);
            addComponent(controller);
            controller.AnimationSpeed = 10;
            renderer.Origin = new Vector2(16, 8);
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
            //spriteBatch.Draw(ghostSprite, Transform.Position, null, Color.White, MathHelper.ToRadians(rotation), new Vector2(16, 8), 1f, SpriteEffects.None, 0f);
        }
        public override void Initialize()
        {
            
            Transform.Position = new Vector2(100, 100);
        }
        public override void LoadContents()
        {
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_right", new Rectangle(0, 0, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_left", new Rectangle(0, 32, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_down", new Rectangle(0, 64, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_up", new Rectangle(0, 96, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostPunch01", "GhostFistSprite", "punch01", new Rectangle(0, 0, 128, 64), 32, 64);
            controller.SetSpriteSheet("GhostPunch02", "GhostFistSprite", "punch02", new Rectangle(0, 64, 128, 64), 32, 64);
        }
        protected void HandleInput()
        {
            velocity = Vector2.Zero;

            if (Input.IsKeyDown(KeyCode.Up))
            {
                controller.SetCurrentSheet("ghost_up");
                velocity += new Vector2(0, -1);
            }
            if (Input.IsKeyDown(KeyCode.Down))
            {                
                controller.SetCurrentSheet("ghost_down");
                velocity += new Vector2(0, 1);
            }
            if (Input.IsKeyDown(KeyCode.Left))
            {
                controller.SetCurrentSheet("ghost_left");
                velocity += new Vector2(-1, 0);
                if (Transform.Rotation > -20f)
                    Transform.Rotation -= 2f;
            }
            if (Input.IsKeyDown(KeyCode.Right))
            {
                controller.SetCurrentSheet("ghost_right");
                velocity += new Vector2(1, 0);
                if (Transform.Rotation < 20)
                    Transform.Rotation += 2f;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            if (Input.IsKeyDown(KeyCode.Space))
            {
                //Instantiate a PlayerWeapon
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
