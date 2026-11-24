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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spirit_energy_project.InputSubsystem;

namespace Spirit_energy_project
{
    public class PlayerWeapon : GameObject
    {
        private int damage;
        public int Damage { get=>damage; }
        private Random damageRange = new Random();
        private int energyCost;
        public int EnergyCost { get => energyCost; }
        private float attackTimer = 10f;
        private Texture2D sprite;
        AnimationController controller;
        SpriteRenderer renderer;
        BoxCollider collider = new BoxCollider();

        public PlayerWeapon()
        {
            damage = damageRange.Next(4, 7);
            renderer = new SpriteRenderer();
            controller = new AnimationController(renderer);
            collider.IsTrigger = true;
            addComponent(renderer);
            addComponent(controller);
            addComponent(collider);
            controller.AnimationSpeed = 16f;
            renderer.Origin = new Vector2(-16, 32);
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
        public override void Update(GameTime gameTime)
        {
            if (GameWorld.Instance.Player.IsAttacking)
            {
                attackTimer -= 0.75f;
                if(attackTimer <= 0)
                {
                    GameWorld.Instance.Player.IsAttacking = false;
                    attackTimer = 10f;
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
            if (!GameWorld.Instance.Player.IsAttacking)
            {
                collider.IsTrigger = false;
            }
            else { collider.IsTrigger = true; }

            base.Update(gameTime);
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!GameWorld.Instance.Player.IsAttacking)
            {
                return;
            }
            controller.Draw(spriteBatch);
            collider.DebugDraw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public override void OnCollisionStay(Collider other)
        {
            if (other.Owner == null)
            {
                return;
            }
            if (other.Owner.Name == null)
            {
                return;
            }
            
            if (other.Owner.Name.StartsWith("ghost"))
            {
                IEnemy Ienemies = (IEnemy)other.Owner;
                Ienemies.OnDamage(damage);
            }

            base.OnCollisionStay(other);
        }

    }
}