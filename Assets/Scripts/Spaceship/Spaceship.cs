using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spaceship : MonoBehaviour
{
  [SerializeField] private SpaceshipConfig _config;

  [SerializeField] private int _currentHealth;
  [SerializeField] private int _currentStamina;
  [SerializeField] private int _currentShield;
  [SerializeField] private float _currentMovementSpeed;
  [SerializeField] private float _currentBoostSpeed;

  public int CurrentHealth => _currentHealth;
  public int CurrentStamina => _currentStamina;
  public int CurrentShield => _currentShield;
  public float CurrentMovementSpeed => _currentMovementSpeed;
  public float CurrentBoostSpeed => _currentBoostSpeed;

  public int MaxHealth => _config.MaxHealth;
  public int MaxStamina => _config.MaxStamina;
  public int MaxShield => _config.MaxShield;

  private void Awake()
  {
    _currentHealth = MaxHealth;
    _currentStamina = MaxStamina;
    _currentShield = MaxShield;
    _currentMovementSpeed = _config.MovementSpeed;
    _currentBoostSpeed = _config.BoostSpeed;
  }

  public void ApplyBoostMultiplier(float xMultiplier)
  {
    _currentBoostSpeed *= xMultiplier;
  }
}
