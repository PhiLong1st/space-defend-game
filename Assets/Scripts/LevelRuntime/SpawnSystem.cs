using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Handles actual enemy instantiation based on wave configurations.
/// Listens to WaveStarted events and spawns enemies according to WaveConfig timing.
/// Uses object pooling for performance.
/// </summary>
public class SpawnSystem
{
  private readonly EventBus eventBus;
  private readonly Transform spawnRoot;
  private MonoBehaviour coroutineRunner;

  // Enemy prefab registry (map enemy type ID to prefab)
  private Dictionary<string, GameObject> enemyPrefabs = new();

  // Simple object pool per enemy type
  private Dictionary<string, Queue<GameObject>> enemyPools = new();

  private Coroutine activeSpawnCoroutine;

  public SpawnSystem(EventBus eventBus, Transform spawnRoot, MonoBehaviour coroutineRunner)
  {
    this.eventBus = eventBus;
    this.spawnRoot = spawnRoot;
    this.coroutineRunner = coroutineRunner;

    eventBus.Subscribe<WaveStartedEvent>(OnWaveStarted);
  }

  /// <summary>
  /// Register enemy prefab for spawning.
  /// Call this during initialization for each enemy type.
  /// </summary>
  public void RegisterEnemyPrefab(string enemyTypeId, GameObject prefab)
  {
    if (!enemyPrefabs.ContainsKey(enemyTypeId))
    {
      enemyPrefabs[enemyTypeId] = prefab;
      enemyPools[enemyTypeId] = new Queue<GameObject>();
      Debug.Log($"Registered enemy type: {enemyTypeId}");
    }
  }

  private void OnWaveStarted(WaveStartedEvent evt)
  {
    // Get current wave config from somewhere (would normally be injected)
    // For now, this is a placeholder showing the pattern
    Debug.Log($"SpawnSystem: Starting spawns for wave {evt.WaveIndex}");
  }

  /// <summary>
  /// Spawn enemies for a specific wave configuration.
  /// Called by WaveSystem or level controller.
  /// </summary>
  public void SpawnWave(WaveConfig waveConfig)
  {
    if (activeSpawnCoroutine != null)
    {
      coroutineRunner.StopCoroutine(activeSpawnCoroutine);
    }

    activeSpawnCoroutine = coroutineRunner.StartCoroutine(SpawnWaveCoroutine(waveConfig));
  }

  private IEnumerator SpawnWaveCoroutine(WaveConfig waveConfig)
  {
    List<Coroutine> groupCoroutines = new();

    // Spawn each group concurrently
    foreach (var group in waveConfig.Groups)
    {
      groupCoroutines.Add(
          coroutineRunner.StartCoroutine(SpawnGroupCoroutine(group))
      );
    }

    // Wait for all groups to finish
    foreach (var coroutine in groupCoroutines)
    {
      yield return coroutine;
    }

    Debug.Log($"All spawns completed for wave: {waveConfig.WaveName}");
  }

  private IEnumerator SpawnGroupCoroutine(WaveGroup group)
  {
    // Wait for group delay
    if (group.DelayBeforeSpawn > 0)
    {
      yield return new WaitForSeconds(group.DelayBeforeSpawn);
    }

    // Spawn enemies one by one
    for (int i = 0; i < group.Count; i++)
    {
      SpawnEnemy(group.EnemyTypeId, group.PathIndex);

      if (i < group.Count - 1)
      {
        yield return new WaitForSeconds(group.SpawnInterval);
      }
    }
  }

  private void SpawnEnemy(string enemyTypeId, int pathIndex)
  {
    GameObject enemyObj = GetOrCreateEnemy(enemyTypeId);

    if (enemyObj == null)
    {
      Debug.LogError($"Failed to spawn enemy: {enemyTypeId}");
      return;
    }

    // Position at spawn point (would get from path system)
    enemyObj.transform.position = GetSpawnPosition(pathIndex);
    enemyObj.SetActive(true);

    // Initialize enemy (would call enemy controller)
    // This is where you'd pass path data, etc.

    int enemyId = enemyObj.GetInstanceID();
    eventBus.Publish(new EnemySpawnedEvent { EnemyId = enemyId });

    Debug.Log($"Spawned enemy: {enemyTypeId} at path {pathIndex}");
  }

  private GameObject GetOrCreateEnemy(string enemyTypeId)
  {
    // Try to get from pool first
    if (enemyPools.ContainsKey(enemyTypeId) && enemyPools[enemyTypeId].Count > 0)
    {
      return enemyPools[enemyTypeId].Dequeue();
    }

    // Create new instance
    if (enemyPrefabs.ContainsKey(enemyTypeId))
    {
      GameObject newEnemy = GameObject.Instantiate(enemyPrefabs[enemyTypeId], spawnRoot);
      newEnemy.name = $"{enemyTypeId}_{newEnemy.GetInstanceID()}";
      return newEnemy;
    }

    return null;
  }

  /// <summary>
  /// Return enemy to pool for reuse.
  /// </summary>
  public void ReturnToPool(string enemyTypeId, GameObject enemyObj)
  {
    enemyObj.SetActive(false);

    if (!enemyPools.ContainsKey(enemyTypeId))
    {
      enemyPools[enemyTypeId] = new Queue<GameObject>();
    }

    enemyPools[enemyTypeId].Enqueue(enemyObj);
  }

  private Vector3 GetSpawnPosition(int pathIndex)
  {
    // Placeholder: would get from path/waypoint system
    return new Vector3(-10f, pathIndex * 2f, 0f);
  }

  public void Cleanup()
  {
    if (activeSpawnCoroutine != null)
    {
      coroutineRunner.StopCoroutine(activeSpawnCoroutine);
    }

    eventBus.Unsubscribe<WaveStartedEvent>(OnWaveStarted);

    // Clear pools
    foreach (var pool in enemyPools.Values)
    {
      while (pool.Count > 0)
      {
        var obj = pool.Dequeue();
        if (obj != null)
        {
          GameObject.Destroy(obj);
        }
      }
    }
  }
}