using UnityEngine;

public abstract class AbstractTouchTrap : MonoBehaviour
{
  [SerializeField] protected int _damage = 1;
  [SerializeField] protected int _lives = 1;
  [SerializeField] protected float _maxSpeed = 1;

  protected Rigidbody2D _rb;
  // private FlashWhite flashWhite;
  // [SerializeField] private GameObject destroyEffect;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  public abstract void Activate();

  public void TakeDamage(int damage)
  {
    _lives -= damage;
    if (_lives <= 0)
    {
      Destroy(gameObject);
    }
  }

  // private void OnCollisionEnter2D(Collision2D collision)
  // {
  //   if (collision.gameObject.CompareTag("Player"))
  //   {
  //     var spaceship = collision.gameObject.GetComponent<SpaceshipController>();
  //     if (spaceship) spaceship.HandleDestroy();
  //   }
  // }
}