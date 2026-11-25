/*
 * File: EnemyManager.cs
 * File Created: 20 Nov 2026
 * Author: Nicolai
 */

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
        TileMap tileMap;
        public EnemyManager() 
        {
            tileMap = new TileMap(32, 32);
            tileMap.Width = GameWorld.Instance.graphics.PreferredBackBufferWidth / 32;
            tileMap.Height = GameWorld.Instance.graphics.PreferredBackBufferHeight / 32;
            tileMap.LayerDepth = 1f;
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }
        
        /// <summary>
        /// Loads ghost hunter and spiritorb sprites
        /// </summary>
        public override void LoadContents()
        {
            ContentEngineManager.Instance.LoadTexture2D("GhostHunterSprite", "Enemy");
            ContentEngineManager.Instance.LoadTexture2D("SpiritOrbSprite", "SpiritOrb");
            ContentEngineManager.Instance.LoadTexture2D("MapTiles", "TileFloor");
            
            tileMap.SetSprite("MapTiles");

            base.LoadContents();
        }


        /// <summary>
        /// Checks if all ghost hunter are dead.
        /// if all ghost hunters are dead then start a new round.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (this.children.Count <= 0)
            {
                GameWorld.Instance.rounds++;
                SpawnRound();
            }

            base.Update(gameTime);
        }


        /// <summary>
        /// Loads ghost hunters content.
        /// Set each ghost hunters name to ghost{i} (ghost{1,2,3,4,5}).
        /// Adds the ghost hunters as children of the enemy manager
        /// </summary>
        public void SpawnRound()
        {
            for (int i = 0; i < 5 + GameWorld.Instance.rounds; i++)
            {
                Debug.WriteLine("Spawned enemy");
                GhostHunter newGhostHunter = new GhostHunter();
                newGhostHunter.Initialize();
                newGhostHunter.LoadContents();
                newGhostHunter.Name = $"ghost{i}";
                addChild(newGhostHunter, this);
                // children.Add(newGhostHunter);
            }
        }

        //Draws spritebatch
        public override void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
