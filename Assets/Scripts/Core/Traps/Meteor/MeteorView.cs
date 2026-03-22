using UnityEngine;
using System;

public class MeteorView : MonoBehaviour
{
  private MeteorController _controller;
  private Animator _animator;
  private bool _hasPlayedExplosionSFX;

  public Action OnExplosionComplete;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _controller = GetComponentInParent<MeteorController>();

    Reset();
  }

  public void PlayTakeDamageAnimation(int damage)
  {
    if (_controller.IsDestroyed()) return;
    GetComponent<DamageFeedback>()?.PlayHitFlash(damage);

    float randomPitch = UnityEngine.Random.Range(0.4f, 0.8f);
    AudioManager.Instance.PlaySFX(AudioSFXEnum.MeteorHit, randomPitch);
  }

  private void Update()
  {
    if (_controller.IsDestroyed() && !_hasPlayedExplosionSFX)
    {
      float randomPitch = UnityEngine.Random.Range(0.4f, 0.8f);
      AudioManager.Instance.PlaySFX(AudioSFXEnum.MeteorExplosion, randomPitch);
      _hasPlayedExplosionSFX = true;
    }

    _animator.SetInteger("health", _controller.HealthPercentage);
  }

  public void OnDestroyAnimationComplete() => OnExplosionComplete?.Invoke();

  public void Reset()
  {
    _hasPlayedExplosionSFX = false;
  }
}