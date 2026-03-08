using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceshipView : MonoBehaviour
{
  [SerializeField] private Animator _engineAnimator;
  [SerializeField] private Animator _spaceshipAnimator;

  [SerializeField] private Spaceship _spaceship;

  #region Stat UI
  [SerializeField] private Slider _healthSlider;
  [SerializeField] private Slider _staminaSlider;
  [SerializeField] private Slider _shieldSlider;

  [SerializeField] private TextMeshProUGUI _healthText;
  [SerializeField] private TextMeshProUGUI _staminaText;
  [SerializeField] private TextMeshProUGUI _shieldText;
  #endregion

  public void EnterBoost()
  {
    _engineAnimator.SetBool("isBoosting", true);
  }

  public void ExitBoost()
  {
    _engineAnimator.SetBool("isBoosting", false);
  }

  private void Update()
  {
    UpdateHealthBar((float)_spaceship.CurrentHealth / _spaceship.MaxHealth);
    UpdateStaminaBar((float)_spaceship.CurrentStamina / _spaceship.MaxStamina);
    UpdateShieldBar((float)_spaceship.CurrentShield / _spaceship.MaxShield);
  }

  public void UpdateHealthBar(float healthPercent)
  {
    _healthSlider.value = healthPercent;
    _healthText.text = $"{_spaceship.CurrentHealth}/{_spaceship.MaxHealth}";
  }

  public void UpdateStaminaBar(float staminaPercent)
  {
    _staminaSlider.value = staminaPercent;
    _staminaText.text = $"{_spaceship.CurrentStamina}/{_spaceship.MaxStamina}";
  }

  public void UpdateShieldBar(float shieldPercent)
  {
    _shieldSlider.value = shieldPercent;
    _shieldText.text = $"{_spaceship.CurrentShield}/{_spaceship.MaxShield}";
  }

  public void PlayDamageAnimation()
  {
    _spaceshipAnimator.SetTrigger("takeDamage");
  }
}