using System;
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
			if (!Settings.RainCleanWaterPuddles)
			{
				foreach (LocalTargetInfo localTargetInfo in __instance.job.targetQueueA)
				{
					Thing thing = localTargetInfo.Thing;
					Filth filth = thing as Filth;
					if (filth != null && (filth.def.defName == "FilthWater" || filth.def.defName == "FilthWaterSpatter") && thing.GetRoom(RegionType.Set_Passable).UsesOutdoorTemperature && !thing.Map.roofGrid.Roofed(thing.Position))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}
	}
}
