using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// UI panel for ship placement buttons.
/// Allows player to select which ship to place.
/// </summary>
public class ShipPlacementPanel : MonoBehaviour
{
  [Header("Ship Buttons")]
  [SerializeField] private ShipPlacementButton[] shipButtons;

  private ShipCommandSystem commandSystem;
  private PlayerInputHandler inputHandler;

  public void Initialize(
      ShipCommandSystem commandSystem,
      PlayerInputHandler inputHandler,
      ShipConfig[] availableShips)
  {
    this.commandSystem = commandSystem;
    this.inputHandler = inputHandler;

    // Setup buttons
    for (int i = 0; i < shipButtons.Length && i < availableShips.Length; i++)
    {
      var ship = availableShips[i];
      shipButtons[i].Initialize(ship, OnShipButtonClicked);
    }
  }

  private void OnShipButtonClicked(ShipConfig config)
  {
    // Enter placement mode
    inputHandler.EnterPlacementMode(config);
    Debug.Log($"Selected ship for placement: {config.DisplayName}");
  }
}

/// <summary>
/// Individual button for ship placement.
/// </summary>
[System.Serializable]
public class ShipPlacementButton
{
  public Button button;
  public TextMeshProUGUI nameText;
  public TextMeshProUGUI costText;

  private ShipConfig config;
  private System.Action<ShipConfig> onClick;

  public void Initialize(ShipConfig config, System.Action<ShipConfig> onClick)
  {
    this.config = config;
    this.onClick = onClick;

    // Update UI
    if (nameText != null)
    {
      nameText.text = config.DisplayName;
    }

    if (costText != null)
    {
      costText.text = $"${config.PlacementCost}";
    }

    // Setup button
    if (button != null)
    {
      button.onClick.RemoveAllListeners();
      button.onClick.AddListener(OnButtonClicked);
    }
  }

  private void OnButtonClicked()
  {
    onClick?.Invoke(config);
  }
}