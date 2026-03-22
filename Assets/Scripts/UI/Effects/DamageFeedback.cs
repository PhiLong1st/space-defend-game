using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageFeedback : MonoBehaviour
{
  [Header("Hit Flash Settings")]
  [SerializeField] private Material _flashMaterial;
  [SerializeField] private float _flashDuration = 0.05f;

  private Material _originalMaterial;
  private Coroutine _flashRoutine;
  private SpriteRenderer _spriteRenderer;

  private void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _originalMaterial = _spriteRenderer.material;
  }

  public void PlayHitFlash(int damageAmount = 0)
  {
    if (_flashRoutine != null) StopCoroutine(_flashRoutine);
    _flashRoutine = StartCoroutine(FlashRoutine());

    if (damageAmount == 0) return;

    var damageText = UIPrefabsManager.Instance.Spawn(UIPrefabType.FloatingDamageText)?.GetComponent<FloatingDamageTextEffect>();
    if (damageText != null) damageText.StartFloating(damageAmount, transform.position);
  }

  private IEnumerator FlashRoutine()
  {
    _spriteRenderer.material = _flashMaterial;
    yield return new WaitForSeconds(_flashDuration);
    _spriteRenderer.material = _originalMaterial;
  }
}