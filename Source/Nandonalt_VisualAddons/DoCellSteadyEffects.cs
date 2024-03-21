using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(SteadyEnvironmentEffects))]
[HarmonyPatch("DoCellSteadyEffects")]
[HarmonyPatch([
    typeof(IntVec3)
])]
internal class DoCellSteadyEffects
{
    private static void Postfix(IntVec3 c, Map ___map)
    {
        if (___map == null)
        {
            return;
        }

        var room = c.GetRoom(___map);
        if (Nandonalt_VisualAddonsMod.instance.Settings.ColdFog || Nandonalt_VisualAddonsMod.instance.Settings.IceLayer)
        {
            var thing = (from t in c.GetThingList(___map)
                where t.def == HarmonyPatches.iceOverlay
                select t).FirstOrDefault();
            if (room == null && thing != null && Nandonalt_VisualAddonsMod.instance.Settings.IceLayer)
            {
                thing.Destroy();
                if (Rand.Range(1, 100) <= 20)
                {
                    FilthMaker.TryMakeFilth(c, ___map, HarmonyPatches.filthWater);
                }
            }

            if (room is { UsesOutdoorTemperature: false, Fogged: false, IsDoorway: false })
            {
                var num = 0.8f;
                if (room.Temperature < Nandonalt_VisualAddonsMod.instance.Settings.FogTemp)
                {
                    if (thing == null && Nandonalt_VisualAddonsMod.instance.Settings.IceLayer)
                    {
                        GenSpawn.Spawn(ThingMaker.MakeThing(HarmonyPatches.iceOverlay), c, ___map);
                    }

                    if (Nandonalt_VisualAddonsMod.instance.Settings.ColdFog)
                    {
                        var vector = c.ToVector3Shifted();
                        var motes = !(!vector.ShouldSpawnMotesAt(___map) || ___map.moteCounter.SaturatedLowPriority);

                        vector += num * new Vector3(Rand.Value - 0.5f, 0f, Rand.Value - 0.5f);
                        if (!vector.InBounds(___map))
                        {
                            motes = false;
                        }

                        if (motes)
                        {
                            var fogFleck = FleckMaker.GetDataStatic(vector, ___map, HarmonyPatches.coldFog,
                                Rand.Range(4f, 6f) * num);
                            fogFleck.rotationRate = Rand.Range(-3f, 3f);
                            fogFleck.velocityAngle = Rand.Bool ? -90 : 90;
                            fogFleck.velocitySpeed =
                                (float)(Nandonalt_VisualAddonsMod.instance.Settings.FogVelocity * 0.01);
                            ___map.flecks.CreateFleck(fogFleck);
                        }
                    }
                }
                else if (thing != null)
                {
                    thing.Destroy();
                    if (Rand.Range(1, 100) <= 20)
                    {
                        FilthMaker.TryMakeFilth(c, ___map, HarmonyPatches.filthWater);
                    }
                }
            }
        }

        if (!Nandonalt_VisualAddonsMod.instance.Settings.IceLayer)
        {
            var thing2 = (from t in c.GetThingList(___map)
                where t.def == HarmonyPatches.iceOverlay
                select t).FirstOrDefault();
            thing2?.Destroy();
        }

        if (___map.roofGrid == null || ___map.roofGrid.Roofed(c) || !(___map.weatherManager.curWeatherAge >= 7500f) ||
            !(___map.weatherManager.curWeather.rainRate <= 0f) && !(___map.weatherManager.curWeather.snowRate > 0f))
        {
            return;
        }

        var thing3 = (from t in c.GetThingList(___map)
            where t.def.defName is "FilthWater" or "FilthWaterSpatter"
            select t).FirstOrDefault();
        if (thing3 != null && Rand.Value <= 0.2f)
        {
            ((Filth)thing3).ThinFilth();
        }
    }
}