using UnityEngine;

public class ProjectileController : MonoBehaviour
{
  [SerializeField] private Projectile _projectile;

  private Transform _transform;

  private void Awake()
  {
    _transform = GetComponent<Transform>();
  }

  private void FixedUpdate()
  {
    HandleMovement();
  }

  public void HandleMovement()
  {
    _transform.position += _transform.right * _projectile.Speed * Time.fixedDeltaTime;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      return;
    }

    Debug.Log("Hit!");
    Destroy(gameObject);
    // other.gameObject.SetActive(false);
  }
}