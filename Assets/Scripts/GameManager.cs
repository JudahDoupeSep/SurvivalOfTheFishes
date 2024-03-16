using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayAreaMovement playAreaMovement;
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
        playAreaMovement.started = true;
    }
}
