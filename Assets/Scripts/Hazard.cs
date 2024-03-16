using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    public float FrontOffset;
    public float BackOffset;
    public float LeftOffset;
    public float RightOffset;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.LoseGame();
        }
        else if (collision.gameObject.CompareTag("AI"))
        {
            Hatchery.KillFish(collision.gameObject);
        }
    }
}
