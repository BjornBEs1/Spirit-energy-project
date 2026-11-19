using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public abstract class Collider : Component
    {
        internal abstract event Action<Collider> OnCollision;
        internal abstract bool IsColliding(Collider other);
        public abstract void Update(Collider other);
        internal abstract Rectangle GetCollisionBox();

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            Texture2D pixel = ContentEngineManager.Instance.GetTexture2D("pixel");

            Rectangle rect = GetCollisionBox();
            Rectangle top = new Rectangle(rect.X, rect.Y, rect.Width, 1);
            Rectangle bottom = new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, 1);
            Rectangle left = new Rectangle(rect.X, rect.Y, 1, rect.Height);
            Rectangle right = new Rectangle(rect.X + rect.Width, rect.Y, 1, rect.Height);

            spriteBatch.Draw(pixel, top, null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, bottom, null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, left, null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, right, null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
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

        internal override event Action<Collider> OnCollision;

        public BoxCollider()
        {
        }

        public override void Update(Collider other)
        {
            if (IsColliding(other))
            {
                OnCollision.Invoke(other);
            }
        }

        internal override void componentAdded(GameObject owner)
        {
        }

        internal override Rectangle GetCollisionBox()
        {
            return box;
        }

        internal override bool IsColliding(Collider other)
        {
            return box.Intersects(other.GetCollisionBox());
        }
    }
}
