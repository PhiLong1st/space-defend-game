using UnityEngine;


/// <summary>
/// ScriptableObject defining ship stats and behavior.
/// Designer-friendly data container for ship types.
/// </summary>
[CreateAssetMenu(fileName = "Ship_", menuName = "SpaceDefend/Ship Config")]
public class ShipConfig : ScriptableObject
{
  [Header("Identity")]
  [Tooltip("Unique identifier for this ship type")]
  public string ShipId;

  [Tooltip("Display name")]
  public string DisplayName;

  [Header("Cost")]
  [Tooltip("Gold cost to place this ship")]
  public int PlacementCost = 50;

  [Tooltip("Gold refund when removing (% of placement cost)")]
  [Range(0f, 1f)]
  public float RefundPercentage = 0.75f;

  [Header("Combat Stats")]
  [Tooltip("Damage per shot")]
  public float Damage = 10f;

  [Tooltip("Shots per second")]
  public float FireRate = 1f;

  [Tooltip("Attack range in units")]
  public float Range = 5f;

  [Header("Targeting")]
  [Tooltip("Default targeting strategy")]
  public TargetingStrategy DefaultTargetingStrategy = TargetingStrategy.Closest;

  [Header("Upgrade")]
  [Tooltip("Can this ship be upgraded?")]
  public bool IsUpgradeable = true;

  [Tooltip("Ship config this upgrades to (null = max level)")]
  public ShipConfig UpgradeTo;

  [Tooltip("Cost to upgrade to next level")]
  public int UpgradeCost = 75;

  /// <summary>
  /// Calculate refund amount when removing this ship.
  /// </summary>
  public int GetRefundAmount()
  {
    return Mathf.RoundToInt(PlacementCost * RefundPercentage);
  }

  /// <summary>
  /// Check if this ship is at max level.
  /// </summary>
  public bool IsMaxLevel()
  {
    return UpgradeTo == null;
  }
}

/// <summary>
/// Available targeting strategies for ships.
/// </summary>
public enum TargetingStrategy
{
  Closest,        // Target nearest enemy
  Strongest,      // Target enemy with most HP
  Weakest,        // Target enemy with least HP
  First,          // Target enemy furthest along path
  Priority        // Target based on enemy priority value
}