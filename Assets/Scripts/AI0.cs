using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AI0 : Fish
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float xDelta = Mathf.Max(0, transform.localPosition.x - StreamWidth);
        xDelta += Mathf.Min(0, transform.localPosition.x + StreamWidth);
        float zDelta = Mathf.Max(0, transform.localPosition.z - SwimDepth);
        zDelta += Mathf.Min(0, transform.localPosition.z + SwimDepth);

        //Debug.Log(name +":"+ transform.localPosition + " MoveX:" + xDelta + " MoveZ:" + zDelta);
        transform.localPosition -= new Vector3(xDelta, 0, zDelta);
    }
}
