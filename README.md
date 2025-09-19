# Co-op 3D Tower Defense

A cooperative, wave-based **3D tower defense** for **1–8 players**. Build turrets, traps, and walls to defend against hordes. Even on defeat, teams earn **XP** toward **permanent account upgrades** (damage, attack speed, currency, movement, build discounts), so every run advances long-term progression. Players can also actively **shoot enemies** during waves.

---

## Vision & Pillars
- **Co-op First:** 1–8 players via Steam lobbies/invites; host-authoritative netcode.
- **Snappy Building:** Grid-snap placement, rotate, upgrade, sell; path rules prevent full seals.
- **Readable Chaos:** Big waves, clear VFX/UX, strong perf on mid-range GPUs.
- **Persistent Progression:** XP → levels → permanent bonuses (capped for fairness).

---

## Core Loop
1) **Build Phase** – spend team currency; place/upgrade/sell; valid path enforced.  
2) **Wave Phase** – enemies spawn and path around carved obstacles; **players can shoot**.  
3) **End of Wave** – lives reduced by leaks; currency & XP awarded.  
4) **Meta** – XP increases account level, unlocking permanent upgrades.

---

## Player Shooting
- **Mode:** TPS/FPS or top-down twin-stick (decide per map; default TPS).  
- **Primary Fire:** hitscan or projectile; modest DPS so turrets remain core.  
- **Balance:** cooldown or ammo to avoid overshadowing towers; team-shared rewards.  
- **Upgrades:** permanent bonuses also affect player damage/ROF within caps.

---

## Permanent Progression
- **XP Sources:** kills, waves survived, run completion (always granted on loss).  
- **Account Level:** unlocks tiered bonuses (e.g., +2% dmg, +3% ROF, +5% start money, +5% movement, −3% build costs).  
- **Fairness:** caps & team scaling tuned for 1–8 players.

---

## Co-op & Networking
- **Players:** 1–8 | **Model:** host-authoritative (listen server).  
- **Transport:** Steam P2P (Steamworks.NET / Facepunch) + Fish-Networking (recommended) or Unity Netcode.  
- **Authority:** host validates placements, economy, XP; per-player build quotas; min path width rule.  
- **Session Flow:** host creates lobby → friends join → ready-up → run → summary.

---

## MVP Feature Set
**Defenses**: Single, Splash, Slow, DOT/Flame, Buff; traps (spikes/tar); walls (NavMesh carve)  
**Enemies**: Runner, Tank, Swarm, Boss (waves 5/10)  
**Systems**: Wave director (SO-driven; endless toggle), shared currency, HUD (lives, money, wave, XP), **player shooting**  
**Style**: URP stylized/low-poly for clarity and performance

---

## Milestones
**Sprint 0 — Foundation**  
URP project; layers; input; Steamworks smoke test (AppID 480 in dev); greybox map; baseline NavMesh.  

**Sprint 1 — Solo Core**  
Pathing to goal; lives; build grid + ghost; basic turret; 5 waves; simple HUD; **player shooting prototype**.  

**Sprint 2 — Co-op**  
Steam lobby host/join; sync spawns; authoritative building; currency/XP sharing.  

**Sprint 3 — Progression**  
XP accrual, level tiers, permanent bonuses; save/load host account.  

**Sprint 4 — Content**  
Add 2 turrets + 1 trap + 1 enemy + boss mechanics; balance pass.  

**Sprint 5 — Polish**  
VFX/audio, UX, perf (pooling, LOD, shadows), bug fixes.

---

## Tech & Conventions
- **Unity:** 2022/2023 LTS (URP)  
- **Packages:** AI Navigation, Input System, (Cinemachine optional), Steamworks.NET, FishNet/NGO  
- **Units:** 1u = 1m · **Grid:** 1m · **Layers:** `Ground`, `Placeables`, `Enemies`, `Projectiles`, `IgnoreRaycast`  
- **Data:** ScriptableObjects for enemies, turrets, waves, upgrades  
- **Folders:**
