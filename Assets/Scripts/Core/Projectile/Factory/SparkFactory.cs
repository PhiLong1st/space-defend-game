using UnityEngine;
using System.Collections.Generic;

public class SparkFactory : ProjectileFactory
{
  public SparkFactory(GameObject attachPoint, float cooldown) : base(attachPoint, cooldown) { }

  public override List<GameObject> CreateProjectile()
  {
    var obj = PrefabsManager.Instance.Spawn(PrefabType.SparkProjectile);

    var projectiles = new List<GameObject> { obj };

    if (obj == null)
    {
      Debug.LogError("Failed to spawn SparkProjectile.");
      return null;
    }

    return projectiles;
  }
}