using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Event : ScriptableObject
{
    public Action<Event> OnRaiseEvent;

    public EventEnum ID;

    public void Raise()
    {
        if( OnRaiseEvent != null )
        {
            OnRaiseEvent(this);
        }
    }
}
