using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameRules : MonoBehaviour
{ 
    [SerializeField]
    private List<State> states;
    private StateHandeler stateHandeler;
    private State nextState;

    [SerializeField]
    private GameObject goLHud, goRHud, goMainHud, goStartHud, goLobbyHud;

    [SerializeField]
    private TextMeshProUGUI readyText,titleText, p1AttackText, p2AttackText;
    private Dictionary<UnityEngine.InputSystem.Gamepad,GameObject> players;

    private bool allPlayersReady = false;
    private bool player1Ready = false;
    private bool player2Ready = false;

    void OnDisable()
    {
        DeregisterEvent();
    
    }

    void Update()
    {
       if(stateHandeler == null)
       {
           return;
       }

       if(stateHandeler.currentState.ID == "Start")
       {
           CheckForPlayers();
       }

    }

    private void CheckForPlayers()
    {
        if(!allPlayersReady && Gamepad.all.Count > 2)
        {
            readyText.text = "Connect another controller . . . ";
        } else if(!allPlayersReady) {
            if(Gamepad.all.Count == 2)
            {
                readyText.text = "- PRESS START -";
                
                var p1Start = Gamepad.all[0].startButton.ReadValue() > 0 ? true : false;
                var p2Start = Gamepad.all[1].startButton.ReadValue() > 0 ? true : false;

                if(p1Start)
                {
                    player1Ready = true;
                }

                if(p2Start)
                {
                    player2Ready = true;
                }
        
                players[Gamepad.all[0]].SetActive(player1Ready);
                players[Gamepad.all[1]].SetActive(player2Ready);

                if(player1Ready && player2Ready)
                {
                    nextState = stateHandeler.StateChange();
                }
            }
        }
    }

    public void Init(StateHandeler stateHandeler)
    {
        players = new Dictionary<Gamepad, GameObject>();
        this.stateHandeler = stateHandeler;
        Debug.Log(stateHandeler);
        RegisterEvents();

        nextState = stateHandeler.StateChange();
    }

    public void SetPlayers(Dictionary<UnityEngine.InputSystem.Gamepad,GameObject> players)
    {
        this.players = players;
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
            allPlayersReady = false;
            goStartHud.gameObject.SetActive(true);
            readyText.gameObject.SetActive(true);
            titleText.gameObject.SetActive(true);

            goLHud.gameObject.SetActive(false);
            goRHud.gameObject.SetActive(false);
            goMainHud.gameObject.SetActive(false);
            goLobbyHud.gameObject.SetActive(false);
            break;
            case "Lobby":
            goStartHud.gameObject.SetActive(false);
            readyText.gameObject.SetActive(false);
            titleText.gameObject.SetActive(false);
            goLHud.gameObject.SetActive(false);
            goRHud.gameObject.SetActive(false);
            
            goLobbyHud.gameObject.SetActive(true);
            goLHud.gameObject.SetActive(true);
            goRHud.gameObject.SetActive(true);
            p1AttackText.text = "Attack : Safty ON";
            p2AttackText.text = "Attack : Safty ON";
            break;
            default:
            return;
        }
    }
}