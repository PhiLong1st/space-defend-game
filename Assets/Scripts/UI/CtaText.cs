using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CtaText : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] private TextMeshProUGUI _text;
  [SerializeField] private float _fadeSpeed = 1f;
  [SerializeField] private float _floatSpeed = 1f;
  [SerializeField] private float _floatAmplitude = 10f;

  private Vector3 _startPos;

  private void Start()
  {
    _startPos = transform.position;

    if (AudioManager.Instance != null)
      AudioManager.Instance.PlayMusic(AudioMusicEnum.MainMenu);
  }

  private void Update()
  {
    HandleVisuals();
    HandleInput();
  }

  private void HandleVisuals()
  {
    float alpha = (Mathf.Sin(Time.time * _fadeSpeed) + 1f) / 2f;
    _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);

    float newY = _startPos.y + Mathf.Sin(Time.time * _floatSpeed) * _floatAmplitude;
    transform.position = new Vector3(_startPos.x, newY, _startPos.z);
  }

  private void HandleInput()
  {
    if (Input.anyKeyDown)
    {
      SceneLoader.LoadScene("Game_Scene");
    }
  }
}