using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AbstractSkill))]
public abstract class AbstractSkillView : MonoBehaviour
{
  [SerializeField] private Image _icon;
  [SerializeField] private TextMeshProUGUI _cooldownText;
  [SerializeField] private Image _overlayImage;

  private AbstractSkill _skill;

  private void Awake()
  {
    _skill = GetComponentInParent<AbstractSkill>();
    if (_skill == null)
    {
      enabled = false;
      Debug.LogError("AbstractSkillView must be a child of an AbstractSkill.");
    }
  }

  private void Update()
  {
    float cooldown = _skill.CooldownRemaining;
    _cooldownText.text = GetCooldownText(cooldown);
    UpdateView();
  }

  public void UpdateView()
  {
    switch (_skill.CurrentState)
    {
      case SkillState.Available:
        HandleStateAvailableView();
        break;
      case SkillState.OutOfEnergy:
        HandleStateOutOfEnergyView();
        break;
      case SkillState.CoolingDown:
        HandleStateCoolingDownView();
        break;
      case SkillState.Disabled:
        HandleStateDisabledView();
        break;
    }
  }

  protected virtual void HandleStateAvailableView()
  {
    _overlayImage.color = Color.clear;
  }

  protected virtual void HandleStateOutOfEnergyView()
  {
    _overlayImage.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
  }

  protected virtual void HandleStateCoolingDownView()
  {
    _overlayImage.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
  }

  protected virtual void HandleStateDisabledView()
  {
    _overlayImage.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
  }

  private string GetCooldownText(float cooldown)
  {
    switch (cooldown)
    {
      case <= 0:
        return "";
      case < 10:
        return $"{cooldown:F1}";
      default:
        return Mathf.CeilToInt(cooldown).ToString();
    }
  }
}