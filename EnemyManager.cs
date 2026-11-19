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
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameWorld.Instance)
            {

            }
            base.Update(gameTime);
        }
        public void SpawnRound()
        {

        }
    }
}
