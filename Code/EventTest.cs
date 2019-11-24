using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    [SerializeField]
    private List<Event> events;

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        DeregisterEvents();
    }

    void Awake()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        foreach(Event e in events)
        {
            e.OnRaiseEvent += ProcessEvent;
        }
    }

    private void DeregisterEvents()
    {
        foreach(Event e in events)
        {
            e.OnRaiseEvent -= ProcessEvent;
        }
    }

    private void ProcessEvent(Event e)
    {
        switch(e.ID)
        {
            
            default:
            return;
        }
    }
}
