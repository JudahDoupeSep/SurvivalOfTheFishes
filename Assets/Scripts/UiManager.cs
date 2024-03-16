using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    
    
    public GameObject TitelScreen;
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public GameObject Prompt;
    
    void Awake()
    {
        _instance = this;
    }
    
    public static void HideAll()
    {
        _instance.TitelScreen.SetActive(false);
        _instance.LoseScreen.SetActive(false);
        _instance.WinScreen.SetActive(false);
        _instance.Prompt.SetActive(false);
    }
    
    public static void ShowTitleScreen()
    {
        _instance.TitelScreen.SetActive(true);
    }
    
    public static void ShowWinScreen()
    {
        _instance.WinScreen.SetActive(true);
    }

    public static void ShowPrompt()
    {
        _instance.Prompt.SetActive(true);
    }
    
    public static void ShowLoseScreen()
    {
        _instance.LoseScreen.SetActive(true);
    }

}
