using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
  protected Dictionary<System.Type, IState> _states = new Dictionary<System.Type, IState>();
  protected IState CurrentState;

  public void AddState(IState state)
  {
    var type = state.GetType();
    if (!_states.ContainsKey(type)) _states.Add(type, state);
  }

  public void Initialize(IState state)
  {
    CurrentState = state;
  }

  public void ChangeState<T>() where T : IState
  {
    var type = typeof(T);
    if (_states.TryGetValue(type, out IState nextState))
    {
      CurrentState?.Exit();
      CurrentState = nextState;
      CurrentState.Enter();
    }
  }

  public void Start()
  {
    if (CurrentState == null) return;
    CurrentState.Enter();
  }

  public void Update()
  {
    if (CurrentState == null) return;
    CurrentState.Execute();
  }
}