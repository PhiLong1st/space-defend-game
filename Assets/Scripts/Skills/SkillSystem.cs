using System;
using UnityEngine;
public class SkillSystem : MonoBehaviour
{
  [SerializeField] private AbstractSkill _skillQ;
  [SerializeField] private AbstractSkill _skillE;
  [SerializeField] private AbstractSkill _skillR;
  [SerializeField] private AbstractSkill _skillSpace;

  public void HandleInput()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      _skillQ.Activate();
    }
    if (Input.GetKeyDown(KeyCode.E))
    {
      _skillE.Activate();
    }
    if (Input.GetKeyDown(KeyCode.R))
    {
      _skillR.Activate();
    }
    if (Input.GetKeyDown(KeyCode.Space))
    {
      _skillSpace.Activate();
    }
  }

  public void HandleOnStaminaChanged(int newStamina)
  {
    // UpdateSkillStates();
  }
}