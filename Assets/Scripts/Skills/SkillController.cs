using UnityEngine;
public class SkillController : MonoBehaviour
{
  [SerializeField] private ShipController _ship;
  [SerializeField] private Skill _skill;
  [SerializeField] private SkillView _skillView;

  private void Awake()
  {
    var initialCooldown = 0;
    _skill.SetSkillCooldown(initialCooldown);
  }

  private void Start()
  {
    var initialState = GetSkillState();
    _skill.SetSkillState(initialState);
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
    if (Input.GetKeyDown(_skill.ActivationKey) && GetSkillState() == SkillState.Available)
    {
      ActivateSkill();
      _ship.UseStamina(_skill.SkillEnergy);
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

  public SkillState GetSkillState()
  {
    if (IsSkillOnCooldown())
    {
      return SkillState.CoolingDown;
    }

    if (IsSkillOutOfEnergy())
    {
      return SkillState.OutOfEnergy;
    }

    return SkillState.Available;
  }

  public bool IsSkillOnCooldown()
  {
    return _skill.CooldownRemaining > 0;
  }

  public bool IsSkillOutOfEnergy()
  {
    return _ship.CurrentStamina < _skill.SkillEnergy;
  }

  public void UpdateSkillState()
  {
    var state = GetSkillState();
    _skill.SetSkillState(state);
  }
}