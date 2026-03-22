using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(DamageFeedback))]
public class KamikazeTrapView : MonoBehaviour
{
  private Animator _animator;

  public Action OnExplosionComplete;

  [SerializeField] private GameObject _warningPrefab;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  public void PlayTargetAnimation() => _warningPrefab.SetActive(true);

  public void StopTargetAnimation() => _warningPrefab.SetActive(false);

  public void PlayTakeDamageAnimation(int damage)
  {
    GetComponent<DamageFeedback>().PlayHitFlash(damage);
  }

  public void OnExplosionAnimationComplete() => OnExplosionComplete?.Invoke();

  public void PlayExplodeAnimation()
  {
    _animator.SetTrigger("explode");
  }
}