using UnityEngine;
using System.Collections.Generic;

public class Kamikaze : AbstractTouchTrap
{
  [Header("Timing & Speed")]
  [SerializeField] private float _timeToTarget = GameData.KamikazeTimeToTarget;
  [SerializeField] private float _targetSpeed = GameData.KamikazeTargetSpeed;

  [Header("References")]
  [SerializeField] private GameObject _warningEffectPrefab;

  public float TargetSpeed => _targetSpeed * GameManager.Instance.WorldSpeed;
  public float MaxSpeed => _maxSpeed * GameManager.Instance.WorldSpeed;

  private bool _isTargetDone = false;
  private float _targetTimer;
  private GameObject _spaceship;

  private void Update()
  {
    if (_spaceship == null) return;

    if (!_isTargetDone)
    {
      if (!_warningEffectPrefab.activeSelf)
      {
        _warningEffectPrefab.SetActive(true);
      }

      HandleTarget();
    }
    else
    {
      HandleAttack();
    }
  }

  private void HandleTarget()
  {
    Vector2 directionToTarget = _spaceship.transform.position - transform.position;
    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, directionToTarget.normalized.y * TargetSpeed);

    _targetTimer -= Time.deltaTime;
    if (_targetTimer <= 0f)
    {
      LockTarget();
    }
  }

  private void LockTarget()
  {
    _isTargetDone = true;
    _warningEffectPrefab.SetActive(false);

    if (AudioManager.Instance != null)
      AudioManager.Instance.PlaySFX(AudioSFXEnum.KamikazeEngine);
  }

  private void HandleAttack()
  {
    _rb.linearVelocity = new Vector2(-MaxSpeed, 0f);
  }

  public override void OnActivate()
  {
    _isTargetDone = false;
    _targetTimer = _timeToTarget;

    if (_spaceship == null)
      _spaceship = GameObject.FindGameObjectWithTag("Player");

    if (_warningEffectPrefab != null)
      _warningEffectPrefab.SetActive(true);

    if (_rb != null)
    {
      _rb.linearVelocity = Vector2.zero;
      _rb.angularVelocity = 0f;
    }
  }

  public override void OnDeactivate()
  {
    if (_rb != null) _rb.linearVelocity = Vector2.zero;
    _warningEffectPrefab?.SetActive(false);
  }
}