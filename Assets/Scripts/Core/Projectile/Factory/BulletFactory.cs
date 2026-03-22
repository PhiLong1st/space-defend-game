// using UnityEngine;

// public class BulletFactory : ProjectileFactory
// {
//   public BulletFactory(GameObject attachPoint, float cooldown) : base(attachPoint, cooldown) { }

//   public override GameObject CreateProjectile()
//   {
//     var obj = PrefabsManager.Instance.Spawn(PrefabType.SparkProjectile);

//     if (obj == null)
//     {
//       Debug.LogError("Failed to spawn BulletProjectile.");
//       return null;
//     }

//     return obj;
//   }
// }