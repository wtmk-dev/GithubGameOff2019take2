using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRig : MonoBehaviour
{
    private PlayerModel model;
    private Timer damageTimer;
    private int id;

    void Awake()
    {
        damageTimer = new Timer();
    }

    public void SetModel(PlayerModel model)
    {
        this.model = model;
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("doood im heet!!");
        Debug.Log(other);

        BasicShot shot = other.gameObject.GetComponent<BasicShot>();

        if(shot == null || shot.id == id)
        {
            return;
        }

        if(model.CanTakeDamage && model.LifeLevel > 0f)
        {
            model.LifeLevel -= 25f;
            model.CanTakeDamage = false;

            if( model.LifeLevel <= 0)
            {
                model.IsAlive = false;
            }
        }
    }

    void Update()
    {
        if(model == null)
        {
            return;
        }

        if(!model.CanTakeDamage)
        {
            Debug.Log("i cant take damage");
            if(!damageTimer.IsLocked())
            {
                damageTimer.SetTimer(3f);
                damageTimer.SetLock(true);
            }

            if(!damageTimer.IsDone())
            {
                damageTimer.RecordTime(Time.fixedDeltaTime);
            }

            if(damageTimer.IsDone())
            {   
                Debug.Log("i can take damage");
                model.CanTakeDamage = true;
                damageTimer.SetLock(false);
            }
        }
    }

}
