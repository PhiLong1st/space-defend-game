# SPACE DEFEND - Architecture Documentation

## 📋 Overview

**Space Defend** is a 2D tactical tower defense game built with Unity 6 using a **system-based architecture**. This codebase prioritizes **clarity, maintainability, and scalability** for a small development team.

### Core Principles

- ✅ **Systems own logic** - MonoBehaviours are thin adapters
- ✅ **Clear responsibility boundaries** - No system directly controls another
- ✅ **Data-driven design** - ScriptableObjects for configuration
- ✅ **Composition over inheritance** - Minimal class hierarchies
- ✅ **Event-based communication** - Decoupled systems via EventBus

---

## 🏗️ Architecture Overview

### System Layers

```
┌─────────────────────────────────────┐
│       PRESENTATION LAYER            │
│  (Views, UI, VFX - no logic)        │
└─────────────────────────────────────┘
            ↕ (Commands)
┌─────────────────────────────────────┐
│       CONTROLLER LAYER              │
│  (ShipController, EnemyController)  │
└─────────────────────────────────────┘
            ↕ (Events)
┌─────────────────────────────────────┐
│         SYSTEM LAYER                │
│  (GameState, Wave, Combat Systems)  │
└─────────────────────────────────────┘
            ↕ (Events)
┌─────────────────────────────────────┐
│       CORE SERVICES                 │
│  (EventBus, Economy, Time)          │
└─────────────────────────────────────┘
```

---

## 📁 Folder Structure

```
Assets/
├── Scripts/
│   ├── Core/                    # Long-lived global systems
│   │   ├── GameState.cs         # State enum
│   │   ├── GameStateSystem.cs   # State machine
│   │   ├── EventBus.cs          # Event system
│   │   ├── TimeSystem.cs        # Time control (pause/slow)
│   │   ├── EconomySystem.cs     # Gold/resources
│   │   └── GameBootstrap.cs     # Entry point
│   │
│   ├── LevelRuntime/            # Reset per level/checkpoint
│   │   ├── WaveSystem.cs        # Wave progression
│   │   ├── SpawnSystem.cs       # Enemy spawning
│   │   ├── ConditionSystem.cs   # Win/loss conditions
│   │   └── ConvoySystem.cs      # Convoy management
│   │
│   ├── Combat/                  # Combat logic
│   │   ├── ShipSystem.cs        # Ship management
│   │   ├── EnemySystem.cs       # Enemy management
│   │   ├── TargetingSystem.cs   # Target selection strategies
│   │   ├── DamageSystem.cs      # Damage calculation
│   │   └── StatusEffectSystem.cs # Buffs/debuffs
│   │
│   ├── Ships/                   # Ship MVC
│   │   ├── ShipConfig.cs        # Ship data (ScriptableObject)
│   │   ├── ShipModel.cs         # Ship instance data
│   │   ├── ShipController.cs    # Ship logic
│   │   └── ShipView.cs          # Ship visuals
│   │
│   ├── Enemies/                 # Enemy MVC
│   │   ├── EnemyConfig.cs       # Enemy data (ScriptableObject)
│   │   ├── EnemyModel.cs        # Enemy instance data
│   │   ├── EnemyController.cs   # Enemy logic (FSM)
│   │   └── EnemyView.cs         # Enemy visuals
│   │
│   ├── Waves/
│   │   └── WaveConfig.cs        # Wave data (ScriptableObject)
│   │
│   ├── Conditions/
│   │   └── ConditionConfig.cs   # Condition data (ScriptableObject)
│   │
│   ├── Input/
│   │   ├── PlayerInputHandler.cs    # Input → commands
│   │   └── ShipCommandSystem.cs     # Validate & execute commands
│   │
│   ├── UI/
│   │   ├── GameHUD.cs           # Main game UI
│   │   └── ShipPlacementPanel.cs # Ship placement UI
│   │
│   ├── Utils/
│   │   ├── ObjectPool.cs        # Object pooling utility
│   │   └── GameMath.cs          # Math helpers
│   │
│   └── Systems/
│       └── LevelController.cs   # Wires systems together
│
└── Data/                        # ScriptableObject assets
    ├── Ships/
    ├── Enemies/
    ├── Waves/
    └── Conditions/
```

---

## 🎮 Game State Machine

```
Boot → Preparation → WaveActive → WaveEnd → UpgradePhase
         ↑              ↓                       ↓
         └──────────────┴───────────────────────┘
                        ↓
                      Fail
```

### State Responsibilities

- **Boot**: System initialization
- **Preparation**: Place/move ships before wave
- **WaveActive**: Combat ongoing
- **WaveEnd**: Wave completed, transition
- **UpgradePhase**: Upgrade ships between waves
- **Fail**: Game over

---

## 🔄 Event-Driven Communication

### Key Events

```csharp
// Core events
GameStateChangedEvent
WaveStartedEvent
WaveCompletedEvent

// Entity events
ShipPlacedEvent
ShipRemovedEvent
EnemySpawnedEvent
EnemyDefeatedEvent

// Economy events
GoldChangedEvent

// Condition events
ConditionViolatedEvent
GameOverEvent
```

### Usage Pattern

```csharp
// Subscribe (in system Init)
eventBus.Subscribe<EnemyDefeatedEvent>(OnEnemyDefeated);

// Publish (when event occurs)
eventBus.Publish(new EnemyDefeatedEvent {
    EnemyId = id,
    GoldReward = reward
});

// Unsubscribe (in Cleanup)
eventBus.Unsubscribe<EnemyDefeatedEvent>(OnEnemyDefeated);
```

---

## 🏛️ MVC Pattern (Ships & Enemies)

### Model (Data Only)

```csharp
// Pure data, no logic
public class ShipModel {
    public int InstanceId;
    public string ShipTypeId;
    public float TimeSinceLastShot;
}
```

### Controller (Logic)

```csharp
// Owns Model, commands View
public class ShipController : MonoBehaviour {
    private ShipModel model;
    private ShipView view;

    void Update() {
        // Game logic here
    }
}
```

### View (Presentation Only)

```csharp
// NO logic, only visuals
public class ShipView : MonoBehaviour {
    public void PlayShootEffect(Vector3 target) {
        // VFX, animations only
    }
}
```

---

## 🎯 Targeting System (Strategy Pattern)

Ships use pluggable targeting strategies:

- **Closest**: Nearest enemy
- **Strongest**: Most HP
- **Weakest**: Least HP
- **First**: Furthest along path
- **Priority**: Highest priority value

### Adding New Strategy

```csharp
public class CustomTargetingStrategy : ITargetingStrategy {
    public EnemyController SelectTarget(
        Vector3 shipPosition,
        List<EnemyController> enemies)
    {
        // Your logic here
        return selectedEnemy;
    }
}

// Register in TargetingSystem constructor
strategies[TargetingStrategy.Custom] = new CustomTargetingStrategy();
```

---

## ⚠️ Condition & Punishment System

### Condition Types

- **Core**: Instant fail (e.g., convoy destroyed)
- **Level**: Checked at level end
- **InGame**: Continuous monitoring

### Punishment Types

- **Fail**: Game over
- **Penalty**: Deduct resources
- **Fix**: Requires gold payment

### Creating Custom Condition

```csharp
public class CustomConditionChecker : IConditionChecker {
    public void Initialize(EventBus eventBus) {
        // Subscribe to relevant events
    }

    public bool IsSatisfied() {
        // Return true if condition met
    }

    public void Cleanup() {
        // Unsubscribe
    }
}

// Register in ConditionSystem
conditionSystem.RegisterCondition(config, new CustomConditionChecker());
```

---

## 🛠️ How To: Common Tasks

### Add a New Ship Type

1. **Create Config**: Right-click → Create → SpaceDefend → Ship Config
2. **Set Stats**: Fill in damage, range, fire rate, cost
3. **Create Prefab**: Add ShipController + ShipView components
4. **Register**: Add to LevelController's shipPrefabs array

### Add a New Enemy Type

1. **Create Config**: Right-click → Create → SpaceDefend → Enemy Config
2. **Set Stats**: Fill in health, speed, rewards
3. **Create Prefab**: Add EnemyController + EnemyView components
4. **Register**: Add to LevelController's enemyPrefabs array

### Create a New Wave

1. **Create Config**: Right-click → Create → SpaceDefend → Wave Config
2. **Add Groups**: Define enemy types, counts, timing
3. **Add to Level**: Assign to LevelController's waveConfigs list

### Add a New System

1. **Create System Class**: Follow pattern (no MonoBehaviour unless needed)
2. **Initialize in LevelController**: Create instance, inject dependencies
3. **Subscribe to Events**: Use EventBus for communication
4. **Cleanup**: Implement Reset/Cleanup methods

---

## 🚫 Anti-Patterns to Avoid

### ❌ DON'T

```csharp
// Don't use FindObjectOfType
var enemy = FindObjectOfType<EnemyController>();

// Don't use static singletons without accessor pattern
public static EnemySystem Instance;

// Don't mix UI and gameplay logic
public class ShipView : MonoBehaviour {
    void Update() {
        // Targeting logic HERE - WRONG!
    }
}

// Don't create deep inheritance chains
public class BasicShip : Ship : Entity : GameObject {}
```

### ✅ DO

```csharp
// Inject dependencies
public WaveSystem(EventBus eventBus, List<WaveConfig> configs) {}

// Use events for communication
eventBus.Publish(new WaveCompletedEvent { ... });

// Keep views dumb
view.PlayAnimation("idle"); // Command only

// Favor composition
ship.AddComponent<TargetingBehavior>();
```

---

## 🔧 System Initialization Order

**CRITICAL**: Systems must be initialized in dependency order.

```csharp
// 1. Core (no dependencies)
eventBus = EventBus.Instance;
gameStateSystem = new GameStateSystem(eventBus);
economySystem = new EconomySystem(eventBus);

// 2. Combat (depends on Core)
targetingSystem = new TargetingSystem();
damageSystem = new DamageSystem(eventBus);

// 3. Level Runtime (depends on Core + Combat)
waveSystem = new WaveSystem(eventBus, waveConfigs);
shipSystem = new ShipSystem(eventBus, economySystem, targetingSystem);

// 4. Input (depends on everything)
shipCommandSystem = new ShipCommandSystem(shipSystem, gameStateSystem);
```

---

## 🧪 Testing Strategy

### Unit Testing (Future)

- Test systems in isolation
- Mock EventBus for event testing
- Test data transformations in Models

### Integration Testing

- Test LevelController system wiring
- Verify event flow between systems
- Validate state machine transitions

---

## 📈 Performance Considerations

### Object Pooling

```csharp
// Use ObjectPool for frequently spawned objects
var pool = new ObjectPool(enemyPrefab, initialSize: 20);
var enemy = pool.Get();
// ... use enemy ...
pool.Return(enemy);
```

### Avoid Allocations

```csharp
// Don't create lists every frame
var enemies = new List<Enemy>(); // ❌

// Reuse collections
private List<Enemy> enemiesInRange = new List<Enemy>(); // ✅
```

---

## 🎨 Extending the Architecture

### Adding a New Layer (e.g., Multiplayer)

1. Create new system: `MultiplayerSystem`
2. Listen to game events (EnemyDefeated, ShipPlaced)
3. Publish network events
4. Keep existing systems unchanged

### Adding New Features

- **Abilities**: Create AbilitySystem, use StatusEffectSystem
- **Upgrades**: Extend ShipConfig, add UpgradeSystem
- **Achievements**: Create AchievementSystem, subscribe to events

---

## 📚 Learning Resources

### For Junior Developers

- **Start Here**: Read GameBootstrap.cs → understand system init
- **Study MVC**: Compare ShipController vs ShipView
- **Follow Events**: Trace an EnemyDefeatedEvent through the system
- **Modify Data**: Change ShipConfig values, observe effects

### Code Reading Order

1. Core/GameState.cs (simple enum)
2. Core/EventBus.cs (event system)
3. Core/GameStateSystem.cs (state machine)
4. Ships/ShipConfig.cs (data)
5. Ships/ShipController.cs (logic)
6. Systems/LevelController.cs (wiring)

---

## ⚡ Quick Reference

### Common Workflows

**Add Gold**:

```csharp
economySystem.AddGold(50);
```

**Transition State**:

```csharp
gameStateSystem.TransitionTo(GameState.WaveActive);
```

**Publish Event**:

```csharp
eventBus.Publish(new CustomEvent { Data = value });
```

**Subscribe to Event**:

```csharp
eventBus.Subscribe<CustomEvent>(OnCustomEvent);
```

---

## 🐛 Troubleshooting

### Systems Not Initializing

- Check initialization order in LevelController
- Verify GameBootstrap is in the scene
- Ensure DontDestroyOnLoad is working

### Events Not Firing

- Confirm subscription happened after EventBus creation
- Check you're using same EventBus instance
- Verify event struct matches exactly

### Ships Not Shooting

- Ensure TargetingSystem initialized
- Check ship has valid config
- Verify enemies are in range

---

## 📝 Coding Standards

### Naming Conventions

- **Classes**: PascalCase (`GameStateSystem`)
- **Methods**: PascalCase (`TransitionTo`)
- **Fields**: camelCase (`currentState`)
- **Events**: PascalCase + "Event" suffix (`WaveStartedEvent`)

### File Organization

- One class per file
- File name matches class name
- Organize by feature, not type

### Comments

- Use XML docs for public APIs
- Explain "why" not "what"
- Keep comments concise

---

## 🎯 Project Goals Checklist

- ✅ Systems own logic, MonoBehaviours are adapters
- ✅ Clear responsibility boundaries
- ✅ Data-driven design (ScriptableObjects)
- ✅ Composition over inheritance
- ✅ Event-based communication
- ✅ No premature optimization
- ✅ Junior-friendly codebase

---

## 👥 Team Workflow

### Adding Features

1. Design: Sketch system interactions
2. Data: Create ScriptableObjects first
3. Systems: Implement logic
4. Views: Add visuals last
5. Wire: Connect in LevelController
6. Test: Verify in-game

### Code Review Focus

- Responsibility separation
- Event usage vs direct calls
- Data vs logic separation
- Naming clarity

---

## 🚀 Next Steps

### Immediate

1. Create ship/enemy prefabs with Controller + View
2. Design initial waves (3-5 configurations)
3. Implement simple path system
4. Add basic UI

### Short-term

- Add more ship types (3-5)
- Create wave progression (10 waves)
- Implement upgrade system
- Add win/loss screens

### Long-term

- Save/load system
- Meta-progression
- Additional game modes
- Polish and VFX

---

**Last Updated**: 2026-02-06  
**Unity Version**: Unity 6 (LTS)  
**Architecture**: System-Based OOP
