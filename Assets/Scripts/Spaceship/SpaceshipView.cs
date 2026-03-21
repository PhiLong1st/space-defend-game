using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class SpaceshipView : MonoBehaviour
{
  [SerializeField] private GameObject _engine;
  [SerializeField] private GameObject[] _shipViewLevelPrefabs;

  private Animator _animator;
  public AnimationClip _explosionClip;

  private GameObject _currentView;

  public Action OnExplosionFinished;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  public void UpdateShipView(int level)
  {
    level = Mathf.Min(level, _shipViewLevelPrefabs.Length);
    if (_currentView != null) Destroy(_currentView);

    GameObject shipView = Instantiate(_shipViewLevelPrefabs[level - 1], transform.position, transform.rotation, transform);
    shipView.SetActive(true);

    _currentView = shipView;
  }

  private void ShowEngine(bool show) => _engine.SetActive(show);

  public void ShowShip(bool show)
  {
    _currentView?.SetActive(show);
    _engine?.SetActive(show);
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
    OnExplosionFinished?.Invoke();
  }
}