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
  public int poolSize = GameData.DefaultPrefabPoolSize;
}

public class PrefabsManager : AbstractSingleton<PrefabsManager>
{
  [SerializeField] private List<PrefabData> _prefabConfigs;

  private readonly Dictionary<PrefabType, ObjectPooling> _pools = new Dictionary<PrefabType, ObjectPooling>();

  private void Start()
  {
    foreach (var config in _prefabConfigs)
    {
      if (config.prefab == null || _pools.ContainsKey(config.type)) continue;
      var objectPool = new ObjectPooling(transform, config.prefab, config.poolSize);
      _pools.Add(config.type, objectPool);
    }
  }

  public GameObject Spawn(PrefabType type)
  {
    if (!_pools.ContainsKey(type))
    {
      Debug.LogError($"Pool with type {type} doesn't exist.");
      return null;
    }

    GameObject objectToSpawn = _pools[type].GetPooledObject();
    return objectToSpawn;
  }
}