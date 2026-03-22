using UnityEngine;

[CreateAssetMenu(fileName = "LinearProjectileConfig", menuName = "SpaceDefend/Linear Projectile Config", order = 0)]
public class LinearProjectileConfig : ScriptableObject
{
  [Header("Projectile Properties")]
  [Tooltip("Unique identifier for this projectile type")]
  public string ProjectileId;

  [Tooltip("Display name of the projectile")]
  public string DisplayName;

  [Tooltip("Damage dealt by the projectile upon impact")]
  public int Damage = 10;

  [Tooltip("Speed of the projectile")]
  public float Speed = 5f;
}