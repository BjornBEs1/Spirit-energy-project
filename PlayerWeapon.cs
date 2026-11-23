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
    public class PlayerAbility : GameObject
    {
        private int damage;
        public int Damage { get=>damage; }
        private Random damageRange = new Random();
        AnimationController controller;
        SpriteRenderer renderer;
        public PlayerAbility()
        {
            damage = damageRange.Next(4, 7);
            renderer = new SpriteRenderer();
            controller = new AnimationController(renderer);
            addComponent(renderer);
            addComponent(controller);
        }
        public override void Initialize()
        {
            controller.SetCurrentSheet("punch01");
            base.Initialize();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            controller.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}