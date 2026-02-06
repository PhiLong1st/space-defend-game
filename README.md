# 🚀 Space Defend

A 2D tactical tower defense game built with **Unity 6** using a clean, system-based architecture.

## 🎮 Game Concept

**Genre**: Tower Defense with Movable Towers  
**Platform**: PC (Windows/macOS)  
**Session Length**: 10-20 minutes  
**Core Mechanic**: Place and move ships to defend a convoy from waves of enemies

## 🏗️ Architecture

This project uses a **system-based architecture** designed for clarity and maintainability:

- **Systems own logic** - MonoBehaviours are thin adapters
- **Event-driven communication** - Decoupled via EventBus
- **Data-driven design** - ScriptableObjects for configuration
- **MVC pattern** - Ships and Enemies use Model-View-Controller
- **Strategy pattern** - Pluggable targeting behaviors

See [ARCHITECTURE.md](ARCHITECTURE.md) for detailed documentation.

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Core/           # Global systems (GameState, Economy, Events)
│   ├── LevelRuntime/   # Per-level systems (Waves, Spawning, Conditions)
│   ├── Combat/         # Combat systems (Targeting, Damage, Status)
│   ├── Ships/          # Ship MVC components
│   ├── Enemies/        # Enemy MVC components
│   ├── Input/          # Player input handling
│   ├── UI/             # User interface
│   └── Utils/          # Utilities (Pooling, Math)
│
└── Data/               # ScriptableObject configurations
    ├── Ships/
    ├── Enemies/
    ├── Waves/
    └── Conditions/
```

## 🚀 Getting Started

### Requirements

- Unity 6 (LTS)
- TextMeshPro (included)
- Input System (included)

### Setup

1. Clone the repository
2. Open project in Unity 6
3. Open `Bootstrap` scene from `Assets/Scenes/`
4. Press Play

### Creating Your First Ship

1. Right-click in `Assets/Data/Ships/` → Create → SpaceDefend → Ship Config
2. Configure stats (damage, range, fire rate, cost)
3. Create a prefab with `ShipController` and `ShipView` components
4. Assign to `LevelController` in the scene

## 🎯 Quick Reference

### Adding New Content

**New Ship Type**:

- Create ShipConfig ScriptableObject
- Create prefab with ShipController + ShipView
- Register in LevelController

**New Enemy Type**:

- Create EnemyConfig ScriptableObject
- Create prefab with EnemyController + EnemyView
- Register in LevelController

**New Wave**:

- Create WaveConfig ScriptableObject
- Define enemy groups, timing, and rewards
- Add to LevelController wave list

## 📚 Documentation

- [ARCHITECTURE.md](ARCHITECTURE.md) - Complete architecture guide
- [Assets/Scripts/Core/](Assets/Scripts/Core/) - Core system documentation
- Code comments - XML docs on all public APIs

## 🛠️ Development Principles

### DO ✅

- Keep systems focused on single responsibility
- Use events for cross-system communication
- Separate data (Model) from logic (Controller) from visuals (View)
- Inject dependencies explicitly

### DON'T ❌

- Use `FindObjectOfType` in runtime code
- Create static singletons without accessor pattern
- Put gameplay logic in View classes
- Create deep inheritance hierarchies

## 🧪 Current State

**Status**: Skeleton Architecture Complete ✅

**Implemented**:

- ✅ Core systems (GameState, Events, Economy, Time)
- ✅ Level systems (Waves, Spawning, Conditions, Convoy)
- ✅ Combat systems (Targeting, Damage, Status Effects)
- ✅ Ship & Enemy MVC architecture
- ✅ Input handling
- ✅ Basic UI framework
- ✅ Example ScriptableObject configs

**Next Steps**:

- Create art assets (sprites, animations)
- Implement path/waypoint system
- Build initial waves (5-10)
- Add VFX and audio hooks
- Create main menu and game over screens

## 👥 Team Structure

**Target**: 2-person team (Junior-Mid level)

**Roles**:

- **Programmer**: Extend systems, implement features
- **Designer/Artist**: Create configs, waves, visual assets

## 📈 Scalability

This architecture is designed to scale from prototype to production:

- **Current**: ~30 scripts, clear structure
- **Future**: Can grow to 100+ scripts without refactoring
- **Maintainability**: New developers can contribute immediately
- **Flexibility**: Easy to add new systems, ship types, mechanics

## 🎓 Learning Path

**For Junior Developers**:

1. Start with `GameBootstrap.cs` - see how systems initialize
2. Study `ShipController.cs` - understand MVC pattern
3. Trace an event through the system (e.g., `EnemyDefeatedEvent`)
4. Modify a `ShipConfig` - see data-driven design
5. Create your own ship type - apply what you learned

## 📝 License

[Your License Here]

## 🤝 Contributing

1. Follow existing code patterns
2. Maintain separation of concerns
3. Document public APIs with XML comments
4. Test changes in-game before committing

---

**Unity Version**: 6.0 (LTS)  
**Architecture**: System-Based OOP  
**Last Updated**: February 2026
