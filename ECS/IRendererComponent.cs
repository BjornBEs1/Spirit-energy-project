using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spirit_energy_project
{
    public interface IRendererComponent
    {
        void SetSprite(object obj);
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
