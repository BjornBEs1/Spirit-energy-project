using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spirit_energy_project
{
    public interface IEnemy
    {
        public int MaxHp { get; set; }
        public int Hp { get; set; }
    }
}
