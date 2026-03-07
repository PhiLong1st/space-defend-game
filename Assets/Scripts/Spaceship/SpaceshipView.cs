using UnityEngine;

public class SpaceshipView : MonoBehaviour
{
  [SerializeField] private Animator _engineAnimator;

  public void EnterBoost()
  {
    _engineAnimator.SetBool("isBoosting", true);
  }

  public void ExitBoost()
  {
    _engineAnimator.SetBool("isBoosting", false);
  }
}