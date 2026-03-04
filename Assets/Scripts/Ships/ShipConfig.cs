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

  [Tooltip("Movement speed of the ship")]
  public float MovementSpeed = 5f;

  [Header("Combat")]
  [Tooltip("Damage dealt by the ship's primary weapon")]
  public int damage = 10;
  public GameObject normalProjectilePrefab;
}