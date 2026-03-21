using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour, IObserver<Spaceship>
{
  [Header("Health")]
  [SerializeField] private Slider _healthSlider;
  [SerializeField] private TextMeshProUGUI _healthProgressText;

  [Header("Stamina")]
  [SerializeField] private Slider _staminaSlider;
  [SerializeField] private TextMeshProUGUI _staminaProgressText;

  [Header("Experience")]
  [SerializeField] private Slider _experienceSlider;
  [SerializeField] private TextMeshProUGUI _experienceProgressText;
  [SerializeField] private TextMeshProUGUI _currentLevelText;

  [Header("Score")]
  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _bestScoreText;

  private void Start()
  {
    SpaceshipController.Instance.AddObserver(this);
  }

  public void UpdateHealth(int currentHealth, int maxHealth)
  {
    _healthSlider.value = currentHealth / maxHealth;
    _healthProgressText.text = $"{currentHealth:000}/{maxHealth:000}";
  }

  public void UpdateStamina(int currentStamina, int maxStamina)
  {
    _staminaSlider.value = currentStamina / maxStamina;
    _staminaProgressText.text = $"{currentStamina:000}/{maxStamina:000}";
  }

  public void UpdateExperience(int currentExperience, int experienceToNextLevel, int currentLevel)
  {
    _currentLevelText.text = $"{currentLevel}";
    _experienceSlider.value = currentExperience / experienceToNextLevel;
    _experienceProgressText.text = $"{currentExperience:000}/{experienceToNextLevel:000}";
  }

  public void UpdateScore(int currentScore, int bestScore)
  {
    _scoreText.text = $"Score: {currentScore:000000}";
    _bestScoreText.text = $"Best: {bestScore:000000}";
  }

  public void OnNotify(Spaceship data)
  {
    UpdateHealth(data.CurrentHealth, data.MaxHealth);
    UpdateStamina(data.CurrentStamina, data.MaxStamina);
    UpdateExperience(data.CurrentExperience, data.ExperienceToNextLevel, data.CurrentLevel);
  }

  private void OnDestroy()
  {
    SpaceshipController.Instance.RemoveObserver(this);
  }
}