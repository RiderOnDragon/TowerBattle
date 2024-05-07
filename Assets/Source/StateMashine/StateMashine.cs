using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMashine
{
    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    private State _currentState;

    public void AddState(State state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T: State
    {
        var type = typeof(T);

        if (_currentState != null && _currentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var state) == true)
        {
            _currentState?.Exit();

            _currentState = state;

            _currentState.Enter();
        }
        else
        {
            throw new NullReferenceException();
        }
    }

    public void Update()
    {
        _currentState.Update();
    }
}
