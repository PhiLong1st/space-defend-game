using UnityEngine;
using System.Collections.Generic;

public enum UIPrefabType
{
  FloatingDamageText,
}

[System.Serializable]
public class UIPrefabData
{
  public UIPrefabType type;
  public GameObject prefab;
  public int poolSize = GameData.DefaultPrefabPoolSize;
}

public class UIPrefabsManager : AbstractSingleton<UIPrefabsManager>
{
  [SerializeField] private List<UIPrefabData> _uiPrefabConfigs;
  [SerializeField] private Transform _uiContainer;

  private readonly Dictionary<UIPrefabType, ObjectPooling> _uiPools = new Dictionary<UIPrefabType, ObjectPooling>();

  private void Start()
  {
    foreach (var config in _uiPrefabConfigs)
    {
      if (config.prefab == null || _uiPools.ContainsKey(config.type)) continue;
      var objectPool = new ObjectPooling(_uiContainer, config.prefab, config.poolSize);
      _uiPools.Add(config.type, objectPool);
    }
  }

  public GameObject Spawn(UIPrefabType type)
  {
    if (!_uiPools.ContainsKey(type))
    {
      Debug.LogError($"UI Pool with type {type} doesn't exist.");
      return null;
    }

    GameObject objectToSpawn = _uiPools[type].GetPooledObject();
    objectToSpawn.SetActive(true);
    objectToSpawn.transform.SetParent(_uiContainer);
    return objectToSpawn;
  }
}