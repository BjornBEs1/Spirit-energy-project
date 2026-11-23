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
        private int energyCost;
        public int EnergyCost { get => energyCost; }
        private Random damageRange = new Random();
        private Texture2D sprite;
        AnimationController controller;
        SpriteRenderer renderer;
        public PlayerWeapon()
        {
            damage = damageRange.Next(4, 7);
            renderer = new SpriteRenderer();
            controller = new AnimationController(renderer);
            addComponent(renderer);
            addComponent(controller);
            controller.AnimationSpeed = 7.5f;
            renderer.Origin = new Vector2(-16, 16);
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
            Vector2 playerPos = GameWorld.Instance.Player.Transform.Position;
            Transform.Position = playerPos;
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            controller.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}