/*
 * File: SpiritEnergyMeter.cs
 * File Created: 23 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 23 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spirit_energy_project;

public class SpiritEnergyMeter : GameObject
{
    internal float Energy;
    internal float MaxEnergy;

    SpriteRenderer BackgroundSprite;
    SpriteRenderer ForegroundSprite;
    SpriteRenderer MeterSprite;
    public SpiritEnergyMeter()
    {
        MaxEnergy = 100;
        Energy = MaxEnergy;
        BackgroundSprite = new SpriteRenderer();
        AddComponent(BackgroundSprite);
        ForegroundSprite = new SpriteRenderer();
        AddComponent(ForegroundSprite);
        MeterSprite = new SpriteRenderer();
        AddComponent(MeterSprite);
    }

    public void AddEnergy(float energy)
    {
        if (Energy < 0)
        {
            return;
        }
        Energy += energy;
        
        // limit it between 0 and MaxEnergy
        if (Energy > MaxEnergy)
        {
            Energy = MaxEnergy;
        }
    }

    public void RemoveEnergy(float energy)
    {
        Energy -= energy;
    }

    public override void LoadContents()
    {
        // 128x32
        BackgroundSprite.srcRectangle = new Rectangle(0, 64, 128, 32);
        ForegroundSprite.srcRectangle = new Rectangle(0, 0, 128, 32);
        MeterSprite.srcRectangle = new Rectangle(0, 32, 128, 32);

        Texture2D sprite = ContentEngineManager.Instance.loadTexture2D("SpiritEnergyMeterTex", "UI");
        BackgroundSprite.SetSprite(sprite);
        ForegroundSprite.SetSprite(sprite);
        MeterSprite.SetSprite(sprite);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        //Energy bar drawing
        float healthPercentage = MathHelper.Clamp((float)Energy / MaxEnergy, 0f, 1f);
        int currentBarWidth = (int)(128 * healthPercentage);

        Rectangle meterSpriteRect;
        if (!MeterSprite.srcRectangle.HasValue)
        {
            // if this happens, then the whole engine if fucked.
            // BjornBEs - 12:03, 24.11.2026 
            throw new Exception();
        }
        meterSpriteRect = MeterSprite.srcRectangle.Value;
        meterSpriteRect.Width = currentBarWidth;
        MeterSprite.srcRectangle = meterSpriteRect;

        BackgroundSprite.Draw(spriteBatch);
        ForegroundSprite.Draw(spriteBatch);
        MeterSprite.Draw(spriteBatch);

        base.Draw(spriteBatch);
    }
}