using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class SpaceshipView : MonoBehaviour
{
  private Animator _animator;
  public AnimationClip _explosionClip;

  [SerializeField] private GameObject _engine;

  public Action OnExplosionFinished;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  public void PlayExplosionEffect()
  {
    StartCoroutine(ExplosionRoutine());
  }

  private IEnumerator ExplosionRoutine()
  {
    _animator.SetTrigger("Explosion");

    if (AudioManager.Instance != null)
    {
      AudioManager.Instance.PlaySFX(AudioSFXEnum.Explosion);
    }

    yield return new WaitForSeconds(_explosionClip.length);

    _engine.SetActive(false);
    this.gameObject.SetActive(false);

    OnExplosionFinished?.Invoke();
  }
}