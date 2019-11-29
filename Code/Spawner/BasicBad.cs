using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBad : MonoBehaviour
{
    private List<PlayerKit> playerKits;

    private float speed = 2f;

   public void Init(List<PlayerKit> playerKits)
   {
       Debug.Log(playerKits.Count);
       this.playerKits = playerKits;
   }

   void Update()
   {
       Debug.Log(playerKits.Count);
       if(playerKits.Count < 1)
       {
           return;
       }

       foreach(PlayerKit player in playerKits)
       {
           if(player.IsActiveTeam())
           {
               Debug.Log("player is active");
               FollowPlayer(player);
           }
       }
   }

   private void FollowPlayer(PlayerKit player)
   {
       float step = speed * Time.fixedDeltaTime;
       gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,player.GetTransform().position,step);
   }
}
