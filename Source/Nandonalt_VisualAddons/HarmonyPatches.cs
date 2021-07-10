using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons
{
    // Token: 0x02000002 RID: 2
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        // Token: 0x04000001 RID: 1
        public static FieldInfo MapFieldInfo;

        // Token: 0x04000002 RID: 2
        public static FieldInfo PawnFieldInfo_FilthTracker;

        // Token: 0x04000003 RID: 3
        public static FieldInfo PawnFieldInfo_Renderer;

        // Token: 0x04000004 RID: 4
        public static FieldInfo PawnFieldInfo_StoryTracker;

        // Token: 0x04000005 RID: 5
        public static FieldInfo JobFieldInfo_CleanFilth;

        // Token: 0x04000006 RID: 6
        public static Graphic poolCue = GraphicDatabase.Get(typeof(Graphic_Single), "Pool/Cue",
            ShaderDatabase.DefaultShader, Vector2.one, Color.white, Color.white);

        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
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
        }
    }
}