/*
 * File: SpiritOrbs.cs
 * File Created: 20 Nov 2026
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
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spirit_energy_project
{
    internal class SpiritOrb : GameObject
    {
        int points;
        AnimationController controller;
        BoxCollider collider;
        public SpiritOrb(int _points)
        {
            points = _points;
            collider = new BoxCollider
            {
                IsTrigger = true,
                Bounds = new Vector2(10, 10)
            };
            AddComponent(collider);
            SpriteRenderer spriteRenderer = new SpriteRenderer();
            AddComponent(spriteRenderer);
            controller = new AnimationController(spriteRenderer);
            AddComponent(controller);
            controller.AnimationSpeed = 10;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContents()
        {
            controller.SetSpriteSheet("SpiritOrb", "SpiritOrbSheet", "SpiritOrb", new Rectangle(0, 0, 128, 32), 32, 32);
        }
        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            collider.DebugDraw(spriteBatch);
            controller.Draw(spriteBatch);
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.Owner is Player player)
            {
                GameWorld.Instance.SpiritEnergy.Energy += 5;
                Destroy();
            }
        }
    }
}
