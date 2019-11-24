using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler 
{
    private List<Event> events;

    private Dictionary<EventEnum,Event> eventDict;

    public EventHandler(List<Event> events)
    {
        this.events = events;
    }

    private void Init()
    {
        eventDict = new Dictionary<EventEnum, Event>();

        foreach(Event e in events)
        {
            eventDict.Add(e.ID,e);
        }
    }

    public void RaiseEvent(EventEnum id)
    {
        eventDict[id].Raise();
    }
}
