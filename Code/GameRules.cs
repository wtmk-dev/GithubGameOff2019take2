using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameRules : MonoBehaviour
{
    [SerializeField]
    private float matchLobbyTimeInSeconds = 20f, matchTimerInSeconds = 120f;
    private float currentLobbyTime = 0.0f, currentMatchTimer = 0.0f; 
    [SerializeField]
    private List<State> states;
    private StateHandeler stateHandeler;
    private State nextState;

    [SerializeField]
    private GameObject goLHud, goRHud, goMainHud, goStartHud, goLobbyHud;

    [SerializeField]
    private TextMeshProUGUI readyText,titleText, p1AttackText, p2AttackText, matchStartTimer;
    [SerializeField]
    private Image lShieldMeter, rShieldMeter, lTurnIcon, rTurnIcon;
    [SerializeField]
    private List<Image> lStrikes, rStrikes;
    private Dictionary<UnityEngine.InputSystem.Gamepad,GameObject> players;

    private Timer lobbyTimer;

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

       if(stateHandeler.currentState.ID == "Lobby")
       {
           var elapse = lobbyTimer.RecordTime(Time.fixedDeltaTime);
           int ct = (int) currentLobbyTime - (int) elapse;

           matchStartTimer.text = ":" + ct;
           
           if(lobbyTimer.IsDone())
           {
               nextState = stateHandeler.StateChange();
           }
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
        lobbyTimer = new Timer();
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

            foreach(Image strike in lStrikes)
            {
                strike.gameObject.SetActive(false);
            }

            foreach(Image strike in rStrikes)
            {
                strike.gameObject.SetActive(false);
            }
            break;
            case "Lobby":
            goStartHud.gameObject.SetActive(false);
            readyText.gameObject.SetActive(false);
            titleText.gameObject.SetActive(false);

            goLobbyHud.gameObject.SetActive(true);
            
            goLHud.gameObject.SetActive(true);
            lShieldMeter.gameObject.SetActive(false);
            lTurnIcon.gameObject.SetActive(false);
            
            goRHud.gameObject.SetActive(true);
            rShieldMeter.gameObject.SetActive(false);
            rTurnIcon.gameObject.SetActive(false);
            
            p1AttackText.text = "ATTACK : Disabled";
            p2AttackText.text = "ATTACK : Disabled";
            currentLobbyTime = matchLobbyTimeInSeconds;
            lobbyTimer.SetTimer(matchLobbyTimeInSeconds);
            break;
            case "Main":
            readyText.gameObject.SetActive(false);
            titleText.gameObject.SetActive(false);
            goLobbyHud.gameObject.SetActive(false);

            goStartHud.gameObject.SetActive(true);
            
            goLHud.gameObject.SetActive(true);
            lShieldMeter.gameObject.SetActive(true);
            lTurnIcon.gameObject.SetActive(false);
            
            goRHud.gameObject.SetActive(true);
            rShieldMeter.gameObject.SetActive(true);
            rTurnIcon.gameObject.SetActive(false);

            p1AttackText.text = "ATTACK : READY!";
            p2AttackText.text = "ATTACK : READY!";
            break;
            default:
            return;
        }
    }


}