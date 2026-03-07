using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
  public static SpaceshipController Instance { get; private set; }

  [SerializeField] float _speed = 10f;
  [SerializeField] float _boost = 1f;

  private SpaceshipView _spaceshipView;

  private Transform _transform;

  public float CurrentSpeed => _speed * _boost;
  public float Boost => _boost;

  private void Awake()
  {
    _transform = GetComponent<Transform>();
    _spaceshipView = GetComponent<SpaceshipView>();

    if (Instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }
  }

  void Start()
  {
  }

  void Update()
  {
    HandleMovement();
  }

  public void HandleMovement()
  {
    if (Input.GetKey(KeyCode.W))
    {
      Vector2 newPosition = _transform.position + _transform.up * CurrentSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
    }

    if (Input.GetKey(KeyCode.S))
    {
      Vector2 newPosition = _transform.position - _transform.up * CurrentSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
    }

    if (Input.GetKey(KeyCode.D))
    {
      Vector2 newPosition = _transform.position + _transform.right * CurrentSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
    }

    if (Input.GetKey(KeyCode.A))
    {
      Vector2 newPosition = _transform.position - _transform.right * CurrentSpeed * Time.fixedDeltaTime;
      _transform.position = newPosition;
    }

    // Boosting
    if (Input.GetKey(KeyCode.E))
    {
      EnterBoost();
    }

    if (Input.GetKey(KeyCode.R))
    {
      StopBoost();
    }
  }

  public void EnterBoost()
  {
    _boost = 2f;
    _spaceshipView.EnterBoost();
  }

  public void StopBoost()
  {
    _boost = 1f;
    _spaceshipView.ExitBoost();
  }
}
