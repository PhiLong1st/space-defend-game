using UnityEngine;

public abstract class ProjectileFactory
{
  private float _cooldown;

  private float _currentCooldown;

  private GameObject _attachPoint;

  public ProjectileFactory(GameObject attachPoint, float cooldown)
  {
    _attachPoint = attachPoint;
    _cooldown = cooldown;
    _currentCooldown = 0f;
  }

  public void ReduceCooldown(float amount) => _currentCooldown = Mathf.Max(0f, _currentCooldown - amount);

  public void ResetCooldown() => _currentCooldown = _cooldown;

  public bool IsReady => _currentCooldown <= 0f;

  public void Spawn()
  {
    var projectile = CreateProjectile();

    projectile.SetActive(true);
    projectile.transform.position = _attachPoint.transform.position;

    ResetCooldown();
  }

  public abstract GameObject CreateProjectile();
}