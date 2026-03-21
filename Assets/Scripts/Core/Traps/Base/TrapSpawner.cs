using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
  public PrefabType type;
  public float spawnTimer;
  public float spawnInterval;
  public int objectsPerWave;
  public int spawnedObjectCount;
}

public class TrapSpawner : MonoBehaviour
{
  [SerializeField] private Transform minPos;
  [SerializeField] private Transform maxPos;

  [SerializeField] private int _waveNumber;
  [SerializeField] private List<Wave> _waves;

  private void Update()
  {
    Wave currentWave = _waves[_waveNumber];
    currentWave.spawnTimer -= Time.deltaTime * GameManager.Instance.WorldSpeed;
    if (currentWave.spawnTimer <= 0)
    {
      currentWave.spawnTimer += currentWave.spawnInterval;
      currentWave.spawnedObjectCount++;
      Spawn();
    }

    if (currentWave.spawnedObjectCount >= currentWave.objectsPerWave)
    {
      currentWave.spawnedObjectCount = 0;
      _waveNumber++;
      if (_waveNumber >= _waves.Count)
      {
        _waveNumber = 0;
      }
    }
  }

  private void Spawn()
  {
    GameObject trap = PrefabsManager.Instance.Spawn(_waves[_waveNumber].type);
    trap.transform.position = RandomSpawnPoint();
    trap.transform.rotation = Quaternion.identity;
    trap.SetActive(true);
    
  }

  private Vector2 RandomSpawnPoint()
  {
    Vector2 spawnPoint;

    spawnPoint.x = minPos.position.x;
    spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);

    return spawnPoint;
  }
}