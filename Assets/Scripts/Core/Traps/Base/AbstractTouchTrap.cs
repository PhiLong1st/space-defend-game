using UnityEngine;

public abstract class AbstractTouchTrap : MonoBehaviour
{
  [SerializeField] protected float _maxSpeed = GameData.DefaultTrapMaxSpeed;

  protected Rigidbody2D _rb;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  private void OnEnable()
  {
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

  public abstract void OnActivate();
  public abstract void OnDeactivate();

  protected void DeactivateTrap()
  {
    gameObject.SetActive(false);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Destroy(gameObject);
    }
  }
}