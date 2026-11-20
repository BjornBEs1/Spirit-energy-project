using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public struct TileData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TileIndex { get; set; }
        public char TileChar { get; set; }
        public TileCollision TileCollision { get; set; }
        public bool CanRotate { get; set; }
        public string RotateTo { get; set; }
        public Dictionary<int, TileRotationData> RotationData { get; set; }
        public Vector2 Normal;
        public int CollisionDataIndex { get; set; }
        public TileData(int x, int y, char tileChar)
        {
            X = x;
            Y = y;
            TileIndex = 0;
            TileChar = tileChar;
            TileCollision = TileCollision.None;
            CanRotate = false;
            RotateTo = "";
            RotationData = new Dictionary<int, TileRotationData>()
            {
                { LevelData.RotationDataDown, TileRotationData.Default },
                { LevelData.RotationDataTop, TileRotationData.Default },
                { LevelData.RotationDataRight, TileRotationData.Default },
                { LevelData.RotationDataLeft, TileRotationData.Default },
            };
            Normal = new Vector2(0, -1);
            CollisionDataIndex = 0;
        }
    }
}
