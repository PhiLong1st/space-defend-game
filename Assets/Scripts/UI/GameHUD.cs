using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Displays current game state and resources.
/// Listens to events and updates UI accordingly.
/// </summary>
public class GameHUD : MonoBehaviour
{
  [Header("Resource Display")]
  [SerializeField] private TextMeshProUGUI goldText;

  [Header("Wave Display")]
  [SerializeField] private TextMeshProUGUI waveText;
  [SerializeField] private Button callWaveButton;

  [Header("Game State")]
  [SerializeField] private TextMeshProUGUI stateText;

  private EventBus eventBus;
  private EconomySystem economySystem;
  private WaveSystem waveSystem;

  public void Initialize(
      EventBus eventBus,
      EconomySystem economySystem,
      WaveSystem waveSystem)
  {
    this.eventBus = eventBus;
    this.economySystem = economySystem;
    this.waveSystem = waveSystem;

    // Subscribe to events
    eventBus.Subscribe<GoldChangedEvent>(OnGoldChanged);
    eventBus.Subscribe<WaveStartedEvent>(OnWaveStarted);
    eventBus.Subscribe<GameStateChangedEvent>(OnGameStateChanged);

    // Setup buttons
    if (callWaveButton != null)
    {
      callWaveButton.onClick.AddListener(OnCallWaveClicked);
    }

    // Initial update
    UpdateGoldDisplay();
    UpdateWaveDisplay();
  }

  private void OnDestroy()
  {
    if (eventBus != null)
    {
      eventBus.Unsubscribe<GoldChangedEvent>(OnGoldChanged);
      eventBus.Unsubscribe<WaveStartedEvent>(OnWaveStarted);
      eventBus.Unsubscribe<GameStateChangedEvent>(OnGameStateChanged);
    }

    if (callWaveButton != null)
    {
      callWaveButton.onClick.RemoveListener(OnCallWaveClicked);
    }
  }

  private void OnGoldChanged(GoldChangedEvent evt)
  {
    UpdateGoldDisplay();
  }

  private void OnWaveStarted(WaveStartedEvent evt)
  {
    UpdateWaveDisplay();
    UpdateCallWaveButton();
  }

  private void OnGameStateChanged(GameStateChangedEvent evt)
  {
    UpdateStateDisplay();
    UpdateCallWaveButton();
  }

  private void UpdateGoldDisplay()
  {
    if (goldText != null && economySystem != null)
    {
      goldText.text = $"Gold: {economySystem.CurrentGold}";
    }
  }

  private void UpdateWaveDisplay()
  {
    if (waveText != null && waveSystem != null)
    {
      int currentWave = waveSystem.CurrentWaveIndex + 1;
      waveText.text = $"Wave: {currentWave}";
    }
  }

  private void UpdateStateDisplay()
  {
    if (stateText != null && GameBootstrap.State != null)
    {
      stateText.text = $"State: {GameBootstrap.State.CurrentState}";
    }
  }

  private void UpdateCallWaveButton()
  {
    if (callWaveButton != null && waveSystem != null)
    {
      // Enable button only when wave is not active
      callWaveButton.interactable = !waveSystem.IsWaveActive;
    }
  }

  private void OnCallWaveClicked()
  {
    if (waveSystem != null)
    {
      waveSystem.CallWaveEarly();
    }
  }
}
