using UnityEngine;

public class Kamikaze : AbstractTouchTrap
{
  [SerializeField] private float _timeToTarget = 3f;

  private bool _isTargetDone = false;
  private float _targetTimer;
  private GameObject _spaceship;

  private void Update()
  {
    if (_isTargetDone || _spaceship == null)
      return;

    Vector2 shipPosition = _spaceship.transform.position;
    transform.position = new Vector2(transform.position.x, shipPosition.y);

    _targetTimer -= Time.deltaTime;
    if (_targetTimer > 0f) return;

    Vector2 direction = (shipPosition - (Vector2)transform.position).normalized;
    _rb.linearVelocity = direction * _maxSpeed;
    _isTargetDone = true;
  }

  public override void OnActivate()
  {
    _isTargetDone = false;
    _targetTimer = _timeToTarget;

    if (_rb != null)
    {
      _rb.linearVelocity = Vector2.zero;
      _rb.angularVelocity = 0f;
    }

    if (_spaceship == null)
    {
      _spaceship = GameObject.FindGameObjectWithTag("Player");
    }
  }

  public override void OnDeactivate()
  {
    if (_rb != null)
    {
      _rb.linearVelocity = Vector2.zero;
      _rb.angularVelocity = 0f;
    }
  }
}