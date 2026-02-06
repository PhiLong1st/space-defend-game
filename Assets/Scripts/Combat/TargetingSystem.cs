using UnityEngine;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Provides targeting logic for ships.
/// Implements various targeting strategies as Strategy pattern.
/// </summary>
public class TargetingSystem
{
  private Dictionary<TargetingStrategy, ITargetingStrategy> strategies = new();

  public TargetingSystem()
  {
    // Register all targeting strategies
    strategies[TargetingStrategy.Closest] = new ClosestTargetingStrategy();
    strategies[TargetingStrategy.Strongest] = new StrongestTargetingStrategy();
    strategies[TargetingStrategy.Weakest] = new WeakestTargetingStrategy();
    strategies[TargetingStrategy.First] = new FirstTargetingStrategy();
    strategies[TargetingStrategy.Priority] = new PriorityTargetingStrategy();
  }

  /// <summary>
  /// Find best target for a ship using specified strategy.
  /// Returns null if no valid targets in range.
  /// </summary>
  public EnemyController FindTarget(
      Vector3 shipPosition,
      float range,
      List<EnemyController> availableEnemies,
      TargetingStrategy strategy)
  {
    if (availableEnemies == null || availableEnemies.Count == 0)
      return null;

    // Filter enemies in range
    var enemiesInRange = availableEnemies
        .Where(e => e != null && !e.IsDead())
        .Where(e => Vector3.Distance(shipPosition, e.transform.position) <= range)
        .ToList();

    if (enemiesInRange.Count == 0)
      return null;

    // Apply strategy
    if (strategies.ContainsKey(strategy))
    {
      return strategies[strategy].SelectTarget(shipPosition, enemiesInRange);
    }

    return null;
  }
}

/// <summary>
/// Interface for targeting strategy implementations.
/// </summary>
public interface ITargetingStrategy
{
  EnemyController SelectTarget(Vector3 shipPosition, List<EnemyController> enemies);
}

// ===== TARGETING STRATEGY IMPLEMENTATIONS =====

public class ClosestTargetingStrategy : ITargetingStrategy
{
  public EnemyController SelectTarget(Vector3 shipPosition, List<EnemyController> enemies)
  {
    return enemies
        .OrderBy(e => Vector3.Distance(shipPosition, e.transform.position))
        .FirstOrDefault();
  }
}

public class StrongestTargetingStrategy : ITargetingStrategy
{
  public EnemyController SelectTarget(Vector3 shipPosition, List<EnemyController> enemies)
  {
    return enemies
        .OrderByDescending(e => e.GetCurrentHealth())
        .FirstOrDefault();
  }
}

public class WeakestTargetingStrategy : ITargetingStrategy
{
  public EnemyController SelectTarget(Vector3 shipPosition, List<EnemyController> enemies)
  {
    return enemies
        .OrderBy(e => e.GetCurrentHealth())
        .FirstOrDefault();
  }
}

public class FirstTargetingStrategy : ITargetingStrategy
{
  public EnemyController SelectTarget(Vector3 shipPosition, List<EnemyController> enemies)
  {
    // Target enemy furthest along path (highest path progress)
    return enemies
        .OrderByDescending(e => e.GetPathProgress())
        .FirstOrDefault();
  }
}

public class PriorityTargetingStrategy : ITargetingStrategy
{
  public EnemyController SelectTarget(Vector3 shipPosition, List<EnemyController> enemies)
  {
    // Target based on enemy priority value
    return enemies
        .OrderByDescending(e => e.GetPriority())
        .FirstOrDefault();
  }
}