using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Manages wave progression: tracking current wave, completion, and sequencing.
/// Does NOT handle spawning (that's SpawnSystem's job).
/// Coordinates wave flow and emits wave-related events.
/// </summary>
public class WaveSystem
{
  private readonly EventBus eventBus;
  private readonly List<WaveConfig> waveConfigs;

  private int currentWaveIndex = -1;
  private bool isWaveActive = false;
  private int activeEnemyCount = 0;

  public int CurrentWaveIndex => currentWaveIndex;
  public bool IsWaveActive => isWaveActive;
  public WaveConfig CurrentWave =>
      currentWaveIndex >= 0 && currentWaveIndex < waveConfigs.Count
      ? waveConfigs[currentWaveIndex]
      : null;

  public WaveSystem(EventBus eventBus, List<WaveConfig> waveConfigs)
  {
    this.eventBus = eventBus;
    this.waveConfigs = waveConfigs;

    // Subscribe to enemy events to track wave completion
    eventBus.Subscribe<EnemySpawnedEvent>(OnEnemySpawned);
    eventBus.Subscribe<EnemyDefeatedEvent>(OnEnemyDefeated);
  }

  /// <summary>
  /// Start the next wave in sequence.
  /// Returns false if no more waves available.
  /// </summary>
  public bool StartNextWave()
  {
    if (isWaveActive)
    {
      Debug.LogWarning("Cannot start wave: previous wave still active");
      return false;
    }

    if (currentWaveIndex + 1 >= waveConfigs.Count)
    {
      Debug.Log("All waves completed!");
      return false;
    }

    currentWaveIndex++;
    isWaveActive = true;
    activeEnemyCount = 0;

    eventBus.Publish(new WaveStartedEvent
    {
      WaveIndex = currentWaveIndex
    });

    Debug.Log($"Wave {currentWaveIndex + 1} started: {CurrentWave.WaveName}");
    return true;
  }

  /// <summary>
  /// Allow player to call wave early (skip preparation time).
  /// </summary>
  public bool CallWaveEarly()
  {
    if (isWaveActive)
    {
      Debug.LogWarning("Cannot call wave early: wave already active");
      return false;
    }

    return StartNextWave();
  }

  /// <summary>
  /// Get preview of upcoming wave (for UI display).
  /// Returns null if no next wave.
  /// </summary>
  public WaveConfig GetNextWavePreview()
  {
    int nextIndex = currentWaveIndex + 1;
    if (nextIndex < waveConfigs.Count)
    {
      return waveConfigs[nextIndex];
    }
    return null;
  }

  /// <summary>
  /// Check if all waves are complete.
  /// </summary>
  public bool AreAllWavesComplete()
  {
    return currentWaveIndex >= waveConfigs.Count - 1 && !isWaveActive;
  }

  private void OnEnemySpawned(EnemySpawnedEvent evt)
  {
    if (!isWaveActive)
      return;

    activeEnemyCount++;
  }

  private void OnEnemyDefeated(EnemyDefeatedEvent evt)
  {
    if (!isWaveActive)
      return;

    activeEnemyCount--;

    // Check if wave is complete
    if (activeEnemyCount <= 0)
    {
      CompleteWave();
    }
  }

  private void CompleteWave()
  {
    isWaveActive = false;

    eventBus.Publish(new WaveCompletedEvent
    {
      WaveIndex = currentWaveIndex
    });

    Debug.Log($"Wave {currentWaveIndex + 1} completed!");
  }

  public void Reset()
  {
    currentWaveIndex = -1;
    isWaveActive = false;
    activeEnemyCount = 0;
  }

  public void Cleanup()
  {
    eventBus.Unsubscribe<EnemySpawnedEvent>(OnEnemySpawned);
    eventBus.Unsubscribe<EnemyDefeatedEvent>(OnEnemyDefeated);
  }
}