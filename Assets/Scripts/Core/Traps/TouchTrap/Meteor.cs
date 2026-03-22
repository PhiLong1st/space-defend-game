using UnityEngine;

public class Meteor : MonoBehaviour, IDamageable
{
  [Header("Wander Settings")]
  [SerializeField] private float _wanderCircleDistance = 2f;
  [SerializeField] private float _wanderCircleRadius = 1f;
  [SerializeField] private float _wanderAngleChange = 15f;
  [SerializeField] private float _maxSteeringForce = 0.5f;
  [SerializeField] private float _maxSpeed = GameData.DefaultTrapMaxSpeed;

  private float _wanderAngle;

  private Vector2 _circleCenterDebug;
  private Vector2 _targetPointDebug;

  private bool _shouldWander = false;
  private Rigidbody2D _rb;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  private void OnEnable()
  {
    _shouldWander = false;

    float pushX = Random.Range(GameData.MeteorPushXMin, GameData.MeteorPushXMax);
    _rb.linearVelocity = new Vector2(pushX, 0f).normalized * _maxSpeed;

    float randomScale = Random.Range(GameData.MeteorScaleMin, GameData.MeteorScaleMax);
    transform.localScale = new Vector2(randomScale, randomScale);
    _wanderAngle = Random.Range(0f, 360f);
  }

  private void FixedUpdate()
  {
    if (_shouldWander)
    {
      ApplyWanderBehavior();
    }
  }

  private void ApplyWanderBehavior()
  {
    Vector2 velocityDir = _rb.linearVelocity.normalized;
    if (velocityDir == Vector2.zero) velocityDir = Vector2.right;

    Vector2 circleCenter = (Vector2)transform.position + (velocityDir * _wanderCircleDistance);

    float jitter = Random.Range(-_wanderAngleChange, _wanderAngleChange) * Time.fixedDeltaTime;
    _wanderAngle += jitter;

    float baseAngle = Mathf.Atan2(velocityDir.y, velocityDir.x);
    float finalAngle = baseAngle + (_wanderAngle * Mathf.Deg2Rad);

    Vector2 displacement = new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle)) * _wanderCircleRadius;
    Vector2 targetPoint = circleCenter + displacement;

    Vector2 desiredVelocity = (targetPoint - (Vector2)transform.position).normalized * _maxSpeed;
    Vector2 steering = desiredVelocity - _rb.linearVelocity;

    steering = Vector2.ClampMagnitude(steering, _maxSteeringForce);

    _rb.AddForce(steering, ForceMode2D.Force);

    if (_rb.linearVelocity.magnitude > _maxSpeed)
    {
      _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;
    }

    _circleCenterDebug = circleCenter;
    _targetPointDebug = targetPoint;
  }

  private void OnDisable()
  {
    _shouldWander = false;

    if (_rb != null)
    {
      _rb.linearVelocity = Vector2.zero;
      _rb.angularVelocity = 0f;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("WanderZone")) _shouldWander = true;
  }

  public void TakeDamage(int damage)
  {
    GetComponent<DamageFeedback>()?.PlayHitFlash(damage);
  }

  private void OnDrawGizmos()
  {
    if (!Application.isPlaying) return;

    Gizmos.color = Color.white;
    Gizmos.DrawLine(transform.position, _circleCenterDebug);

    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(_circleCenterDebug, _wanderCircleRadius);

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(_targetPointDebug, 0.1f);

    if (_rb != null)
    {
      Gizmos.color = Color.green;
      Gizmos.DrawLine(transform.position, (Vector2)transform.position + _rb.linearVelocity);
    }
  }
}