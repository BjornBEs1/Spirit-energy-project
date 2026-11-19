using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    internal class GhostHunter : GameObject, IEnemy
    {
        int hp;
        int maxHp;
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int Hp { get => hp; set => hp = value; }
    }
}
