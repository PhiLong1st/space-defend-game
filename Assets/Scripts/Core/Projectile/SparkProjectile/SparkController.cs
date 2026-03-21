using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class SparkController : MonoBehaviour, IProjectileStrategy
{
  [SerializeField] private ProjectileConfig _config;
  [SerializeField] private readonly float _timeLaunch = 0.1f;
  [SerializeField] private readonly float _timeExplode = 0.3f;
  private Spark _model;
  private SparkView _view;
  private Collider2D _collider;

  #region Unity Callbacks
  private void Awake()
  {
    _model = new Spark(_config);
    _view = GetComponentInChildren<SparkView>();
    _collider = GetComponent<Collider2D>();
  }

  private void OnEnable()
  {
    _model.Reset();
    _view.Reset();

    EnableDetector();
    Launch();
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
      transform.Translate(Vector2.right * _model.Speed * Time.deltaTime);
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