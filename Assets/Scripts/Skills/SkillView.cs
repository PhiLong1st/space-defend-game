using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillView : MonoBehaviour
{
  [SerializeField] private Image _icon;
  [SerializeField] private Image _backgroundIcon;
  [SerializeField] private Image _overlayImage;

  [SerializeField] private Skill _skill;
  [SerializeField] private TextMeshProUGUI _cooldownText;

  private void Update()
  {
    if (_skill == null) return;

    float cooldown = _skill.CooldownRemaining;
    _cooldownText.text = cooldown > 0 ? cooldown.ToString("F1") : "";

    switch (_skill.CurrentState)
    {
      case SkillState.Available:
        _overlayImage.color = new Color(0, 0, 0, 0);
        break;
      case SkillState.CoolingDown:
        _overlayImage.color = new Color(0, 0, 0, 0.5f);
        break;
      case SkillState.Disabled:
        _overlayImage.color = new Color(0, 0, 0, 0.5f);
        break;
      case SkillState.OutOfEnergy:
        _overlayImage.color = Color.blue;
        break;
    }
  }
}