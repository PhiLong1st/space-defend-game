using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
  public static SpaceshipController Instance { get; private set; }

  [SerializeField] private Spaceship _spaceship;
  [SerializeField] private SpaceshipView _spaceshipView;
  [SerializeField] private GameObject _attachPoint;

  private ProjectileSystem _projectileSystem;

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
    _projectileSystem = new ProjectileSystem();

    var sparkFactory = new SparkFactory(_attachPoint, 0.5f);
    _projectileSystem.AddProjectile(sparkFactory);

    _spaceshipView.OnExplosionFinished += GameManager.Instance.GameOver;
  }

  private void Update()
  {
    _projectileSystem.Update();
    HandleMovement();
  }

  public void HandleMovement()
  {
    if (_spaceship.IsDestroyed) return;
    if (Input.GetKey(KeyCode.W)) _spaceship.Move(Vector2.up);
    if (Input.GetKey(KeyCode.A)) _spaceship.Move(Vector2.left);
    if (Input.GetKey(KeyCode.S)) _spaceship.Move(Vector2.down);
    if (Input.GetKey(KeyCode.D)) _spaceship.Move(Vector2.right);
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
