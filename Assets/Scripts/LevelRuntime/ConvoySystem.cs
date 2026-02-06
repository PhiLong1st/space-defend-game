using UnityEngine;
/// <summary>
/// Manages the convoy (objectives that need protection).
/// Tracks convoy health, position, and triggers failure if destroyed.
/// </summary>
public class ConvoySystem
{
  private readonly EventBus eventBus;

  private int currentHealth;
  private int maxHealth;
  private bool isDestroyed = false;

  public int CurrentHealth => currentHealth;
  public int MaxHealth => maxHealth;
  public bool IsDestroyed => isDestroyed;

  public ConvoySystem(EventBus eventBus, int maxHealth = 100)
  {
    this.eventBus = eventBus;
    this.maxHealth = maxHealth;
    this.currentHealth = maxHealth;
  }

  /// <summary>
  /// Damage the convoy. If health reaches zero, triggers destruction.
  /// </summary>
  public void TakeDamage(int damage)
  {
    if (isDestroyed)
      return;

    currentHealth = Mathf.Max(0, currentHealth - damage);

    Debug.Log($"Convoy damaged: -{damage} HP (Remaining: {currentHealth}/{maxHealth})");

    if (currentHealth <= 0)
    {
      DestroyConvoy();
    }
  }

  /// <summary>
  /// Heal the convoy (for repair mechanics).
  /// </summary>
  public void Heal(int amount)
  {
    if (isDestroyed)
      return;

    currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    Debug.Log($"Convoy repaired: +{amount} HP (Current: {currentHealth}/{maxHealth})");
  }

  private void DestroyConvoy()
  {
    isDestroyed = true;

    // This would trigger core failure condition
    Debug.LogError("CONVOY DESTROYED - GAME OVER");

    eventBus.Publish(new ConditionViolatedEvent
    {
      ConditionId = "convoy_destroyed",
      PunishmentType = "Fail"
    });
  }

  public void Reset(int newMaxHealth = 100)
  {
    maxHealth = newMaxHealth;
    currentHealth = maxHealth;
    isDestroyed = false;
  }
}