/*
 * File: TileMap.cs
 * File Created: 24 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 24 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spirit_energy_project;

public class TileMap : Component, IRendererComponent
{
    private TileSet tileSet;
    private TextureRegion textureRegion;

    private int width;
    private int height;

    private int[] tileMapIndex;

    public int Width
    {
        get
        {
            return width;
        }

        set
        {
            onChangeSize(value, Height, Width, Height);
            width = value;
        }
    }
    public int Height
    {
        get => height;
        set
        {
            onChangeSize(Width, value, Width, Height);
            height = value;
        }
    }

    private int mapSize => Width * Height;

    int tileWidth;
    int tileHeight;

    public int TileWidth { get => tileWidth; set => tileWidth = value; }
    public int TileHeight { get => tileHeight; set => tileHeight = value; }

    float layerDepth;
    public float LayerDepth { get => layerDepth; set => layerDepth = value; }

    public TileMap(int _tileWidth, int _tileHeight)
    {
        TileWidth = _tileWidth;
        TileHeight = _tileHeight;
    }

    void onChangeSize(int newWidth, int newHeight, int oldWidth, int oldHeight)
    {
        if (tileMapIndex == null)
        {
            tileMapIndex = new int[newWidth * newHeight];
            return;
        }
        int[] oldMap = new int[oldWidth * oldHeight];
        Array.Copy(tileMapIndex, oldMap, oldWidth * oldHeight);
        tileMapIndex = new int[newWidth * newHeight];
        Array.Copy(oldMap, tileMapIndex, oldWidth * oldHeight);
    }

    public void SetSprite(string sprite)
    {
        Texture2D texture = ContentEngineManager.Instance.GetTexture2D(sprite);
        textureRegion = new TextureRegion(texture, texture.Bounds);
        tileSet = new TileSet(textureRegion, TileWidth, TileHeight);
    }
    public void SetSprite(object obj)
    {
    }

    internal override void componentAdded(GameObject owner)
    {
    }

    public void Update()
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < mapSize; i++)
        {
            // credit: https://stackoverflow.com/questions/47951361/finding-x-y-based-off-of-index
            int x = i % width;
            int y = i / width;

            int tileIndex = tileMapIndex[i];

            TextureRegion textureRegion = tileSet.GetTile(tileIndex);
            Vector2 position = new Vector2(x, y) * textureRegion.SourceRectangle.Size.ToVector2();
            spriteBatch.Draw(textureRegion.Texture, position, textureRegion.SourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerDepth);
        }
    }
}