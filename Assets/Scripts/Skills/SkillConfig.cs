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

  [Header("Behavior")]
  [Tooltip("Time in seconds for skill cooldown")]
  public float SkillCooldown = 5f;

  [Tooltip("Energy required for skill activation")]
  public int SkillEnergy = 5;

  [Tooltip("Activation key for skill (e.g., KeyCode.Space)")]
  public KeyCode ActivationKey = KeyCode.Space;
}