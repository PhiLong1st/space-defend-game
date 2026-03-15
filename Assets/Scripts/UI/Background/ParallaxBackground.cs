using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 0.5f;
  private SpriteRenderer _image;
  private float _imageWidth;

  private void Awake()
  {
    _image = GetComponent<SpriteRenderer>();
    _imageWidth = _image.sprite.texture.width / _image.sprite.pixelsPerUnit;
  }

  private void Start()
  {
    if (AudioManager.Instance != null)
      AudioManager.Instance.PlayMusic(AudioMusicEnum.InGame);
  }

  private void Update()
  {
    float moveX = moveSpeed * GameData.ParallaxWorldSpeedMultiplier * GameManager.Instance.WorldSpeed * Time.deltaTime;
    transform.position += new Vector3(moveX, 0);

    if (Mathf.Abs(transform.position.x) - _imageWidth > 0)
    {
      transform.position = new Vector3(0, transform.position.y);
    }
  }
}
