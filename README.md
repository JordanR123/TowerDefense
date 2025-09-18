# Co-op 3D Tower Defense

## Overview
A cooperative, wave-based **3D tower defense game** where **1–8 players** builds turrets, traps, walls, and etc to defend against hordes of enemies. The game emphasizes **co-op strategy, replayability, and persistent progression**: even if players lose, they earn XP toward permanent upgrades, ensuring that every run contributes to long-term growth.

---

## Vision
- **Co-op First**: Designed for up to 8 players, with seamless join/invite via Steam lobbies.  
- **Dynamic Building**: Place turrets, traps, walls, etc on a snap-to-grid system during and between waves.  
- **Replayable Waves**: Procedural modifiers, enemy mixes, and scaling bosses for endless variety.  
- **Persistent Progression**: XP → Account Levels → Permanent upgrades that enhance future runs.

---

## Core Gameplay Loop
1. **Build Phase**
   - Players spend team currency to place, upgrade, or sell defenses.
   - Placement rules enforce valid pathing (no full seals).
2. **Wave Phase**
   - Enemies spawn in groups and path toward the base, avoiding player-built walls.
   - Turrets target automatically; traps apply effects; players can still build mid-wave at a surcharge.
   - **Players can also shoot enemies directly**, adding an active combat layer.
3. **End of Wave**
   - Surviving enemies reduce team lives.
   - Defeated enemies award team currency and XP.
4. **Meta Progression**
   - Players earn XP regardless of win/loss.
   - XP contributes to account level and unlocks permanent upgrades.

---

## Permanent Progression System
- **XP Rewards**
  - Earned from kills, waves survived, and run completion.
  - Awarded to all players at end of wave or run.
- **Account Levels**
  - Unlock permanent, account-bound bonuses:
    - +% Turret Damage
    - +% Attack Speed
    - +% Starting Currency
    - +% Player Movement Speed
    - Build/upgrade discounts
- **Fairness Rules**
  - Upgrades capped/scaled to maintain balance across 1–8 players.
  - Progression complements teamwork, not replace it.

---

## Co-op & Networking
- **Players**: 1–8.  
- **Model**: Host-authoritative (listen server).  
- **Transport**: Steamworks.NET (or Facepunch.Steamworks) with Fish-Networking.  
- **Session Flow**:
  1. Host creates Steam lobby (public/friends).  
  2. Players join via Steam overlay/invite.  
  3. Lobby displays player slots, ready states, map selection.  
  4. Host launches match → synchronized wave RNG + XP tracking.  

**Authority**
- Host validates builds, currency, and XP distribution.
- Per-player build quotas to prevent griefing.
- Server-side rule checks for placement/path blocking.

---

## MVP Feature Set
- **Defenses**  
  - Turrets: Single-target, Splash, Slow, Fire/DOT, Buff.  
  - Traps: Spikes, tar/slows.  
  - Walls: Basic blockades (NavMesh carve).  

- **Enemies**  
  - Runner (fast, weak), Tank (slow, strong), Swarm (small, weak), Boss (wave 5/10).  

- **Systems**  
  - Wave director (scriptable waves, endless toggle).  
  - Currency + resource management.  
  - HUD: currency, lives, wave timer, XP meter.  
  - Persistent XP/level system with permanent upgrades.  
  - Multiplayer with Steam lobbies & host/join.  

- **Player Combat**  
  - Each player controls an avatar with basic shooting abilities:
    - **Primary Fire**: projectile weapon (hitscan or bullet).  
    - **Damage scales with upgrades** (global + player-specific).  
    - Limited ammo or cooldown to keep balance with turrets.  
    - Rewards currency/XP for kills (shared with team).  

- **Visual Style**  
  - Stylized low-poly (URP) for clarity and performance.  

---

## Stretch Goals
- Multiple maps with unique path layouts.  
- Daily/weekly mutators (fog, double bosses, turret bans).  
- Unlockable turrets, cosmetics, and prestige levels.  
- Dedicated server support (non-Steam fallback).  

---

## Development Milestones
**Sprint 0 — Foundation**
- Unity URP project setup, layers, grid size, Steamworks integration test, greybox map.  

**Sprint 1 — Solo Core Loop**
- Enemy pathfinding, base lives, turret building, wave director, simple HUD.  
- **Player combat prototype** (shoot basic projectiles at enemies).  

**Sprint 2 — Multiplayer Integration**
- Steam lobby host/join, synchronized spawns, shared currency.  

**Sprint 3 — XP & Permanent Progression**
- XP gain system, account levels, upgrade unlocks.  

**Sprint 4 — Content Expansion**
- More turrets, traps, enemies, maps, and balancing.  

**Sprint 5 — Polish & Performance**
- VFX, audio, UX improvements, optimization, bug fixes.  

---

## Technical Setup
- **Engine**: Unity 2022/2023 LTS (URP).  
- **Packages**:  
  - AI Navigation  
  - Input System  
  - Cinemachine (optional) 
  - Steamworks.NET + Fish-Networking  
- **Conventions**:  
  - Units: 1 Unity unit = 1 meter.  
  - Grid size: 1m.  
  - Layers: Ground, Placeables, Enemies, Projectiles, IgnoreRaycast.  
  - ScriptableObjects for enemies, turrets, waves, and upgrades.  
