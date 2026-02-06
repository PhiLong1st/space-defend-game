using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Handles player input for ship placement, selection, and control.
/// Translates raw input into game commands via ShipCommandSystem.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
  [Header("References")]
  [SerializeField] private Camera mainCamera;

  // Systems
  private ShipCommandSystem commandSystem;

  // State
  private ShipConfig selectedShipConfig;
  private ShipController selectedShip;
  private bool isPlacementMode = false;

  private void Awake()
  {
    if (mainCamera == null)
    {
      mainCamera = Camera.main;
    }
  }

  public void Initialize(ShipCommandSystem commandSystem)
  {
    this.commandSystem = commandSystem;
  }

  private void Update()
  {
    HandleMouseInput();
  }

  private void HandleMouseInput()
  {
    // Get mouse position in world space
    Vector3 mouseWorldPos = GetMouseWorldPosition();

    // Left click
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      OnLeftClick(mouseWorldPos);
    }

    // Right click (cancel)
    if (Mouse.current.rightButton.wasPressedThisFrame)
    {
      OnRightClick();
    }

    // Preview during placement mode
    if (isPlacementMode)
    {
      UpdatePlacementPreview(mouseWorldPos);
    }
  }

  private void OnLeftClick(Vector3 position)
  {
    if (isPlacementMode && selectedShipConfig != null)
    {
      // Attempt to place ship
      bool success = commandSystem.PlaceShip(selectedShipConfig, position);

      if (success)
      {
        Debug.Log("Ship placed successfully");
        // Could stay in placement mode or exit depending on design
      }
    }
    else
    {
      // Try to select existing ship
      TrySelectShip(position);
    }
  }

  private void OnRightClick()
  {
    // Cancel placement or deselect
    ExitPlacementMode();
    DeselectShip();
  }

  private void TrySelectShip(Vector3 position)
  {
    // Raycast to find ship at position
    RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

    if (hit.collider != null)
    {
      var ship = hit.collider.GetComponent<ShipController>();
      if (ship != null)
      {
        SelectShip(ship);
      }
    }
  }

  private void SelectShip(ShipController ship)
  {
    DeselectShip(); // Deselect previous

    selectedShip = ship;
    // Show ship UI, range, etc.
    Debug.Log($"Selected ship: {ship.Config.DisplayName}");
  }

  private void DeselectShip()
  {
    if (selectedShip != null)
    {
      // Hide ship UI
      selectedShip = null;
    }
  }

  /// <summary>
  /// Enter ship placement mode (called by UI).
  /// </summary>
  public void EnterPlacementMode(ShipConfig config)
  {
    selectedShipConfig = config;
    isPlacementMode = true;
    Debug.Log($"Entering placement mode: {config.DisplayName}");
  }

  /// <summary>
  /// Exit ship placement mode.
  /// </summary>
  public void ExitPlacementMode()
  {
    selectedShipConfig = null;
    isPlacementMode = false;
  }

  private void UpdatePlacementPreview(Vector3 position)
  {
    // Would show ghost preview of ship at mouse position
    // Check if placement is valid (pathfinding, collision)
  }

  /// <summary>
  /// Remove currently selected ship (called by UI button).
  /// </summary>
  public void RemoveSelectedShip()
  {
    if (selectedShip != null)
    {
      commandSystem.RemoveShip(selectedShip);
      DeselectShip();
    }
  }

  /// <summary>
  /// Upgrade currently selected ship (called by UI button).
  /// </summary>
  public void UpgradeSelectedShip()
  {
    if (selectedShip != null)
    {
      commandSystem.UpgradeShip(selectedShip);
    }
  }

  private Vector3 GetMouseWorldPosition()
  {
    if (mainCamera == null)
      return Vector3.zero;

    Vector3 mousePos = Mouse.current.position.ReadValue();
    Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
    worldPos.z = 0f;

    return worldPos;
  }
}