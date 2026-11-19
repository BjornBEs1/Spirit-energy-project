using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private float speed;
        private Vector2 direction;
        private Random random = new Random();
        public GhostHunter()
        {
            speed = 100f;
            direction = Vector2.Zero;
            int randomPosition = random.Next(1, 4);
            if (randomPosition == 1)
            {
                Transform.Position = new Vector2(random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth), 0);
            }
            else if (randomPosition == 2)
            {
                Transform.Position = new Vector2(random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth), GraphicsDeviceManager.DefaultBackBufferHeight);
            }
            else if (randomPosition == 3)
            {
                Transform.Position = new Vector2(0, random.Next(0, GraphicsDeviceManager.DefaultBackBufferHeight));
            }
            else if (randomPosition == 4)
            {
                Transform.Position = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth, random.Next(0, GraphicsDeviceManager.DefaultBackBufferHeight));
            }


        }

        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int Hp { get => hp; set => hp = value; }
       
        public override void LoadContents()
        {
            base.LoadContents();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void Move(GameTime gameTime)
        {
            Transform.Position += ((direction * speed) * Time.DeltaTime);
        }
    }
}
