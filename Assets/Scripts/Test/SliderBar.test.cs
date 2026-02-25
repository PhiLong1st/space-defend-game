using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarTest : MonoBehaviour
{
  [SerializeField] private SliderBarPresenter healthbar;
  [SerializeField] private SliderBarPresenter staminaBar;

  private void Update()
  {
    staminaBar.Increment(1);
    healthbar.Increment(2);
  }
}