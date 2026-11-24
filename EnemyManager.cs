/*
 * File: EnemyManager.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
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

        public override void LoadContents()
        {
            ContentEngineManager.Instance.LoadTexture2D("GhostHunterSprite", "Enemy");
            ContentEngineManager.Instance.LoadTexture2D("SpiritOrbSprite", "SpiritOrb");
            ContentEngineManager.Instance.LoadTexture2D("MapTiles", "TileFloor");
            
            tileMap.SetSprite("MapTiles");

            base.LoadContents();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.children.Count <= 0)
            {
                GameWorld.Instance.rounds++;
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
                newGhostHunter.Name = $"ghost{i}";
                addChild(newGhostHunter, this);
                // children.Add(newGhostHunter);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
