/*
 * File: CollisionResolver.cs
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

namespace Spirit_energy_project.ECS.ColliderSystem
{
    public static class CollisionResolver
    {
        public static Vector2 ComputePushOut(Rectangle a, Rectangle b)
        {
            float overlapX =
                MathF.Min(a.Right, b.Right) -
                MathF.Max(a.Left, b.Left);

            float overlapY =
                MathF.Min(a.Bottom, b.Bottom) -
                MathF.Max(a.Top, b.Top);

            if (overlapX <= 0 || overlapY <= 0)
                return Vector2.Zero;

            Vector2 centerA = new(
                a.Left + a.Width / 2f,
                a.Top + a.Height / 2f
            );

            Vector2 centerB = new(
                b.Left + b.Width / 2f,
                b.Top + b.Height / 2f
            );

            if (overlapX < overlapY)
            {
                return centerA.X < centerB.X
                    ? new Vector2(-overlapX, 0)
                    : new Vector2(overlapX, 0);
            }
            else
            {
                return centerA.Y < centerB.Y
                    ? new Vector2(0, -overlapY)
                    : new Vector2(0, overlapY);
            }
        }
    }

}
