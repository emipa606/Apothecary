# GitHub Copilot Instructions for Apothecary (Continued)

## Mod Overview and Purpose

**Apothecary (Continued)** is a RimWorld mod designed to enhance gameplay by introducing herbal remedies and medicinal preparations suitable for the Neolithic and Medieval epochs. It serves as a bridge before transitioning into more technical medical supplements typical of the industrial period, making it ideal for players interested in a fantasy or early epochs theme. While less potent than industrial-era medicines, these herbal solutions are effective, providing variety and depth to early-game medical systems.

## Key Features and Systems

- **Herbal Remedies and Medicines**: Integrates various herbal treatments suitable for Neolithic and Medieval settings, effectively managing player colonists' health needs in early game settings.
- **Research Projects**: Introduces new research options in their own tab, simplifying the management of new technologies and methodologies.
- **Integration with Other Mods**: Offers compatibility with a variety of mods by leveraging charcoal as a functional resource across multiple systems.
- **Charcoal Economy**: Implementations for creating and using charcoal in kilns and various production processes, enhancing the crafting and resource management experience.
- **Seeds Please Support**: New textures for seeds are available when used in conjunction with the Seeds Please mod, thanks to community contributions.

## Coding Patterns and Conventions

- **Class Naming**: Classes use a prefix of 'AY' for swift identification, e.g., `AYCureUtility`, `AYWashUtility`.
- **Methods**: Consistent method naming conventions are used with 'VerbNoun' structure, ensuring clarity in method functionality, e.g., `SetTicksToCure`, `TryHealRandomOldWound`.
- **Access Modifiers**: Predominantly uses `public` and `internal` access modifiers to define class and method visibility.
- **Static Classes**: Utilized for utility functions where instance state is unnecessary, improving efficiency and reducing overhead where possible.

## XML Integration

- **Defining Items and Recipes**: Integrate new items and plant definitions through XML files, allowing patching and extension without altering base game files.
- **Patch Integration**: Using XML patches to merge added functionality like seed definitions, charcoal production methods, and disease treatments seamlessly with the game's core mechanics.

## Harmony Patching

- **Patch Structure**: Uses 'prepatch' and 'postpatch' classes to integrate new mechanics without altering existing game code.
- **Class Implementation**: Observes classes like `AYFoodUtility_AddFoodPoisoningHediff_prepatch` and `HealthAIUtility_FindBestMedicine_PostPatch` to enhance or modify existing game logic.
- **Dynamic Features**: Offers flexible adjustments with patch methods, ensuring compatibility with updates and other mods while minimizing conflicts.

## Suggestions for Copilot

1. **Prompt for Class Extensions**: When suggesting new features, Copilot could recommend appropriate class extensions and method additions, ensuring alignment with existing naming conventions and structures.
2. **XML Patch Suggestions**: Provide templates for new XML patches tailored to items and recipes, streamlining the integration of new elements.
3. **Harmony Patching Queries**: Help outline potential Harmony patches by suggesting 'prefix' and 'postfix' methods for modding specific game logic, particularly in older epochs-related content.
4. **Error Handling**: Suggest robust error handling paradigms for methods interacting with crucial gameplay elements like medicine and research utility.
5. **Compatibility Advisories**: Highlight code blocks or XML patches that intersect with other mods, prompting validations for versions and compatibility.

By leveraging the above patterns and suggestions, developers can ensure efficient utilization of GitHub Copilot for the continued development and enhancement of the Apothecary mod in RimWorld.

## Closing Notes

For further details, refer to the [Apothecary Notes PDF](#) and explore cross-mod synergies and additional content that enhances the gameplay experience. Your contributions towards refining the early epochs of RimWorld are valuable, welcome, and appreciated under the Creative Commons CC BY-NC-SA 4.0 License.
