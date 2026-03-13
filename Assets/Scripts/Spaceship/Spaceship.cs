using UnityEngine;
using System;

public class Spaceship : MonoBehaviour
{
  [SerializeField] private SpaceshipConfig _config;

  private float _currentMovementSpeed;
  private float _currentDamage;
  private int _currentLevel;
  private int _currentExperience;

  public float CurrentMovementSpeed => _currentMovementSpeed * GameManager.Instance.WorldSpeed;
  public float CurrentDamage => _currentDamage;
  public int CurrentLevel => _currentLevel;
  public int CurrentExperience => _currentExperience;

  private void Awake()
  {
    _currentMovementSpeed = _config.MovementSpeed;
    _currentDamage = _config.Damage;
    _currentLevel = _config.Level;
    _currentExperience = 0;
  }

  public void GainExperience(int amount)
  {
    _currentExperience += amount;
    _currentExperience = Mathf.Min(_currentExperience, 100);
  }

  public void Move(Vector2 direction)
  {
    Vector2 newPosition = (Vector2)transform.position + direction.normalized * CurrentMovementSpeed * Time.deltaTime;
    transform.position = newPosition;
  }

  public bool CanLevelUp()
  {
    return _currentExperience >= 100 && _currentLevel < _config.MaxLevel;
  }

  public void LevelUp()
  {
    _currentLevel++;
    _currentMovementSpeed += _config.MovementSpeedIncreasePerLevel;
    _currentDamage += _config.DamageIncreasePerLevel;
    _currentExperience = 0;
  }
}
