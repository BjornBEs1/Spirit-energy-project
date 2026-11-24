/*
 * File: BoxCollider.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System;
using Microsoft.Xna.Framework;
using Spirit_energy_project.ECS.ColliderSystem;

namespace Spirit_energy_project
{
    public class BoxCollider : Collider
    {
        private Rectangle box
        {
            get
            {
                Vector2 spriteSize = Vector2.One;
                if (Owner.TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    if (spriteRenderer.Sprite != null)
                    {
                        spriteSize = spriteRenderer.SpriteSize;
                    }
                }
                return new Rectangle(
                    (int)(Owner.Transform.Position.X + OffsetX),
                    (int)(Owner.Transform.Position.Y + OffsetY),
                    (int)(spriteSize.X + Bounds.X),
                    (int)(spriteSize.Y + Bounds.Y)
                );
            }
        }


        public BoxCollider()
        {
            Bounds = Vector2.Zero;
        }

        internal override Rectangle GetWorldBounds()
        {
            return box;
        }

        public override void Update(Collider other)
        {
        }

        internal override void componentAdded(GameObject owner)
        {
        }
        public override int GetHashCode() => HashCode.Combine(box, Bounds, OffsetX, OffsetY, IsTrigger, IsTouching, Owner);
        public override bool Equals(object obj)
        {
            if (obj is CollisionPair other && Equals(other))
            {
                return true;
            }
            return base.Equals(obj);
        }
    }
}
