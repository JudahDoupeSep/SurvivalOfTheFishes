using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AI0 : Fish
{

    void Start()
    {
        StartFish();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFish();
        
        float xDelta = Mathf.Max(0, transform.localPosition.x - StreamWidth);
        xDelta += Mathf.Min(0, transform.localPosition.x + StreamWidth);
        float zDelta = Mathf.Max(0, transform.localPosition.z - SwimDepth);
        zDelta += Mathf.Min(0, transform.localPosition.z + SwimDepth);

        //Debug.Log(name +":"+ transform.localPosition + " MoveX:" + xDelta + " MoveZ:" + zDelta);
        Swim(new Vector3(-1*xDelta, 0, -1*zDelta));
    }
}
