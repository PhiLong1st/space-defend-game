using UnityEngine;
public abstract class AbstractSliderBarView : MonoBehaviour
{
  public abstract void UpdateView(int currentValue, int maxValue);
}