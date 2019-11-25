using System;
using System.Collections.Generic;
using UnityEngine;

public class StateHandeler
{
    public Action<State> OnStateChange;

    private List<State> states;
    private Dictionary<string,State> stateDict;

    public State currentState;
    private State previousState;

    public StateHandeler(List<State> states, State startingState)
    {
        states = new List<State>();
        previousState = null;
        currentState = startingState;
        this.states = states;
        Init();
    }

    private void Init()
    {
        stateDict = new Dictionary<string,State>();
        
        foreach(State state in states)
        {
            stateDict.Add(state.ID, state);
        }

    }

    public State StateChange()
    {
        if(OnStateChange != null)
        {
            previousState = currentState;
            currentState = currentState.nextState;

            OnStateChange(currentState);
            return currentState.nextState;
        } else { Debug.LogWarning("OnStateChange is null"); return null; }
    }

}