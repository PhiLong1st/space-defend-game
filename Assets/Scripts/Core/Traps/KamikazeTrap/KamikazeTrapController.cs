using UnityEngine;

public class KamikazeTrapController : MonoBehaviour, IDamageable
{
  [SerializeField] private KamikazeTrapConfig _config;
  private KamikazeTrapView _view;
  private KamikazeTrap _model;
  private StateMachine _stateMachine;

  public float Speed => _model.Speed * GameManager.Instance.WorldSpeed;
  public float WarningSpeed => _model.WarningSpeed * GameManager.Instance.WorldSpeed;
  public int HealthPercentage => _model.HealthPercentage;

  private void OnEnable()
  {
    _model = new KamikazeTrap(_config);
    _stateMachine = new StateMachine();

    _view = GetComponentInChildren<KamikazeTrapView>();
    _view.OnExplosionComplete += Despawn;

    IState targetingState = new KamikazeTargetingState(this, _stateMachine);
    IState attackState = new KamikazeAttackState(this, _stateMachine);

    _stateMachine.AddState(targetingState);
    _stateMachine.AddState(attackState);
    _stateMachine.Initialize(targetingState);
  }

  private void Start()
  {
    _stateMachine.Start();
  }

  private void Update()
  {
    _stateMachine.Update();
  }

  private void OnDisable()
  {
    _stateMachine = null;
    _model = null;
  }

  public void TakeDamage(int damage)
  {
    _model.TakeDamage(damage);

    if (_model.IsDestroyed())
    {
      _view.PlayExplodeAnimation();
    }
    else
    {
      _view.PlayTakeDamageAnimation(damage);
    }
  }

  public void Move(Vector2 direction)
  {
    if (_model.IsDestroyed()) return;
    transform.Translate(direction);
  }

  public void StartWarning() => _view.PlayTargetAnimation();

  public void StopWarning() => _view.StopTargetAnimation();

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player")) other.GetComponent<IDamageable>()?.TakeDamage(_model.Damage);
  }

  private void Despawn()
  {
    gameObject.SetActive(false);
  }
}