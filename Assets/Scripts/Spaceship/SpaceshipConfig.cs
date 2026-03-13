using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShip_", menuName = "SpaceDefend/SpaceShip Config")]
public class SpaceshipConfig : ScriptableObject
{
  [Header("Identity")]
  [Tooltip("Unique identifier for this ship type")]
  public string ShipId;

  [Tooltip("Display name")]
  public string DisplayName;

  [Tooltip("Movement speed of the ship")]
  public float MovementSpeed = 5f;

  [Tooltip("Damage of the ship")]
  public float Damage = 10f;

  [Tooltip("Movement speed increase per level")]
  public float MovementSpeedIncreasePerLevel = 0.5f;

  [Tooltip("Damage increase per level")]
  public float DamageIncreasePerLevel = 2f;

  [Tooltip("Level of the ship")]
  public int Level = 1;

  [Tooltip("Max Level of the ship")]
  public int MaxLevel = 7;
}