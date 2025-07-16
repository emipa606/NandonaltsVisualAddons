using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons;

[StaticConstructorOnStartup]
public class WeatherOverlay_Cloudy : WeatherOverlayDualPanner
{
    private const float PanSpeed = 0.0004f;
    private static readonly Material cloudyOverlayWorld = new(MatLoader.LoadMat("Weather/FogOverlayWorld"));

    public WeatherOverlay_Cloudy()
    {
        worldOverlayMat = cloudyOverlayWorld;

        if (Rand.Bool)
        {
            worldPanDir1 = new Vector2(0f, 1f);
            worldPanDir2 = new Vector2(0f, -1f);
        }
        else
        {
            worldPanDir1 = new Vector2(1f, 0f);
            worldPanDir2 = new Vector2(-1f, 0f);
        }

        worldOverlayPanSpeed1 = PanSpeed;
        worldOverlayPanSpeed2 = PanSpeed;
    }
}