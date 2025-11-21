# SimpleMOBA

Initial Unity scaffold for a portrait-oriented, single-lane mobile MOBA prototype (Android/iOS).

## Script roster
- **Assets/Scripts/Core/GameManager.cs** — handles match flow, spawning heroes/minions, and win conditions.
- **Assets/Scripts/Core/HealthComponent.cs** — shared health/damage logic for heroes, minions, and towers.
- **Assets/Scripts/Core/TargetingSystem.cs** — helper utilities for selecting nearest enemies plus team provider interface.
- **Assets/Scripts/Core/Team.cs** — simple team enumeration for Team A/Team B.
- **Assets/Scripts/Gameplay/Managers/MinionSpawner.cs** — spawns minion waves for each team on a timer.
- **Assets/Scripts/Gameplay/Minions/MinionController.cs** — owns team data and connects AI/health.
- **Assets/Scripts/Gameplay/Minions/MinionAI.cs** — basic lane-following movement and auto-attack behavior.
- **Assets/Scripts/Gameplay/Towers/TowerController.cs** — tower state wrapper with vulnerability toggles and data application.
- **Assets/Scripts/Gameplay/Towers/TowerAI.cs** — tower targeting and attack cadence.
- **Assets/Scripts/Gameplay/Heroes/HeroController.cs** — movement, ability triggers, and player/AI separation.
- **Assets/Scripts/Gameplay/Heroes/HeroAutoAttack.cs** — continuous auto-attack targeting the nearest enemy in range.
- **Assets/Scripts/Gameplay/Heroes/Ability.cs** — base ability class with cooldown handling.
- **Assets/Scripts/Gameplay/Heroes/BasicBurstAbility.cs** — sample basic ability (AoE damage around hero).
- **Assets/Scripts/Gameplay/Heroes/UltimateBuffAbility.cs** — sample ultimate (temporary move/attack speed buff).
- **Assets/Scripts/UI/MobileJoystick.cs** — floating joystick for movement input.
- **Assets/Scripts/UI/UIManager.cs** — HUD orchestration (health, cooldowns, towers, timer hooks).
- **Assets/ScriptableObjects/Heroes/HeroData.cs** — hero stat and prefab definition.
- **Assets/ScriptableObjects/Minions/MinionData.cs** — minion stat and prefab definition.
- **Assets/ScriptableObjects/Towers/TowerData.cs** — tower stat and prefab definition.

## Notes
- Scripts are stubbed to compile and include TODO markers for FX, AI polish, and UI hookups; towers respect vulnerability for base gates.
- Game flow defaults to single-player vs. AI; networking is intentionally decoupled for later integration.
- Portrait orientation should be enforced in Unity player settings when creating the project.

## Quick-create example prefabs

To make Play testing immediate, the project includes an Editor utility to generate simple placeholder prefabs:

- In the Unity Editor menu choose `SimpleMOBA -> Create Example Prefabs`.
- This creates three prefabs under `Assets/Prefabs/`: `MinionPrefab`, `HeroPrefab`, and `TowerPrefab`.
- You can then assign those prefabs to the corresponding ScriptableObjects (MinionData/HeroData/TowerData) and open the demo scene.

Also use `SimpleMOBA -> Create Demo Scene` to scaffold `Assets/Scenes/DemoScene.unity` with spawn points and a `MinionSpawner`.
