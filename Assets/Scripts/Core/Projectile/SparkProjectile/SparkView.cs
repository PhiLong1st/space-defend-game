using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class SparkView : MonoBehaviour
{
  [SerializeField] private GameObject _launchEffect;

  [SerializeField] private GameObject _flightEffect;

  [SerializeField] private GameObject _explosionEffect;

  public void PlayLaunchEffect()
  {
    _launchEffect?.SetActive(true);
    _flightEffect?.SetActive(false);
    _explosionEffect?.SetActive(false);

    if (AudioManager.Instance != null)
    {
      var randomPitch = UnityEngine.Random.Range(0.05f, 0.07f);
      AudioManager.Instance.PlaySFX(AudioSFXEnum.SparkLaunch, randomPitch);
    }
  }

  public void PlayFlightEffect()
  {
    _launchEffect?.SetActive(false);
    _flightEffect?.SetActive(true);
    _explosionEffect?.SetActive(false);
  }

  public void PlayExplosionEffect()
  {
    _launchEffect?.SetActive(false);
    _flightEffect?.SetActive(false);
    _explosionEffect?.SetActive(true);
  }

  public void Reset()
  {
    _launchEffect?.SetActive(false);
    _flightEffect?.SetActive(false);
    _explosionEffect?.SetActive(false);
  }
}