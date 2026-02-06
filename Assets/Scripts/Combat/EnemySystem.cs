using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Manages all enemies in the level: tracking, cleanup.
/// Acts as registry for enemy instances.
/// </summary>
public class EnemySystem
{
  private readonly EventBus eventBus;
  private readonly EconomySystem economySystem;
  private readonly ConvoySystem convoySystem;

  private Dictionary<int, EnemyController> activeEnemies = new();

  public EnemySystem(
      EventBus eventBus,
      EconomySystem economySystem,
      ConvoySystem convoySystem)
  {
    this.eventBus = eventBus;
    this.economySystem = economySystem;
    this.convoySystem = convoySystem;

    // Subscribe to enemy defeated events to award gold
    eventBus.Subscribe<EnemyDefeatedEvent>(OnEnemyDefeated);
  }

  /// <summary>
  /// Register an enemy (called by spawn system).
  /// </summary>
  public void RegisterEnemy(EnemyController enemy)
  {
    if (enemy == null)
      return;

    int enemyId = enemy.GetInstanceID();
    activeEnemies[enemyId] = enemy;

    Debug.Log($"Enemy registered: {enemy.Config.DisplayName}");
  }

  /// <summary>
  /// Unregister an enemy (called when destroyed).
  /// </summary>
  public void UnregisterEnemy(int enemyId)
  {
    if (activeEnemies.ContainsKey(enemyId))
    {
      activeEnemies.Remove(enemyId);
    }
  }

  /// <summary>
  /// Get all active enemies.
  /// </summary>
  public List<EnemyController> GetAllEnemies()
  {
    return new List<EnemyController>(activeEnemies.Values);
  }

  /// <summary>
  /// Get count of alive enemies.
  /// </summary>
  public int GetAliveEnemyCount()
  {
    int count = 0;
    foreach (var enemy in activeEnemies.Values)
    {
      if (enemy != null && !enemy.IsDead())
      {
        count++;
      }
    }
    return count;
  }

  private void OnEnemyDefeated(EnemyDefeatedEvent evt)
  {
    // Award gold to player
    economySystem.AddGold(evt.GoldReward);

    // Unregister enemy
    UnregisterEnemy(evt.EnemyId);
  }

  public void Reset()
  {
    // Cleanup all enemies
    foreach (var enemy in activeEnemies.Values)
    {
      if (enemy != null)
      {
        Object.Destroy(enemy.gameObject);
      }
    }

    activeEnemies.Clear();
  }

  public void Cleanup()
  {
    eventBus.Unsubscribe<EnemyDefeatedEvent>(OnEnemyDefeated);
  }
}