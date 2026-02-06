using System;
using System.Collections.Generic;

/// <summary>
/// Global event bus for decoupled system communication.
/// Systems publish events, other systems subscribe without direct references.
/// </summary>
public class EventBus
{
  private static EventBus instance;
  public static EventBus Instance => instance ??= new EventBus();

  private readonly Dictionary<Type, List<Delegate>> eventSubscribers = new();

  /// <summary>
  /// Subscribe to an event type. Remember to Unsubscribe to prevent memory leaks.
  /// </summary>
  public void Subscribe<T>(Action<T> handler) where T : struct
  {
    var eventType = typeof(T);

    if (!eventSubscribers.ContainsKey(eventType))
    {
      eventSubscribers[eventType] = new List<Delegate>();
    }

    eventSubscribers[eventType].Add(handler);
  }

  /// <summary>
  /// Unsubscribe from an event type.
  /// </summary>
  public void Unsubscribe<T>(Action<T> handler) where T : struct
  {
    var eventType = typeof(T);

    if (eventSubscribers.ContainsKey(eventType))
    {
      eventSubscribers[eventType].Remove(handler);
    }
  }

  /// <summary>
  /// Publish an event to all subscribers.
  /// </summary>
  public void Publish<T>(T eventData) where T : struct
  {
    var eventType = typeof(T);

    if (!eventSubscribers.ContainsKey(eventType))
      return;

    foreach (var subscriber in eventSubscribers[eventType])
    {
      (subscriber as Action<T>)?.Invoke(eventData);
    }
  }

  /// <summary>
  /// Clear all subscriptions. Use when restarting level or cleaning up systems.
  /// </summary>
  public void Clear()
  {
    eventSubscribers.Clear();
  }
}

// ===== GAME EVENTS =====
// Define event structs here for documentation and type safety

public struct GameStateChangedEvent
{
  public GameState PreviousState;
  public GameState NewState;
}

public struct WaveStartedEvent
{
  public int WaveIndex;
}

public struct WaveCompletedEvent
{
  public int WaveIndex;
}

public struct EnemySpawnedEvent
{
  public int EnemyId;
}

public struct EnemyDefeatedEvent
{
  public int EnemyId;
  public int GoldReward;
}

public struct ShipPlacedEvent
{
  public int ShipId;
}

public struct ShipRemovedEvent
{
  public int ShipId;
}

public struct GoldChangedEvent
{
  public int PreviousAmount;
  public int NewAmount;
}

public struct ConditionViolatedEvent
{
  public string ConditionId;
  public string PunishmentType;
}

public struct GameOverEvent
{
  public bool Victory;
  public string Reason;
}
