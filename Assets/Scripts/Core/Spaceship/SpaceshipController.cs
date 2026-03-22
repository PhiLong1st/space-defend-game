using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SpaceshipController : MonoBehaviour, ISubject<Spaceship>, IDamageable
{
  public static SpaceshipController Instance { get; private set; }

  [SerializeField] private SpaceshipConfig _config;
  [SerializeField] private GameObject _attachPoint;

  private SpaceshipView _view;
  private ProjectileSystem _projectileSystem;
  private Spaceship _model;

  private readonly List<IObserver<Spaceship>> _observers = new();

  public int CurrentLevel => _model.CurrentLevel;

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }

    _view = GetComponentInChildren<SpaceshipView>();
  }

  private void Start()
  {
    _model = new Spaceship(_config);
    NotifyObservers(_model);

    _projectileSystem = new ProjectileSystem();
    var sparkFactory = new SparkFactory(_attachPoint, 0.5f);
    _projectileSystem.AddProjectile(sparkFactory);

    _view.OnExplosionFinished += GameManager.Instance.GameOver;
    _view.UpdateShipView(_model.CurrentLevel);
  }

  private void Update()
  {
    if (_model.IsDestroyed) return;

    _projectileSystem.Update();

    if (Input.GetKey(KeyCode.W)) HandleMove(Vector2.up);
    if (Input.GetKey(KeyCode.A)) HandleMove(Vector2.left);
    if (Input.GetKey(KeyCode.S)) HandleMove(Vector2.down);
    if (Input.GetKey(KeyCode.D)) HandleMove(Vector2.right);

    //Test level up 
    if (Input.GetKeyDown(KeyCode.Space)) HandleLevelUp();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Obstacle"))
    {
      HandleExplosion();
    }
  }

  #region Public Methods for Handling Spaceship Actions
  public void HandleHeal(int amount)
  {
    _model.Heal(amount);
    NotifyObservers(_model);
  }

  public void TakeDamage(int amount)
  {
    _model.TakeDamage(amount);
    NotifyObservers(_model);

    if (_model.IsDestroyed) HandleExplosion();
  }

  public void HandleUseStamina(int amount)
  {
    bool canUse = _model.UseStamina(amount);
    if (!canUse) return;
    NotifyObservers(_model);
  }

  public void HandleRecoverStamina(int amount)
  {
    _model.RecoverStamina(amount);
    NotifyObservers(_model);
  }

  public void HandleLevelUp()
  {
    if (!_model.CanLevelUp()) return;

    _model.LevelUp();
    _view.UpdateShipView(_model.CurrentLevel);
    NotifyObservers(_model);
  }

  public void HandleMove(Vector2 direction)
  {
    transform.Translate(direction * Time.deltaTime * _model.Speed);
  }

  public void HandleExplosion()
  {
    _view.PlayExplosionEffect();
  }
  #endregion

  #region  Observer Pattern
  public void AddObserver(IObserver<Spaceship> observer) => _observers.Add(observer);

  public void RemoveObserver(IObserver<Spaceship> observer) => _observers.Remove(observer);

  public void NotifyObservers(Spaceship data)
  {
    foreach (var observer in _observers) observer.OnNotify(data);
  }
  #endregion
}
