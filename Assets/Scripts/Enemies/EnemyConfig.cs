using UnityEngine;


/// <summary>
/// ScriptableObject defining enemy stats and behavior.
/// Designer-friendly data container for enemy types.
/// </summary>
[CreateAssetMenu(fileName = "Enemy_", menuName = "SpaceDefend/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
  [Header("Identity")]
  [Tooltip("Unique identifier for this enemy type")]
  public string EnemyId;

  [Tooltip("Display name")]
  public string DisplayName;

  [Header("Stats")]
  [Tooltip("Maximum health points")]
  public float MaxHealth = 100f;

  [Tooltip("Movement speed (units per second)")]
  public float MoveSpeed = 2f;

  [Tooltip("Damage dealt to convoy on reach")]
  public int ConvoyDamage = 10;

  [Header("Rewards")]
  [Tooltip("Gold reward when defeated")]
  public int GoldReward = 10;

  [Header("Behavior")]
  [Tooltip("Priority value for targeting (higher = more important)")]
  public int TargetPriority = 1;

  [Tooltip("Is this enemy a boss/elite?")]
  public bool IsBoss = false;

  [Header("Resistances")]
  [Tooltip("Damage reduction (0 = none, 0.5 = 50% reduction)")]
  [Range(0f, 0.9f)]
  public float DamageReduction = 0f;

  /// <summary>
  /// Calculate actual damage taken after resistances.
  /// </summary>
  public float CalculateDamageTaken(float incomingDamage)
  {
    return incomingDamage * (1f - DamageReduction);
  }
}

/// <summary>
/// Enemy AI states.
/// </summary>
public enum EnemyState
{
  Moving,     // Following path
  Attacking,  // Reached end, attacking convoy
  Dead        // Defeated
}