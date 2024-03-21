using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(SkyOverlay))]
[HarmonyPatch("TickOverlay")]
[HarmonyPatch([
    typeof(Map)
])]
internal class TickOverlay
{
    private static void Postfix(Map map)
    {
        if (!Nandonalt_VisualAddonsMod.instance.Settings.RainWaterPuddles)
        {
            return;
        }

        if (map.weatherManager.curWeather.rainRate >= 1f && map.weatherManager.curWeather.snowRate <= 0f &&
            Rand.Value <= Nandonalt_VisualAddonsMod.instance.Settings.PuddleChance)
        {
            FilthMaker.TryMakeFilth(
                CellFinderLoose.RandomCellWith(sq => sq.Standable(map) && !map.roofGrid.Roofed(sq), map), map,
                HarmonyPatches.filthWater);
        }
    }
}