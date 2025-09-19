# Co-op 3D Tower Defense

## Overview
A cooperative, wave-based **3D tower defense game** built in Unity using the **FPS Engine** as a foundation.  
Players combine **first-person combat** with **strategic turret placement** to survive against endless waves of enemies.  
The game emphasizes **co-op strategy, replayability, and persistent progression**: even if players lose, they earn XP toward permanent upgrades, ensuring that every run contributes to long-term growth.

---

## Vision
- **Co-op First**: Designed for 1–8 players, with seamless join/invite through Steam lobbies.  
- **Hybrid Gameplay**: Players can fight enemies directly (FPS) while also building turrets, traps, and walls.  
- **Replayable Waves**: Procedural modifiers, enemy mixes, and scaling bosses keep every run fresh.  
- **Persistent Progression**: XP unlocks permanent buffs and upgrades that carry across games.  

---

## Core Gameplay Loop
1. **Build Phase**  
   - Spend currency to place turrets, traps, and walls on a grid.  
   - Ghost previews and placement rules make building clear and fair.  

2. **Wave Phase**  
   - Enemies spawn in waves and attempt to reach the base.  
   - Turrets auto-fire while players actively fight in first-person.  
   - Currency and XP earned for kills and surviving waves.  

3. **Progression**  
   - Even on defeat, players earn XP.  
   - XP unlocks permanent buffs (damage, speed, currency, etc.) that improve future runs.  

---

## High-Level Roadmap
Development is staged so the game grows from a simple solo loop into a full multiplayer experience.  
Details and acceptance criteria are in [Docs/ROADMAP.md](Docs/ROADMAP.md).

### Stage 0 — Baseline
- FPS Engine imported and working (movement, shooting, turrets, managers, UI).  

### Stage 1 — Vertical Slice
- Solo loop: build → wave → reward.  
- Currency, spawner, and minimal HUD in place.  

### Stage 2 — P0 Must-Haves
- Build-phase banner + turret ghost.  
- Enemy & base health bars.  
- Lose screen with XP + persistent buffs.  
- First-person camera polish.  
- Pause menu.  
- Enemies march directly to base.  

### Stage 3+ — Expansions
- **Data-Driven Content**: ScriptableObjects for turrets, enemies, waves.  
- **Build UX v2**: Build menu and stronger placement rules.  
- **Pathing Rules**: NavMesh carve system to prevent path sealing.  
- **Juice & Clarity**: VFX, audio, floating damage numbers, pooling.  
- **Save System v1.5**: JSON saves for XP, buffs, and options.  
- **Co-op Foundations**: Up to 8-player support via Steam lobby/relay.  

---

## Tech & Tools
- **Unity** (2021.1.16 or newer recommended).  
- **FPS Engine** (base kit providing player controller, turrets, enemies, UI, managers).  
- **TextMeshPro** for UI clarity.  
- **NavMesh** for future pathing rules.  
- **Steamworks / NGO / Photon** (planned for multiplayer).  

---

## Contributing
- See [Docs/ROADMAP.md](Docs/ROADMAP.md) for detailed milestones and commit examples.  
- Please make small, atomic commits (e.g., `feat(build): add turret ghost`).  
- Use branches per feature (`feature/build-ghost`, `feature/health-bars`).  

---

## License
TBD
