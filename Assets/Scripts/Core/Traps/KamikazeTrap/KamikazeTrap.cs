using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeTrap
{
  public int Damage { get; private set; }

  public int Health { get; private set; }

  public float Speed { get; private set; }

  public float WarningSpeed { get; private set; }

  public KamikazeTrap(KamikazeTrapConfig config)
  {
    Damage = config.Damage;
    Health = config.Health;
    Speed = config.Speed;
    WarningSpeed = config.WarningSpeed;
  }

  public void TakeDamage(int damage) => Health = Mathf.Max(Health - damage, 0);

  public bool IsDestroyed() => Health <= 0;
}