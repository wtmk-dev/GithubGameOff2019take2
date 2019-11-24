using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShield : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        BasicShot bs = other.gameObject.GetComponent<BasicShot>();

        if(bs == null)
        {
            return;
        }     

        bs.Kill();
    }
}
