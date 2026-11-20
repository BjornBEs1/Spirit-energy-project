/*
 * File: ICollisionEvents.cs
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
using Spirit_energy_project;

namespace GravityJump.FSESS.ECS.ColliderSystem
{
    public interface ICollisionEvents
    {
        void OnCollisionEnter(Collider other);
        void OnCollisionStay(Collider other);
        void OnCollisionExit(Collider other);
    }

}
