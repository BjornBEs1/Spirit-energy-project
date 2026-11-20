using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    internal class EnemyManager : GameObject
    {
        private Texture2D ghostHunterSprite;
        public EnemyManager() 
        {
            
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void LoadContents()
        {
            ContentEngineManager.Instance.LoadTexture2D("GhostHunterSprite", "Enemy");
            base.LoadContents();
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
                Debug.WriteLine("Spawned enemy");
                GhostHunter newGhostHunter = new GhostHunter();
                newGhostHunter.Initialize();
                newGhostHunter.LoadContents();
                addChlid(newGhostHunter, this);
                children.Add(newGhostHunter);
            }
        }
    }
}
