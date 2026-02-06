using UnityEngine;


/// <summary>
/// Visual representation of an enemy. Handles rendering, animations, VFX.
/// NO game logic - only presentation.
/// </summary>
public class EnemyView : MonoBehaviour
{
  [Header("Visual Components")]
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private Transform modelTransform;

  [Header("Health Bar")]
  [SerializeField] private GameObject healthBarRoot;
  [SerializeField] private SpriteRenderer healthBarFill;

  [Header("VFX")]
  [SerializeField] private GameObject deathEffectPrefab;

  private EnemyController controller;

  /// <summary>
  /// Initialize view with controller reference.
  /// </summary>
  public void Initialize(EnemyController controller)
  {
    this.controller = controller;

    if (healthBarRoot != null)
    {
      healthBarRoot.SetActive(true);
    }
  }

  /// <summary>
  /// Update movement visuals.
  /// </summary>
  public void UpdateMovement(Vector3 direction)
  {
    // Flip sprite based on movement direction
    if (spriteRenderer != null && direction.x != 0)
    {
      spriteRenderer.flipX = direction.x < 0;
    }

    // Could trigger walk animation here
  }

  /// <summary>
  /// React to damage taken.
  /// </summary>
  public void OnDamaged(float healthPercentage)
  {
    // Update health bar
    if (healthBarFill != null)
    {
      healthBarFill.transform.localScale = new Vector3(healthPercentage, 1f, 1f);
    }

    // Flash effect
    if (spriteRenderer != null)
    {
      StartCoroutine(DamageFlash());
    }
  }

  private System.Collections.IEnumerator DamageFlash()
  {
    Color originalColor = spriteRenderer.color;
    spriteRenderer.color = Color.red;

    yield return new WaitForSeconds(0.1f);

    spriteRenderer.color = originalColor;
  }

  /// <summary>
  /// Play death animation and VFX.
  /// </summary>
  public void OnDeath()
  {
    // Hide health bar
    if (healthBarRoot != null)
    {
      healthBarRoot.SetActive(false);
    }

    // Play death effect
    if (deathEffectPrefab != null)
    {
      var effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
      Destroy(effect, 2f);
    }

    // Fade out sprite
    if (spriteRenderer != null)
    {
      StartCoroutine(FadeOut());
    }
  }

  private System.Collections.IEnumerator FadeOut()
  {
    float duration = 0.5f;
    float elapsed = 0f;
    Color color = spriteRenderer.color;

    while (elapsed < duration)
    {
      elapsed += Time.deltaTime;
      color.a = 1f - (elapsed / duration);
      spriteRenderer.color = color;
      yield return null;
    }
  }
}