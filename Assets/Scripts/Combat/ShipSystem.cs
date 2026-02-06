using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Manages all ships in the level: placement, removal, tracking.
/// Acts as registry and coordinator for ship instances.
/// </summary>
public class ShipSystem
{
  private readonly EventBus eventBus;
  private readonly EconomySystem economySystem;
  private readonly TargetingSystem targetingSystem;
  private readonly DamageSystem damageSystem;

  private Dictionary<int, ShipController> activeShips = new();
  private Transform shipRoot;

  public ShipSystem(
      EventBus eventBus,
      EconomySystem economySystem,
      TargetingSystem targetingSystem,
      DamageSystem damageSystem,
      Transform shipRoot)
  {
    this.eventBus = eventBus;
    this.economySystem = economySystem;
    this.targetingSystem = targetingSystem;
    this.damageSystem = damageSystem;
    this.shipRoot = shipRoot;
  }

  /// <summary>
  /// Attempt to place a ship at position.
  /// Returns ship instance if successful, null if failed (insufficient gold, etc).
  /// </summary>
  public ShipController PlaceShip(ShipConfig config, Vector3 position, GameObject shipPrefab)
  {
    // Check if can afford
    if (!economySystem.CanAfford(config.PlacementCost))
    {
      Debug.LogWarning($"Cannot afford ship: {config.DisplayName} (costs {config.PlacementCost})");
      return null;
    }

    // Spend gold
    if (!economySystem.TrySpendGold(config.PlacementCost))
    {
      return null;
    }

    // Instantiate ship
    var shipObj = GameObject.Instantiate(shipPrefab, position, Quaternion.identity, shipRoot);
    var shipController = shipObj.GetComponent<ShipController>();

    if (shipController == null)
    {
      Debug.LogError("Ship prefab missing ShipController component!");
      GameObject.Destroy(shipObj);
      economySystem.AddGold(config.PlacementCost); // Refund
      return null;
    }

    // Initialize ship
    shipController.Initialize(config, targetingSystem, damageSystem);

    // Register ship
    int shipId = shipController.GetInstanceID();
    activeShips[shipId] = shipController;

    // Emit event
    eventBus.Publish(new ShipPlacedEvent { ShipId = shipId });

    Debug.Log($"Ship placed: {config.DisplayName} at {position}");
    return shipController;
  }

  /// <summary>
  /// Remove a ship and refund gold.
  /// </summary>
  public void RemoveShip(ShipController ship)
  {
    if (ship == null)
      return;

    int shipId = ship.GetInstanceID();

    if (!activeShips.ContainsKey(shipId))
    {
      Debug.LogWarning("Ship not found in registry");
      return;
    }

    // Refund gold
    int refund = ship.Config.GetRefundAmount();
    economySystem.AddGold(refund);

    // Remove from registry
    activeShips.Remove(shipId);

    // Emit event
    eventBus.Publish(new ShipRemovedEvent { ShipId = shipId });

    // Destroy ship
    ship.Remove();

    Debug.Log($"Ship removed, refunded {refund} gold");
  }

  /// <summary>
  /// Upgrade a ship to next level.
  /// </summary>
  public bool UpgradeShip(ShipController ship)
  {
    if (ship == null || ship.Config.IsMaxLevel())
      return false;

    // Check cost
    if (!economySystem.TrySpendGold(ship.Config.UpgradeCost))
    {
      Debug.LogWarning("Cannot afford upgrade");
      return false;
    }

    // Perform upgrade
    bool success = ship.Upgrade();

    if (!success)
    {
      // Refund if upgrade failed
      economySystem.AddGold(ship.Config.UpgradeCost);
    }

    return success;
  }

  /// <summary>
  /// Get all active ships.
  /// </summary>
  public List<ShipController> GetAllShips()
  {
    return new List<ShipController>(activeShips.Values);
  }

  public void Reset()
  {
    // Cleanup all ships
    foreach (var ship in activeShips.Values)
    {
      if (ship != null)
      {
        GameObject.Destroy(ship.gameObject);
      }
    }

    activeShips.Clear();
  }
}