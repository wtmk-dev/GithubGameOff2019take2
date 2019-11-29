using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private List<State> states;

    private List<GameObject> spawns;

    [SerializeField]
    private List<GameObject> spawnPoints;
    
    private GameObjectPooler gameObjectPooler;

    private StateHandeler stateHandeler;

    private List<PlayerKit> playerKits;

    private Timer spawnTimer;

    void OnDisable()
    {
        DeregisterEvent();
    }

    void Awake()
    {
        gameObjectPooler = new GameObjectPooler();
        spawns = new List<GameObject>();
        playerKits = new List<PlayerKit>();
        spawnTimer = new Timer();
    }

    void Update()
    {
        if(stateHandeler == null)
        {
            return;
        }

        if(stateHandeler.currentState.ID == "Main")
        {
            if(!spawnTimer.IsLocked())
            {
                spawnTimer.SetLock(true);
                spawnTimer.SetTimer(10f);
            }

            if(spawnTimer.IsLocked())
            {
                spawnTimer.RecordTime(Time.fixedDeltaTime);
            }

            if(spawnTimer.IsDone())
            {
                spawnTimer.SetLock(false);
                Spawn(1);
            }
        }
    }

    public void Init(StateHandeler stateHandeler, List<PlayerKit> playerKits)
    {
       this.stateHandeler = stateHandeler;
       this.playerKits = playerKits;

       LoadPool();
       RegisterEvents();
    }

    private void LoadPool()
    {
        gameObjectPooler.Init();

        for(int i = 0; i < poolSize; i++)
        {
            GameObject clone = Instantiate(spawn);
            BasicBad bb = clone.GetComponent<BasicBad>();

            bb.Init(playerKits);
            gameObjectPooler.SetPoolable(clone);
        }
    }

    private void Spawn(int round)
    {
        var point = 0;
        Debug.Log("is spawning");
        for(int i = 0; i < 10 * round; i++)
        {
            GameObject go = gameObjectPooler.GetPoolable();

            go.SetActive(true);
            
            switch(point)
            {
                case 0:
                go.transform.position = spawnPoints[0].transform.position;
                break;
                case 1:
                go.transform.position = spawnPoints[0].transform.position;
                break;
                case 2:
                go.transform.position = spawnPoints[0].transform.position;
                break;
                case 4:
                go.transform.position = spawnPoints[0].transform.position;
                break;
            }

            // if(point <)
            
            
            spawns.Add(go);
        }
    }

    private void RegisterEvents()
    {
        if(stateHandeler != null)
        {
            stateHandeler.OnStateChange += HandleState;
        }
    }

    private void DeregisterEvent()
    {
        if(stateHandeler != null)
        {
            stateHandeler.OnStateChange -= HandleState;
        }
    }

    private void HandleState(State state)
    {
        
        if(!states.Contains(state))
        {
            return;
        }

        switch(state.ID)
        {
            case "Init":
            Debug.Log("State Change: Init");
            break;
            case "Start":
            Debug.Log("State Change: Start");
            break;
            case "Main":
            Spawn(1);
            Debug.Log("State Change: Main");
            break;
            default:
            return;
        }
    }
}
