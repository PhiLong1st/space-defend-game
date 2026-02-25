using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarPresenter : MonoBehaviour
{
  [SerializeField] SliderBar sliderBar;
  [SerializeField] Slider slider;

  private void Start()
  {
    if (sliderBar == null)
    {
      Debug.LogError("SliderBar is not assigned in the inspector.");
      return;
    }

    sliderBar.OnValueChanged += OnValueChanged;
    UpdateView();

    Debug.Log("SliderBarPresenter initialized.");
  }

  private void OnDestroy()
  {
    if (sliderBar == null)
    {
      Debug.LogError("SliderBar is not assigned in the inspector.");
      return;
    }

    sliderBar.OnValueChanged -= OnValueChanged;
  }

  public void Decrement(int amount)
  {
    sliderBar?.Decrement(amount);
  }

  public void Increment(int amount)
  {
    sliderBar?.Increment(amount);
  }

  public void Reset()
  {
    sliderBar?.Restore();
  }

  public void UpdateView()
  {
    if (sliderBar == null)
      return;

    if (slider != null && sliderBar.MaxValue != 0)
    {
      slider.value = (float)sliderBar.CurrentValue / (float)sliderBar.MaxValue;
    }
  }

  public void OnValueChanged()
  {
    UpdateView();
    Debug.Log($"SliderBar value changed: {sliderBar.CurrentValue}");
  }
}