﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons
{
    // Token: 0x02000004 RID: 4
    [HarmonyPatch(typeof(SkyOverlay))]
    [HarmonyPatch("TickOverlay")]
    [HarmonyPatch(new[]
    {
        typeof(Map)
    })]
    internal class TickOverlay
    {
        // Token: 0x06000004 RID: 4 RVA: 0x0000257C File Offset: 0x0000077C
        private static void Postfix(Map map)
        {
            if (!Settings.RainWaterPuddles)
            {
                return;
            }

            if (map.weatherManager.curWeather.rainRate >= 1f && map.weatherManager.curWeather.snowRate <= 0f &&
                Rand.Range(1, 100) <= Settings.PuddleChance)
            {
                FilthMaker.TryMakeFilth(
                    CellFinderLoose.RandomCellWith(sq => sq.Standable(map) && !map.roofGrid.Roofed(sq), map), map,
                    ThingDef.Named("FilthWater"));
            }
        }
    }
}