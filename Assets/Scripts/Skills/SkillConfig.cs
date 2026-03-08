using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_", menuName = "SpaceDefend/Skill Config")]
public class SkillConfig : ScriptableObject
{
  [Header("Identity")]
  [Tooltip("Unique identifier for this Skill type")]
  public string SkillId;

  [Tooltip("Display name")]
  public string DisplayName;

  [Tooltip("Skill Icon")]
  public Sprite SkillIcon;

  [Header("Behavior")]
  [Tooltip("Time in seconds for skill cooldown")]
  public float SkillCooldown = 5f;

  [Tooltip("Energy required for skill activation")]
  public int SkillEnergy = 5;

  [Tooltip("Duration in seconds for skill effect (if applicable)")]
  public int SkillTimeDuration = 2;

  [Tooltip("Is this skill passive (always available, no cooldown)")]
  public bool IsPassive = false;
}