using System.Collections;
using UnityEngine;

public class KamikazeTargetingState : IState
{
  private readonly KamikazeTrapController _controller;
  private readonly StateMachine _stateMachine;

  private float _targetTimer;
  private float _thresholdY = 0.5f;

  private GameObject _spaceship;

  public KamikazeTargetingState(KamikazeTrapController controller, StateMachine stateMachine)
  {
    _controller = controller;
    _stateMachine = stateMachine;
  }

  public void Enter()
  {
    if (_spaceship == null) _spaceship = GameObject.FindGameObjectWithTag("Player");

    _controller.StartWarning();
    _targetTimer = GameData.KamikazeTimeToTarget;
  }

  public void Execute()
  {
    _targetTimer -= Time.deltaTime;
    if (_targetTimer <= 0f) _stateMachine.ChangeState<KamikazeAttackState>();

    if (Mathf.Abs(_spaceship.transform.position.y - _controller.transform.position.y) > _thresholdY)
    {
      Vector2 direction = (_spaceship.transform.position.y - _controller.transform.position.y > 0) ? Vector2.up : Vector2.down;
      _controller.Move(direction);
    }
  }

  public void Exit()
  {
    _controller.StopWarning();
  }
}