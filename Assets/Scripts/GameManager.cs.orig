using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WaitingForInput,
    Playing,
    CutScene,
}

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject playArea;

    private GameObject[] aiPlayers;

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            StartGame();
        }

        
=======
    private static GameManager _instance;
    private GameState _state = GameState.WaitingForInput;
    
    public PlayAreaMovement playAreaMovement;

    private void Start()
    {
        _instance = this;
>>>>>>> e2ba3b8 (added win)
    }
    
    private void Update()
    {
        switch (_state)
        {
            case GameState.WaitingForInput:
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    StartGame();
                }
                break;
            case GameState.Playing:
                if (Hatchery.CompetitorCount == 0)
                {
                    WinGame();
                }
                break;
            case GameState.CutScene:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
    
    private void StartGame()
    {
        UiManager.HideTitleScreen();
<<<<<<< HEAD
        playArea.GetComponent<PlayAreaMovement>().started = true;
        aiPlayers = GameObject.FindGameObjectsWithTag("AI");
        Debug.Log("AIs:" + aiPlayers.Length);

        var hazards = GameObject.FindGameObjectsWithTag("Hazard");
        Debug.Log("Hazards:" + hazards.Length);

        foreach (var aiPlayer in aiPlayers)
        {
            aiPlayer.GetComponent<AIPlayer>().Hazards = hazards;
        }


=======
        Hatchery.SpawnFish(10, ArtiFishalIntelligence.Dory);
        Hatchery.SpawnFish(10, ArtiFishalIntelligence.Nemo);
        playAreaMovement.started = true;
        _state = GameState.Playing;
    }

    public static GameState State => _instance._state;

    public static void LoseGame()
    {
        _instance._state = GameState.CutScene;
    }
    
    private void WinGame()
    {
        _instance._state = GameState.CutScene;
>>>>>>> e2ba3b8 (added win)
    }
}
