/*
 * File: PlayerWeapon.cs
 * File Created: 23 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 23 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spirit_energy_project.InputSubsystem;

namespace Spirit_energy_project
{
    public class PlayerWeapon : GameObject
    {
        private int damage;
        public int Damage { get => damage; }

        private int energyCost;
        public int EnergyCost { get => energyCost; }

        private Random damageRange = new Random();

        private float attackTimer = 2000f;
        private const float StartTimer = 2000f;

        private AnimationController controller;
        private SpriteRenderer renderer;
        private BoxCollider collider;
        
        private bool isAttacking;

        public PlayerWeapon()
        {
            isAttacking = false;

            damage = damageRange.Next(4, 7);
            
            energyCost = 12;
            
            renderer = new SpriteRenderer();
            renderer.Origin = new Vector2(-16, 32);
            AddComponent(renderer);

            controller = new AnimationController(renderer);
            controller.AnimationSpeed = 16f;
            AddComponent(controller);

            collider = new BoxCollider();
            collider.IsTrigger = true;
            AddComponent(collider);

        }

        public override void Initialize()
        {

            base.Initialize();
        }
        public override void LoadContents()
        {
            //sprite = ContentEngineManager.Instance.GetTexture2D("GhostFistSprite");
            controller.SetSpriteSheet("GhostPunch01", "GhostFistSpriteSheet", "punch01", new Rectangle(0, 0, 128, 64), 32, 64);
            controller.SetSpriteSheet("GhostPunch02", "GhostFistSpriteSheet", "punch02", new Rectangle(0, 64, 128, 64), 32, 64);
            controller.SetCurrentSheet("punch01");
            base.LoadContents();
        }

        public void Attack()
        {
            if (attackTimer > 0)
            {
                return;
            }
            GameWorld.Instance.SpiritEnergy.RemoveEnergy(EnergyCost);
            controller.AnimTime = 0;
            isAttacking = true;
            attackTimer = StartTimer;
        }

        public override void Update(GameTime gameTime)
        {
            if (attackTimer > 0)
            {
                attackTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (attackTimer <= 0)
                {
                    attackTimer = 0;
                    isAttacking = false;
                }
            }
            Vector2 playerPos = GameWorld.Instance.Player.Transform.Position;
            Transform.Position = playerPos;
            if (GameWorld.Instance.Player.Facing.X > 0) //Right
            {
                Transform.Rotation = 0;
                collider.Bounds = new Vector2(16, 16);
                collider.OffsetX = 8;
                collider.OffsetY = -40;
            }
            else if (GameWorld.Instance.Player.Facing.X < 0) //Left
            {
                Transform.Rotation = 180;
                collider.Bounds = new Vector2(16, 16);
                collider.OffsetX = -56;
                collider.OffsetY = -40;
            }
            else if (GameWorld.Instance.Player.Facing.Y > 0) //Down
            {
                Transform.Rotation = 90;
                collider.Bounds = new Vector2(48, -16);
                collider.OffsetX = -40;
                collider.OffsetY = 8;
            }
            else if (GameWorld.Instance.Player.Facing.Y < 0) //Up
            {
                Transform.Rotation = 270;
                collider.Bounds = new Vector2(48, -16);
                collider.OffsetX = -40;
                collider.OffsetY = -56;
            }
            if (!isAttacking)
            {
                collider.IsTrigger = false;
            }
            else
            {
                collider.IsTrigger = true;
            }

            if (isAttacking)
            {
                if (controller.AnimTime >= controller.AnimCount - 1)
                {
                    isAttacking = false;
                }
            }

            base.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isAttacking)
            {
                return;
            }

            controller.Draw(spriteBatch);
            collider.DebugDraw(spriteBatch);
        }

        public override void OnCollisionStay(Collider other)
        {
            if (other.Owner is IEnemy enemy)
            {
                enemy.OnDamage(damage);
            }

            base.OnCollisionStay(other);
        }

    }
}