using UnityEngine;
using System.Collections;

public class KamikazeAttackState : IState
{
  private readonly KamikazeTrapController _controller;
  private readonly StateMachine _stateMachine;

  public KamikazeAttackState(KamikazeTrapController controller, StateMachine stateMachine)
  {
    _controller = controller;
    _stateMachine = stateMachine;
  }

  public void Enter()
  {
    if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(AudioSFXEnum.KamikazeEngine);
    Debug.Log("KamikazeTrap entered Attack State");
  }

  public void Execute()
  {
    _controller.Move(Vector2.left);
  }

  public void Exit()
  {
    Debug.Log("KamikazeTrap exited Attack State");
  }
}