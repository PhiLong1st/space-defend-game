using UnityEngine;

/// <summary>
/// Owns the current game state and controls state transitions.
/// Other systems react to state changes via events rather than querying this directly.
/// </summary>
public class GameStateSystem
{
  private GameState currentState = GameState.Boot;
  private readonly EventBus eventBus;

  public GameState CurrentState => currentState;

  public GameStateSystem(EventBus eventBus)
  {
    this.eventBus = eventBus;
  }

  /// <summary>
  /// Request a state transition. May be rejected if transition is invalid.
  /// </summary>
  public bool TransitionTo(GameState newState)
  {
    if (!IsValidTransition(currentState, newState))
    {
      Debug.LogWarning($"Invalid state transition: {currentState} -> {newState}");
      return false;
    }

    var previousState = currentState;
    currentState = newState;

    eventBus.Publish(new GameStateChangedEvent
    {
      PreviousState = previousState,
      NewState = newState
    });

    Debug.Log($"State transition: {previousState} -> {newState}");
    return true;
  }

  /// <summary>
  /// Defines valid state transitions to prevent invalid game flow.
  /// </summary>
  private bool IsValidTransition(GameState from, GameState to)
  {
    return (from, to) switch
    {
      (GameState.Boot, GameState.Preparation) => true,
      (GameState.Preparation, GameState.WaveActive) => true,
      (GameState.WaveActive, GameState.WaveEnd) => true,
      (GameState.WaveActive, GameState.Fail) => true,
      (GameState.WaveEnd, GameState.UpgradePhase) => true,
      (GameState.WaveEnd, GameState.Fail) => true,
      (GameState.UpgradePhase, GameState.Preparation) => true,
      (GameState.UpgradePhase, GameState.WaveActive) => true,
      _ => false
    };
  }

  /// <summary>
  /// Reset to initial state for new game session.
  /// </summary>
  public void Reset()
  {
    currentState = GameState.Boot;
  }
}
