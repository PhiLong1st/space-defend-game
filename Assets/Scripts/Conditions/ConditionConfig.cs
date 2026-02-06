using UnityEngine;


/// <summary>
/// Types of conditions that can be violated.
/// </summary>
public enum ConditionType
{
  Core,       // Failure = instant game over (e.g., convoy destroyed)
  Level,      // Checked at level end (e.g., no ship losses)
  InGame      // Continuous monitoring (e.g., gold threshold)
}

/// <summary>
/// Types of punishments for condition violations.
/// </summary>
public enum PunishmentType
{
  Fail,       // Instant game over
  Penalty,    // Deduct gold/resources
  Fix         // Requires gold payment to resolve
}

/// <summary>
/// ScriptableObject defining a game condition and its punishment.
/// Designer-friendly container for fail states and penalties.
/// </summary>
[CreateAssetMenu(fileName = "Condition_", menuName = "SpaceDefend/Condition")]
public class ConditionConfig : ScriptableObject
{
  [Header("Identity")]
  [Tooltip("Unique identifier for this condition")]
  public string ConditionId;

  [Tooltip("Display name for UI")]
  public string DisplayName;

  [TextArea(2, 4)]
  [Tooltip("Description of the condition")]
  public string Description;

  [Header("Condition Type")]
  public ConditionType Type;

  [Header("Punishment")]
  public PunishmentType Punishment;

  [Tooltip("Gold penalty amount (for Penalty/Fix types)")]
  public int PenaltyAmount;

  [Header("Monitoring")]
  [Tooltip("Should this condition be actively monitored?")]
  public bool IsActive = true;

  /// <summary>
  /// Returns true if this condition causes instant game over.
  /// </summary>
  public bool IsFailCondition()
  {
    return Punishment == PunishmentType.Fail;
  }
}