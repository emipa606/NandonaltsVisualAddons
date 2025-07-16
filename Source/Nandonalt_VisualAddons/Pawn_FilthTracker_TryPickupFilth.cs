using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(Pawn_FilthTracker), "TryPickupFilth")]
internal class Pawn_FilthTracker_TryPickupFilth
{
    public static void Postfix(Pawn_FilthTracker __instance, Pawn ___pawn)
    {
        if (___pawn == null)
        {
            return;
        }

        var thingList = ___pawn.Position.GetThingList(___pawn.Map);
        for (var i = thingList.Count - 1; i >= 0; i--)
        {
            if (thingList[i] is not Filth filth || filth.def != HarmonyPatches.FilthWater)
            {
                continue;
            }

            __instance.GainFilth(HarmonyPatches.FilthWaterSpatter, filth.sources);
            filth.ThinFilth();
        }
    }
}