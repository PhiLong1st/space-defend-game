using UnityEngine;
using System;

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

  void Update()
  {
    HandleMovement();
    HandleAttack();

    if (Input.GetKeyDown(KeyCode.Space))
    {
      _spaceship.LevelUp();
      _spaceshipView.PlayLevelUpAnimation();
    }
  }

  public void HandleAttack()
  {
    // Instantiate(_projectileGO, _gun.transform.position, _gun.transform.rotation);
  }

  public void HandleGainExperience(int amount)
  {
    _spaceship.GainExperience(amount);

    if (_spaceship.CanLevelUp())
    {
      _spaceship.LevelUp();
      _spaceshipView.PlayLevelUpAnimation();
    }
  }

  public void HandleMovement()
  {
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

    if (Input.GetKey(KeyCode.A))
    {
      var direction = -transform.right;
      _spaceship.Move(direction);
    }

    if (Input.GetKey(KeyCode.D))
    {
      var direction = transform.right;
      _spaceship.Move(direction);
    }
  }
}
