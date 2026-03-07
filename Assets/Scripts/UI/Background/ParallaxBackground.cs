using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 0.5f;
  private SpriteRenderer _backgroundImage;
  private float _backgroundWidth;

  private void Awake()
  {
    _backgroundImage = GetComponent<SpriteRenderer>();
    _backgroundWidth = _backgroundImage.bounds.size.x;
  }

  private void Update()
  {
    float moveX = moveSpeed * SpaceshipController.Instance.CurrentMovementSpeed * Time.deltaTime;
    transform.position += new Vector3(moveX, 0);

    if (Mathf.Abs(transform.position.x) - _backgroundWidth > 0)
    {
      transform.position = new Vector3(0, transform.position.y);
    }
  }
}
