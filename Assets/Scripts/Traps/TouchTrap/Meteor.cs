using System.Collections;
using UnityEngine;

public class Meteor : AbstractTouchTrap
{
  [Header("Wander Settings")]
  public float wanderCircleDistance = 2f;
  public float wanderCircleRadius = 1f;
  public float wanderAngleChange = 15f;
  public float maxSteeringForce = 0.5f;

  private float _wanderAngle;


  private Vector2 _circleCenterDebug;
  private Vector2 _targetPointDebug;

  private bool _shouldWander = false;

  void Start()
  {
    float pushX = Random.Range(-1f, 0f);
    _rb.linearVelocity = new Vector2(pushX, 0).normalized * _maxSpeed;

    float randomScale = Random.Range(0.6f, 1f);
    transform.localScale = new Vector2(randomScale, randomScale);
    _wanderAngle = Random.Range(0f, 360f);
  }

  void FixedUpdate()
  {
    if (_shouldWander)
    {
      ApplyWanderBehavior();
    }
  }

  private void ApplyWanderBehavior()
  {
    Vector2 velocityDir = _rb.linearVelocity.normalized;
    Vector2 circleCenter = (Vector2)transform.position + (velocityDir * wanderCircleDistance);

    float jitter = Random.Range(-wanderAngleChange, wanderAngleChange);
    _wanderAngle += jitter;

    float angleRad = _wanderAngle * Mathf.Deg2Rad;
    Vector2 displacement = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * wanderCircleRadius;

    Vector2 targetPoint = circleCenter + displacement;

    Vector2 desiredVelocity = (targetPoint - (Vector2)transform.position).normalized * _maxSpeed;
    Vector2 steeringForce = desiredVelocity - _rb.linearVelocity;

    steeringForce = Vector2.ClampMagnitude(steeringForce, maxSteeringForce);

    _rb.AddForce(steeringForce, ForceMode2D.Force);

    _circleCenterDebug = circleCenter;
    _targetPointDebug = targetPoint;
  }

  public override void Activate()
  {

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("WanderZone"))
    {
      _shouldWander = true;
    }
  }

  private void OnDrawGizmos()
  {
    if (!Application.isPlaying) return;

    Gizmos.color = Color.white;
    Gizmos.DrawLine(transform.position, _circleCenterDebug);

    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(_circleCenterDebug, wanderCircleRadius);

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(_targetPointDebug, 0.1f);

    if (_rb != null)
    {
      Gizmos.color = Color.green;
      Gizmos.DrawLine(transform.position, (Vector2)transform.position + _rb.linearVelocity);
    }
  }
}