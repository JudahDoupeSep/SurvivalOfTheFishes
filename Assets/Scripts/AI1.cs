using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI1 : MonoBehaviour
{
    public float Speed = 4f;
    public float StreamWidth = 100f;
    public float SwimDepth = 30f;

    private float lastX = 0;
    private float lastZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = (Random.Range(0, 20) < 1) ? Random.Range(-1, 2) : lastX;

        float zMove = (Random.Range(0, 20) < 1) ? Random.Range(-1, 2) : lastZ;

        lastX = xMove;
        lastZ = zMove;

        float xDelta = Mathf.Max(Mathf.Min(xMove * Speed * Time.deltaTime, StreamWidth - transform.localPosition.x),
            -1 * StreamWidth - transform.localPosition.x);
        float zDelta = Mathf.Max(Mathf.Min(zMove * Speed * Time.deltaTime, SwimDepth - transform.localPosition.z),
            -1 * SwimDepth - transform.localPosition.z);

        Debug.Log(name + ":" + transform.localPosition + " MoveX:" + xMove + " MoveZ:" + zMove);

        transform.localPosition += new Vector3(xDelta, 0, zDelta);
    }
}
