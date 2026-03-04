using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
  [SerializeField] private SkillConfig _config;

  public string DisplayName => _config.DisplayName;
  public KeyCode ActivationKey => _config.ActivationKey;
  public float SkillCooldown => _config.SkillCooldown;
  public int SkillEnergy => _config.SkillEnergy;
  public float CooldownRemaining => _cooldownRemaining;
  public SkillState CurrentState => _state;

  private float _cooldownRemaining;
  private SkillState _state;

  public void SetSkillState(SkillState newState)
  {
    _state = newState;
    Debug.Log($"Skill {_config.DisplayName} state changed to: {_state}");
  }

  public void SetSkillCooldown(float amount)
  {
    _cooldownRemaining = amount;
    // Debug.Log($"Skill {_config.DisplayName} cooldown set to: {_cooldownRemaining}");
  }
}