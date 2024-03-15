using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    
    
    public GameObject TitelScreen;
    
    void Start()
    {
        _instance = this;
    }
    
    public static void ShowTitleScreen()
    {
        _instance.TitelScreen.SetActive(true);
    }

    public static void HideTitleScreen()
    {
        _instance.TitelScreen.SetActive(false);
    }
}
