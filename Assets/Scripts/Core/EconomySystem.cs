using UnityEngine;

/// <summary>
/// Manages player's economy (gold, resources).
/// Systems request transactions, this system validates and publishes changes.
/// </summary>
public class EconomySystem
{
  private int currentGold;
  private readonly EventBus eventBus;

  public int CurrentGold => currentGold;

  public EconomySystem(EventBus eventBus, int startingGold = 0)
  {
    this.eventBus = eventBus;
    this.currentGold = startingGold;
  }

  /// <summary>
  /// Add gold and emit event.
  /// </summary>
  public void AddGold(int amount)
  {
    if (amount <= 0)
      return;

    var previousAmount = currentGold;
    currentGold += amount;

    PublishGoldChanged(previousAmount);
    Debug.Log($"Gold added: +{amount} (Total: {currentGold})");
  }

  /// <summary>
  /// Attempt to spend gold. Returns true if successful.
  /// </summary>
  public bool TrySpendGold(int amount)
  {
    if (amount <= 0)
    {
      Debug.LogWarning("Cannot spend zero or negative gold");
      return false;
    }

    if (currentGold < amount)
    {
      Debug.LogWarning($"Insufficient gold: {currentGold}/{amount}");
      return false;
    }

    var previousAmount = currentGold;
    currentGold -= amount;

    PublishGoldChanged(previousAmount);
    Debug.Log($"Gold spent: -{amount} (Remaining: {currentGold})");
    return true;
  }

  /// <summary>
  /// Check if player can afford a cost without spending.
  /// </summary>
  public bool CanAfford(int cost)
  {
    return currentGold >= cost;
  }

  /// <summary>
  /// Set gold to specific amount (use for debugging or level start).
  /// </summary>
  public void SetGold(int amount)
  {
    var previousAmount = currentGold;
    currentGold = Mathf.Max(0, amount);
    PublishGoldChanged(previousAmount);
  }

  /// <summary>
  /// Reset economy to initial state.
  /// </summary>
  public void Reset(int startingGold = 0)
  {
    currentGold = startingGold;
  }

  private void PublishGoldChanged(int previousAmount)
  {
    eventBus.Publish(new GoldChangedEvent
    {
      PreviousAmount = previousAmount,
      NewAmount = currentGold
    });
  }
}
