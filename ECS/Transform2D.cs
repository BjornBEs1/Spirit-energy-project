/*
 * File: Transform2D.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public class Transform2D : Component
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public Vector2 Center => new Vector2(Scale.X / 2, Scale.Y / 2) + Position;

        public Transform2D()
        {
            Scale = Vector2.One;
            Position = new Vector2();
        }

        internal override void componentAdded(GameObject owner)
        {
        }

        public void Translate(Vector2 vector3)
        {
            Position += vector3;
        }
    }
}
