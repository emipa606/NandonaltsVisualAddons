using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons
{
    // Token: 0x02000007 RID: 7
    [HarmonyPatch(typeof(JobDriver_CleanFilth), "TryMakePreToilReservations", null)]
    public static class CleanFilthPrefixPatch
    {
        // Token: 0x0600000B RID: 11 RVA: 0x000028CC File Offset: 0x00000ACC
        [HarmonyPrefix]
        public static bool CleanFilthPrefix(JobDriver_CleanFilth __instance)
        {
            if (Settings.RainCleanWaterPuddles)
            {
                return true;
            }

            foreach (var localTargetInfo in __instance.job.targetQueueA)
            {
                var thing = localTargetInfo.Thing;
                if (thing is Filth filth &&
                    (filth.def.defName == "FilthWater" || filth.def.defName == "FilthWaterSpatter") &&
                    thing.GetRoom(RegionType.Set_Passable).UsesOutdoorTemperature &&
                    !thing.Map.roofGrid.Roofed(thing.Position))
                {
                    return false;
                }
            }

            return true;
        }
    }
}