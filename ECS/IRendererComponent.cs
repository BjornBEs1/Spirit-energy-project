/*
 * File: IRendererComponent.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 24 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spirit_energy_project
{
    public interface IRendererComponent
    {
        float LayerDepth { get; set; }
        void SetSprite(object obj);
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
