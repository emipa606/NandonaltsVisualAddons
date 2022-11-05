using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(Pawn_FilthTracker))]
[HarmonyPatch("TryPickupFilth")]
[HarmonyPatch(new Type[]
{
})]
internal class TryPickupFilth
{
    private static void Postfix(Pawn_FilthTracker __instance, Pawn ___pawn)
    {
        if (___pawn == null)
        {
            return;
        }

        var thingList = ___pawn.Position.GetThingList(___pawn.Map);
        for (var i = thingList.Count - 1; i >= 0; i--)
        {
            if (thingList[i] is not Filth filth || filth.def != HarmonyPatches.filthWater)
            {
                continue;
            }

            __instance.GainFilth(HarmonyPatches.filthWaterSpatter, filth.sources);
            filth.ThinFilth();
        }
    }
}