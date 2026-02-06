/// <summary>
/// Defines the main game states for session flow control.
/// Each state represents a distinct phase with different system behaviors.
/// </summary>
public enum GameState
{
  Boot,           // Initial loading and system initialization
  Preparation,    // Player can place/move ships before wave starts
  WaveActive,     // Enemies spawning and combat ongoing
  WaveEnd,        // Wave completed, transitioning to next phase
  UpgradePhase,   // Player can upgrade ships/abilities between waves
  Fail            // Loss condition met, session ends
}
