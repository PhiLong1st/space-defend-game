using UnityEngine;
public class Spark
{
  private float _speed;

  private int _damage;

  private bool _isLaunched;
  private bool _hasExploded;

  public float Speed => _speed;

  public int Damage => _damage;

  public bool IsLaunched => _isLaunched;
  public bool HasExploded => _hasExploded;

  public Spark(ProjectileConfig config)
  {
    _speed = config.Speed;
    _damage = config.Damage;
    _isLaunched = false;
    _hasExploded = false;
  }

  public void Launch() => _isLaunched = true;

  public void Explode() => _hasExploded = true;

  public void Reset()
  {
    _isLaunched = false;
    _hasExploded = false;
  }
}