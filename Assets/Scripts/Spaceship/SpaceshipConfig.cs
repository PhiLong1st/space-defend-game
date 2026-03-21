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

  [Tooltip("Health points of the ship")]
  public int Health = 100;

  [Tooltip("Stamina points of the ship")]
  public int Stamina = 100;

  [Tooltip("Experience points multiplier for the ship")]
  public float ExperienceMultiplier = 2f;

  [Header("Level Up Increments")]
  [Tooltip("Health increase per level")]
  public int HealthIncrementPerLevel = 20;

  [Tooltip("Stamina increase per level")]
  public int StaminaIncrementPerLevel = 20;

  [Tooltip("Maximum level the ship can reach")]
  public int MaxLevel = 7;
}