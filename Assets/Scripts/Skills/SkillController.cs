using UnityEngine;
public class SkillController : MonoBehaviour
{
  [SerializeField] private Skill _skill;
  [SerializeField] private SkillView _skillView;
  // [SerializeField] private Ship _ship;

  private void Start()
  {
    Initialize();
  }

  private void FixedUpdate()
  {
    if (IsSkillOnCooldown())
    {
      float newCooldown = Mathf.Max(0, _skill.CooldownRemaining - Time.fixedDeltaTime);
      _skill.SetSkillCooldown(newCooldown);
    }
  }

  private void Update()
  {
    if (Input.GetKeyDown(_skill.ActivationKey))
    {
      ActivateSkill();
    }

    UpdateSkillState();
  }

  public void ActivateSkill()
  {
    var currentState = GetSkillState();
    if (currentState == SkillState.Available)
    {
      var skillCooldown = _skill.SkillCooldown;
      _skill.SetSkillCooldown(skillCooldown);
    }
    else
    {
      Debug.Log($"Skill {_skill.DisplayName} is on cooldown for {_skill.CooldownRemaining:F1} seconds.");
    }
  }

  public void Initialize()
  {
    var initialCooldown = 0;
    _skill.SetSkillCooldown(initialCooldown);

    var initialState = GetSkillState();
    _skill.SetSkillState(initialState);
  }

  public SkillState GetSkillState()
  {
    if (IsSkillOnCooldown())
    {
      return SkillState.CoolingDown;
    }
    return SkillState.Available;
  }

  public bool IsSkillOnCooldown()
  {
    return _skill.CooldownRemaining > 0;
  }

  public void UpdateSkillState()
  {
    var state = GetSkillState();
    _skill.SetSkillState(state);
  }
}