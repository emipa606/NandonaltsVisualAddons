using HugsLib;
using HugsLib.Settings;
using Verse;

namespace Nandonalt_VisualAddons
{
    // Token: 0x02000008 RID: 8
    [StaticConstructorOnStartup]
    public class Settings : ModBase
    {
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

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x0600000C RID: 12 RVA: 0x0000299C File Offset: 0x00000B9C
        public override string ModIdentifier => "NanondaltVisualAddons";

        // Token: 0x0600000D RID: 13 RVA: 0x000029A4 File Offset: 0x00000BA4
        public override void DefsLoaded()
        {
            ColdFog = Settings.GetHandle("coldFog", "Cold Fog",
                "Enable/disable cold fog on rooms with temperatures below 0C.", true);
            FogVelocity = Settings.GetHandle("fogVelocity", "Fog Velocity", string.Empty, 12,
                Validators.IntRangeValidator(0, 30));
            FogTemp = Settings.GetHandle("fogTemp", "Fog Temperature",
                "Fog will appear below this temperature (Celsius)", 0, Validators.IntRangeValidator(-273, 0));
            IceLayer = Settings.GetHandle("iceLayer", "Ice Layer",
                "Enable/disable formation of a layer of ice on rooms with temperatures below 0C.", true);
            BilliardsCue = Settings.GetHandle("billiardsCue", "Billiards Cue",
                "Enable/disable showing cues when pawns are playing billards.", true);
            RainWaterPuddles = Settings.GetHandle("rain_WaterPuddles", "Water Puddles",
                "Enable/disable formation of water puddles during rain.", true);
            PuddleChance = Settings.GetHandle("puddleChance", "Puddle % Chance",
                "Chance at which puddles will spawn on the map (Default 20)", 20, Validators.IntRangeValidator(1, 100));
            RainCleanWaterPuddles = Settings.GetHandle<bool>("rain_cleanWaterPuddles", "Clean Water Puddles",
                "Enable/disable cleaning of unrooofed water puddles on home area.");
        }
    }
}