using UnityEngine;

public class SparkFactory : ProjectileFactory
{
  public SparkFactory(GameObject attachPoint, float cooldown) : base(attachPoint, cooldown) { }

  public override GameObject CreateProjectile()
  {
    var obj = PrefabsManager.Instance.Spawn(PrefabType.SparkProjectile);

    if (obj == null)
    {
      Debug.LogError("Failed to spawn SparkProjectile.");
      return null;
    }

    return obj;
  }
}