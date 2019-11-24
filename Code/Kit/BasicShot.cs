﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour
{
   public List<Event> events;

   [SerializeField]
   private float maxTimeAlive;
   private float timeAlive;

   public GameObjectPooler pooler;

   void OnEnable()
   {
       timeAlive = 0;

       foreach(Event e in events)
       {
           e.OnRaiseEvent += HandleEvent;
       }
   }

   void Update()
   {
       timeAlive += Time.deltaTime;
       if(timeAlive > maxTimeAlive)
       {    
           Kill();
       }

   }

   public void Kill()
   {
        pooler.SetPoolable(this.gameObject);
   }

   private void HandleEvent(Event e)
   {
       switch(e.ID)
       {
            case EventEnum.OnCollisionDestroy:
            Kill();
            break;
            default:
            return;
       }
   }
}
