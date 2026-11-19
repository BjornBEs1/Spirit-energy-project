using Microsoft.Xna.Framework;

namespace Spirit_energy_project
{
    public class Rigidbody2D : Component
    {
        public Vector2 Velocity { get; set; }

        internal override void componentAdded(GameObject owner)
        {
        }
    }
}
