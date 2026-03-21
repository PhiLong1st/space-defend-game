using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Config", menuName = "SpaceDefend/Projectile Config")]
public class ProjectileConfig : ScriptableObject
{
  public float Speed;
  public float Damage;
  public float TimeCooldown;
}