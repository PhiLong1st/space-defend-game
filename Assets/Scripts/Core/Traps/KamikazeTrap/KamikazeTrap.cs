using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeTrap
{
  private int _damage;
  private int _health;
  private float _speed;

  public int Damage => _damage;
  public int Health => _health;
  public float Speed => _speed;

  public KamikazeTrap()
  {
    _damage = KamikazeTrapConstants.Damage;
    _health = KamikazeTrapConstants.Health;
    _speed = KamikazeTrapConstants.Speed;
  }

  public void TakeDamage(int damage) => _health = Mathf.Max(_health - damage, 0);

  public bool IsDestroyed() => _health <= 0;
}