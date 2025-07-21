# GitHub Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

The "Touch Animal" RimWorld mod introduces new interactions between pawns and animals, focusing on enhancing the player's experience through unique game mechanics. It allows pawns to interact with animals in ways that affect their needs and behaviors, introducing new job assignments, needs, and addictions related to animal interaction.

## Key Features and Systems

- **Job System:** Implements new JobGiver classes for animal interaction, including `JobGiverTouchAnimal` and `JobGiverTouchAnimalAddiction`, extending ThinkNode classes to integrate with the RimWorld job systems.
- **Addiction and Needs:** Introduces new need and addiction mechanics, such as the `NeedTouchAnimal` and `HediffAddictionTouchAnimal`, which influence pawn behavior towards animals.
- **Custom UI:** Adds new UI elements using `UIModelMain` and `UIModWindowMain` classes, allowing players to manage animal interactions and settings.
- **Configuration Settings:** Provides custom settings using `SettingHandleSelectedAnimalDefs`, giving players control over which animals can be interacted with.

## Coding Patterns and Conventions

- **Inherit from Base Classes:** Extend existing RimWorld base classes such as `ThinkNode_JobGiver`, `Need`, and `JobDriver` to ensure compatibility and reduce code duplication.
- **Static Def Classes:** Use static classes like `HediffDefOf`, `JobDefOf`, `NeedDefOf`, and `ThoughtDefOf` for defining constants and ensuring centralized management of definitions.
- **Method Organization:** Maintain clear method naming and encapsulate functionality within methods like `CalcAddiction`, `CalcJoin`, and `Notify_NeedCategoryChanged` to enhance readability and maintainability.

## XML Integration

- Utilize XML to define mod settings, job definitions, and other in-game elements. Although specific XML files aren't summarized here, XML usually complements C# definitions in RimWorld mods by defining structure, defaults, and integration points for the mod content.

## Harmony Patching

- Use Harmony for method patching to safely alter the behavior of core game methods without direct modification. This allows for compatibility with other mods and the ability to override or extend base game functionalities as needed.

## Suggestions for Copilot

- **Documentation Assistance:** Use comments in code to outline how each component interacts with others. Copilot can help by suggesting relevant comments and documentation snippets to ensure clarity in complex systems.
- **Code Completion:** Assist in the completion of method implementations for classes like `JobDriverTouchAnimal` and `NeedTouchAnimal` by providing typical patterns seen in modding scenarios.
- **Pattern Recognition:** Recognize and suggest extensions of the base RimWorld classes to adhere to modding conventions.
- **XML Generation:** Facilitate the automatic generation of XML structures when data definitions are updated in C#, ensuring a coherent integration between the code and data layers.

This guide provides an overview for developers working on the "Touch Animal" mod and how to leverage GitHub Copilot to enhance their workflow.
