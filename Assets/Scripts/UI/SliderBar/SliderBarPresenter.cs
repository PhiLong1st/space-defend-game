using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarPresenter : MonoBehaviour
{
  [SerializeField] SliderBar sliderBar;
  [SerializeField] Slider slider;

  private void Awake()
  {
    if (sliderBar == null)
    {
      Debug.LogError("SliderBar is not assigned in the inspector.");
      return;
    }

    sliderBar.OnValueChanged += OnValueChanged;
  }

  private void Start()
  {
    UpdateView();

    sliderBar.Restore();
    Debug.Log($"{sliderBar.Name} initialized with CurrentValue: {sliderBar.CurrentValue}, MaxValue: {sliderBar.MaxValue}");
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
    sliderBar.Decrement(amount);
  }

  public void Increment(int amount)
  {
    sliderBar.Increment(amount);
  }

  public void UpdateView()
  {
    if (slider != null && sliderBar.MaxValue != 0)
    {
      slider.value = (float)sliderBar.CurrentValue / (float)sliderBar.MaxValue;
    }
  }

  public void SetMaxValue(int maxValue)
  {
    sliderBar.SetMaxValue(maxValue);
  }

  public void OnValueChanged()
  {
    UpdateView();
    Debug.Log($"{sliderBar.Name} value changed: {sliderBar.CurrentValue}");
  }
}