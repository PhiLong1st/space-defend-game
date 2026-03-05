using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(AbstractSliderBarView))]
public class SliderBarPresenter : MonoBehaviour
{
  private Slider _slider;
  private AbstractSliderBarView _sliderBarView;

  private void Awake()
  {
    _slider = GetComponent<Slider>();
    _sliderBarView = GetComponent<AbstractSliderBarView>();
  }

  public void HandleOnValueChanged(int currentValue, int maxValue)
  {
    _slider.value = (float)currentValue / (float)maxValue;
    _sliderBarView.UpdateView(currentValue, maxValue);
  }
}