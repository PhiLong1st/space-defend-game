using UnityEngine;

/// <summary>
/// Simple object pool for reusable GameObjects.
/// Reduces instantiation overhead for frequently spawned objects.
/// </summary>
public class ObjectPool
{
  private readonly GameObject prefab;
  private readonly Transform parent;
  private readonly System.Collections.Generic.Queue<GameObject> pool = new();
  private readonly int initialSize;

  public ObjectPool(GameObject prefab, int initialSize = 10, Transform parent = null)
  {
    this.prefab = prefab;
    this.initialSize = initialSize;
    this.parent = parent;

    // Pre-populate pool
    for (int i = 0; i < initialSize; i++)
    {
      CreateNewObject();
    }
  }

  /// <summary>
  /// Get an object from the pool.
  /// Creates a new one if pool is empty.
  /// </summary>
  public GameObject Get()
  {
    GameObject obj;

    if (pool.Count > 0)
    {
      obj = pool.Dequeue();
    }
    else
    {
      obj = CreateNewObject();
    }

    obj.SetActive(true);
    return obj;
  }

  /// <summary>
  /// Return an object to the pool for reuse.
  /// </summary>
  public void Return(GameObject obj)
  {
    obj.SetActive(false);
    pool.Enqueue(obj);
  }

  /// <summary>
  /// Clear and destroy all pooled objects.
  /// </summary>
  public void Clear()
  {
    while (pool.Count > 0)
    {
      var obj = pool.Dequeue();
      if (obj != null)
      {
        Destroy(obj);
      }
    }
  }

  private GameObject CreateNewObject()
  {
    var obj = Instantiate(prefab, parent);
    obj.SetActive(false);
    return obj;
  }
}
