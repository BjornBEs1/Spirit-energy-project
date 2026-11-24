/*
 * File: Collider.cs
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
using Spirit_energy_project.ECS.ColliderSystem;

namespace Spirit_energy_project
{
    public abstract class Collider : Component
    {
        public Vector2 Bounds { get; set; }
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        public bool IsTrigger { get; set; } = false;

        public bool IsTouching { get; private set; }

        internal abstract event Action<Collider> OnCollisionEnter;
        internal abstract event Action<Collider> OnCollisionStay;
        internal abstract event Action<Collider> OnCollisionExit;
        internal abstract event Action<Collider> OnTriggerEnter;
        internal abstract event Action<Collider> OnTriggerStay;
        internal abstract event Action<Collider> OnTriggerExit;
        public abstract void Update(Collider other);
        internal abstract Rectangle GetWorldBounds();

        internal abstract void callOnCollisionEnter(Collider other);
        internal abstract void callOnCollisionStay(Collider other);
        internal abstract void callOnCollisionExit(Collider other);
        internal abstract void callOnTriggerEnter(Collider other);
        internal abstract void callOnTriggerStay(Collider other);
        internal abstract void callOnTriggerExit(Collider other);

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            Texture2D pixel = ContentEngineManager.Instance.GetTexture2D("pixel");

            Rectangle rect = GetWorldBounds();
            Rectangle top = new Rectangle(rect.X, rect.Y, rect.Width, 1);
            Rectangle bottom = new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, 1);
            Rectangle left = new Rectangle(rect.X, rect.Y, 1, rect.Height);
            Rectangle right = new Rectangle(rect.X + rect.Width, rect.Y, 1, rect.Height);

            spriteBatch.Draw(pixel, top, null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, bottom, null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, left, null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, right, null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
