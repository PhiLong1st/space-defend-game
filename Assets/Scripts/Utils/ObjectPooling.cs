using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling
{
  private int _poolSize = GameData.DefaultPoolSize;
  private List<GameObject> pool;
  private Transform _transform;
  private GameObject _prefab;

  public ObjectPooling(Transform transform, GameObject prefab, int size)
  {
    _transform = transform;
    _prefab = prefab;
    _poolSize = size;
    pool = new List<GameObject>();
    for (int i = 0; i < _poolSize; i++)
    {
      CreateNewObject();
    }
  }

  private GameObject CreateNewObject()
  {
    GameObject obj = Object.Instantiate(_prefab, _transform);
    obj.SetActive(false);
    pool.Add(obj);
    return obj;
  }

  public GameObject GetPooledObject()
  {
    foreach (GameObject obj in pool)
    {
      if (!obj.activeSelf)
      {
        return obj;
      }
    }
    return CreateNewObject();
  }

  public void ReturnToPool(GameObject obj)
  {
    obj.SetActive(false);
  }
}