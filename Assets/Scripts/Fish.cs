using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float Speed = 4f;
    public float StreamWidth = 100f;
    public float SwimDepth = 30f;
    public GameObject[] Hazards;
    protected void StartFish()
    {
        UpdateAnimationSpeed(Random.Range(0.5f, 1));
    }

    protected void UpdateFish()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Waterfall")
        {
            GetComponentInChildren<MeshRenderer>().gameObject.transform.Rotate(-90, 0, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Waterfall")
        {
            GetComponentInChildren<MeshRenderer>().gameObject.transform.Rotate(90, 0, 0);
        }
    }

    public void UpdateAnimationSpeed(float speed)
    {
        GetComponentInChildren<Animator>().SetFloat("SwimSpeed", speed);
    }

    protected void Swim(Vector3 direction)
    {
        transform.localPosition += direction;
    }
}
