using System;
using System.Collections;
using UnityEngine;

class FSM
{
    public delegate IEnumerator StateMethod();
    private MonoBehaviour script;
    private StateMethod[] states;
    private int currentState;

    public FSM(MonoBehaviour script, StateMethod[] states)
    {
        this.states = states;
        this.script = script;
    }

    public void Start(int state = 0)
    {
        script.StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        while (true)
            yield return script.StartCoroutine(states[currentState]());
    }

    public void ChangeState(int nextState)
    {
        currentState = nextState;
    }

    public int CurrentState()
    {
        return currentState;
    }
}

