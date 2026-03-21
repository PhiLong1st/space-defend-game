using UnityEngine;

public class SparkController : MonoBehaviour, IProjectileStrategy
{
  [SerializeField] private ProjectileConfig _config;
  [SerializeField] private bool _isLaunched;

  private bool _isExploding;

  private Spark _model;
  private SparkView _view;

  private void Awake()
  {
    _model = new Spark(_config);
    _view = GetComponentInChildren<SparkView>();

    _view.OnExplosionFinished += () =>
    {
      _isLaunched = false;
      gameObject.SetActive(false);
    };
  }

  private void OnEnable()
  {
    _model.Reset();
    _isLaunched = false;
    _isExploding = false;

    Ready();
    Launch();
  }

  private void Update()
  {
    if (!_isLaunched || _isExploding) return;
    Move();
  }

  private void Move()
  {
    transform.Translate(Vector2.right * _model.Speed * Time.deltaTime);
  }

  public void Ready()
  {
    _view.PlayReadyEffect();
  }

  public void Launch()
  {
    if (_isLaunched) return;

    _isLaunched = true;
    _view.PlayLaunchEffect();
  }

  public void Explode()
  {
    if (_isExploding) return;

    _isExploding = true;
    _view.PlayExplosionEffect();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!_isLaunched) return;

    Explode();
    Debug.Log($"Spark collided with {other.gameObject.name}");
  }
}