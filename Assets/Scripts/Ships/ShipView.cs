using UnityEngine;


/// <summary>
/// Visual representation of a ship. Handles rendering, animations, VFX.
/// NO game logic here - only presentation.
/// Receives commands from ShipController, never makes decisions.
/// </summary>
public class ShipView : MonoBehaviour
{
  [Header("Visual Components")]
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private Transform turretTransform;

  [Header("VFX Hooks")]
  [SerializeField] private GameObject muzzleFlashPrefab;
  [SerializeField] private LineRenderer projectileTrail;

  [Header("Range Indicator")]
  [SerializeField] private GameObject rangeIndicator;
  [SerializeField] private SpriteRenderer rangeCircle;

  private ShipController controller;
  private Transform currentTarget;

  /// <summary>
  /// Initialize view with controller reference.
  /// </summary>
  public void Initialize(ShipController controller)
  {
    this.controller = controller;
    HideRangeIndicator();
  }

  private void Update()
  {
    UpdateTurretRotation();
  }

  /// <summary>
  /// Set current target for visual tracking.
  /// </summary>
  public void SetTarget(Transform target)
  {
    currentTarget = target;
  }

  /// <summary>
  /// Rotate turret to face target.
  /// </summary>
  private void UpdateTurretRotation()
  {
    if (turretTransform == null || currentTarget == null)
      return;

    Vector3 direction = currentTarget.position - turretTransform.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    turretTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);
  }

  /// <summary>
  /// Play shoot animation/VFX.
  /// </summary>
  public void PlayShootEffect(Vector3 targetPosition)
  {
    // Muzzle flash
    if (muzzleFlashPrefab != null && turretTransform != null)
    {
      var flash = Instantiate(muzzleFlashPrefab, turretTransform.position, Quaternion.identity);
      Destroy(flash, 0.2f);
    }

    // Projectile trail
    if (projectileTrail != null)
    {
      StartCoroutine(DrawProjectileTrail(turretTransform.position, targetPosition));
    }

    // Could trigger animation, sound, etc.
  }

  private System.Collections.IEnumerator DrawProjectileTrail(Vector3 start, Vector3 end)
  {
    projectileTrail.enabled = true;
    projectileTrail.SetPosition(0, start);
    projectileTrail.SetPosition(1, end);

    yield return new WaitForSeconds(0.1f);

    projectileTrail.enabled = false;
  }

  /// <summary>
  /// Called when ship is upgraded.
  /// </summary>
  public void OnUpgrade(int newLevel)
  {
    // Visual upgrade feedback
    // - Change sprite
    // - Play upgrade VFX
    // - Update UI elements

    Debug.Log($"Ship view updated for level {newLevel}");
  }

  /// <summary>
  /// Called when ship is being removed.
  /// </summary>
  public void OnRemoved()
  {
    // Play removal VFX
    // Fade out, explosion, etc.
  }

  /// <summary>
  /// Show range indicator (for placement preview).
  /// </summary>
  public void ShowRangeIndicator()
  {
    if (rangeIndicator != null)
    {
      rangeIndicator.SetActive(true);

      // Scale to match ship range
      if (rangeCircle != null && controller != null)
      {
        float range = controller.GetRange();
        float scale = range * 2f; // Diameter
        rangeCircle.transform.localScale = new Vector3(scale, scale, 1f);
      }
    }
  }

  /// <summary>
  /// Hide range indicator.
  /// </summary>
  public void HideRangeIndicator()
  {
    if (rangeIndicator != null)
    {
      rangeIndicator.SetActive(false);
    }
  }

  private void OnDrawGizmosSelected()
  {
    // Draw range in editor
    if (controller != null)
    {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, controller.GetRange());
    }
  }
}