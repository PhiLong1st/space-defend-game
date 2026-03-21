using UnityEngine;

public class KamikazeTrapController : MonoBehaviour, IDamageable
{
  [SerializeField] private KamikazeTrap _kamikazeTrap;
  [SerializeField] private KamikazeTrapView _kamikazeTrapView;

  private StateMachine _stateMachine;

  public float Speed => _kamikazeTrap.Speed * GameManager.Instance.WorldSpeed;

  private void OnEnable()
  {
    _stateMachine = new StateMachine();

    IState targetingState = new KamikazeTargetingState(this, _stateMachine);
    IState attackState = new KamikazeAttackState(this, _stateMachine);

    _stateMachine.AddState(targetingState);
    _stateMachine.AddState(attackState);

    _stateMachine.Initialize(targetingState);
  }

  private void OnDisable()
  {
    _stateMachine = null;
  }

  private void Update()
  {
    _stateMachine.Update();
  }

  public void TakeDamage(int damage)
  {
    _kamikazeTrap.TakeDamage(damage);
    _kamikazeTrapView.PlayTakeDamageAnimation();

    if (_kamikazeTrap.IsDestroyed()) Explode();
  }

  public void Move(Vector2 direction)
  {
    transform.Translate(direction * Time.deltaTime * Speed);
  }

  public void Explode()
  {
    _kamikazeTrap.Explode();
    _kamikazeTrapView.PlayExplodeAnimation();
  }

  public void StartWarning() => _kamikazeTrapView.PlayTargetAnimation();

  public void StopWarning() => _kamikazeTrapView.StopTargetAnimation();

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      gameObject.SetActive(false);
    }
  }
}