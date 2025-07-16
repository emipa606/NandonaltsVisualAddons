using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(WeatherOverlayDualPanner), nameof(WeatherOverlayDualPanner.TickOverlay))]
internal class WeatherOverlayDualPanner_TickOverlay
{
    public static void Postfix(Map map)
    {
        if (!Nandonalt_VisualAddonsMod.Instance.Settings.RainWaterPuddles)
        {
            return;
        }

        if (map.weatherManager.curWeather.rainRate >= 1f && map.weatherManager.curWeather.snowRate <= 0f &&
            Rand.Value <= Nandonalt_VisualAddonsMod.Instance.Settings.PuddleChance)
        {
            FilthMaker.TryMakeFilth(
                CellFinderLoose.RandomCellWith(sq => sq.Standable(map) && !map.roofGrid.Roofed(sq), map), map,
                HarmonyPatches.FilthWater);
        }
    }
}