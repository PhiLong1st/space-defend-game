using UnityEngine;

public abstract class AbstractTouchTrap : MonoBehaviour
{
  [SerializeField] protected int _damage = 1;
  [SerializeField] protected int _lives = 1;
  [SerializeField] protected float _maxSpeed = 1;
  [SerializeField] private float _despawnX = -15f;
  [SerializeField] private Animator _animator;

  protected Rigidbody2D _rb;
  private int _initialLives;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _initialLives = _lives;
  }

  private void OnEnable()
  {
    _lives = _initialLives;
    if (_rb != null)
    {
      _rb.linearVelocity = Vector2.zero;
      _rb.angularVelocity = 0f;
    }

    OnActivate();
  }

  private void OnDisable()
  {
    OnDeactivate();
  }

  private void Update()
  {
    if (transform.position.x < _despawnX)
    {
      DeactivateTrap();
    }
  }

  public abstract void OnActivate();
  public abstract void OnDeactivate();

  protected void DeactivateTrap()
  {
    gameObject.SetActive(false);
  }

  public void TakeDamage(int damage)
  {
    _lives -= damage;
    if (_lives <= 0)
    {
      DeactivateTrap();
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Destroy(gameObject);
    }
  }
}