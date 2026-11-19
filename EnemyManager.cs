using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    internal class EnemyManager : GameObject
    {
        
        public EnemyManager() 
        { 
            
        }

        public override void Initialize()
        {
            SpawnRound();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.children.Count <= 0)
            {
                SpawnRound();
            }

            base.Update(gameTime);
        }
        public void SpawnRound()
        {
            for (int i = 0; i < 5 + GameWorld.Instance.rounds; i++)
            {
                GhostHunter newGhostHunter = new GhostHunter();
                newGhostHunter.Initialize();
                newGhostHunter.LoadContents();
                addChlid(newGhostHunter, this);
                children.Add(newGhostHunter);
            }
        }
    }
}
