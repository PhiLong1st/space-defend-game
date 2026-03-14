using UnityEngine;
using System.Collections.Generic;

public enum PrefabType
{
  KamikazeTrap,
  MeteorTrap,
  WarningEffect,
}

[System.Serializable]
public class PrefabData
{
  public PrefabType type;
  public GameObject prefab;
  public int poolSize = 10;
}

public class PrefabsManager : MonoBehaviour
{
  public static PrefabsManager Instance { get; private set; }
  [SerializeField] private List<PrefabData> _prefabConfigs;

  private Dictionary<PrefabType, Queue<GameObject>> _pools = new Dictionary<PrefabType, Queue<GameObject>>();

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }

    InitializePools();
  }

  private void InitializePools()
  {
    foreach (var config in _prefabConfigs)
    {
      if (config.prefab == null) continue;

      if (!_pools.ContainsKey(config.type))
      {
        var objectPool = new Queue<GameObject>();

        for (int i = 0; i < config.poolSize; i++)
        {
          GameObject obj = Instantiate(config.prefab, transform);
          obj.SetActive(false);
          objectPool.Enqueue(obj);
        }

        _pools.Add(config.type, objectPool);
      }
    }
  }

  public GameObject Spawn(PrefabType type)
  {
    if (!_pools.ContainsKey(type))
    {
      Debug.LogError($"Pool with type {type} doesn't exist.");
      return null;
    }

    GameObject objectToSpawn = _pools[type].Dequeue();
    objectToSpawn.SetActive(true);
    _pools[type].Enqueue(objectToSpawn);
    return objectToSpawn;
  }
}