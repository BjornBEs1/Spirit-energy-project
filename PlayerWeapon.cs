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
    /// <summary>
    /// Author: Magnus
    /// Represents the player's ghost punch ability. The class handles attack timing,
    /// damage, energy consumption, animation, positioning, and collision detection.
    /// The weapon object is created along with the player, and always exists in the game world, but is only rendered when the player attacks.
    /// This saves on processing power, as the object and textures, are only loaded once.
    /// </summary>
    public class PlayerWeapon : GameObject
    {
        private int damage; //The abilitis base damage.
        public int Damage { get => damage; }

        private int energyCost; //energy cost to use the ability.
        public int EnergyCost { get => energyCost; }

        private Random damageRange = new Random(); //Random variable to set a damage range for the ability

        private float attackTimer = 200f; //delay timer, controls how often the ability can be used.
        private const float StartTimer = 200f;
        private AnimationController controller;
        private SpriteRenderer renderer;
        private BoxCollider collider;        
        private bool isAttacking;
        /// <summary>
        /// Creates the weapon and initializes its damage, energy cost, renderer,
        /// animation controller, and collision box.
        /// </summary>
        public PlayerWeapon()
        {
            isAttacking = false;

            damage = damageRange.Next(4, 7);
            
            energyCost = 8;
            
            renderer = new SpriteRenderer();
            renderer.Origin = new Vector2(-32, 32); //sets the origin to the middle of the left edge, on the sprite.
            AddComponent(renderer);

            controller = new AnimationController(renderer);
            controller.AnimationSpeed = 16f;
            AddComponent(controller);

            collider = new BoxCollider();
            collider.IsTrigger = true;
            AddComponent(collider);

        }
        /// <summary>
        /// Loads the sprite sheet, and sets up 2 different animation for punching. Only one is used in the prototype.
        /// </summary>
        public override void LoadContents()
        {
            controller.SetSpriteSheet("GhostPunch01", "GhostFistSpriteSheet", "punch01", new Rectangle(0, 0, 128, 64), 32, 64);
            controller.SetSpriteSheet("GhostPunch02", "GhostFistSpriteSheet", "punch02", new Rectangle(0, 64, 128, 64), 32, 64);
            controller.SetCurrentSheet("punch01");
            base.LoadContents();
        }
        /// <summary>
        /// Starts an attack if the weapon's cooldown has expired.
        /// </summary>
        public void Attack()
        {
            if (attackTimer > 0) //does nothing if the timer hasn't reached 0.
            {
                return;
            }
            GameWorld.Instance.SpiritEnergy.RemoveEnergy(EnergyCost); //drains the abilitis energy cost from the spirit meter
            controller.AnimTime = 0; //resets the animation to the first frame for consistency.
            isAttacking = true;
            attackTimer = StartTimer; //Resets the attack timer.
        }
        /// <summary>
        /// Updates the weapon's cooldown, position, orientation,
        /// collider, and attack animation state.
        /// </summary>
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
            Vector2 playerPos = GameWorld.Instance.Player.Transform.Position; //The weapon is constantly following the players position.
            Transform.Position = playerPos;
            //Rotates the weapon around the player, so it matches the players facing. Also adjusts the colliderbox to match the animation.
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
            
            if (!isAttacking) //If the player isn't attacking, the collider box is disabled.
            {
                collider.IsTrigger = false;
            }
            else
            {
                collider.IsTrigger = true;
            }

            if (isAttacking) //sets isAttacking to false when the animation has finished playing.
            {
                if (controller.AnimTime >= controller.AnimCount - 1)
                {
                    isAttacking = false;
                }
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the weapon only while an attack is active.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isAttacking)
            {
                return;
            }
            controller.Draw(spriteBatch);
            //collider.DebugDraw(spriteBatch);
            base.Draw(spriteBatch);
        }
        /// <summary>
        /// Damages an enemy when the weapon's trigger collider remains
        /// in contact with it.
        /// </summary>
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