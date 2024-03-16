using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Fish
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartFish();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFish();

        if (GameManager.State == GameState.Playing)
        {
            float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
            float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

            float xDelta = Mathf.Max(Mathf.Min(xMove * Speed * Time.deltaTime, StreamWidth - transform.localPosition.x),
                -1 * StreamWidth - transform.localPosition.x);
            float zDelta = Mathf.Max(Mathf.Min(zMove * Speed * Time.deltaTime, SwimDepth - transform.localPosition.z),
                -1 * SwimDepth - transform.localPosition.z);
            
           Swim(new Vector3(xDelta, 0, zDelta));
           Debug.Log(xDelta + " " + zDelta);
        }
    }
}
