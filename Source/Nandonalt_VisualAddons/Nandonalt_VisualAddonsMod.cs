using Mlie;
using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons;

[StaticConstructorOnStartup]
internal class Nandonalt_VisualAddonsMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static Nandonalt_VisualAddonsMod Instance;

    private static string currentVersion;

    /// <summary>
    ///     The private settings
    /// </summary>
    private Nandonalt_VisualAddonsSettings settings;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public Nandonalt_VisualAddonsMod(ModContentPack content) : base(content)
    {
        Instance = this;
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal Nandonalt_VisualAddonsSettings Settings
    {
        get
        {
            settings ??= GetSettings<Nandonalt_VisualAddonsSettings>();

            return settings;
        }
        set => settings = value;
    }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Nandonalt Visual Addons";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        listingStandard.Gap();
        listingStandard.CheckboxLabeled("NVA.ColdFog".Translate(), ref Settings.ColdFog,
            "NVA.ColdFog.Tooltip".Translate(0f.ToStringTemperature()));
        listingStandard.CheckboxLabeled("NVA.IceLayer".Translate(), ref Settings.IceLayer,
            "NVA.IceLayer.Tooltip".Translate(0f.ToStringTemperature()));
        listingStandard.CheckboxLabeled("NVA.BilliardsCue".Translate(), ref Settings.BilliardsCue,
            "NVA.BilliardsCue.Tooltip".Translate());
        listingStandard.Gap();

        listingStandard.Label("NVA.FogVelocity".Translate(Settings.FogVelocity), -1,
            "NVA.FogVelocity.Tooltip".Translate());
        listingStandard.IntAdjuster(ref Settings.FogVelocity, 1);
        listingStandard.Gap();

        listingStandard.Label("NVA.FogTemp".Translate(((float)Settings.FogTemp).ToStringTemperature()), -1,
            "NVA.FogTemp.Tooltip".Translate());
        listingStandard.IntAdjuster(ref Settings.FogTemp, 1, -273);
        if (Settings.FogTemp > 0)
        {
            Settings.FogTemp = 0;
        }

        listingStandard.Gap();

        listingStandard.CheckboxLabeled("NVA.RainWaterPuddles".Translate(), ref Settings.RainWaterPuddles,
            "NVA.RainWaterPuddles.Tooltip".Translate());
        if (Settings.RainWaterPuddles)
        {
            listingStandard.CheckboxLabeled("NVA.RainCleanWaterPuddles".Translate(),
                ref Settings.RainCleanWaterPuddles,
                "NVA.RainCleanWaterPuddles.Tooltip".Translate());
            listingStandard.Label("NVA.PuddleChance".Translate(Settings.PuddleChance.ToStringPercent()), -1f,
                "NVA.PuddleChance.Tooltip".Translate());
            Settings.PuddleChance = listingStandard.Slider(Settings.PuddleChance, 0, 1f);
        }

        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("NVA.CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}