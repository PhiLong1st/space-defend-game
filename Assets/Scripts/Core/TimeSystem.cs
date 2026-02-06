using UnityEngine;

/// <summary>
/// Controls game time flow: pause, slow-motion, and time scaling.
/// Used for tactical pause or special effects.
/// </summary>
public class TimeSystem
{
  private float currentTimeScale = 1f;
  private bool isPaused = false;

  public float CurrentTimeScale => currentTimeScale;
  public bool IsPaused => isPaused;

  /// <summary>
  /// Delta time adjusted for current time scale (but not pause).
  /// Use this instead of Time.deltaTime for gameplay logic.
  /// </summary>
  public float DeltaTime => isPaused ? 0f : Time.deltaTime * currentTimeScale;

  /// <summary>
  /// Pause game time completely.
  /// </summary>
  public void Pause()
  {
    if (isPaused)
      return;

    isPaused = true;
    Time.timeScale = 0f;
    Debug.Log("Game paused");
  }

  /// <summary>
  /// Resume game time.
  /// </summary>
  public void Resume()
  {
    if (!isPaused)
      return;

    isPaused = false;
    Time.timeScale = currentTimeScale;
    Debug.Log("Game resumed");
  }

  /// <summary>
  /// Set time scale (0.5 = half speed, 2.0 = double speed).
  /// Does not override pause state.
  /// </summary>
  public void SetTimeScale(float scale)
  {
    currentTimeScale = Mathf.Clamp(scale, 0f, 4f);

    if (!isPaused)
    {
      Time.timeScale = currentTimeScale;
    }

    Debug.Log($"Time scale set to {currentTimeScale}x");
  }

  /// <summary>
  /// Reset to normal time flow.
  /// </summary>
  public void Reset()
  {
    isPaused = false;
    currentTimeScale = 1f;
    Time.timeScale = 1f;
  }
}
