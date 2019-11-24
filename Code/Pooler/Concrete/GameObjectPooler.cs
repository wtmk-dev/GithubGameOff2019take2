using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameObjectPooler : ScriptableObject
{
    private Queue<GameObject> gameObjectPool;

    public void Init()
    {
        gameObjectPool = new Queue<GameObject>();
    }

    public void SetPoolable(GameObject go)
    {
        go.transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
        go.SetActive(false);
        gameObjectPool.Enqueue(go);
    }

    public GameObject GetPoolable()
    {
        GameObject go = gameObjectPool.Dequeue();
        go.SetActive(true);
        return go;
    }

    public int AmountInPool()
    {
        return gameObjectPool.Count;
    }



}