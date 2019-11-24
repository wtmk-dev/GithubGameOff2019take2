using System;
using System.Collections.Generic;
using UnityEngine;

public class StateHandeler
{
    public Action<State> OnStateChange;

    private List<State> states;
    private Dictionary<string,State> stateDict;

    private State currentState;
    private State previousState;

    public StateHandeler(List<State> states)
    {
        Init();
    }

    private void Init()
    {
        stateDict = new Dictionary<string,State>();
        
        foreach(State state in states)
        {
            stateDict.Add(state.ID, state);
        }

        previousState = null;
        currentState = states[0];
    }

    public void StateChange()
    {
        if(OnStateChange != null)
        {
            previousState = currentState;
            currentState = currentState.nextState;

            OnStateChange(currentState);
        } else { Debug.LogWarning("OnStateChange is null"); }
    }

}