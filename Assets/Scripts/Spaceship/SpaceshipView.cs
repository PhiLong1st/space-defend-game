using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceshipView : MonoBehaviour
{
  [SerializeField] private Animator _engineAnimator;
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
    _healthSlider.value = (float)_spaceship.CurrentHealth / _spaceship.MaxHealth;
    _staminaSlider.value = (float)_spaceship.CurrentStamina / _spaceship.MaxStamina;
    _shieldSlider.value = (float)_spaceship.CurrentShield / _spaceship.MaxShield;

    _healthText.text = $"{_spaceship.CurrentHealth}/{_spaceship.MaxHealth}";
    _staminaText.text = $"{_spaceship.CurrentStamina}/{_spaceship.MaxStamina}";
    _shieldText.text = $"{_spaceship.CurrentShield}/{_spaceship.MaxShield}";
  }
}