using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    public Vector3 LeftFrontCorner;
    public Vector3 RightFrontCorner;
    public float FrontOffset;
    public float LeftOffset;
    public float RightOffset;

    private void Start()
    {
        LeftFrontCorner = transform.position + new Vector3(LeftOffset, 0, FrontOffset);
        RightFrontCorner = transform.position + new Vector3(RightOffset, 0, FrontOffset);
    }

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
