using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// 有限ステートマシン
/// </summary>
/// <typeparam name="T">Mainクラス</typeparam>
/// <typeparam name="U">ステータス定義</typeparam>
public class FiniteStateMachine<T, U>
{
    private T Owner;
    public FSMState<T, U> CurrentState { get; private set; }
    private FSMState<T, U> PreviousState;

    private Dictionary<U, FSMState<T, U>> stateRef;

    public void Awake()
    {
        CurrentState = null;
        PreviousState = null;

    }

    public FiniteStateMachine(T owner)
    {
        Owner = owner;
        stateRef = new Dictionary<U, FSMState<T, U>>();
    }

    public void Update()
    {
        foreach (U nextStateId in CurrentState.NextStateIDs)
        {
            FSMState<T, U> nextState = stateRef[nextStateId];
            if (nextState.CanEnter(CurrentState))
            {
                ChangeState(nextState);
                return;
            }
        }
        if (CurrentState != null) CurrentState.Execute();
    }

    public void ChangeState(FSMState<T, U> NewState)
    {
        PreviousState = CurrentState;

        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = NewState;

        if (CurrentState != null)
            CurrentState.Enter();
    }

    public void RevertToPreviousState()
    {
        if (PreviousState != null)
            ChangeState(PreviousState);
    }

    public FSMState<T, U> RegisterState(FSMState<T, U> state)
    {
        state.RegisterEntity(Owner);
        stateRef.Add(state.StateID, state);
        return state;
    }

    public void UnregisterState(FSMState<T, U> state)
    {
        stateRef.Remove(state.StateID);

    }
};