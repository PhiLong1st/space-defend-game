using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SparkView : MonoBehaviour
{
  private Animator _animator;

  public Action OnExplosionFinished;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  public void PlayReadyEffect()
  {
    // Implementation for playing ready effect
  }

  public void PlayLaunchEffect()
  {
    // Implementation for playing launch effect
  }

  public void PlayExplosionEffect() => _animator.SetTrigger("explode");

  public void OnExplosionAnimationComplete() => OnExplosionFinished?.Invoke();
}