Where you are now (baseline)

Solo POC runs: Player moves & shoots, enemies spawn via spawner and follow a path, turrets place during Build and auto-shoot, waves advance, currency increments.

Minimal HUD exists (wave/currency).

You’ve proven the loop works. Now we’ll add clarity, persistence, and polish.

P0 — Must-Do (your 6 items)
1) Build-mode banner + turret ghost

Context (why): Players need unmistakable feedback they’re in Build phase and exactly where a placement will land. This reduces mis-clicks and teaches the loop.

Create / Update

UI: “BUILD PHASE” banner (TextMeshProUGUI) anchored top-center; hidden outside Build.

Prefab: Turret_Ghost (duplicate of your turret mesh), set transparent material, layer = Ignore Raycast.

Script responsibilities (no code yet):

UIBuildBanner — Shows/hides banner based on GameManager phase.

BuildGhost — Tracks mouse projection on ground, snaps to grid, positions ghost; tints green/red based on validity.

Reuse existing BuildSystem placement rules for “valid/invalid” checks to avoid drift between ghost and placement.

Editor wiring

Canvas: add text element; hook to banner script.

Scene: empty “BuildGhost” object with ghost logic; assign Camera, Ground layer mask, and Turret_Ghost.

Material: ghost material with ~35–50% alpha; ensure it renders above ground (slightly raised Y).

Acceptance

During Build: banner visible; translucent turret follows cursor; turns red over blocked spots; click places exactly where ghost shows.

During Wave: banner hidden; ghost hidden.

Pitfalls

Ghost blocking its own raycast: ensure ghost is on Ignore Raycast layer.

Snap mismatch: ghost and placement must use the same grid rounding and validity check.

Future-proof

The same ghost logic can later preview walls/traps by swapping the ghost prefab based on the selected buildable.

2) Health bars on enemies and on the base

Context: HP visualization is core feedback: shows turret value, player impact, and urgency.

Create / Update

Prefab: UI/HealthBar (World-Space canvas with background + fill Image).

Enemy prefab: child “Head” transform at top; reference for bar position.

Base object: add max HP / current HP; reference a health bar positioned above it.

Script responsibilities:

HealthBar — Orients toward camera, updates fill on HP change.

Hooks from Enemy and BaseHealth — Invoke “Set(current, max)” on damage.

Editor wiring

Put the health bar in a Resources folder or expose it on prefabs for quick instantiation.

Ensure world-space canvas is scaled properly (tiny scale, e.g., 0.01) and billboards toward the camera.

Acceptance

Enemies show HP bars that shrink on hits; disappear on death.

Base shows HP bar that shrinks as enemies reach and damage it.

Pitfalls

Bars not facing the camera (no billboarding).

Bar scale wrong (giant in world space) — fix with world-space canvas scale.

Future-proof

Convert to a pooled system later to avoid instantiate/destroy spikes.

3) Lose screen with XP counter + permanent buffs

Context: Even on a loss, players progress. Simple persistence keeps players engaged between runs.

Create / Update

UI: “Defeat” panel with: final XP, upgrade points, and 2-3 simple permanent upgrades (e.g., +5% Turret Damage, +5% Player Move Speed, +5% Starting Currency), plus “Play Again” button.

Data (lightweight for now): progression numbers (XP per kill/wave, loss bonus), simple “points” to spend.

Script responsibilities:

XPSystem — Tracks XP/level/points across runs (persisted).

GameOverUI — Shows panel when GameManager hits GameOver; buttons grant chosen buffs (persist) and restart.

SaveService (lightweight) — Read/write a JSON or PlayerPrefs keys for buffs and XP.

Editor wiring

Canvas: add a hidden panel; wire buttons to “Add buff” and “Play Again.”

On scene start: read saved buffs; apply passive modifiers (e.g., multiply turret damage, add to starting currency).

Acceptance

On loss, a panel appears with accurate XP.

Choosing a buff reduces points available and persists to the next run.

Restart applies the saved buffs.

Pitfalls

Forgetting to apply buffs at the start of the next run.

Over-stacking (cap or display cumulative values clearly).

Future-proof

Replace simple buttons with an UpgradeDefinition asset tree later; keep today’s keys forward-compatible.

4) First-person view (FP)

Context: You already move and shoot; FP alignment makes aiming natural.

Create / Update

Camera: move the player Camera to head height (e.g., y ≈ 1.6) inside the player; ensure clipping plane is small (~0.01) to avoid near-plane clipping of muzzle.

Player mesh (optional): hide own body if it clips the camera.

Script responsibilities:

Reconfirm yaw on player root and pitch on camera.

Muzzle transform slightly in front of the camera to avoid shooting into your capsule.

Editor wiring

Adjust camera local position; confirm cursor lock toggles properly with Pause.

Acceptance

Mouse look rotates smoothly; shots travel straight from the camera/muzzle; no self-collisions.

Pitfalls

Bullet colliding with player — push muzzle forward, exclude player layer from bullet collision.

Future-proof

Swap to Cinemachine FP rig later for damping and head-bob; keep your current layout Camera-as-child of Player.

5) Pause menu

Context: Essential for testing and UX (resume/restart/quit; time scale 0; cursor unlock).

Create / Update

UI: Pause panel with Resume / Restart / Quit.

Script responsibilities:

PauseMenu — Toggles time scale, cursor state, and panel on Esc; handles button events.

Editor wiring

Default: panel hidden. Buttons wired to Resume, Restart(scene), Quit.

Acceptance

Press Esc: game pauses, cursor unlocks, panel shows.

Resume returns to locked cursor and time scale = 1.

Pitfalls

Forget to re-lock cursor after resuming.

Time scale conflicts with tween/FX systems — keep pause-sensitive systems in mind later.

Future-proof

Add Options (audio sliders, sensitivity) in the same panel, routed to an AudioMixer and settings store.

6) Remove waypoints; enemies auto-seek the base

Context: Simplifies pathing for now; enemies make a beeline to the base. Later you’ll replace this with NavMesh.

Create / Update

Enemy logic: instead of an array of waypoints, each enemy finds the Base by tag and moves directly toward it; on proximity, apply base damage and self-destruct.

Script responsibilities:

Enemy — Look up base transform on init; move vector toward base; base damage threshold; keep existing HP/loot calls.

Editor wiring

Ensure base object is tagged Base.

Tweak enemy speed and damage to keep waves fair with direct paths.

Acceptance

Enemies spawn, rotate toward the base, and march straight to it; base loses lives/HP on contact.

Pitfalls

Enemy spawns partially below ground (set spawn and base Y ≈ 1).

Enemies stack inside the base cube; consider a small radius check or simple collider trigger to handle “hit & die” gracefully.

Future-proof

Replace direct movement with NavMeshAgent + NavMeshObstacle carve later to support mazes and path-blocking rules.

P1 — Should-Do (after the 6 items)
A) Content definitions (data-driven)

Why: Enable variety without new code.

ScriptableObjects:

TurretDefinition (cost, range, fire rate, damage, status effects, projectile/hitscan).

EnemyDefinition (HP, speed, armor, bounty/XP).

WaveDefinition (spawn groups, counts, delays, boss flag).

GameBalanceConfig (global multipliers, start currency by player count, sell penalty).

ProgressionConfig (XP per kill/wave, loss bonus, XP curve).

UpgradeDefinition (effect type, tiers, costs).

Editor: Build a few turret and enemy variants; 5–10 hand-crafted waves.

Acceptance: Swapping a definition changes behavior without script edits.

Pitfalls: Forgetting to assign definitions to prefabs/controllers.

B) Build UX v2

Why: Clarity and speed.

Build menu: a simple panel with buttons/icons for each TurretDefinition.

Placement validator v1: overlap check + “min distance from base area.”

Acceptance: Choosing a turret in the menu updates ghost and cost UI; invalid placements show red ghost.

Pitfalls: Ghost not matching placement rules.

C) Pathing rules (proper TD)

Why: Prevent full path sealing.

NavMesh Surface on ground; Walls with NavMeshObstacle + Carve.

Validator v2: temporary carve test to ensure path remains from spawns to base and that minimum corridor width is respected.

Acceptance: Attempts to seal the path are rejected with a clear toast.

Pitfalls: Re-baking whole NavMesh on every preview; rely on carve, not rebake.

D) Juice & clarity

Hit VFX, muzzle flash, impact sparks, camera shake on base hit.

World-space damage numbers (pooled).

Audio bus + mixer: SFX/Music sliders; add basic SFX set for fire/hit/death/UI.

Acceptance: Combat feels “alive” without frame spikes.

E) Performance & reliability

Object pooling (bullets, hits, numbers, health bars).

QA test scenes: Solo_Loop, Balance_Sandbox (DPS meter), and a future Net_Ping.

Acceptance: Stable frame-time; GC allocs near zero during combat.

F) Save system v1

Replace PlayerPrefs with a SaveService JSON (persistentDataPath) for XP, upgrades, and options (volume, sensitivity).

Version your save to allow migrations.

Acceptance: Quit/restart retains XP and selected buffs; options persist.

Suggested implementation order & commits

Build banner + ghost

Health bars (enemy + base)

Lose screen + XP + simple buff persistence

First-person polish (camera position, muzzle, cursor lock)

Pause menu

Base-seeking enemies (remove waypoints)

Definitions (turrets/enemies/waves)

Build menu + validator v1

NavMesh carve + validator v2

Juice + pooling + save v1

Commit examples:

feat(build): add build banner and turret ghost with grid snap

feat(ui): world-space health bars for enemies and base

feat(meta): defeat panel with XP and permanent buffs

feat(fp): first-person camera setup and muzzle alignment

feat(core): pause menu (timescale, cursor lock)

refactor(enemy): remove waypoints; base-seeking behavior

feat(data): add turret/enemy/wave definitions and hook-ups

feat(build): simple build menu and placement validator v1

feat(path): navmesh carve and no-seal rule

chore/juice: audio, vfx, damage numbers; pool bullets

feat(save): JSON save for XP, buffs, and options

Done-when checklist (vertical slice)

Clear build phase with ghost preview and banner.

Enemies and base show HP; combat readable at a glance.

Loss leads to a simple XP/buff choice that persists to the next run.

First-person feels natural; pause works.

Enemies beeline to base (for now) and damage it reliably.

Basic economy pacing (costs, bounties) feels fair for the first 5–10 waves.