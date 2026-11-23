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
    int energy;
    int maxEnergy;

    SpriteRenderer BackgroundSprite;
    SpriteRenderer ForegroundSprite;
    SpriteRenderer MeterSprite;
    public SpiritEnergyMeter()
    {
        energy = 100;
        maxEnergy = 100;
        BackgroundSprite = new SpriteRenderer();
        AddComponent(BackgroundSprite);
        ForegroundSprite = new SpriteRenderer();
        AddComponent(ForegroundSprite);
        MeterSprite = new SpriteRenderer();
        AddComponent(MeterSprite);
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
        /*         int barWidth = 200;
                int barHeight = 14;
                int posX = 20;
                int posY = 50;
                int borderThickness = 2; */
        float healthPercentage = MathHelper.Clamp((float)energy / maxEnergy, 0f, 1f);
        int currentBarWidth = (int)(128 * healthPercentage);

        Rectangle meterSpriteRect;
        if (!MeterSprite.srcRectangle.HasValue)
        {
            throw new Exception();
        }
        meterSpriteRect = MeterSprite.srcRectangle.Value;
        meterSpriteRect.Width = currentBarWidth;
        MeterSprite.srcRectangle = meterSpriteRect;

        BackgroundSprite.Draw(spriteBatch);
        ForegroundSprite.Draw(spriteBatch);
        MeterSprite.Draw(spriteBatch);

        /* Color goldBorder = new Color(229, 193, 88);
        Color darkGoldBg = new Color(75, 62, 28, 220);
        Color cyanFill = new Color(124, 232, 255);
        Color whiteGlow = new Color(240, 253, 255);

        Color energyColor;
        if (healthPercentage > 0.5f)
        {
            energyColor = Color.Lerp(Color.Yellow, Color.Green, (healthPercentage - 0.5f) * 2);
        }
        else
        {
            energyColor = Color.Lerp(Color.Red, Color.Yellow, healthPercentage * 2f);
        }

        // spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        Texture2D pixel = ContentEngineManager.Instance.GetTexture2D("pixel");
        //energy bar background
        spriteBatch.Draw(pixel, new Rectangle(posX, posY, barWidth, barHeight), darkGoldBg);
        spriteBatch.Draw(pixel, new Rectangle(posX, posY, currentBarWidth, barHeight), energyColor);

        if (currentBarWidth > 0)
        {
            spriteBatch.Draw(pixel, new Rectangle(posX, posY, currentBarWidth, barHeight), cyanFill);
            int glowHeight = Math.Max(2, barHeight / 3);
            int glowY = posY + (barHeight - glowHeight) / 2;
            spriteBatch.Draw(pixel, new Rectangle(posX, glowY, currentBarWidth, glowHeight), whiteGlow);
        }
        spriteBatch.Draw(pixel, new Rectangle(posX - borderThickness, posY - borderThickness, barWidth + borderThickness * 2, borderThickness), goldBorder);
        spriteBatch.Draw(pixel, new Rectangle(posX - borderThickness, posY - borderThickness, barWidth + borderThickness * 2, borderThickness), goldBorder);
        spriteBatch.Draw(pixel, new Rectangle(posX - borderThickness, posY, borderThickness, barHeight), goldBorder);
        spriteBatch.Draw(pixel, new Rectangle(posX + barWidth, posY, borderThickness, barHeight), goldBorder); */

        //energy bar border0
        // if (energy <= 0)
        // {
        //     string winText = "WIN";
        //     Debug.WriteLine(winText);
        // }

        base.Draw(spriteBatch);
    }
}