using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playArea;

    private GameObject[] aiPlayers;

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            StartGame();
        }

        
    }
    
    void StartGame()
    {
        UiManager.HideTitleScreen();
        playArea.GetComponent<PlayAreaMovement>().started = true;
        aiPlayers = GameObject.FindGameObjectsWithTag("AI");
        Debug.Log("AIs:" + aiPlayers.Length);

        var hazards = GameObject.FindGameObjectsWithTag("Hazard");
        Debug.Log("Hazards:" + hazards.Length);

        foreach (var aiPlayer in aiPlayers)
        {
            aiPlayer.GetComponent<AIPlayer>().Hazards = hazards;
        }


    }
}
