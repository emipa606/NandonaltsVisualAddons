using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(SteadyEnvironmentEffects))]
[HarmonyPatch("DoCellSteadyEffects")]
[HarmonyPatch(new[]
{
    typeof(IntVec3)
})]
internal class DoCellSteadyEffects
{
    private static void Postfix(IntVec3 c, SteadyEnvironmentEffects __instance)
    {
        if (HarmonyPatches.MapFieldInfo.GetValue(__instance) is not Map map)
        {
            return;
        }

        var room = c.GetRoom(map);
        if (Nandonalt_VisualAddonsMod.instance.Settings.ColdFog || Nandonalt_VisualAddonsMod.instance.Settings.IceLayer)
        {
            var thing = (from t in c.GetThingList(map)
                where t.def.defName == "IceOverlay"
                select t).FirstOrDefault();
            if (room == null && thing != null && Nandonalt_VisualAddonsMod.instance.Settings.IceLayer)
            {
                thing.Destroy();
                if (Rand.Range(1, 100) <= 20)
                {
                    FilthMaker.TryMakeFilth(c, map, ThingDef.Named("FilthWater"));
                }
            }

            if (room is { UsesOutdoorTemperature: false, Fogged: false, IsDoorway: false })
            {
                var num = 0.8f;
                if (room.Temperature < Nandonalt_VisualAddonsMod.instance.Settings.FogTemp)
                {
                    if (thing == null && Nandonalt_VisualAddonsMod.instance.Settings.IceLayer)
                    {
                        GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("IceOverlay")), c, map);
                    }

                    if (Nandonalt_VisualAddonsMod.instance.Settings.ColdFog)
                    {
                        var vector = c.ToVector3Shifted();
                        var motes = !(!vector.ShouldSpawnMotesAt(map) || map.moteCounter.SaturatedLowPriority);

                        vector += num * new Vector3(Rand.Value - 0.5f, 0f, Rand.Value - 0.5f);
                        if (!vector.InBounds(map))
                        {
                            motes = false;
                        }

                        if (motes)
                        {
                            var moteThrown = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_FrostGlow"));
                            moteThrown.Scale = Rand.Range(4f, 6f) * num;
                            moteThrown.rotationRate = Rand.Range(-3f, 3f);
                            moteThrown.exactPosition = vector;
                            moteThrown.SetVelocity(Rand.Bool ? -90 : 90,
                                (float)(Nandonalt_VisualAddonsMod.instance.Settings.FogVelocity * 0.01));
                            GenSpawn.Spawn(moteThrown, vector.ToIntVec3(), map);
                        }
                    }
                }
                else if (thing != null)
                {
                    thing.Destroy();
                    if (Rand.Range(1, 100) <= 20)
                    {
                        FilthMaker.TryMakeFilth(c, map, ThingDef.Named("FilthWater"));
                    }
                }
            }
        }

        if (!Nandonalt_VisualAddonsMod.instance.Settings.IceLayer)
        {
            var thing2 = (from t in c.GetThingList(map)
                where t.def.defName == "IceOverlay"
                select t).FirstOrDefault();
            thing2?.Destroy();
        }

        if (map.roofGrid == null || map.roofGrid.Roofed(c) || !(map.weatherManager.curWeatherAge >= 7500f) ||
            !(map.weatherManager.curWeather.rainRate <= 0f) && !(map.weatherManager.curWeather.snowRate > 0f))
        {
            return;
        }

        var thing3 = (from t in c.GetThingList(map)
            where t.def.defName == "FilthWater" || t.def.defName == "FilthWaterSpatter"
            select t).FirstOrDefault();
        if (thing3 != null && Rand.Value <= 0.2f)
        {
            ((Filth)thing3).ThinFilth();
        }
    }
}