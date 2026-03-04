using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
  public event Action OnValueChanged;

  [SerializeField] private int _currentValue;
  [SerializeField] private int _maxValue;
  [SerializeField] private string _name;

  public string Name => _name;

  public int CurrentValue => _currentValue;
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
    _currentValue = Mathf.Clamp(_currentValue, 0, _maxValue);
    OnValueChanged?.Invoke();
  }

  public void SetMaxValue(int maxValue)
  {
    _maxValue = maxValue;
  }
}