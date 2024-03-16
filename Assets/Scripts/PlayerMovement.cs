using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 4f;
    public float StreamWidth = 100f;
    public float SwimDepth = 30f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

        float xDelta = Mathf.Max(Mathf.Min(xMove * Speed * Time.deltaTime, StreamWidth - transform.localPosition.x),
            -1 * StreamWidth - transform.localPosition.x);
        float zDelta = Mathf.Max(Mathf.Min(zMove * Speed * Time.deltaTime, SwimDepth - transform.localPosition.z),
            -1 * SwimDepth - transform.localPosition.z);

<<<<<<< HEAD
        //Debug.Log(name + ":" + transform.localPosition);
=======
>>>>>>> e2ba3b8 (added win)
        transform.localPosition += new Vector3(xDelta, 0, zDelta);

    }
}
