using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{
    [SerializeField]
    private List<State> states;
    private StateHandeler stateHandeler;
    [SerializeField]
    private State startingState;
    private State nextState;

    private GameRules gr;

    [SerializeField]
    private GameObject player, gameRulesPrefab;
    private List<GameObject> goPlayerList;
    private Dictionary<UnityEngine.InputSystem.Gamepad,GameObject> players;

    private EventHandler eventHandler;

    [SerializeField]
    private List<Vector3> playersStartingPos;
    [SerializeField]
    private List<float> playersRotation;
    [SerializeField]
    private List<Sprite> playerSkins, playerPrimaryFire, playerShield;
    [SerializeField]
    private List<GameObjectPooler> primaryFirePools;

    void OnDisable()
    {
        DeregisterEvent();
    }
    

    void Awake()
    {
        goPlayerList = new List<GameObject>();
        players = new Dictionary<Gamepad, GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        stateHandeler = new StateHandeler(states,startingState);
        RegisterEvents();

        gr = gameRulesPrefab.GetComponent<GameRules>();
        gr.Init(stateHandeler);
    }

    void Update()
    {
        if(stateHandeler == null)
        {
            return;
        }
    }

    private void BuildGame()
    {
        for(int i = 0; i < Gamepad.all.Count; i++)
        {
            GameObject clone = Instantiate(player,playersStartingPos[i],Quaternion.identity);
            PlayerKit ipg = clone.GetComponent<PlayerKit>();

            ipg.SetGamePad(Gamepad.all[i],playersRotation[i]);
            players.Add(Gamepad.all[i],clone);
            goPlayerList.Add(clone);

            ipg.primaryFireSprite = playerPrimaryFire[i];
            ipg.SetPrimaryFirePool(primaryFirePools[i]);

            Skinner knife = clone.GetComponent<Skinner>();
            List<Sprite> sprites = new List<Sprite> { playerSkins[i], playerShield[i] };
            knife.Init(sprites);

            clone.SetActive(false);
        }

        gr.SetPlayers(players);
    }

    private void RegisterEvents()
    {
        stateHandeler.OnStateChange += HandleState;
    }

    private void DeregisterEvent()
    {
        stateHandeler.OnStateChange -= HandleState;
    }

    private void HandleState(State state)
    {
        
        if(!states.Contains(state))
        {
            return;
        }

        switch(state.ID)
        {
            case "Start":
            BuildGame();
            break;
            case "Main":
            Debug.Log("Battle TIME!");
            break;
            default:
            return;
        }
    }

}
