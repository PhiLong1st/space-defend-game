using UnityEngine;

public class MeteorController : MonoBehaviour, IDamageable
{
  [Header("Meteor Properties")]
  [SerializeField] private MeteorConfig _config;

  private Meteor _model;
  private MeteorView _view;

  [Header("Wander Settings")]
  [SerializeField] private float _wanderCircleDistance = 2f;
  [SerializeField] private float _wanderCircleRadius = 1f;
  [SerializeField] private float _wanderAngleChange = 15f;
  [SerializeField] private float _maxSteeringForce = 0.5f;

  private float _wanderAngle;
  private Vector2 _circleCenterDebug;
  private Vector2 _targetPointDebug;
  private bool _shouldWander = false;

  private Rigidbody2D _rb;
  private CapsuleCollider2D _collider;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _collider = GetComponent<CapsuleCollider2D>();

    _model = new Meteor(_config);

    _view = GetComponentInChildren<MeteorView>();
    _view.OnExplosionComplete += Despawn;
  }

  public int HealthPercentage => _model.HealthPercentage;
  public bool IsDestroyed() => _model.IsDestroyed();

  private void OnEnable()
  {
    Reset();
  }

  private void Start()
  {
    transform.localScale = _model.Scale;
    _rb.linearVelocity = Vector2.left * _model.Speed;
    _wanderAngle = Random.Range(0f, 360f);
  }

  private void FixedUpdate()
  {
    if (_shouldWander) ApplyWanderBehavior();
  }

  private void Update()
  {
    if (IsDestroyed())
    {
      Stop();
      DisableDetector();
    }
  }

  public void TakeDamage(int damage)
  {
    _model.TakeDamage(damage);
    _view.PlayTakeDamageAnimation(damage);
  }

  #region Physics
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

    Vector2 desiredVelocity = (targetPoint - (Vector2)transform.position).normalized * _model.Speed;
    Vector2 steering = desiredVelocity - _rb.linearVelocity;

    steering = Vector2.ClampMagnitude(steering, _maxSteeringForce);

    _rb.AddForce(steering, ForceMode2D.Force);

    if (_rb.linearVelocity.magnitude > _model.Speed) _rb.linearVelocity = _rb.linearVelocity.normalized * _model.Speed;

    _circleCenterDebug = circleCenter;
    _targetPointDebug = targetPoint;
  }

  private void EnableDetector() => _collider.enabled = true;

  private void DisableDetector() => _collider.enabled = false;

  private void Stop()
  {
    _rb.linearVelocity = Vector2.zero;
    _rb.angularVelocity = 0f;
  }

  private void Despawn() => gameObject.SetActive(false);

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("WanderZone")) _shouldWander = true;
  }
  #endregion

  public void Reset()
  {
    _model.Reset();
    _view.Reset();

    _shouldWander = false;
    EnableDetector();
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