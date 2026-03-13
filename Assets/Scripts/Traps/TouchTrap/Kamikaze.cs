using UnityEngine;

public class Kamikaze : AbstractTouchTrap
{
  private bool _isTargetDone = false;
  private float _timeToTarget = 3f;

  private GameObject _player;

  void Start()
  {
    _player = GameObject.FindGameObjectWithTag("Player");
  }

  private void Update()
  {
    if (_isTargetDone || _player == null)
      return;

    transform.position = new Vector2(transform.position.x, _player.transform.position.y);

    _timeToTarget -= Time.deltaTime;
    if (_timeToTarget > 0f) return;

    Vector2 playerPosition = _player.transform.position;
    Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
    _rb.linearVelocity = direction * _maxSpeed;
    _isTargetDone = true;
  }

  public override void Activate()
  {
    // No special activation behavior for Kamikaze, it just moves in a straight line and damages the player on contact.
  }
}