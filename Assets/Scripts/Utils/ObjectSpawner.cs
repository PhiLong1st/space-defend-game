using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
  [SerializeField] private Transform minPos;
  [SerializeField] private Transform maxPos;

  [SerializeField] private int waveNumber;
  [SerializeField] private List<Wave> waves;

  [System.Serializable]
  public class Wave
  {
    public float spawnTimer;
    public float spawnInterval;
    public int objectsPerWave;
    public int spawnedObjectCount;
    public ObjectPooler pooler;
  }

  void Update()
  {
    // waves[waveNumber].spawnTimer -= Time.deltaTime * GameManager.Instance.worldSpeed;
    waves[waveNumber].spawnTimer -= Time.deltaTime;
    if (waves[waveNumber].spawnTimer <= 0)
    {
      waves[waveNumber].spawnTimer += waves[waveNumber].spawnInterval;
      SpawnObject();
    }

    if (waves[waveNumber].spawnedObjectCount >= waves[waveNumber].objectsPerWave)
    {
      waves[waveNumber].spawnedObjectCount = 0;
      waveNumber++;
      if (waveNumber >= waves.Count)
      {
        waveNumber = 0;
      }
    }
  }

  private void SpawnObject()
  {
    GameObject obj = waves[waveNumber].pooler.GetPooledObject();
    if (obj != null)
    {
      obj.transform.position = RandomSpawnPoint();
      obj.transform.rotation = transform.rotation;
      obj.SetActive(true);
    }
    waves[waveNumber].spawnedObjectCount++;
  }

  private Vector2 RandomSpawnPoint()
  {
    Vector2 spawnPoint;

    spawnPoint.x = minPos.position.x;
    spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);

    return spawnPoint;
  }
}