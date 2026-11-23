/*
 * File: GameWorld.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spirit_energy_project.ECS.ColliderSystem;
using Spirit_energy_project.InputSubsystem;

namespace Spirit_energy_project
{
    public class GameWorld : Game
    {
        static internal GameWorld Instance;
        public GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private InputSystem InputSystem { get; set; }
        public List<GameObject> GameObjects;
        public Player Player { get; set; }
        public SpiritEnergyMeter SpiritEnergy { get; set; }
        internal CollisionSystem CollisionSystem { get; set; }

        public int rounds = 0;

        public GameWorld()
        {
            CollisionSystem = new CollisionSystem();
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            SpiritEnergy = new SpiritEnergyMeter();
            Player = new Player();

            new ContentEngineManager(Content);

            InputSystem = new InputSystem();

            GameObjects = new List<GameObject>();

            GameObjects.Add(Player);
            GameObjects.Add(SpiritEnergy);
            GameObjects.Add(new EnemyManager());
            // add player
            // add enemyManager
        }

        protected override void Initialize()
        {
            GameObjects.ForEach(x => x.intInitialize());

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentEngineManager.Instance.LoadTexture2D("pixel", "pixel");

            GameObjects.ForEach(x => x.intLoadContents());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            Time.Update(gameTime);

            InputSystem.Update();

            List<Collider> colliders = new List<Collider>();
            GameObjects.ForEach(x => colliders.AddRange(x.intUpdate(gameTime)));
            PhysicsStep(colliders);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void PhysicsStep(List<Collider> lastFrameColliders)
        {
            for (int iteration = 0; iteration < 4; iteration++)
            {
                GameWorld.Instance.CollisionSystem.Step(lastFrameColliders);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            GameObjects.ForEach(x => x.intDraw(spriteBatch));
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
