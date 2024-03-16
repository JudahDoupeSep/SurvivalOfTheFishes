using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float Speed = 4f;
    public float StreamWidth = 100f;
    public float SwimDepth = 30f;
    public GameObject[] Hazards;

    protected float ActiveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ActiveSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Waterfall")
        {
            ActiveSpeed = 0;
            GetComponentInChildren<MeshRenderer>().gameObject.transform.Rotate(-90, 0, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Waterfall")
        {
            ActiveSpeed = Speed;
            GetComponentInChildren<MeshRenderer>().gameObject.transform.Rotate(90, 0, 0);
        }
    }
}
