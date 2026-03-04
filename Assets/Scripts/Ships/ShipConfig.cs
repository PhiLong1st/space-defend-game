using UnityEngine;

[CreateAssetMenu(fileName = "Ship_", menuName = "SpaceDefend/Ship Config")]
public class ShipConfig : ScriptableObject
{
  [Header("Identity")]
  [Tooltip("Unique identifier for this ship type")]
  public string ShipId;

  [Tooltip("Display name")]
  public string DisplayName;

  [Tooltip("Health points of the ship")]
  public int MaxHealth = 100;

  [Tooltip("Stamina points of the ship")]
  public int MaxStamina = 100;
}