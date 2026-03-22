using UnityEngine;
using System;

public class MeteorView : MonoBehaviour
{
  private MeteorController _controller;
  private Animator _animator;

  public Action OnExplosionComplete;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _controller = GetComponentInParent<MeteorController>();
  }

  public void PlayTakeDamageAnimation(int damage)
  {
    if (_controller.IsDestroyed()) return;
    GetComponent<DamageFeedback>()?.PlayHitFlash(damage);
  }

  private void Update()
  {
    _animator.SetInteger("health", _controller.HealthPercentage);
  }

  public void OnDestroyAnimationComplete() => OnExplosionComplete?.Invoke();
}