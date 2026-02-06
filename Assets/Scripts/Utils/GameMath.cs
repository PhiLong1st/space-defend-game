using UnityEngine;


/// <summary>
/// Utility class for common game math operations.
/// </summary>
public static class GameMath
{
  /// <summary>
  /// Calculate percentage between min and max.
  /// </summary>
  public static float GetPercentage(float current, float max)
  {
    return max > 0 ? Mathf.Clamp01(current / max) : 0f;
  }

  /// <summary>
  /// Remap a value from one range to another.
  /// </summary>
  public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
  {
    float normalized = (value - fromMin) / (fromMax - fromMin);
    return Mathf.Lerp(toMin, toMax, normalized);
  }

  /// <summary>
  /// Check if position is within bounds.
  /// </summary>
  public static bool IsInBounds(Vector3 position, Bounds bounds)
  {
    return bounds.Contains(position);
  }

  /// <summary>
  /// Get direction from one point to another (2D).
  /// </summary>
  public static Vector2 GetDirection2D(Vector3 from, Vector3 to)
  {
    Vector2 direction = new Vector2(to.x - from.x, to.y - from.y);
    return direction.normalized;
  }

  /// <summary>
  /// Calculate angle to target (2D).
  /// </summary>
  public static float GetAngleToTarget(Vector3 from, Vector3 to)
  {
    Vector2 direction = GetDirection2D(from, to);
    return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
  }
}