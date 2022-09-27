using Verse;

namespace Nandonalt_VisualAddons;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class Nandonalt_VisualAddonsSettings : ModSettings
{
    public bool BilliardsCue = true;
    public bool ColdFog = true;

    public int FogTemp;

    public int FogVelocity = 12;

    public bool IceLayer = true;

    public float PuddleChance = 0.2f;

    public bool RainCleanWaterPuddles;

    public bool RainWaterPuddles = true;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ColdFog, "ColdFog", true);
        Scribe_Values.Look(ref IceLayer, "IceLayer", true);
        Scribe_Values.Look(ref BilliardsCue, "BilliardsCue", true);
        Scribe_Values.Look(ref RainWaterPuddles, "RainWaterPuddles", true);
        Scribe_Values.Look(ref RainCleanWaterPuddles, "RainCleanWaterPuddles");
        Scribe_Values.Look(ref FogVelocity, "FogVelocity", 12);
        Scribe_Values.Look(ref FogTemp, "FogTemp");
        Scribe_Values.Look(ref PuddleChance, "PuddleChance", 0.2f);
    }
}