using UnityEngine;
public class LinearProjectile
{
  public bool IsLaunched { get; private set; }
  public bool HasExploded { get; private set; }
  public float Speed { get; private set; }
  public int Damage { get; private set; }

  public LinearProjectile(LinearProjectileConfig config)
  {
    Speed = config.Speed;
    Damage = config.Damage;
    IsLaunched = false;
    HasExploded = false;
  }

  public void Launch() => IsLaunched = true;

  public void Explode() => HasExploded = true;

  public void Reset()
  {
    IsLaunched = false;
    HasExploded = false;
  }
}