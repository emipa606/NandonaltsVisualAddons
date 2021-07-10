using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Nandonalt_VisualAddons
{
    // Token: 0x02000005 RID: 5
    [HarmonyPatch(typeof(Pawn_FilthTracker))]
    [HarmonyPatch("TryPickupFilth")]
    [HarmonyPatch(new Type[]
    {
    })]
    internal class TryPickupFilth
    {
        // Token: 0x06000006 RID: 6 RVA: 0x00002624 File Offset: 0x00000824
        private static void Postfix(Pawn_FilthTracker __instance)
        {
            if (!(HarmonyPatches.PawnFieldInfo_FilthTracker.GetValue(__instance) is Pawn pawn))
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
}