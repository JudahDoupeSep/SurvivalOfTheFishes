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
    private GameState _state = GameState.CutScene;
    private int _currentLevel = 0;
    
    public PlayAreaMovement playAreaMovement;

    private void Start()
    {
        _instance = this;
        SetupLevel();
        UiManager.ShowTitleScreen();
        Hatchery.AnimateSpawn();
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
        Hatchery.HatchFish();
        PlayAreaMovement.StartMoving();
        _state = GameState.Playing;
    }

    public static GameState State => _instance._state;

    public static void LoseGame() => _instance.StartCoroutine(_instance.LoseGameCutscene());

    private void WinGame() => StartCoroutine(WinGameCutscene());

    private IEnumerator LoseGameCutscene()
    {
        _instance._state = GameState.CutScene;
        UiManager.ShowLoseScreen();
        Camera.main.transform.parent = null;
        Destroy(FindObjectOfType<PlayerController>().gameObject);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private IEnumerator WinGameCutscene()
    {
        _instance._state = GameState.CutScene;
        UiManager.ShowWinScreen();
        var breedingGroundPosition = StreamGeneration.SpawnBreadingGrounds();
        var player = FindObjectOfType<PlayerController>();
        foreach (var hazard in FindObjectsOfType<Hazard>())
        {
            Destroy(hazard.gameObject);
        }
        
        while (player.transform.position.z < breedingGroundPosition.z)
        {
            yield return new WaitForEndOfFrame();
        }
        
        PlayAreaMovement.StopMoving();
        
        SetupLevel();
        
        Hatchery.AnimateSpawn();
    }

    public static void WaitForInput()
    {
        UiManager.ShowPrompt();
        _instance._state = GameState.WaitingForInput;
    }

    private void SetupLevel()
    {
        switch (_currentLevel)
        {
            case 0:
                Hatchery.SpawnFish(10, ArtiFishalIntelligence.Nemo);
                break;
            case 1:
                Hatchery.SpawnFish(10, ArtiFishalIntelligence.Nemo);
                Hatchery.SpawnFish(5, ArtiFishalIntelligence.Marlin);
                break;
            case 2:
                Hatchery.SpawnFish(10, ArtiFishalIntelligence.Nemo);
                Hatchery.SpawnFish(10, ArtiFishalIntelligence.Marlin);
                break;
            case 3:
                Hatchery.SpawnFish(10, ArtiFishalIntelligence.Nemo);
                Hatchery.SpawnFish(20, ArtiFishalIntelligence.Marlin);
                break;
            default:
                Hatchery.SpawnFish(25, ArtiFishalIntelligence.Nemo);
                Hatchery.SpawnFish(25, ArtiFishalIntelligence.Marlin);
                break;
        }

        _currentLevel++;
    }
}
