/*
 * File: CollisionSystem.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Spirit_energy_project.ECS.ColliderSystem
{
    public sealed class CollisionSystem
    {
        private readonly HashSet<CollisionPair> previous = new();
        internal static readonly HashSet<CollisionPair> current = new();
        internal static Collider[] currentColliders;

        public void Step(IReadOnlyList<Collider> colliders)
        {
            current.Clear();
            currentColliders = colliders.ToArray();

            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    Collider a = colliders[i];
                    Collider b = colliders[j];

                    if (a == b)
                    {
                        continue;
                    }

                    if (!a.GetWorldBounds().Intersects(b.GetWorldBounds()))
                        continue;

                    // Track collision pair
                    CollisionPair pair = new CollisionPair(a, b);
                    current.Add(pair);

                    // === RESOLUTION ===
                    ResolveCollision(a, b);
                }
            }

            ResolveEvents();
            SwapBuffers();
        }

        internal void ResolveCollision(Collider a, Collider b)
        {
            // Only resolve if exactly one is dynamic
            if (a.Owner.IsStatic == b.Owner.IsStatic)
                return;

            if (a.IsTrigger || b.IsTrigger)
            {
                return;
            }

            Collider dynamicColl = a.Owner.IsStatic ? b : a;
            Collider staticColl = a.Owner.IsStatic ? a : b;

            Vector2 push = CollisionResolver.ComputePushOut(
                dynamicColl.GetWorldBounds(),
                staticColl.GetWorldBounds()
            );

            if (push == Vector2.Zero)
                return;

            if (dynamicColl.Owner.TryGetComponent(out Transform2D parent))
            {
                parent.Position += push;
                dynamicColl.Owner.velocity = new Vector2();
            }
        }

        private void ResolveEvents()
        {
            foreach (CollisionPair pair in current)
            {
                if (!previous.Contains(pair))
                    pair.Enter();
                else
                    pair.Stay();
            }

            foreach (CollisionPair pair in previous)
            {
                if (!current.Contains(pair))
                    pair.Exit();
            }
        }

        private void SwapBuffers()
        {
            previous.Clear();
            foreach (CollisionPair pair in current)
                previous.Add(pair);
        }
    }
}