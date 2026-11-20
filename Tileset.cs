using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public class Tileset
    {
        internal readonly TextureRegion[] _tiles;

        public int TileWidth { get; set; }

        public int TileHeight { get; set; }

        public int Columns { get; set; }

        public int Rows { get; set; }

        public int Count { get; set; }

        public Tileset(TextureRegion textureRegion, int tileWidth, int tileHeight)
        {
            TileHeight = tileHeight;
            TileWidth = tileWidth;
            Columns = textureRegion.Width / tileWidth;
            Rows = textureRegion.Height / tileHeight;
            Count = Columns * Rows;

            _tiles = new TextureRegion[Count];

            for (int y = 0; y < Columns; y++)
            {
                for (int x = 0; x < Rows; x++)
                {
                    int i = y * Rows + x;
                    int offsetX = i % Columns * tileWidth;
                    int offsetY = i / Columns * tileHeight;
                    _tiles[i] = new TextureRegion(textureRegion.Texture, textureRegion.SourceRectangle.X + offsetX, textureRegion.SourceRectangle.Y + offsetY, tileWidth, tileHeight);
                }
            }
        }

        public TextureRegion GetTile(int index) => _tiles[index];

        public TextureRegion GetTile(int column, int row)
        {
            int index = row * Columns + column;
            return GetTile(index);
        }
    }
}
