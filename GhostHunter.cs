/*
 * File: GhostHunter.cs
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
    internal class GhostHunter : GameObject, IEnemy
    {
        int hp;
        int maxHp;
        private float speed;
        private Vector2 direction;
        private Random random = new Random();
        private bool isAttacking = false;
        AnimationController controller;

        public GhostHunter()
        {
            BoxCollider collider = new BoxCollider();
            AddComponent(collider);
            SpriteRenderer spriteRenderer = new SpriteRenderer();
            AddComponent(spriteRenderer);
            controller = new AnimationController(spriteRenderer);
            AddComponent(controller);
            controller.AnimationSpeed = 2;
            maxHp = 10;
            hp = maxHp;
            speed = 50f;
            direction = Vector2.Zero;
            int randomPosition = random.Next(1, 4);
            if (randomPosition == 1)
            {
                Transform.Position = new Vector2(random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth), 0);
            }
            else if (randomPosition == 2)
            {
                Transform.Position = new Vector2(random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth), GraphicsDeviceManager.DefaultBackBufferHeight);
            }
            else if (randomPosition == 3)
            {
                Transform.Position = new Vector2(0, random.Next(0, GraphicsDeviceManager.DefaultBackBufferHeight));
            }
            else if (randomPosition == 4)
            {
                Transform.Position = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth, random.Next(0, GraphicsDeviceManager.DefaultBackBufferHeight));
            }
        }

        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int Hp { get => hp; set => hp = value; }

        public override void LoadContents()
        {
            controller.SetSpriteSheet("HunterSpriteSheet", "Enemy", "LeftWalk", new Rectangle(0, 0, 64, 32), 32, 32);
            controller.SetSpriteSheet("HunterSpriteSheet", "Enemy", "RightWalk", new Rectangle(0, 32, 64, 32), 32, 32);
            controller.SetSpriteSheet("HunterSpriteSheet", "Enemy", "AttackRight", new Rectangle(0, 32, 32, 32), 32, 32);
            controller.SetSpriteSheet("HunterSpriteSheet", "Enemy", "AttackLeft", new Rectangle(0, 0, 32, 32), 32, 32);
            base.LoadContents();
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 playerPos = GameWorld.Instance.Player.Transform.Position;
            Vector2 difference = Transform.Position - playerPos;

            direction = Vector2.Normalize(-difference);
            if (difference.X < 0)
            {
                if (isAttacking)
                {
                    controller.SetCurrentSheet("AttackRight");
                }
                else
                {
                    controller.SetCurrentSheet("RightWalk");
                }
            }
            else
            {
                if (isAttacking)
                {
                    controller.SetCurrentSheet("AttackLeft");
                    
                }
                else
                {
                    controller.SetCurrentSheet("LeftWalk");
                }
            }

            if (Vector2.Distance(Transform.Center, playerPos) > 80f)
            {
                Move(gameTime);
                isAttacking = false;
            }
            else
            {
                isAttacking = true;
            }

            base.Update(gameTime);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            controller.Draw(spriteBatch);
            // spriteBatch.Draw(sprite, Transform.Position, CurrentSprite, Color.White);

            base.Draw(spriteBatch);
        }




        public void Move(GameTime gameTime)
        {
            Transform.Position += ((direction * speed) * Time.DeltaTime);
        }
    }
}
