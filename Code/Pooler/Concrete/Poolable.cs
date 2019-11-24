using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour , IPoolable
{
    private GameObjectPooler goPooler;
    public void Init(GameObjectPooler goPooler)
    {
        this.goPooler = goPooler;
    }

    public void Dress(int data)
    {
        
    }

    public void Act()
    {

    }
}
