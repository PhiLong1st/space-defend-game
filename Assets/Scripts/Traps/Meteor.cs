using System.Collections;
using UnityEngine;

public class Meteor : MonoBehaviour
{
  private Rigidbody2D rb;
  // private FlashWhite flashWhite;
  // [SerializeField] private GameObject destroyEffect;

  [SerializeField] private int _lives = 3;
  [SerializeField] private int _damage = 3;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    // flashWhite = GetComponent<FlashWhite>();

    float pushX = Random.Range(-1f, 0);
    float pushY = Random.Range(-1f, 1f);
    rb.linearVelocity = new Vector2(pushX, pushY);

    float randomScale = Random.Range(0.6f, 1f);
    transform.localScale = new Vector2(randomScale, randomScale);
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      var player = collision.gameObject.GetComponent<SpaceshipController>();
      if (player) player.TakeDamage(_damage);
    }
  }

  public void TakeDamage(int damage)
  {
    // AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);
    // flashWhite.Flash();

    _lives -= damage;
    if (_lives <= 0)
    {
      // Instantiate(destroyEffect, transform.position, transform.rotation);
      // AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
      Destroy(gameObject);
    }
  }
}