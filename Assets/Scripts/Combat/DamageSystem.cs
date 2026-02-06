using UnityEngine;
/// <summary>
/// Handles damage calculations and application.
/// Centralizes all damage logic for consistency.
/// </summary>
public class DamageSystem
{
  private readonly EventBus eventBus;

  public DamageSystem(EventBus eventBus)
  {
    this.eventBus = eventBus;
  }

  /// <summary>
  /// Apply damage from ship to enemy.
  /// Handles resistances, critical hits, etc.
  /// </summary>
  public void DealDamage(
      ShipController source,
      EnemyController target,
      float baseDamage)
  {
    if (target == null || target.IsDead())
      return;

    // Calculate final damage (could add modifiers here)
    float finalDamage = CalculateDamage(baseDamage, target);

    // Apply damage to enemy
    target.TakeDamage(finalDamage);

    // Could emit damage event for VFX/feedback
    Debug.Log($"Ship dealt {finalDamage} damage to {target.name}");
  }

  /// <summary>
  /// Calculate final damage after resistances and modifiers.
  /// </summary>
  private float CalculateDamage(float baseDamage, EnemyController target)
  {
    // Get enemy resistances
    float damageReduction = target.GetDamageReduction();

    // Apply resistance
    float finalDamage = baseDamage * (1f - damageReduction);

    // Future: add critical hits, damage types, etc.

    return finalDamage;
  }

  /// <summary>
  /// Apply area damage to multiple enemies.
  /// </summary>
  public void DealAreaDamage(
      Vector3 position,
      float radius,
      float damage,
      EnemyController[] allEnemies)
  {
    int hitCount = 0;

    foreach (var enemy in allEnemies)
    {
      if (enemy == null || enemy.IsDead())
        continue;

      float distance = Vector3.Distance(position, enemy.transform.position);
      if (distance <= radius)
      {
        // Could scale damage by distance
        float scaledDamage = damage * (1f - distance / radius);
        enemy.TakeDamage(scaledDamage);
        hitCount++;
      }
    }

    Debug.Log($"Area damage hit {hitCount} enemies");
  }
}