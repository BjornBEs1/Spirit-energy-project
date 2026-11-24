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
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
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
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int Hp { get => hp; set => hp = value; }

        private float speed;
        private Vector2 direction;
        private Random random = new Random();
        private bool isAttacking = false;

        private SpriteRenderer spriteRenderer;
        private AnimationController controller;
        private BoxCollider collider;

        private bool justHit = false;
        public float dmgTakeTimer = 0f;
        public float suckTimer = 0f;
        private float takeDmgCooldown = 300f;
        private float suckCooldown = 300f;

        Texture2D circle;

        public GhostHunter()
        {
            int size = 160;
            circle = new Texture2D(GameWorld.Instance.GraphicsDevice, size, size);

            Color[] data = new Color[size * size];
            Vector2 center = new Vector2(size / 2f, size / 2f);
            float radius = size / 2f;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Vector2 diff = new Vector2(x, y) - center;
                    if (diff.LengthSquared() <= radius * radius)
                    {
                        data[y * size + x] = new Color(255, 255, 0, 128);

                    }
                    else
                    {
                        data[y * size + x] = Color.Transparent;
                    }
                }
            }
            circle.SetData(data);

            collider = new BoxCollider();
            AddComponent(collider);

            spriteRenderer = new SpriteRenderer();
            spriteRenderer.LayerDepth = 0f;
            AddComponent(spriteRenderer);

            controller = new AnimationController(spriteRenderer);
            controller.AnimationSpeed = 2.5f;
            AddComponent(controller);

            maxHp = 10;
            hp = maxHp;
            speed = 50f;
            direction = Vector2.Zero;

            int randomPosition = random.Next(1, 5);
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

            dmgTakeTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            suckTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (isAttacking && suckTimer >= suckCooldown)
            {
                suckTimer = 0f;
                GameWorld.Instance.SpiritEnergy.RemoveEnergy(4);
            }

            if (dmgTakeTimer >= takeDmgCooldown)
            {
                dmgTakeTimer = 0f;
                justHit = false;
            }

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

            if (Vector2.Distance(Transform.Position, playerPos) > 80f)
            {
                Transform.Position += direction * speed * Time.DeltaTime;
                isAttacking = false;
            }
            else
            {
                if (!justHit)
                {
                    isAttacking = true;
                }
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isAttacking)
            {
                spriteBatch.Draw(circle, Transform.Position + spriteRenderer.SpriteSize / 2, null, Color.LightGoldenrodYellow * 0.5f, 0, new Vector2(160 / 2), 1, SpriteEffects.None, 0.5f);
            }
            controller.Draw(spriteBatch);
            collider.DebugDraw(spriteBatch);
            // spriteBatch.Draw(sprite, Transform.Position, CurrentSprite, Color.White);
        }

        public override void OnDestroy()
        {
            SpiritOrb spiritOrb = new SpiritOrb(10);
            spiritOrb.Transform = Transform;
            spiritOrb.Initialize();
            spiritOrb.LoadContents();
            addChild(spiritOrb, Parent);
            base.OnDestroy();
        }

        public void OnDamage(int dmg)
        {
            Transform.Position += GameWorld.Instance.Player.Facing * 1000 * Time.DeltaTime; //Knockback 
            if (!justHit)
            {
                hp -= dmg;
                justHit = true;
                Debug.WriteLine($"Ghost hunter took {dmg} Damage");
            }

            if (hp <= 0)
            {
                Destroy();
            }
        }
    }
}
