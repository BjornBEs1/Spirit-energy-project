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
        public Rectangle box
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
                    (int)(Owner.Transform.Position.X - spriteSize.X / 2),
                    (int)(Owner.Transform.Position.Y - spriteSize.Y / 2),
                    (int)(spriteSize.X),
                    (int)(spriteSize.Y)
                );
            }
        }

        internal override event Action<Collider> OnCollisionEnter;
        internal override event Action<Collider> OnCollisionStay;
        internal override event Action<Collider> OnCollisionExit;
        internal override event Action<Collider> OnTriggerEnter;
        internal override event Action<Collider> OnTriggerStay;
        internal override event Action<Collider> OnTriggerExit;

        public BoxCollider()
        {
        }

        internal override Rectangle GetWorldBounds()
        {
            return box;
        }

        public override void Update(Collider other)
        {
        }

        internal override void callOnCollisionEnter(Collider other)
        {
            OnCollisionEnter.Invoke(other);
        }
        internal override void callOnCollisionStay(Collider other)
        {
            OnCollisionStay.Invoke(other);
        }
        internal override void callOnCollisionExit(Collider other)
        {
            OnCollisionExit.Invoke(other);
        }
        internal override void callOnTriggerEnter(Collider other)
        {
            OnTriggerEnter.Invoke(other);
        }
        internal override void callOnTriggerStay(Collider other)
        {
            OnTriggerStay.Invoke(other);
        }
        internal override void callOnTriggerExit(Collider other)
        {
            OnTriggerExit.Invoke(other);
        }

        internal override void componentAdded(GameObject owner)
        {
        }
    }
}
