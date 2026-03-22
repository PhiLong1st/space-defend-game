using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(DamageFeedback))]
public class KamikazeTrapView : MonoBehaviour
{
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;

  [SerializeField] private GameObject _warningPrefab;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void PlayTargetAnimation() => _warningPrefab.SetActive(true);

  public void StopTargetAnimation() => _warningPrefab.SetActive(false);

  public void PlayTakeDamageAnimation(int damage)
  {
    GetComponent<DamageFeedback>().PlayHitFlash(damage);
  }

  public void PlayExplodeAnimation()
  {
    Debug.Log("Animation KamikazeTrap exploded!");
  }
}