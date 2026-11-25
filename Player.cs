using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spirit_energy_project.InputSubsystem;


namespace Spirit_energy_project
{
    /// <summary>
    /// Author: Magnus
    /// Class representing the player character.
    /// </summary>
    public class Player : GameObject
    {
        internal int Points { get; set; }
        private float speed = 150;
        public Vector2 Facing { get; set; } //A seperate variable from velocity, which represents which direction the player is facing.
        AnimationController controller;
        SpriteRenderer renderer;
        BoxCollider collider;
        PlayerWeapon ghostPunch;

        /// <summary>
        /// Creates the player and initializes its renderer, animation controller,
        /// and collision component.
        /// </summary>
        public Player()
        {
            Name = "Player";
            collider = new BoxCollider
            {
                IsTrigger = true,
            };
            AddComponent(collider);
            collider.OffsetX = -16;
            collider.OffsetY = -8;

            renderer = new SpriteRenderer();
            AddComponent(renderer);
            renderer.Origin = new Vector2(16, 8); //sets the origin to be centered on the ghosts head.
            renderer.LayerDepth = 0f;

            controller = new AnimationController(renderer);
            AddComponent(controller);
            controller.AnimationSpeed = 10;
        }
        /// <summary>
        /// Updates the player's health, input, and movement.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (GameWorld.Instance.SpiritEnergy.Energy <= 0) //destroys the player object if no spirit energy remains.
            {
                GameWorld.Instance.SpiritEnergy.Energy = 0;
                Debug.WriteLine("player is dead");
                GameWorld.Instance.Exit();
            }
            HandleInput();            
            Transform.Position += velocity * speed * Time.DeltaTime;
            
            if (velocity.X == 0)//Normalizes the sprites rotation, if the player isn't moving horizontally.
            {
                NormalizeRotation();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            controller.Draw(spriteBatch);
            collider.DebugDraw(spriteBatch);
        }
        /// <summary>
        /// Creates and attaches the player's weapon, then sets the starting position.
        /// </summary>
        public override void Initialize()
        {
            Debug.WriteLine("Added Weapon");
            ghostPunch = new PlayerWeapon();
            ghostPunch.LoadContents();
            ghostPunch.Name = $"ghostPunch";
            addChild(ghostPunch, this);
            
            //Sets the position of the player to the center of the screen.
            Transform.Position = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth, GraphicsDeviceManager.DefaultBackBufferHeight) / 2; 
        }
        /// <summary>
        /// Loads the sprite sheet, and sets up 4 animations for each direction.
        /// </summary>
        public override void LoadContents()
        {
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_right", new Rectangle(0, 0, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_left", new Rectangle(0, 32, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_down", new Rectangle(0, 64, 128, 32), 32, 32);
            controller.SetSpriteSheet("GhostSprite", "GhostSpriteSheet", "ghost_up", new Rectangle(0, 96, 128, 32), 32, 32);
        }
        /// <summary>
        /// Processes keyboard input and updates the player's movement,
        /// facing direction, animation, and attack state.
        /// </summary>
        protected void HandleInput()
        {
            velocity = Vector2.Zero; //resets the movement direction of no input is recieved.

            if (Input.IsKeyDown(KeyCode.Up))
            {
                controller.SetCurrentSheet("ghost_up");
                velocity += new Vector2(0, -1);
                Facing = velocity;
            }
            if (Input.IsKeyDown(KeyCode.Down))
            {
                controller.SetCurrentSheet("ghost_down");
                velocity += new Vector2(0, 1);
                Facing = velocity;
            }
            if (Input.IsKeyDown(KeyCode.Left))
            {
                controller.SetCurrentSheet("ghost_left");
                velocity += new Vector2(-1, 0);
                if (Transform.Rotation > -20f) //gradually rotates the sprite to -20 degrees, creating a floating effect.
                {
                    Transform.Rotation -= 2f;
                }
                Facing = velocity;
            }
            if (Input.IsKeyDown(KeyCode.Right))
            {
                controller.SetCurrentSheet("ghost_right");
                velocity += new Vector2(1, 0);
                if (Transform.Rotation < 20) //gradually rotates the sprite to +20 degrees.
                {
                    Transform.Rotation += 2f;
                }
                Facing = velocity;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            if (Input.IsKeyDown(KeyCode.Space)) //Attack input.
            {
                ghostPunch.Attack();
            }
        }
        /// <summary>
        /// Gradually rotates the player back to the default rotation.
        /// </summary>
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
