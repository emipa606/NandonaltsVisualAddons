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
    private static void Postfix(Pawn_FilthTracker __instance)
    {
        if (HarmonyPatches.PawnFieldInfo_FilthTracker.GetValue(__instance) is not Pawn pawn)
        {
            return;
        }

        var thingList = pawn.Position.GetThingList(pawn.Map);
        for (var i = thingList.Count - 1; i >= 0; i--)
        {
            if (thingList[i] is not Filth filth || filth.def.defName != "FilthWater")
            {
                continue;
            }

            __instance.GainFilth(ThingDef.Named("FilthWaterSpatter"), filth.sources);
            filth.ThinFilth();
        }
    }
}