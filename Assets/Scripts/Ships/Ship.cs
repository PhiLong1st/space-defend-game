
using UnityEngine;

public class Ship : MonoBehaviour
{
  [SerializeField] private ShipConfig _config;
  [SerializeField] private int _currentHealth;
  [SerializeField] private int _currentStamina;

  public int CurrentHealth => _currentHealth;
  public int CurrentStamina => _currentStamina;

  public int MaxHealth => _config.MaxHealth;
  public int MaxStamina => _config.MaxStamina;
  public float MovementSpeed => _config.MovementSpeed;
  public GameObject NormalProjectilePrefab => _config.normalProjectilePrefab;

  public void SetCurrentHealth(int amount)
  {
    _currentHealth = Mathf.Clamp(amount, 0, MaxHealth);
  }

  public void SetCurrentStamina(int amount)
  {
    _currentStamina = Mathf.Clamp(amount, 0, MaxStamina);
  }
}
