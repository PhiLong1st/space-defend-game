using UnityEngine;

/// <summary>
/// Entry point that initializes all global systems.
/// Attach to a GameObject in Bootstrap scene.
/// Systems are initialized in dependency order.
/// </summary>
public class GameBootstrap : MonoBehaviour
{
  [Header("Starting Configuration")]
  [SerializeField] private int startingGold = 100;

  // Global systems (persist across scenes)
  private EventBus eventBus;
  private GameStateSystem gameStateSystem;
  private TimeSystem timeSystem;
  private EconomySystem economySystem;

  // Public accessors (consider using dependency injection for production)
  public static EventBus EventBus { get; private set; }
  public static GameStateSystem State { get; private set; }
  public static TimeSystem Time { get; private set; }
  public static EconomySystem Economy { get; private set; }

  private void Awake()
  {
    // Ensure singleton behavior
    if (EventBus != null)
    {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);
    InitializeSystems();
  }

  private void InitializeSystems()
  {
    // Order matters: dependencies first
    eventBus = EventBus.Instance;
    gameStateSystem = new GameStateSystem(eventBus);
    timeSystem = new TimeSystem();
    economySystem = new EconomySystem(eventBus, startingGold);

    // Expose globally
    EventBus = eventBus;
    State = gameStateSystem;
    Time = timeSystem;
    Economy = economySystem;

    Debug.Log("Core systems initialized");

    // Transition to first gameplay state
    gameStateSystem.TransitionTo(GameState.Preparation);
  }

  private void OnDestroy()
  {
    // Cleanup
    if (EventBus == this.eventBus)
    {
      eventBus.Clear();
      timeSystem.Reset();
    }
  }
}