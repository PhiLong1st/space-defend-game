using UnityEngine;

public class NormalBullet : AbstractProjectile
{
  protected override float Speed => 10f;
  protected override float AttackCooldown => 0.5f;
  protected override float AttackDamage => 20f;

  public override void StartAttack()
  {
    // Implement any logic needed to start the attack, e.g., play an animation or sound
  }

  public override void AttackLogic()
  {
    // Move the bullet forward
    transform.Translate(Vector2.right * Speed * Time.deltaTime);
  }

  public override void Explode()
  {
    // Implement explosion logic here, e.g., instantiate the explosion effect and apply damage to nearby enemies
    // Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
  }
}