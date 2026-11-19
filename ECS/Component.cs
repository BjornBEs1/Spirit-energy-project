using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public abstract class Component
    {
        /// <summary>
        /// The parent of the given component
        /// </summary>
        public GameObject Owner { get; set; }

        internal abstract void componentAdded(GameObject owner);
    }
}
