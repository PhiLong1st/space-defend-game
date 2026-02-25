using System;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
  public event Action OnValueChanged;

  [SerializeField] private int _currentValue;
  [SerializeField] private int _minValue;
  [SerializeField] private int _maxValue;

  public int CurrentValue => _currentValue;
  public int MinValue => _minValue;
  public int MaxValue => _maxValue;

  public void Increment(int amount)
  {
    _currentValue += amount;
    UpdateValue();
  }

  public void Decrement(int amount)
  {
    _currentValue -= amount;
    UpdateValue();
  }

  public void Restore()
  {
    _currentValue = _maxValue;
    UpdateValue();
  }

  public void UpdateValue()
  {
    _currentValue = Mathf.Clamp(_currentValue, _minValue, _maxValue);
    OnValueChanged?.Invoke();
  }
}