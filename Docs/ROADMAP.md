# TowerDefense Roadmap

This roadmap builds on **Docs.md** and the FPS Engine foundation.  
It’s staged so you can grow from a vertical slice into a co-op game with persistent upgrades.

---

## Stage 0 — Project & Kit Baseline
- Import FPS Engine (Unity ≥2021.1.16).
- Verify FPS sandbox: player moves, shoots, enemies, turrets, PauseMenu, ExperienceManager, PoolManager exist.
- Done when: player controller runs and shooting works in a blank scene.

---

## Stage 1 — Vertical Slice (Core Loop)
- **Spawner & Waves:** simple script; enemies spawn in groups and march to base.
- **Turret Placement v1:** snap-to-grid, valid/invalid check; use kit Turret logic.
- **Economy v1:** earn currency per kill; spend in build phase.
- **HUD v1:** wave #, currency, build/fight indicator.

---

## Stage 2 — P0 Must-Dos (from Docs.md)
1. **Build-mode banner + turret ghost**  
   Banner + translucent ghost with green/red placement feedback.
2. **Health bars**  
   World-space bars for enemies and base.
3. **Lose screen + XP counter + buffs**  
   Simple progression; persistent buffs via JSON/PlayerPrefs.
4. **First-person view polish**  
   Camera alignment; muzzle forward.
5. **Pause menu**  
   ESC toggles timeScale 0, cursor unlock, menu UI.
6. **Remove waypoints**  
   Enemies seek base directly; damage on contact.

---

## Stage 3 — Data-Driven Content
- ScriptableObjects: TurretDefinition, EnemyDefinition, WaveDefinition, GameBalanceConfig, ProgressionConfig, UpgradeDefinition.
- Done when: editing SOs changes gameplay without script edits.

---

## Stage 4 — Build UX v2
- Build menu (UI buttons/icons for turrets).
- Validator v1: overlap checks, distance from base.

---

## Stage 5 — Pathing Rules
- Use NavMeshSurface + NavMeshObstacle carve.  
- Validator v2: reject placements that seal the path.

---

## Stage 6 — Juice & Performance
- VFX: muzzle flash, hit sparks, camera shake.  
- Floating damage numbers.  
- Audio bus + mixer sliders.  
- Pooling bullets, VFX, health bars.

---

## Stage 7 — Save System v1.5
- JSON save in `persistentDataPath`.  
- Persist XP, buffs, unlocked turrets, options.

---

## Stage 8 — Co-op Foundations
- Netcode + Steam lobby/relay.  
- Sync build placement, waves, projectiles, and shared currency.

---

## Commit Examples
- `feat(build): add build banner and turret ghost with grid snap`
- `feat(ui): world-space health bars (enemies/base)`
- `feat(meta): defeat panel with XP + persistent buffs`
- `feat(fp): first-person camera + muzzle alignment`
- `feat(core): pause menu (timescale, cursor lock)`
- `refactor(enemy): remove waypoints; direct base-seek`
- `feat(data): add SOs for turrets/enemies/waves`
- `feat(build): build menu + validator v1`
- `feat(path): navmesh carve + no-seal rule`
- `chore/juice: VFX/audio/damage numbers + pooling`
- `feat(save): JSON save for XP, buffs, and options`

---

## Done-When Checklist (Vertical Slice)
- Build phase banner + ghost preview.
- Enemy + base HP bars visible.
- Loss → XP + buff choice persists to next run.
- FPS aiming feels natural; pause works.
- Enemies reliably damage base.
- Basic economy pacing for first 5–10 waves.
