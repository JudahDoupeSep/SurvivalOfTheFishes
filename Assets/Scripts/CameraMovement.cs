using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool started = false;
    public float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            transform.position += speed * Vector3.forward * Time.deltaTime;
        }
    }
}
