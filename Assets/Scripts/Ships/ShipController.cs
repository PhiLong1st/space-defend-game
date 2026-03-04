using UnityEngine;

public class ShipController : MonoBehaviour
{
  [SerializeField] private Ship _ship;
  [SerializeField] private ShipView _shipView;
  [SerializeField] private SkillController[] _skills;
  [SerializeField] private SliderBarPresenter _healthBar;
  [SerializeField] private SliderBarPresenter _staminaBar;

  public int CurrentHealth => _ship.CurrentHealth;
  public int CurrentStamina => _ship.CurrentStamina;

  private void Awake()
  {
    _ship.SetCurrentHealth(_ship.MaxHealth);
    _ship.SetCurrentStamina(_ship.MaxStamina);

    _healthBar.SetMaxValue(_ship.MaxHealth);
    _staminaBar.SetMaxValue(_ship.MaxStamina);
  }

  public void TakeDamage(int amount)
  {
    int newHealth = Mathf.Max(_ship.CurrentHealth - amount, 0);
    _ship.SetCurrentHealth(newHealth);
    _healthBar.Decrement(amount);
  }

  public void UseStamina(int amount)
  {
    int newStamina = Mathf.Max(_ship.CurrentStamina - amount, 0);
    _ship.SetCurrentStamina(newStamina);
    _staminaBar.Decrement(amount);
  }

  public void RestoreHealth(int amount)
  {
    int newHealth = Mathf.Min(_ship.CurrentHealth + amount, _ship.MaxHealth);
    _ship.SetCurrentHealth(newHealth);
    _healthBar.Increment(amount);
  }

  public void RestoreStamina(int amount)
  {
    int newStamina = Mathf.Min(_ship.CurrentStamina + amount, _ship.MaxStamina);
    _ship.SetCurrentStamina(newStamina);
    _staminaBar.Increment(amount);
  }
}

