/*
 * File: CollisionPair.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System;
using Spirit_energy_project.ECS;

namespace Spirit_energy_project.ECS.ColliderSystem
{
    public readonly struct CollisionPair : IEquatable<CollisionPair>
    {
        public readonly Collider A;
        public readonly Collider B;

        public CollisionPair(Collider a, Collider b)
        {
            // enforce deterministic order
            if (ReferenceEquals(a, b))
                throw new InvalidOperationException();

            if (a.GetHashCode() < b.GetHashCode())
            {
                A = a;
                B = b;
            }
            else
            {
                A = b;
                B = a;
            }
        }

        public bool Equals(CollisionPair other)
            => ReferenceEquals(A, other.A) && ReferenceEquals(B, other.B);

        public override bool Equals(object obj)
            => obj is CollisionPair other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(A, B);

        public void Enter()
        {
            if (!B.IsTrigger)
            {
                A.callOnCollisionEnter(B);
            }
            else
            {
                A.callOnTriggerEnter(B);
            }

            if (!A.IsTrigger)
            {
                B.callOnCollisionEnter(A);
            }
            else
            {
                B.callOnTriggerEnter(A);
            }
        }

        public void Stay()
        {
            if (!B.IsTrigger)
            {
                A.callOnCollisionStay(B);
            }
            else
            {
                A.callOnTriggerStay(B);
            }

            if (!A.IsTrigger)
            {
                B.callOnCollisionStay(A);
            }
            else
            {
                B.callOnTriggerStay(A);
            }
        }

        public void Exit()
        {
            if (!B.IsTrigger)
            {
                A.callOnCollisionExit(B);
            }
            else
            {
                A.callOnTriggerExit(B);
            }

            if (!A.IsTrigger)
            {
                B.callOnCollisionExit(A);
            }
            else
            {
                B.callOnTriggerExit(A);
            }
        }
    }
}