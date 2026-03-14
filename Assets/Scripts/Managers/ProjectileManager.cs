using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
class ProjectileKeyValue
{
  public ProjectileType Type;
  public AbstractProjectile Projectile;
}

public class ProjectileManager : MonoBehaviour
{
  public static ProjectileManager Instance { get; private set; }
  [SerializeField] private ProjectileKeyValue[] _projectiles;

  private Dictionary<ProjectileType, AbstractProjectile> _projectilesDict;

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }
  }

  private void Start()
  {
    _projectilesDict = new Dictionary<ProjectileType, AbstractProjectile>();
    foreach (var kv in _projectiles)
    {
      _projectilesDict[kv.Type] = kv.Projectile;
    }
  }

  public AbstractProjectile GetProjectile(ProjectileType type)
  {
    if (_projectilesDict.ContainsKey(type))
    {
      return _projectilesDict[type];
    }
    return null;
  }
}