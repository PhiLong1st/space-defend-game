using UnityEngine;
/// <summary>
/// Controls ship logic: targeting, shooting, upgrades.
/// Owns ShipModel (data) and commands ShipView (visuals).
/// MonoBehaviour only for Unity lifecycle - logic lives here.
/// </summary>
public class ShipController : MonoBehaviour
{
  [Header("Configuration")]
  [SerializeField] private ShipConfig config;

  [Header("References")]
  [SerializeField] private ShipView view;

  // Data model
  private ShipModel model;

  // Systems (injected or accessed globally)
  private TargetingSystem targetingSystem;
  private DamageSystem damageSystem;

  // Runtime state
  private EnemyController currentTarget;
  private TargetingStrategy currentStrategy;

  public ShipModel Model => model;
  public ShipConfig Config => config;

  /// <summary>
  /// Initialize ship with configuration and systems.
  /// </summary>
  public void Initialize(
      ShipConfig shipConfig,
      TargetingSystem targetingSystem,
      DamageSystem damageSystem)
  {
    this.config = shipConfig;
    this.targetingSystem = targetingSystem;
    this.damageSystem = damageSystem;

    // Create model
    model = new ShipModel(GetInstanceID(), config.ShipId);
    currentStrategy = config.DefaultTargetingStrategy;

    // Initialize view
    if (view != null)
    {
      view.Initialize(this);
    }

    Debug.Log($"Ship initialized: {config.DisplayName}");
  }

  private void Update()
  {
    if (!model.IsActive)
      return;

    UpdateTargeting();
    UpdateShooting(Time.deltaTime);
  }

  private void UpdateTargeting()
  {
    // Find enemies (would get from enemy manager)
    var enemies = FindObjectsOfType<EnemyController>();
    var enemyList = new System.Collections.Generic.List<EnemyController>(enemies);

    // Use targeting system to find best target
    currentTarget = targetingSystem.FindTarget(
        transform.position,
        config.Range,
        enemyList,
        currentStrategy
    );

    // Update view
    if (view != null)
    {
      view.SetTarget(currentTarget?.transform);
    }
  }

  private void UpdateShooting(float deltaTime)
  {
    model.TimeSinceLastShot += deltaTime;

    float fireInterval = 1f / config.FireRate;

    if (model.TimeSinceLastShot >= fireInterval && currentTarget != null)
    {
      Shoot();
      model.TimeSinceLastShot = 0f;
    }
  }

  private void Shoot()
  {
    if (currentTarget == null || currentTarget.IsDead())
      return;

    // Deal damage via damage system
    damageSystem.DealDamage(this, currentTarget, config.Damage);

    // Trigger view shot animation/VFX
    if (view != null)
    {
      view.PlayShootEffect(currentTarget.transform.position);
    }

    Debug.Log($"{config.DisplayName} shot for {config.Damage} damage");
  }

  /// <summary>
  /// Change targeting strategy (player control or auto).
  /// </summary>
  public void SetTargetingStrategy(TargetingStrategy strategy)
  {
    currentStrategy = strategy;
    Debug.Log($"Targeting strategy changed to: {strategy}");
  }

  /// <summary>
  /// Upgrade ship to next level.
  /// Returns true if successful.
  /// </summary>
  public bool Upgrade()
  {
    if (config.IsMaxLevel())
    {
      Debug.LogWarning("Ship already at max level");
      return false;
    }

    // Replace config with upgraded version
    config = config.UpgradeTo;
    model.CurrentLevel++;

    // Update view
    if (view != null)
    {
      view.OnUpgrade(model.CurrentLevel);
    }

    Debug.Log($"Ship upgraded to level {model.CurrentLevel}");
    return true;
  }

  /// <summary>
  /// Remove this ship (sell/destroy).
  /// </summary>
  public void Remove()
  {
    model.IsActive = false;

    if (view != null)
    {
      view.OnRemoved();
    }

    Destroy(gameObject);
  }

  public float GetRange() => config.Range;
}