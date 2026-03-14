using System.Collections.Generic;
using UnityEngine;

public class ProjectileSystem
{
  private List<(GameObject, AbstractProjectile)> _projectiles = new List<(GameObject, AbstractProjectile)>();

  public void HandleAttack()
  {
    foreach (var (gameObject, projectile) in _projectiles)
    {
      if (projectile.CanAttack())
      {
        // var prefabInstance =  Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        // prefabInstance.StartAttack();
        // projectile.Attack();
      }
    }
  }

  public void AddProjectile(GameObject attackPoint, AbstractProjectile projectile)
  {
    _projectiles.Add((attackPoint, projectile));
  }
}