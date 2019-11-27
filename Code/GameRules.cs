using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameRules : MonoBehaviour
{
    [SerializeField]
    private float matchLobbyTimeInSeconds = 20f, matchStartTimer = 5f, matchTimerInSeconds = 120f;
    private float currentLobbyTime = 0.0f, currentMatchTimer = 0.0f, currentMathcStartTime = 0.0f; 
    [SerializeField]
    private List<State> states;
    private StateHandeler stateHandeler;
    private State nextState;

    [SerializeField]
    private GameObject goLHud, goRHud, goMainHud, 
                       goStartHud, goLobbyHud, goLaunchSprite, 
                       goSouthText;

    [SerializeField]
    private TextMeshProUGUI readyText,titleText, p1AttackText, p2AttackText, launchText, lobbyTimerText, matchTimerText;
    [SerializeField]
    private Image lShieldMeter, rShieldMeter, lTurnIcon, rTurnIcon;
    [SerializeField]
    private List<Image> lStrikes, rStrikes;
    private Dictionary<UnityEngine.InputSystem.Gamepad,GameObject> players;

    private Timer lobbyTimer, gameStartTimer, matchTimer;

    private List<PlayerKit> playerKits;
    private PlayerKit activePlayer, player1, player2;

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

           lobbyTimerText.text = ":" + ct;
           
           if(lobbyTimer.IsDone())
           {
               nextState = stateHandeler.StateChange();
           }
       }

       if(stateHandeler.currentState.ID == "Main")
       {
           if(!gameStartTimer.IsDone())
           {
               var elapse = gameStartTimer.RecordTime(Time.fixedDeltaTime);
               int ct = (int) currentMathcStartTime - (int) elapse;
               launchText.text = ":" + ct;
           }
           
           if(gameStartTimer.IsDone() && !allPlayersReady)
           {
               allPlayersReady = true;
                player1Ready = true;
                player2Ready= true;

               var roll = Random.Range(0,1);
               Debug.Log(roll); 
               UnityEngine.InputSystem.Gamepad ag = roll > .5f ? Gamepad.all[0] : Gamepad.all[1];
               SetActivePlayer(ag);
               StartMatch();
           }

           var p1South = Gamepad.all[0].buttonSouth.ReadValue() > 0 ? true : false;
           var p2South = Gamepad.all[1].buttonSouth.ReadValue() > 0 ? true : false;

            if(p1South && !allPlayersReady)
            {
                allPlayersReady = true;
                player1Ready = true;
                player2Ready= true;

                SetActivePlayer(Gamepad.all[0]);
                StartMatch();
            }

            if(p2South && !allPlayersReady)
            {
                allPlayersReady = true;
                player2Ready = true;
                player2Ready= true;

                SetActivePlayer(Gamepad.all[1]);
                StartMatch();
            }

            UpdatePlayerStats();
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
        
                GameObject p1 = players[Gamepad.all[0]];
                GameObject p2 = players[Gamepad.all[1]];

                p1.SetActive(player1Ready);
                p2.SetActive(player2Ready);

                if(player1Ready && player2Ready)
                {
                    player1 = p1.GetComponent<PlayerKit>();
                    player2 = p2.GetComponent<PlayerKit>();
                    
                    nextState = stateHandeler.StateChange();
                }
            }
        }
    }

    private void SetActivePlayer(UnityEngine.InputSystem.Gamepad gamepad)
    {
        activePlayer = players[gamepad].GetComponent<PlayerKit>();
    }

    private void StartMatch()
    {
        foreach(PlayerKit player in playerKits)
        {
            player.isActive = true;
        }

        goLaunchSprite.SetActive(false);
        goSouthText.SetActive(false);
        launchText.gameObject.SetActive(false);
    }

    private void UpdatePlayerStats()
    {
        Debug.Log(player1.id);
    }

    public void Init(StateHandeler stateHandeler)
    {
        lobbyTimer = new Timer();
        gameStartTimer = new Timer();
        matchTimer = new Timer();

        players = new Dictionary<Gamepad, GameObject>();
        playerKits = new List<PlayerKit>();

        this.stateHandeler = stateHandeler;
        Debug.Log(stateHandeler);
        RegisterEvents();

        nextState = stateHandeler.StateChange();
    }

    public void SetPlayers(Dictionary<UnityEngine.InputSystem.Gamepad,GameObject> players)
    {
        this.players = players;
        foreach(KeyValuePair<UnityEngine.InputSystem.Gamepad,GameObject> player in players)
        {
            playerKits.Add(player.Value.GetComponent<PlayerKit>());
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

            allPlayersReady = false;
            player1Ready = false;
            player2Ready = false;

            goMainHud.SetActive(true);
            launchText.gameObject.SetActive(true);
            goLaunchSprite.SetActive(true);
            goSouthText.SetActive(true);
            currentMathcStartTime = matchStartTimer;
            gameStartTimer.SetTimer(matchStartTimer);
            break;
            default:
            return;
        }
    }
}