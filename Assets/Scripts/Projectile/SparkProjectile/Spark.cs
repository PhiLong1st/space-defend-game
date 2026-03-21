using UnityEngine;
public class Spark
{
  private float _speed;
  private float _damage;

  public float Speed => _speed;

  public Spark(ProjectileConfig config)
  {
    _speed = config.Speed;
    _damage = config.Damage;
  }

  public void Reset()
  {
    // Reset any state if needed
  }
}