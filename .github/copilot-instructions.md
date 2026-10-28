# GitHub Copilot Instructions for Nandonalt's Visual Addons (Continued)

## Mod Overview and Purpose

Nandonalt's Visual Addons (Continued) is an updated version of the original Nandonalt's Visual Addons mod, now maintained by Jastro and continued up to RimWorld version B18. The mod introduces various visual enhancements to the game, including cold fog effects, water puddles, cloudy weather, and billiard cues. It aims to provide a more immersive experience without requiring a new save file.

## Key Features and Systems

### Cold Fog and "Ice Layer"
- When a room's temperature goes below 0°C, a cold fog effect similar to freezers is emitted.
- An ice layer forms just above the floor.

### Water Puddles
- Rain forms water puddles on the map.
- Puddles dry up when it stops raining.
- Pawns can spread water easily.

### Cloudy Weather
- A new weather overlay with minimal effects on accuracy and wind speed.

### Billiard Cues
- Pawns use billiard cues when playing billiards.

## Coding Patterns and Conventions

- **Harmony Patching**: The mod uses the Harmony library by Pardeike for patching existing game code. Harmony patches are encapsulated in the `HarmonyPatches.cs` file.
- **Settings Management**: Settings are managed through a custom settings window, accessible via the `DoSettingsWindowContents` method in the `Nandonalt_VisualAddonsMod.cs` file.
- **Weather Overlays**: Custom weather overlays are implemented by extending the `WeatherOverlay` class, as seen in `WeatherOverlay_Cloudy.cs`.

## XML Integration

The mod integrates with RimWorld's XML definition system to add new filth types, mote effects, and weather definitions. Key XML files include:

- **Filth_Various.xml**: Defines various filth types.
- **Mote_Visual.xml**: Defines visual mote effects and things.
- **Weathers.xml**: Defines new weather conditions.

## Harmony Patching

Harmony is used to patch the following methods:

- **JobDriver_CleanFilth_TryMakePreToilReservations.cs**: Contains a prefix patch for `TryMakePreToilReservations`.
- **WeatherOverlayDualPanner_TickOverlay.cs**: Contains a postfix patch for `TickOverlay`.
- **WeatherOverlay_Cloudy.cs**: Contains the implementation of the `WeatherOverlay_Cloudy` class.

## Suggestions for Copilot

When using GitHub Copilot in the context of this mod, consider the following guidelines:

1. **Harmony Patching**: When writing Harmony patches, ensure that the patch methods are correctly annotated with `Prefix`, `Postfix`, or `Transpiler` attributes.
2. **XML Definitions**: When generating XML definitions, ensure that they conform to the RimWorld XML schema and are placed in the appropriate directories.
3. **Settings Management**: When adding new settings, ensure that they are properly integrated into the settings window and that the UI is updated accordingly.
4. **Weather Overlays**: When implementing new weather overlays, ensure that they extend the `WeatherOverlay` class and that their behavior is correctly defined.

By following these guidelines, you can effectively use GitHub Copilot to enhance and maintain the Nandonalt's Visual Addons (Continued) mod.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

