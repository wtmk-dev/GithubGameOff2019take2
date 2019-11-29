using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShield : MonoBehaviour , IDamageable
{
    private PlayerModel model;
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        BasicShot bs = other.gameObject.GetComponent<BasicShot>();

        if(bs == null)
        {
            return;
        }     

        bs.Kill();
        Damage();
    }

    public void SetModel(PlayerModel model)
    {
        this.model = model;
    }

    public void Damage()
    {
        model.ShieldLevel -= 3f;
    }
}
