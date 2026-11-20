.github/copilot-instructions.md

# GitHub Copilot Instructions for [SR]Touch Animal (Continued) Mod

## Mod Overview and Purpose
The [SR]Touch Animal (Continued) mod is an update of the Shadowrabbits mod. It adds a new behavior to pawns where they try to find animals to touch. This behavior will lift the pawn's mood and increase their animal skill experience. The mod also introduces addiction mechanics where excessive touching can cause pawns to become addicted, leading to more frequent touching but also potential mood swings during withdrawal. Additionally, the mod has a chance to make animals join the player's faction if they have strong animal skills.

## Key Features and Systems
### Animal Touching Behavior
Pawns will sometimes try to find animals to touch. This behavior requires a minimum animal skill level, which varies by animal type. Touching animals will increase the pawn's mood and animal skill experience.

### Addiction Mechanics
Excessive touching can cause pawns to become addicted. Addiction increases the frequency of animal touching but may also lead to mood swings during withdrawal.

### Animal Joining
Pawns with strong animal skills have a chance to make animals join their faction. Animals with owners will not join other factions.

### Custom Animal Types
The mod provides a UI panel where players can set the kinds of animals they want their pawns to touch.

## Coding Patterns and Conventions
### C# Files
The mod uses C# files to define various behaviors and systems. Key C# files include:
- `JobGiverTouchAnimal.cs` - Manages the job of touching animals.
- `JobGiverTouchAnimalAddiction.cs` - Manages the addiction mechanics related to touching animals.
- `ThinkNodeChancePerHourTouchAnimal.cs` - Manages the frequency of animal touching based on time.
- `ThinkNodeConditionalSatisfyTouchAnimal.cs` - Manages the conditional satisfaction of the animal touching behavior.
- `ModDef.cs` - Defines the mod itself.

### XML Integration
The mod uses XML files to define various game elements such as hediffs, jobs, needs, think trees, and thoughts. Key XML files include:
- `HediffsAddiction.xml` - Defines addiction hediffs.
- `Jobs.xml` - Defines job definitions.
- `Needs.xml` - Defines need definitions.
- `SubTree.xml` - Defines think tree sub-trees.
- `ThoughtsHediff.xml` - Defines thought definitions related to hediffs.

## Harmony Patching
The mod uses Harmony patching to modify the base game behavior. Harmony is a popular modding library for RimWorld that allows mods to safely and efficiently patch the game code without causing conflicts.

## Suggestions for Copilot
When using GitHub Copilot to assist with the [SR]Touch Animal (Continued) mod, consider the following:

1. **Follow Coding Patterns and Conventions**: Use the established patterns and conventions in the C# files to ensure consistency in your code.
2. **Integrate with XML**: When adding new game elements, ensure they are properly defined in the appropriate XML files.
3. **Use Harmony Patching**: When modifying base game behavior, use Harmony patching to avoid conflicts with other mods.
4. **Test Thoroughly**: After making changes, thoroughly test the mod to ensure it works as expected and does not introduce new bugs or issues.
5. **Document Changes**: Clearly document any changes you make, including the purpose and any potential side effects, to help other contributors understand the mod better.

By following these guidelines, you can effectively use GitHub Copilot to enhance the [SR]Touch Animal (Continued) mod and contribute to its continued development.

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

