using UnityEngine;
public abstract class AbstractProjectile : MonoBehaviour
{
  protected abstract float Speed { get; }
  protected abstract float AttackCooldown { get; }
  protected abstract float AttackDamage { get; }

  private float _currentCooldown;

  public float CurrentCooldown => _currentCooldown;

  private void Start()
  {
    _currentCooldown = 0;
  }

  private void Update()
  {
    if (_currentCooldown > 0)
    {
      ReduceCooldown(Time.deltaTime);
    }
    else
    {
      AttackLogic();
    }
  }

  public void ReduceCooldown(float amount)
  {
    _currentCooldown -= amount;
  }

  public void StartCooldown()
  {
    _currentCooldown = AttackCooldown;
  }

  public bool CanAttack()
  {
    return _currentCooldown <= 0;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Enemy"))
    {
      // Implement damage logic here, e.g., reduce enemy health by _attackDamage
      // Enemy enemy = other.gameObject.GetComponent<Enemy>();
      // if (enemy != null)
      // {
      //   enemy.TakeDamage(_attackDamage);
      // }
    }

    Explode();
    Destroy(gameObject);
  }

  public void Attack()
  {
    StartAttack();
    StartCooldown();
  }

  public abstract void StartAttack();

  public abstract void AttackLogic();

  public abstract void Explode();
}