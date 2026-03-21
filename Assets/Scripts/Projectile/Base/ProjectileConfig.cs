using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Config", menuName = "SpaceDefend/Projectile Config")]
public class ProjectileConfig : ScriptableObject
{
  public float Speed;
  public int Damage;
  public float TimeCooldown;
}