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

    private static GameManager _instance;
    private GameState _state = GameState.WaitingForInput;
    
    public PlayAreaMovement playAreaMovement;

    private void Start()
    {
        _instance = this;
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
    }
}
