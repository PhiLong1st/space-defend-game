using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
  public static SpaceshipController Instance { get; private set; }
  [SerializeField] private Spaceship _spaceship;
  [SerializeField] private GameObject _projectileGO;
  [SerializeField] private GameObject _gun;

  private SpaceshipView _spaceshipView;

  private Transform _transform;

  #region Stat System
  public float CurrentMovementSpeed => _spaceship.CurrentMovementSpeed * _spaceship.CurrentBoostSpeed;
  public float CurrentBoostSpeed => _spaceship.CurrentBoostSpeed;
  public float CurrentShield => _spaceship.CurrentShield;
  public float CurrentHealth => _spaceship.CurrentHealth;
  public float CurrentStamina => _spaceship.CurrentStamina;
  public float MaxHealth => _spaceship.MaxHealth;
  public float MaxStamina => _spaceship.MaxStamina;
  public float MaxShield => _spaceship.MaxShield;
  #endregion

  private void Awake()
  {
    _transform = GetComponent<Transform>();
    _spaceshipView = GetComponent<SpaceshipView>();

    if (Instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }
  }

  void Update()
  {
    HandleMovement();
    HandleShoot();
  }

  public void HandleShoot()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Instantiate(_projectileGO, _gun.transform.position, _gun.transform.rotation);
      Debug.Log("Shoot!");
    }
  }

  public void HandleMovement()
  {
    if (Input.GetKey(KeyCode.W))
    {
      Vector2 newPosition = _transform.position + _transform.up * CurrentMovementSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
    }

    if (Input.GetKey(KeyCode.S))
    {
      Vector2 newPosition = _transform.position - _transform.up * CurrentMovementSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
    }

    if (Input.GetKey(KeyCode.D))
    {
      Vector2 newPosition = _transform.position + _transform.right * CurrentMovementSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
    }

    if (Input.GetKey(KeyCode.A))
    {
      Vector2 newPosition = _transform.position - _transform.right * CurrentMovementSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
    }

    // Boosting
    if (Input.GetKeyDown(KeyCode.E))
    {
      EnterBoost();
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
      StopBoost();
    }
  }

  public void EnterBoost()
  {
    _spaceship.ApplyBoostMultiplier(2);
    _spaceshipView.EnterBoost();
  }

  public void StopBoost()
  {
    _spaceship.ApplyBoostMultiplier(0.5f);
    _spaceshipView.ExitBoost();
  }

  public void TakeDamage(int damage)
  {
    _spaceship.TakeDamage(damage);
    _spaceshipView.UpdateHealthBar(CurrentHealth / MaxHealth);
    _spaceshipView.PlayDamageAnimation();
    Debug.Log($"Player took {damage} damage. Current health: {CurrentHealth}/{MaxHealth}");
  }
}
