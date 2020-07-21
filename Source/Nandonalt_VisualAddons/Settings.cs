using System;
using HugsLib;
using HugsLib.Settings;
using Verse;

namespace Nandonalt_VisualAddons
{
	// Token: 0x02000008 RID: 8
	[StaticConstructorOnStartup]
	public class Settings : ModBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000299C File Offset: 0x00000B9C
		public override string ModIdentifier
		{
			get
			{
				return "NanondaltVisualAddons";
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000029A4 File Offset: 0x00000BA4
		public override void DefsLoaded()
		{
			ColdFog = base.Settings.GetHandle<bool>("coldFog", "Cold Fog", "Enable/disable cold fog on rooms with temperatures below 0C.", true, null, null);
			FogVelocity = base.Settings.GetHandle<int>("fogVelocity", "Fog Velocity", string.Empty, 12, Validators.IntRangeValidator(0, 30), null);
			FogTemp = base.Settings.GetHandle<int>("fogTemp", "Fog Temperature", "Fog will appear below this temperature (Celsius)", 0, Validators.IntRangeValidator(-273, 0), null);
			IceLayer = base.Settings.GetHandle<bool>("iceLayer", "Ice Layer", "Enable/disable formation of a layer of ice on rooms with temperatures below 0C.", true, null, null);
			BilliardsCue = base.Settings.GetHandle<bool>("billiardsCue", "Billiards Cue", "Enable/disable showing cues when pawns are playing billards.", true, null, null);
			RainWaterPuddles = base.Settings.GetHandle<bool>("rain_WaterPuddles", "Water Puddles", "Enable/disable formation of water puddles during rain.", true, null, null);
			PuddleChance = base.Settings.GetHandle<int>("puddleChance", "Puddle % Chance", "Chance at which puddles will spawn on the map (Default 20)", 20, Validators.IntRangeValidator(1, 100), null);
			RainCleanWaterPuddles = base.Settings.GetHandle<bool>("rain_cleanWaterPuddles", "Clean Water Puddles", "Enable/disable cleaning of unrooofed water puddles on home area.", false, null, null);
		}

		// Token: 0x04000007 RID: 7
		public static SettingHandle<bool> ColdFog;

		// Token: 0x04000008 RID: 8
		public static SettingHandle<bool> IceLayer;

		// Token: 0x04000009 RID: 9
		public static SettingHandle<bool> BilliardsCue;

		// Token: 0x0400000A RID: 10
		public static SettingHandle<bool> RainWaterPuddles;

		// Token: 0x0400000B RID: 11
		public static SettingHandle<bool> RainCleanWaterPuddles;

		// Token: 0x0400000C RID: 12
		public static SettingHandle<int> FogVelocity;

		// Token: 0x0400000D RID: 13
		public static SettingHandle<int> FogTemp;

		// Token: 0x0400000E RID: 14
		public static SettingHandle<int> PuddleChance;
	}
}
