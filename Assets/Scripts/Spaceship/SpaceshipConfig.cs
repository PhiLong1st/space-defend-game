using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShip_", menuName = "SpaceDefend/SpaceShip Config")]
public class SpaceshipConfig : ScriptableObject
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

  [Tooltip("Shield points of the ship")]
  public int MaxShield = 100;

  [Tooltip("Movement speed of the ship")]
  public float MovementSpeed = 5f;

  [Tooltip("Boost speed of the ship")]
  public float BoostSpeed = 1f;

  [Tooltip("Level of the ship")]
  public int Level = 1;
}