using UnityEngine;


/// <summary>
/// Data definition for a single enemy group within a wave.
/// Describes what enemies spawn, how many, and timing.
/// </summary>
[System.Serializable]
public class WaveGroup
{
  [Tooltip("Type of enemy to spawn in this group")]
  public string EnemyTypeId;

  [Tooltip("Number of enemies in this group")]
  public int Count = 1;

  [Tooltip("Delay before this group starts spawning (seconds)")]
  public float DelayBeforeSpawn = 0f;

  [Tooltip("Time between individual enemy spawns in this group (seconds)")]
  public float SpawnInterval = 0.5f;

  [Tooltip("Path index enemies should follow (0 = default path)")]
  public int PathIndex = 0;
}

/// <summary>
/// ScriptableObject defining a complete wave configuration.
/// Designer-friendly data container for wave design.
/// </summary>
[CreateAssetMenu(fileName = "Wave_", menuName = "SpaceDefend/Wave Config")]
public class WaveConfig : ScriptableObject
{
  [Header("Wave Identity")]
  [Tooltip("Display name for this wave")]
  public string WaveName;

  [Tooltip("Wave number in sequence")]
  public int WaveNumber;

  [Header("Enemy Groups")]
  [Tooltip("Groups of enemies that comprise this wave")]
  public WaveGroup[] Groups;

  [Header("Rewards")]
  [Tooltip("Gold rewarded upon wave completion")]
  public int CompletionGoldReward = 50;

  [Header("Preview")]
  [Tooltip("Should this wave be shown in preview UI?")]
  public bool ShowInPreview = true;

  /// <summary>
  /// Calculate total number of enemies in this wave.
  /// </summary>
  public int GetTotalEnemyCount()
  {
    int total = 0;
    foreach (var group in Groups)
    {
      total += group.Count;
    }
    return total;
  }

  /// <summary>
  /// Calculate total wave duration (rough estimate).
  /// </summary>
  public float GetEstimatedDuration()
  {
    float maxDuration = 0f;
    foreach (var group in Groups)
    {
      float groupDuration = group.DelayBeforeSpawn + (group.Count - 1) * group.SpawnInterval;
      maxDuration = Mathf.Max(maxDuration, groupDuration);
    }
    return maxDuration;
  }
}