using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float leftBound = 5.0f;
    public float rightBound = 5.0f;
    public bool movingLeft = true;
    private float startPosition;

    private void Start()
    {
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
            if (transform.position.x < startPosition - leftBound)
            {
                movingLeft = false;
            }
        }
        else
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
            if (transform.position.x > startPosition + rightBound)
            {
                movingLeft = true;
            }
        }
    }
}
