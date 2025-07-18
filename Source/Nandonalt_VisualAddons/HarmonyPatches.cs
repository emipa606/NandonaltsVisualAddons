using System.IO;
using System.Reflection;
using System.Xml.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    public static readonly Graphic PoolCue = GraphicDatabase.Get(typeof(Graphic_Single), "Pool/Cue",
        ShaderDatabase.DefaultShader, Vector2.one, Color.white, Color.white);

    public static readonly FleckDef ColdFog;
    public static readonly ThingDef IceOverlay;
    public static readonly ThingDef FilthWater;
    public static readonly ThingDef FilthWaterSpatter;


    static HarmonyPatches()
    {
        ColdFog = DefDatabase<FleckDef>.GetNamedSilentFail("Mote_FrostGlow");
        IceOverlay = ThingDef.Named("IceOverlay");
        FilthWater = ThingDef.Named("FilthWater");
        FilthWaterSpatter = ThingDef.Named("FilthWaterSpatter");
        foreach (var biomeDef in DefDatabase<BiomeDef>.AllDefs)
        {
            if (biomeDef == null)
            {
                continue;
            }

            var weatherCommonalityRecord = new WeatherCommonalityRecord
            {
                weather = WeatherDef.Named("Cloudy"), commonality = 5f
            };
            biomeDef.baseWeatherCommonalities.Add(weatherCommonalityRecord);
        }

        var cloudy = WeatherDef.Named("Cloudy");
        var clear = WeatherDef.Named("Clear");
        cloudy.skyColorsDay = clear.skyColorsDay;
        cloudy.skyColorsDusk = clear.skyColorsDusk;
        cloudy.skyColorsNightEdge = clear.skyColorsNightEdge;
        cloudy.skyColorsNightMid = clear.skyColorsNightMid;
        AccessTools.Field(typeof(WeatherDef), "workerInt")
            .SetValue(cloudy, new WeatherWorker(WeatherDef.Named("Cloudy")));

        new Harmony("Mlie.NanondaltVisualAddons").PatchAll(Assembly.GetExecutingAssembly());

        var hugsLibConfig = Path.Combine(GenFilePaths.SaveDataFolderPath, Path.Combine("HugsLib", "ModSettings.xml"));
        if (!new FileInfo(hugsLibConfig).Exists)
        {
            return;
        }

        var xml = XDocument.Load(hugsLibConfig);

        var modSettings = xml.Root?.Element("NanondaltVisualAddons");
        if (modSettings == null)
        {
            return;
        }

        foreach (var modSetting in modSettings.Elements())
        {
            if (modSetting.Name == "coldFog")
            {
                Nandonalt_VisualAddonsMod.Instance.Settings.ColdFog = bool.Parse(modSetting.Value);
            }

            if (modSetting.Name == "fogVelocity")
            {
                Nandonalt_VisualAddonsMod.Instance.Settings.FogVelocity = int.Parse(modSetting.Value);
            }

            if (modSetting.Name == "fogTemp")
            {
                Nandonalt_VisualAddonsMod.Instance.Settings.FogTemp = int.Parse(modSetting.Value);
            }

            if (modSetting.Name == "iceLayer")
            {
                Nandonalt_VisualAddonsMod.Instance.Settings.IceLayer = bool.Parse(modSetting.Value);
            }

            if (modSetting.Name == "billiardsCue")
            {
                Nandonalt_VisualAddonsMod.Instance.Settings.BilliardsCue = bool.Parse(modSetting.Value);
            }

            if (modSetting.Name == "rain_WaterPuddles")
            {
                Nandonalt_VisualAddonsMod.Instance.Settings.RainWaterPuddles = bool.Parse(modSetting.Value);
            }

            if (modSetting.Name == "puddleChance")
            {
                Nandonalt_VisualAddonsMod.Instance.Settings.PuddleChance = int.Parse(modSetting.Value) / 100f;
            }

            if (modSetting.Name == "rain_cleanWaterPuddles")
            {
                Nandonalt_VisualAddonsMod.Instance.Settings.RainCleanWaterPuddles = bool.Parse(modSetting.Value);
            }
        }

        xml.Root.Element("NanondaltVisualAddons")?.Remove();
        xml.Save(hugsLibConfig);

        Log.Message("[NanondaltVisualAddons]: Imported old HugLib-settings");
    }
}