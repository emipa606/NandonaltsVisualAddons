using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons
{
    // Token: 0x02000009 RID: 9
    [StaticConstructorOnStartup]
    public class WeatherOverlay_Cloudy : SkyOverlay
    {
        // Token: 0x0400000F RID: 15
        public static readonly Material CloudyOverlayWorld = new Material(MatLoader.LoadMat("Weather/FogOverlayWorld"));

        // Token: 0x0600000F RID: 15 RVA: 0x00002AE4 File Offset: 0x00000CE4
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
}