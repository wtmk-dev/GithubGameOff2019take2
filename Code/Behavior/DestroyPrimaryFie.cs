using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrimaryFie : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) 
    {
        BasicShot bs = other.gameObject.GetComponent<BasicShot>();

        if(bs == null)
        {
           return;
        }

        bs.Kill();
    }
}
