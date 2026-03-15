using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
  public static SpaceshipController Instance { get; private set; }

  [SerializeField] private Spaceship _spaceship;
  [SerializeField] private SpaceshipView _spaceshipView;

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }

  }

  private void Start()
  {
    _spaceshipView.OnExplosionFinished += GameManager.Instance.GameOver;

  }

  void Update()
  {
    HandleMovement();
  }

  public void HandleMovement()
  {
    if (_spaceship.IsDestroyed)
    {
      return;
    }

    if (Input.GetKey(KeyCode.W))
    {
      var direction = transform.up;
      _spaceship.Move(direction);
    }

    if (Input.GetKey(KeyCode.S))
    {
      var direction = -transform.up;
      _spaceship.Move(direction);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Obstacle"))
    {
      _spaceship.HandleDestroy();
      _spaceshipView.PlayExplosionEffect();
    }
  }
}
