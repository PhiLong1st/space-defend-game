using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] private float _scaleAmount = 1.2f;
  [SerializeField] private float _scaleSpeed = 5f;

  [Header("References")]
  [SerializeField] private Button _button;

  private Vector3 _originalScale;
  private bool _isHovering = false;

  private void Start()
  {
    _originalScale = transform.localScale;
  }

  private void OnEnable()
  {
    if (_button != null)
    {
      _button.onClick.AddListener(OnButtonClick);
    }
  }

  private void Update()
  {
    if (_isHovering)
    {
      transform.localScale = Vector3.Lerp(transform.localScale, _originalScale * _scaleAmount, Time.deltaTime * _scaleSpeed);
    }
    else
    {
      transform.localScale = Vector3.Lerp(transform.localScale, _originalScale, Time.deltaTime * _scaleSpeed);
    }
  }

  public void OnPointerEnter()
  {
    _isHovering = true;
  }

  public void OnPointerExit()
  {
    _isHovering = false;
  }

  public void OnButtonClick()
  {
    if (AudioManager.Instance != null)
    {
      AudioManager.Instance.PlaySFX(AudioSFXEnum.ButtonClick);
    }
  }
}