using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// High-level command system for ship operations.
/// Validates commands and delegates to appropriate systems.
/// Acts as facade between input and systems.
/// </summary>
public class ShipCommandSystem
{
  private readonly ShipSystem shipSystem;
  private readonly GameStateSystem gameStateSystem;
  private readonly EventBus eventBus;

  // Ship prefab registry
  private Dictionary<string, GameObject> shipPrefabs = new();

  public ShipCommandSystem(
      ShipSystem shipSystem,
      GameStateSystem gameStateSystem,
      EventBus eventBus)
  {
    this.shipSystem = shipSystem;
    this.gameStateSystem = gameStateSystem;
    this.eventBus = eventBus;
  }

  /// <summary>
  /// Register a ship prefab for spawning.
  /// </summary>
  public void RegisterShipPrefab(string shipId, GameObject prefab)
  {
    shipPrefabs[shipId] = prefab;
  }

  /// <summary>
  /// Attempt to place a ship.
  /// Validates state, position, and delegates to ShipSystem.
  /// </summary>
  public bool PlaceShip(ShipConfig config, Vector3 position)
  {
    // Validate game state
    if (!CanPlaceShips())
    {
      Debug.LogWarning("Cannot place ships in current game state");
      return false;
    }

    // Validate position
    if (!IsValidPlacementPosition(position))
    {
      Debug.LogWarning("Invalid placement position");
      return false;
    }

    // Get prefab
    if (!shipPrefabs.ContainsKey(config.ShipId))
    {
      Debug.LogError($"Ship prefab not registered: {config.ShipId}");
      return false;
    }

    GameObject prefab = shipPrefabs[config.ShipId];

    // Delegate to ShipSystem
    var ship = shipSystem.PlaceShip(config, position, prefab);
    return ship != null;
  }

  /// <summary>
  /// Remove a ship.
  /// </summary>
  public bool RemoveShip(ShipController ship)
  {
    if (ship == null)
      return false;

    if (!CanPlaceShips())
    {
      Debug.LogWarning("Cannot remove ships in current game state");
      return false;
    }

    shipSystem.RemoveShip(ship);
    return true;
  }

  /// <summary>
  /// Upgrade a ship.
  /// </summary>
  public bool UpgradeShip(ShipController ship)
  {
    if (ship == null)
      return false;

    return shipSystem.UpgradeShip(ship);
  }

  /// <summary>
  /// Change targeting strategy for a ship.
  /// </summary>
  public void SetShipTargeting(ShipController ship, TargetingStrategy strategy)
  {
    if (ship == null)
      return;

    ship.SetTargetingStrategy(strategy);
  }

  private bool CanPlaceShips()
  {
    // Can only place ships during Preparation or between waves
    var state = gameStateSystem.CurrentState;
    return state == GameState.Preparation || state == GameState.UpgradePhase;
  }

  private bool IsValidPlacementPosition(Vector3 position)
  {
    // Would check:
    // - Not on path
    // - Not overlapping other ships
    // - Within play area bounds
    // For now, simple validation
    return true;
  }
}
