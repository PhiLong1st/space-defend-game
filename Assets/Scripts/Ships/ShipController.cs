using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
  [SerializeField] private Ship _ship;
  [SerializeField] private ShipView _shipView;
  [SerializeField] private SkillController[] _skills;
  [SerializeField] private SliderBarPresenter _healthBar;
  [SerializeField] private SliderBarPresenter _staminaBar;
  [SerializeField] private GameObject _gun;

  public int CurrentHealth => _ship.CurrentHealth;
  public int CurrentStamina => _ship.CurrentStamina;

  public Action<int, int> OnHealthChange;

  public Action<int, int> OnStaminaChange;

  private Transform _transform;

  private void Awake()
  {
    _transform = GetComponent<Transform>();
    OnHealthChange += _healthBar.HandleOnValueChanged;
    OnStaminaChange += _staminaBar.HandleOnValueChanged;
  }

  private void Start()
  {
    _ship.SetCurrentHealth(_ship.MaxHealth);
    _ship.SetCurrentStamina(_ship.MaxStamina);

    OnHealthChange?.Invoke(_ship.CurrentHealth, _ship.MaxHealth);
    OnStaminaChange?.Invoke(_ship.CurrentStamina, _ship.MaxStamina);
  }

  private void Update()
  {
    HandleMovement();
    HandleRotateGun();
    HandleShoot();
  }

  private void OnDestroy()
  {
    OnHealthChange -= _healthBar.HandleOnValueChanged;
    OnStaminaChange -= _staminaBar.HandleOnValueChanged;
  }

  public void HandleMovement()
  {
    if (Input.GetKey(KeyCode.W))
    {
      Vector2 newPosition = _transform.position + _transform.up * _ship.MovementSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
      // _shipView.MoveForward();
    }

    if (Input.GetKey(KeyCode.S))
    {
      Vector2 newPosition = _transform.position - _transform.up * _ship.MovementSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
      // _shipView.MoveBackward();
    }

    if (Input.GetKey(KeyCode.D))
    {
      Vector2 newPosition = _transform.position + _transform.right * _ship.MovementSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
      // _shipView.RotateLeft();
    }

    if (Input.GetKey(KeyCode.A))
    {
      Vector2 newPosition = _transform.position - _transform.right * _ship.MovementSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
      // _shipView.RotateRight();
    }
  }

  public void HandleRotateGun()
  {
    Vector3 mousePosition = Input.mousePosition;
    Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    Vector2 direction = (worldMousePosition - _gun.transform.position).normalized;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
    _gun.transform.rotation = Quaternion.Euler(0, 0, angle);
  }

  public void HandleShoot()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Instantiate(_ship.NormalProjectilePrefab, _gun.transform.position, _gun.transform.rotation);
      Debug.Log("Shoot!");
    }
  }

  public void TakeDamage(int amount)
  {
    int newHealth = Mathf.Max(_ship.CurrentHealth - amount, 0);
    _ship.SetCurrentHealth(newHealth);
    OnHealthChange?.Invoke(_ship.CurrentHealth, _ship.MaxHealth);
  }

  public void UseStamina(int amount)
  {
    int newStamina = Mathf.Max(_ship.CurrentStamina - amount, 0);
    _ship.SetCurrentStamina(newStamina);
    OnStaminaChange?.Invoke(_ship.CurrentStamina, _ship.MaxStamina);
  }

  public void RestoreHealth(int amount)
  {
    int newHealth = Mathf.Min(_ship.CurrentHealth + amount, _ship.MaxHealth);
    _ship.SetCurrentHealth(newHealth);
    OnHealthChange?.Invoke(_ship.CurrentHealth, _ship.MaxHealth);
  }

  public void RestoreStamina(int amount)
  {
    int newStamina = Mathf.Min(_ship.CurrentStamina + amount, _ship.MaxStamina);
    _ship.SetCurrentStamina(newStamina);
    OnStaminaChange?.Invoke(_ship.CurrentStamina, _ship.MaxStamina);
  }
}