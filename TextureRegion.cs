using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public struct TextureRegion
    {
        public Texture2D Texture;

        public int Width => SourceRectangle.Width;
        public int Height => SourceRectangle.Height;

        public Rectangle SourceRectangle { get; set; }

        public TextureRegion(Texture2D texture, Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
            Texture = texture;
        }
        public TextureRegion(Texture2D texture, int x, int y, int width, int height)
        {
            SourceRectangle = new Rectangle(x, y, width, height);
            Texture = texture;
        }
    }
}
