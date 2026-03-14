using UnityEngine;
using TMPro;

public class CtaText : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _text;

  private void Start()
  {
    AudioManager.Instance.PlayMusic(AudioMusicEnum.MainMenu);
  }

  private void Update()
  {
    float alpha = Mathf.PingPong(Time.time, 1f);
    _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
    transform.position += Vector3.up * Time.deltaTime;

    if (Input.anyKeyDown)
    {
      SceneLoader.LoadScene("Game_Scene");
    }
  }
}