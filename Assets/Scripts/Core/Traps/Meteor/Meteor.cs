using UnityEngine;

public class Meteor
{
  public int Damage { get; private set; }

  public int Health { get; private set; }

  public float Speed { get; private set; }

  public Vector2 Scale { get; private set; }

  public int MaxHealth => _config.Health;

  public int HealthPercentage => Health > 0 ? Mathf.FloorToInt((float)Health / MaxHealth * 100) : 0;

  public bool IsDestroyed() => Health <= 0;

  private MeteorConfig _config;

  public Meteor(MeteorConfig config)
  {
    _config = config;
    Damage = config.Damage;
    Health = config.Health;
    Speed = GetRandomSpeed();
    Scale = GetRandomScale();
  }

  public void TakeDamage(int damage) => Health = Mathf.Max(Health - damage, 0);

  public void Reset()
  {
    Health = MaxHealth;
  }

  private float GetRandomSpeed() => Random.Range(_config.MinSpeed, _config.MaxSpeed);

  private Vector2 GetRandomScale() => Vector2.one * Random.Range(_config.MinScale, _config.MaxScale);
}