using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeTrap : MonoBehaviour
{
  [SerializeField] int _damage = 50;
  [SerializeField] int _health = 50;
  [SerializeField] float _speed = 2f;

  public int Damage => _damage;

  public float Speed => _speed;

  public void TakeDamage(int damage) => _health = Mathf.Max(_health - damage, 0);

  public bool IsDestroyed() => _health <= 0;

  public void Explode()
  {
    Debug.Log("KamikazeTrap exploded!");
  }
}