using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(JobDriver_CleanFilth), nameof(JobDriver_CleanFilth.TryMakePreToilReservations), null)]
public static class JobDriver_CleanFilth_TryMakePreToilReservations
{
    public static bool Prefix(JobDriver_CleanFilth __instance)
    {
        if (Nandonalt_VisualAddonsMod.Instance.Settings.RainCleanWaterPuddles)
        {
            return true;
        }

        foreach (var localTargetInfo in __instance.job.targetQueueA)
        {
            var thing = localTargetInfo.Thing;
            if (thing is Filth filth &&
                filth.def.defName is "FilthWater" or "FilthWaterSpatter" &&
                thing.GetRoom(RegionType.Set_Passable).UsesOutdoorTemperature &&
                !thing.Map.roofGrid.Roofed(thing.Position))
            {
                return false;
            }
        }

        return true;
    }
}