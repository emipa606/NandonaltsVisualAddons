# Copilot Instructions for Nandonalt Visual Addons Mod

## Mod Overview and Purpose
The Nandonalt Visual Addons Mod for RimWorld is designed to enhance the visual aesthetics of the game by adding detailed graphical overlays, effects, and enhanced functionalities. This mod primarily focuses on improving the game's visual feedback mechanisms, providing players with a more immersive and visually appealing experience.

## Key Features and Systems
- **Visual Enhancements:** Includes additional overlays such as weather effects and equipment visuals to augment the game's aesthetic.
- **Customizable Settings:** Users can modify visual settings to suit their preferences within the game options.
- **Filth Management Enhancements:** Introduces new mechanics for managing and cleaning filth effectively in the game environment.
- **Dynamic Overlays:** Provides overlays that change dynamically with different in-game events, such as weather conditions.

## Coding Patterns and Conventions
- **Naming Conventions:** Classes and methods follow PascalCase naming conventions. Internal classes and methods are used for encapsulation, whereas public classes are exposed when necessary.
- **File Organization:** Each class is separated into its own file to maintain a clean codebase and facilitate maintainability.
- **Static Classes:** Used for utility and patching purposes where instances of the class aren’t needed (e.g., `HarmonyPatches`).

## XML Integration
Although the XML integration details are not included in the summary, it is typically used within RimWorld mods to define game objects, textures, and specific properties. For new features that require XML definitions:
- Ensure XML files are appropriately structured and referenced within the mod.
- Use XML to handle configurations that go beyond core C# functionalities.
- Validate XML files for proper schema and formatting, ensuring seamless integration with the game's existing systems.

## Harmony Patching
Harmony patches are crucial for modifying and extending the behavior of existing game methods without altering the original codebase:
- **Static Class `HarmonyPatches`:** Initialize Harmony patches within this class to maintain organization and simplicity.
- **Prefix and Postfix Methods:** Define prefix methods to execute logic before the original method and postfix methods to execute logic after the original method.
- **Patch Example:** Use `[HarmonyPatch]` attributes to target specific methods in the game's assemblies. For instance, `CleanFilthPrefixPatch` exemplifies how to intercept and modify the cleaning mechanics.
  
## Suggestions for Copilot

When using GitHub Copilot to assist with mod development:
- **Enhance Visual Effects:** Prompt Copilot for advanced graphical overlay algorithms to improve effects like weather dynamics or equipment visuals.
- **Extend Harmony Patches:** Suggest Copilot generate new patches for additional gameplay features or optimizations by targeting relevant game methods.
- **Code Refactoring:** Utilize Copilot for identifying optimization opportunities, such as code simplification or performance improvements.
- **User Interface Enhancements:** Ask Copilot for solutions to integrate custom mod settings into the game’s UI.
- **Documentation Assistance:** Encourage Copilot to help draft comprehensive documentation and comments for better code understanding and future modification.

By following these guidelines and leveraging Copilot effectively, developers can streamline their workflow and enhance the modding capabilities of RimWorld.
