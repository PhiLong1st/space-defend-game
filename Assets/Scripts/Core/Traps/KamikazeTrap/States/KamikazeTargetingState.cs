using System.Collections;
using UnityEngine;

public class KamikazeTargetingState : IState
{
  private readonly KamikazeTrapController _controller;
  private readonly StateMachine _stateMachine;

  private float _targetTimer;
  private float _thresholdY = 0.5f;

  public KamikazeTargetingState(KamikazeTrapController controller, StateMachine stateMachine)
  {
    _controller = controller;
    _stateMachine = stateMachine;
  }

  public void Enter()
  {
    _controller.StartWarning();
    _targetTimer = GameData.KamikazeTimeToTarget;
  }

  public void Execute()
  {
    _targetTimer -= Time.deltaTime;
    if (_targetTimer <= 0f) _stateMachine.ChangeState<KamikazeAttackState>();

    var spaceshipPosition = SpaceshipController.Instance.transform.position;

    if (Mathf.Abs(spaceshipPosition.y - _controller.transform.position.y) > _thresholdY)
    {
      Vector2 direction = (spaceshipPosition.y - _controller.transform.position.y > 0) ? Vector2.up : Vector2.down;
      _controller.Move(direction * _controller.WarningSpeed * Time.deltaTime);
    }
  }

  public void Exit()
  {
    _controller.StopWarning();
  }
}