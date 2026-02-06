using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Main level controller that wires all systems together.
/// Attach to a GameObject in Gameplay scene.
/// Initializes level-specific systems and manages level lifecycle.
/// </summary>
public class LevelController : MonoBehaviour
{
  [Header("Level Configuration")]
  [SerializeField] private List<WaveConfig> waveConfigs;
  [SerializeField] private int convoyMaxHealth = 100;

  [Header("References")]
  [SerializeField] private Transform shipRoot;
  [SerializeField] private Transform enemyRoot;
  [SerializeField] private PlayerInputHandler inputHandler;
  [SerializeField] private GameHUD gameHUD;

  [Header("Prefab Registries")]
  [SerializeField] private ShipPrefabEntry[] shipPrefabs;
  [SerializeField] private EnemyPrefabEntry[] enemyPrefabs;

  // Core systems (from bootstrap)
  private EventBus eventBus;
  private GameStateSystem gameStateSystem;
  private TimeSystem timeSystem;
  private EconomySystem economySystem;

  // Level systems
  private WaveSystem waveSystem;
  private SpawnSystem spawnSystem;
  private ConditionSystem conditionSystem;
  private ConvoySystem convoySystem;
  private TargetingSystem targetingSystem;
  private DamageSystem damageSystem;
  private StatusEffectSystem statusEffectSystem;
  private ShipSystem shipSystem;
  private EnemySystem enemySystem;
  private ShipCommandSystem shipCommandSystem;

  private void Awake()
  {
    InitializeSystems();
  }

  private void Start()
  {
    StartLevel();
  }

  private void InitializeSystems()
  {
    // Get core systems from bootstrap
    eventBus = GameBootstrap.EventBus;
    gameStateSystem = GameBootstrap.State;
    timeSystem = GameBootstrap.Time;
    economySystem = GameBootstrap.Economy;

    // Initialize level-specific systems
    waveSystem = new WaveSystem(eventBus, waveConfigs);
    spawnSystem = new SpawnSystem(eventBus, enemyRoot, this);
    conditionSystem = new ConditionSystem(eventBus, economySystem, gameStateSystem);
    convoySystem = new ConvoySystem(eventBus, convoyMaxHealth);

    targetingSystem = new TargetingSystem();
    damageSystem = new DamageSystem(eventBus);
    statusEffectSystem = new StatusEffectSystem();

    shipSystem = new ShipSystem(eventBus, economySystem, targetingSystem, damageSystem, shipRoot);
    enemySystem = new EnemySystem(eventBus, economySystem, convoySystem);

    shipCommandSystem = new ShipCommandSystem(shipSystem, gameStateSystem, eventBus);

    // Register prefabs
    RegisterPrefabs();

    // Initialize UI
    if (gameHUD != null)
    {
      gameHUD.Initialize(eventBus, economySystem, waveSystem);
    }

    if (inputHandler != null)
    {
      inputHandler.Initialize(shipCommandSystem);
    }

    Debug.Log("Level systems initialized");
  }

  private void RegisterPrefabs()
  {
    // Register ship prefabs
    foreach (var entry in shipPrefabs)
    {
      if (entry.Config != null && entry.Prefab != null)
      {
        shipCommandSystem.RegisterShipPrefab(entry.Config.ShipId, entry.Prefab);
      }
    }

    // Register enemy prefabs
    foreach (var entry in enemyPrefabs)
    {
      if (entry.Config != null && entry.Prefab != null)
      {
        spawnSystem.RegisterEnemyPrefab(entry.Config.EnemyId, entry.Prefab);
      }
    }
  }

  private void StartLevel()
  {
    // Transition to preparation phase
    gameStateSystem.TransitionTo(GameState.Preparation);

    Debug.Log("Level started - Preparation phase");
  }

  /// <summary>
  /// Start the next wave (can be called by UI or manually).
  /// </summary>
  public void StartNextWave()
  {
    if (waveSystem.StartNextWave())
    {
      gameStateSystem.TransitionTo(GameState.WaveActive);

      // Trigger spawn system
      var currentWave = waveSystem.CurrentWave;
      if (currentWave != null)
      {
        spawnSystem.SpawnWave(currentWave);
      }
    }
  }

  private void Update()
  {
    // Update status effects
    if (statusEffectSystem != null)
    {
      statusEffectSystem.UpdateEffects(Time.deltaTime);
    }
  }

  private void OnDestroy()
  {
    // Cleanup systems
    waveSystem?.Cleanup();
    spawnSystem?.Cleanup();
    shipSystem?.Reset();
    enemySystem?.Cleanup();
  }

  // Helper classes for inspector
  [System.Serializable]
  public class ShipPrefabEntry
  {
    public ShipConfig Config;
    public GameObject Prefab;
  }

  [System.Serializable]
  public class EnemyPrefabEntry
  {
    public EnemyConfig Config;
    public GameObject Prefab;
  }
}
