using UnityEngine;
/// <summary>
/// Controls enemy logic: FSM, movement, health.
/// Owns EnemyModel (data) and commands EnemyView (visuals).
/// </summary>
public class EnemyController : MonoBehaviour
{
  [Header("Configuration")]
  [SerializeField] private EnemyConfig config;

  [Header("References")]
  [SerializeField] private EnemyView view;

  // Data model
  private EnemyModel model;

  // Systems
  private EventBus eventBus;
  private ConvoySystem convoySystem;

  // Runtime
  private Vector3[] pathWaypoints;
  private int currentWaypointIndex = 0;

  public EnemyModel Model => model;
  public EnemyConfig Config => config;

  /// <summary>
  /// Initialize enemy with configuration and path.
  /// </summary>
  public void Initialize(
      EnemyConfig enemyConfig,
      Vector3[] path,
      EventBus eventBus,
      ConvoySystem convoySystem)
  {
    this.config = enemyConfig;
    this.pathWaypoints = path;
    this.eventBus = eventBus;
    this.convoySystem = convoySystem;

    // Create model
    model = new EnemyModel(GetInstanceID(), config.EnemyId, config.MaxHealth);

    // Initialize view
    if (view != null)
    {
      view.Initialize(this);
    }

    Debug.Log($"Enemy initialized: {config.DisplayName}");
  }

  private void Update()
  {
    if (model.IsDead())
      return;

    // Simple FSM
    switch (model.CurrentState)
    {
      case EnemyState.Moving:
        UpdateMovement(Time.deltaTime);
        break;

      case EnemyState.Attacking:
        UpdateAttacking(Time.deltaTime);
        break;
    }
  }

  private void UpdateMovement(float deltaTime)
  {
    if (pathWaypoints == null || pathWaypoints.Length == 0)
      return;

    // Move toward current waypoint
    Vector3 targetWaypoint = pathWaypoints[currentWaypointIndex];
    Vector3 direction = (targetWaypoint - transform.position).normalized;

    transform.position += direction * config.MoveSpeed * deltaTime;

    // Check if reached waypoint
    if (Vector3.Distance(transform.position, targetWaypoint) < 0.1f)
    {
      currentWaypointIndex++;

      // Update path progress
      model.PathProgress = (float)currentWaypointIndex / pathWaypoints.Length;

      // Check if reached end
      if (currentWaypointIndex >= pathWaypoints.Length)
      {
        ReachConvoy();
      }
    }

    // Update view
    if (view != null)
    {
      view.UpdateMovement(direction);
    }
  }

  private void UpdateAttacking(float deltaTime)
  {
    // Attack convoy (if implemented)
    // For now, this is handled in ReachConvoy
  }

  private void ReachConvoy()
  {
    model.CurrentState = EnemyState.Attacking;

    // Damage convoy
    if (convoySystem != null)
    {
      convoySystem.TakeDamage(config.ConvoyDamage);
      Debug.Log($"{config.DisplayName} damaged convoy for {config.ConvoyDamage}");
    }

    // Enemy is consumed
    Die(false);
  }

  /// <summary>
  /// Apply damage to this enemy.
  /// </summary>
  public void TakeDamage(float damage)
  {
    if (model.IsDead())
      return;

    // Calculate actual damage
    float actualDamage = config.CalculateDamageTaken(damage);

    model.CurrentHealth -= actualDamage;

    // Update view
    if (view != null)
    {
      view.OnDamaged(model.GetHealthPercentage());
    }

    Debug.Log($"{config.DisplayName} took {actualDamage} damage ({model.CurrentHealth}/{model.MaxHealth})");

    // Check death
    if (model.CurrentHealth <= 0)
    {
      Die(true);
    }
  }

  private void Die(bool wasKilled)
  {
    model.CurrentState = EnemyState.Dead;

    // Award gold if killed by player
    if (wasKilled)
    {
      eventBus?.Publish(new EnemyDefeatedEvent
      {
        EnemyId = model.InstanceId,
        GoldReward = config.GoldReward
      });

      // Economy system should listen to this event
    }

    // Update view
    if (view != null)
    {
      view.OnDeath();
    }

    // Cleanup after animation
    Destroy(gameObject, 1f);
  }

  // Public accessors for targeting system
  public bool IsDead() => model.IsDead();
  public float GetCurrentHealth() => model.CurrentHealth;
  public float GetPathProgress() => model.PathProgress;
  public int GetPriority() => config.TargetPriority;
  public float GetDamageReduction() => config.DamageReduction;
}