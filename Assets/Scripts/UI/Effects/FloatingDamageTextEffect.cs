using UnityEngine;
using TMPro;
using DG.Tweening;

public class FloatingDamageTextEffect : MonoBehaviour
{
  [Header("UI Components")]
  [SerializeField] private TextMeshProUGUI _text;

  [Header("Animation Settings")]
  [SerializeField] private float _duration = 0.65f;
  [SerializeField] private float _minFloatDistance = 0.7f;
  [SerializeField] private float _maxFloatDistance = 1f;

  private Vector3 _originalScale = Vector3.one;

  private void Awake()
  {
    if (_text == null) _text = GetComponentInChildren<TextMeshProUGUI>();
    _originalScale = transform.localScale;
  }

  private void OnEnable()
  {
    _text.alpha = 1f;
    transform.localScale = _originalScale;
  }

  private void OnDisable()
  {
    transform.DOKill();
    _text.DOKill();
  }

  public void StartFloating(int damageAmount, Vector2 spawnPosition)
  {
    transform.position = spawnPosition;

    _text.text = damageAmount.ToString();
    _text.alpha = 1f;

    transform.localScale = _originalScale * 0.5f;

    Sequence sequence = DOTween.Sequence();
    sequence.Append(transform.DOScale(_originalScale, 0.2f).SetEase(Ease.OutBack));

    float targetY = spawnPosition.y + Random.Range(_minFloatDistance, _maxFloatDistance);
    sequence.Join(transform.DOMoveY(targetY, _duration).SetEase(Ease.OutCirc));

    sequence.Insert(_duration * 0.5f, _text.DOFade(0f, _duration * 0.5f));
    sequence.OnComplete(Despawn);
  }

  private void Despawn()
  {
    gameObject.SetActive(false);
  }
}