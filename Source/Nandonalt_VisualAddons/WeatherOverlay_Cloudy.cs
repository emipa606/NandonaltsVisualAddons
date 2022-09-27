using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons;

[StaticConstructorOnStartup]
public class WeatherOverlay_Cloudy : SkyOverlay
{
    public static readonly Material CloudyOverlayWorld = new Material(MatLoader.LoadMat("Weather/FogOverlayWorld"));

    public WeatherOverlay_Cloudy()
    {
        worldOverlayMat = CloudyOverlayWorld;
        if (Rand.Bool)
        {
            worldPanDir1 = new Vector2(0f, 1f);
            worldPanDir2 = new Vector2(0f, -1f);
            worldOverlayPanSpeed1 = 0.0004f;
            worldOverlayPanSpeed2 = 0.0004f;
            return;
        }

        worldPanDir1 = new Vector2(1f, 0f);
        worldPanDir2 = new Vector2(-1f, 0f);
        worldOverlayPanSpeed1 = 0.0004f;
        worldOverlayPanSpeed2 = 0.0004f;
    }
}