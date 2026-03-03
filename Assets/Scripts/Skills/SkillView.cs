using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillView : MonoBehaviour
{
  [SerializeField] private Image _icon;
  [SerializeField] private Image _backgroundIcon;
  [SerializeField] private Image _cooldownOverlay;

  [SerializeField] private Skill _skill;
  [SerializeField] private TextMeshProUGUI _cooldownText;

  private void Update()
  {
    float cooldown = _skill.CooldownRemaining;
    _cooldownText.text = cooldown > 0 ? cooldown.ToString("F1") : "";

    bool isCoolingDown = _skill.CurrentState == SkillState.CoolingDown;
    _cooldownOverlay.gameObject.SetActive(isCoolingDown);
  }
}