using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{  
    [SerializeField]
    private List<State> states;
    [SerializeField]
    private List<Event> events;

    private StateHandeler stateHandler;

    void OnDisable()
    {
        UnregesterEvents();
    }

    void Awake()
    {
        stateHandler = new StateHandeler(states);

        RegisterEvents();    
    }

    private void ProcessEvent(Event e)
    {
        switch (e.ID)
        {
            default:
            break;
        }
    }

    private void RegisterEvents()
    {
        foreach(Event e in events)
        {
            e.OnRaiseEvent += ProcessEvent;
        }
    }   

    private void UnregesterEvents()
    {
        foreach(Event e in events)
        {
            e.OnRaiseEvent -= ProcessEvent;
        }
    }
}