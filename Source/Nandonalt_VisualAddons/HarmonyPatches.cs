using System;
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
    public static FieldInfo MapFieldInfo;

    public static FieldInfo PawnFieldInfo_FilthTracker;

    public static FieldInfo PawnFieldInfo_Renderer;

    public static FieldInfo PawnFieldInfo_StoryTracker;

    public static FieldInfo JobFieldInfo_CleanFilth;

    public static Graphic poolCue = GraphicDatabase.Get(typeof(Graphic_Single), "Pool/Cue",
        ShaderDatabase.DefaultShader, Vector2.one, Color.white, Color.white);

    static HarmonyPatches()
    {
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

        WeatherDef.Named("Cloudy").skyColorsDay = WeatherDef.Named("Clear").skyColorsDay;
        WeatherDef.Named("Cloudy").skyColorsDusk = WeatherDef.Named("Clear").skyColorsDusk;
        WeatherDef.Named("Cloudy").skyColorsNightEdge = WeatherDef.Named("Clear").skyColorsNightEdge;
        WeatherDef.Named("Cloudy").skyColorsNightMid = WeatherDef.Named("Clear").skyColorsNightMid;
        Traverse.Create(WeatherDef.Named("Cloudy")).Field("workerInt")
            .SetValue(new WeatherWorker(WeatherDef.Named("Cloudy")));
        MapFieldInfo = typeof(SteadyEnvironmentEffects).GetField("map",
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (MapFieldInfo == null)
        {
            throw new Exception("Could not get FieldInfo!");
        }

        PawnFieldInfo_FilthTracker = typeof(Pawn_FilthTracker).GetField("pawn",
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (PawnFieldInfo_FilthTracker == null)
        {
            throw new Exception("Could not get FieldInfo!");
        }

        PawnFieldInfo_Renderer = typeof(PawnRenderer).GetField("pawn",
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (PawnFieldInfo_Renderer == null)
        {
            throw new Exception("Could not get FieldInfo!");
        }

        PawnFieldInfo_StoryTracker = typeof(Pawn_StoryTracker).GetField("pawn",
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (PawnFieldInfo_StoryTracker == null)
        {
            throw new Exception("Could not get FieldInfo!");
        }

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
                Nandonalt_VisualAddonsMod.instance.Settings.ColdFog = bool.Parse(modSetting.Value);
            }

            if (modSetting.Name == "fogVelocity")
            {
                Nandonalt_VisualAddonsMod.instance.Settings.FogVelocity = int.Parse(modSetting.Value);
            }

            if (modSetting.Name == "fogTemp")
            {
                Nandonalt_VisualAddonsMod.instance.Settings.FogTemp = int.Parse(modSetting.Value);
            }

            if (modSetting.Name == "iceLayer")
            {
                Nandonalt_VisualAddonsMod.instance.Settings.IceLayer = bool.Parse(modSetting.Value);
            }

            if (modSetting.Name == "billiardsCue")
            {
                Nandonalt_VisualAddonsMod.instance.Settings.BilliardsCue = bool.Parse(modSetting.Value);
            }

            if (modSetting.Name == "rain_WaterPuddles")
            {
                Nandonalt_VisualAddonsMod.instance.Settings.RainWaterPuddles = bool.Parse(modSetting.Value);
            }

            if (modSetting.Name == "puddleChance")
            {
                Nandonalt_VisualAddonsMod.instance.Settings.PuddleChance = int.Parse(modSetting.Value) / 100f;
            }

            if (modSetting.Name == "rain_cleanWaterPuddles")
            {
                Nandonalt_VisualAddonsMod.instance.Settings.RainCleanWaterPuddles = bool.Parse(modSetting.Value);
            }
        }

        xml.Root.Element("NanondaltVisualAddons")?.Remove();
        xml.Save(hugsLibConfig);

        Log.Message("[NanondaltVisualAddons]: Imported old HugLib-settings");
    }
}