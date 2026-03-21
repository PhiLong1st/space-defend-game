using UnityEngine;
using System;

public class Spaceship : MonoBehaviour
{
  [SerializeField] private SpaceshipConfig _config;

  private float _currentMovementSpeed;
  public float CurrentMovementSpeed => _currentMovementSpeed * GameManager.Instance.WorldSpeed;

  private bool _isDestroyed = false;
  public bool IsDestroyed => _isDestroyed;

  private void Awake()
  {
    _currentMovementSpeed = _config.MovementSpeed;
  }

  public void Move(Vector2 direction)
  {
    transform.Translate(direction * Time.deltaTime * CurrentMovementSpeed);
  }

  public void HandleDestroy()
  {
    _isDestroyed = true;
  }
}
