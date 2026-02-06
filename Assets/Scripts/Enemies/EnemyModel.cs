
/// <summary>
/// Pure data model for an enemy instance.
/// No logic, just state. Can be serialized.
/// </summary>
public class EnemyModel
{
  // Identity
  public int InstanceId;
  public string EnemyTypeId;

  // Health
  public float CurrentHealth;
  public float MaxHealth;

  // Movement
  public float PathProgress = 0f; // 0-1 along path
  public int PathIndex = 0;

  // State
  public EnemyState CurrentState = EnemyState.Moving;

  public EnemyModel(int instanceId, string enemyTypeId, float maxHealth)
  {
    InstanceId = instanceId;
    EnemyTypeId = enemyTypeId;
    MaxHealth = maxHealth;
    CurrentHealth = maxHealth;
  }

  public bool IsDead()
  {
    return CurrentState == EnemyState.Dead || CurrentHealth <= 0f;
  }

  public float GetHealthPercentage()
  {
    return MaxHealth > 0 ? CurrentHealth / MaxHealth : 0f;
  }
}