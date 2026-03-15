public static class GameData
{
  // Core game flow
  public const float InitialWorldSpeed = 1f;
  public const float InitialScore = 0f;
  public const float InitialScoreMilestone = 200f;
  public const float ScorePerSecond = 10f;
  public const float WorldSpeedIncrement = 0.5f;
  public const float MaxWorldSpeed = 2f;
  public const float ScoreMilestoneMultiplier = 2f;
  public const float MaxScoreMilestone = 10000f;

  // Time scale
  public const float NormalTimeScale = 1f;
  public const float PausedTimeScale = 0f;

  // Trap defaults
  public const int DefaultTrapDamage = 1;
  public const int DefaultTrapLives = 1;
  public const float DefaultTrapMaxSpeed = 2f;

  // Meteor behavior
  public const float MeteorPushXMin = -1f;
  public const float MeteorPushXMax = 0f;
  public const float MeteorScaleMin = 0.6f;
  public const float MeteorScaleMax = 1.2f;

  // Kamikaze behavior
  public const float KamikazeTimeToTarget = 4f;
  public const float KamikazeTargetSpeed = 3f;


  // Background movement
  public const float ParallaxWorldSpeedMultiplier = 2f;

  // Pooling
  public const int DefaultPoolSize = 5;
  public const int DefaultPrefabPoolSize = 20;
}
