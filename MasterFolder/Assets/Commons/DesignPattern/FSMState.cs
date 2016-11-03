using System;
using System.Collections.Generic;

public class FSMState<T, U>
{

    protected T entity;

    public void RegisterEntity(T entity)
    {
        this.entity = entity;
    }

    virtual public U StateID
    {
        get
        {
            throw new ArgumentException("State ID not spicified in child class");
        }
    }

    virtual public List<U> NextStateIDs
    {
        get
        {
            throw new ArgumentException("Next State ID List not spicified in child class");
        }
    }

    virtual public bool CanEnter(FSMState<T, U> currentState)
    {
        return false;
    }

    virtual public void Enter() { }

    virtual public void Execute() { }

    virtual public void Exit() { }
}