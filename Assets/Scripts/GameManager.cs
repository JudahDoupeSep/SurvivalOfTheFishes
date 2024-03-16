using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        UiManager.ShowTitleScreen();
        UiManager.ShowPrompt();
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
        UiManager.HideAll();
        Hatchery.SpawnFish(10, ArtiFishalIntelligence.Dory);
        Hatchery.SpawnFish(10, ArtiFishalIntelligence.Nemo);
        PlayAreaMovement.StartMoving();
        _state = GameState.Playing;
    }

    public static GameState State => _instance._state;

    public static void LoseGame() => _instance.StartCoroutine(_instance.LoseGameCutscene());

    private void WinGame() => StartCoroutine(WinGameCutscene());

    private IEnumerator LoseGameCutscene()
    {
        _instance._state = GameState.CutScene;
        PlayAreaMovement.StopMoving();
        UiManager.ShowLoseScreen();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private IEnumerator WinGameCutscene()
    {
        _instance._state = GameState.CutScene;
        PlayAreaMovement.StopMoving();
        UiManager.ShowWinScreen();
        yield return new WaitForSeconds(3);
        UiManager.ShowPrompt();
        _instance._state = GameState.WaitingForInput;
    }
}
