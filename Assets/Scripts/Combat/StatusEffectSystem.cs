using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Manages status effects (buffs/debuffs) on entities.
/// Handles application, duration, and expiration of effects.
/// </summary>
public class StatusEffectSystem
{
  private Dictionary<int, List<StatusEffect>> activeEffects = new();

  /// <summary>
  /// Apply a status effect to an entity.
  /// </summary>
  public void ApplyEffect(int entityId, StatusEffectType type, float duration, float magnitude)
  {
    if (!activeEffects.ContainsKey(entityId))
    {
      activeEffects[entityId] = new List<StatusEffect>();
    }

    var effect = new StatusEffect
    {
      Type = type,
      RemainingDuration = duration,
      Magnitude = magnitude
    };

    activeEffects[entityId].Add(effect);
    Debug.Log($"Applied {type} to entity {entityId} for {duration}s");
  }

  /// <summary>
  /// Update all active effects (call from Update loop).
  /// </summary>
  public void UpdateEffects(float deltaTime)
  {
    foreach (var kvp in activeEffects)
    {
      var effects = kvp.Value;

      // Update durations
      for (int i = effects.Count - 1; i >= 0; i--)
      {
        effects[i].RemainingDuration -= deltaTime;

        if (effects[i].RemainingDuration <= 0)
        {
          // Effect expired
          effects.RemoveAt(i);
        }
      }
    }
  }

  /// <summary>
  /// Get total modifier for a specific effect type on an entity.
  /// </summary>
  public float GetEffectModifier(int entityId, StatusEffectType type)
  {
    if (!activeEffects.ContainsKey(entityId))
      return 0f;

    float totalModifier = 0f;

    foreach (var effect in activeEffects[entityId])
    {
      if (effect.Type == type)
      {
        totalModifier += effect.Magnitude;
      }
    }

    return totalModifier;
  }

  /// <summary>
  /// Check if entity has a specific effect active.
  /// </summary>
  public bool HasEffect(int entityId, StatusEffectType type)
  {
    if (!activeEffects.ContainsKey(entityId))
      return false;

    return activeEffects[entityId].Exists(e => e.Type == type);
  }

  /// <summary>
  /// Remove all effects from an entity.
  /// </summary>
  public void ClearEffects(int entityId)
  {
    if (activeEffects.ContainsKey(entityId))
    {
      activeEffects[entityId].Clear();
    }
  }

  public void Reset()
  {
    activeEffects.Clear();
  }
}

/// <summary>
/// Data structure for a single status effect instance.
/// </summary>
public class StatusEffect
{
  public StatusEffectType Type;
  public float RemainingDuration;
  public float Magnitude; // Effect strength (e.g., slow %, damage over time)
}

/// <summary>
/// Available status effect types.
/// </summary>
public enum StatusEffectType
{
  Slow,           // Reduces movement speed
  Stun,           // Prevents movement
  Burn,           // Damage over time
  Poison,         // Damage over time (different from burn)
  SpeedBoost,     // Increases movement speed
  DamageBoost,    // Increases damage dealt
  Shield          // Absorbs damage
}