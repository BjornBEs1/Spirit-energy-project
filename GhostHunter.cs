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
        private Texture2D sprite;
        private Random random = new Random();
        private Rectangle CurrentSprite = new Rectangle(0,0,32,32);
        public GhostHunter()
        {
            speed = 50f;
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
            sprite = ContentEngineManager.Instance.GetTexture2D("GhostHunterSprite");
            base.LoadContents();
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 playerPos = GameWorld.Instance.Player.Transform.Position;
            Vector2 difference = Transform.Position - playerPos;
            
            direction = Vector2.Normalize(-difference);


            if (Vector2.Distance(Transform.Center, playerPos) > 80f)
            {
                Move(gameTime);
            }
            else
            {
                
            }
            
            base.Update(gameTime);
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Transform.Position, CurrentSprite, Color.White);
            
            base.Draw(spriteBatch);
        }

        public override void OnCollision(Collider other)
        {

            base.OnCollision(other);
        }
        public void Move(GameTime gameTime)
        {
            Transform.Position += ((direction * speed) * Time.DeltaTime);
        }
    }
}
