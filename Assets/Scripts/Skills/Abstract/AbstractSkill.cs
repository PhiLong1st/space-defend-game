using UnityEngine;

[RequireComponent(typeof(AbstractSkillView))]
public abstract class AbstractSkill : MonoBehaviour
{
  [SerializeField] protected SkillConfig _config;

  public SkillConfig Config => _config;
  public float CooldownRemaining => _config.IsPassive ? 0 : _cooldownRemaining;
  public SkillState CurrentState => _config.IsPassive ? SkillState.Available : _state;
  // public int TimeDuration => _config.IsPassive ? 0 : _config.TimeDuration;

  protected float _cooldownRemaining;
  protected SkillState _state;

  private void Awake()
  {
    _state = SkillState.Available;
    _cooldownRemaining = 0;
  }

  private void FixedUpdate()
  {
    if (_state == SkillState.CoolingDown)
    {
      ReduceCooldown(Time.fixedDeltaTime);
    }
  }

  public void ChangeState(SkillState newState)
  {
    _state = newState;
  }

  public void ReduceCooldown(float amount)
  {
    _cooldownRemaining = Mathf.Max(0f, _cooldownRemaining - amount);
  }

  public void Reset()
  {
    _cooldownRemaining = 0;
  }

  #region Abstract 
  public abstract bool CanActive();
  public abstract void Activate();
  #endregion
}