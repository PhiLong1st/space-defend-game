using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class LinearProjectileController : MonoBehaviour, IProjectileStrategy
{
  [SerializeField] private LinearProjectileConfig _config;
  [SerializeField] private readonly float _timeLaunch = 0.1f;
  [SerializeField] private readonly float _timeExplode = 0.3f;
  private LinearProjectile _model;
  private LinearProjectileView _view;
  private Collider2D _collider;

  private Vector2 _direction;

  #region Unity Callbacks
  private void Awake()
  {
    _model = new LinearProjectile(_config);
    _view = GetComponentInChildren<LinearProjectileView>();
    _collider = GetComponent<Collider2D>();
  }

  private void OnEnable()
  {
    _model.Reset();
    _view.Reset();

    EnableDetector();
  }

  private void Update()
  {
    if (!_model.IsLaunched) Launch();
  }

  private void OnDisable()
  {
    StopAllCoroutines();
  }
  #endregion

  #region Public Methods for Handling Projectile Actions
  public void Launch()
  {
    _view.PlayLaunchEffect();
    StartCoroutine(LaunchRoutine());
  }

  public void Flight()
  {
    if (!_model.IsLaunched) return;

    _view.PlayFlightEffect();
    StartCoroutine(FlyingRoutine());
  }

  public void Explode()
  {
    _model.Explode();

    DisableDetector();
    _view.PlayExplosionEffect();

    StartCoroutine(ExplodeRoutine());
  }

  public void SetDirection(Vector2 direction) => _direction = direction.normalized;
  #endregion

  #region Routines
  private IEnumerator LaunchRoutine()
  {
    yield return new WaitForSeconds(_timeLaunch);
    _model.Launch();
    Flight();
  }

  private IEnumerator FlyingRoutine()
  {
    while (_model.IsLaunched && !_model.HasExploded)
    {
      Vector2 direction = (_direction == Vector2.zero) ? Vector2.right : _direction;
      transform.Translate(direction * _model.Speed * Time.deltaTime);
      yield return null;
    }
  }

  private IEnumerator ExplodeRoutine()
  {
    yield return new WaitForSeconds(_timeExplode);
    gameObject.SetActive(false);
  }
  #endregion

  #region Physics
  private void EnableDetector() => _collider.enabled = true;

  private void DisableDetector() => _collider.enabled = false;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!_model.IsLaunched || _model.HasExploded) return;

    if (other.CompareTag("Obstacle"))
    {
      other.GetComponent<IDamageable>()?.TakeDamage(_model.Damage);
      Explode();
    }
  }
  #endregion
}