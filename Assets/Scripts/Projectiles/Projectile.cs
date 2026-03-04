using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private ProjectileConfig _config;

  public int Damage => _config.damage;
  public float Speed => _config.speed;
}