using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(JobDriver_CleanFilth), "TryMakePreToilReservations", null)]
public static class CleanFilthPrefixPatch
{
    [HarmonyPrefix]
    public static bool CleanFilthPrefix(JobDriver_CleanFilth __instance)
    {
        if (Nandonalt_VisualAddonsMod.instance.Settings.RainCleanWaterPuddles)
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