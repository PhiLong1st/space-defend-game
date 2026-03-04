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
    _transform.position += _transform.up * _projectile.Speed * Time.fixedDeltaTime;
  }
}