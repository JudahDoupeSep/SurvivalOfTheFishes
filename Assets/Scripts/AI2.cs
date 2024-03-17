using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI2 : Fish
{
    public float Tolerance = .05f;

    void Start()
    {
        StartFish();
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateFish();
        var hazards = FindObjectsOfType<Hazard>();
        var globalFish = transform.parent.TransformPoint(transform.localPosition);
        Debug.Log(name + " Hazards Seen:" + hazards.Length); 
        float xMove = 0; //todo detect obstacles and avoid
        var nextHazard = hazards
            .Where(h => h.BackGlobal > globalFish.z - GetComponentInChildren<CapsuleCollider>().height/2 - Tolerance)
            .OrderBy(h => h.FrontGlobal).FirstOrDefault();

        
        if (nextHazard != null)
        {
            var fishPosition = globalFish.x;
            //Debug.Log(name + " Checking Hazard: " + nextHazard.name + " Location: " + hazardPosition + "From: " + fishPosition);


            if (nextHazard.LeftGlobal + (nextHazard.RightGlobal - nextHazard.LeftGlobal)/2 <= fishPosition && nextHazard.RightGlobal > fishPosition - GetComponentInChildren<CapsuleCollider>().radius - Tolerance)
            {
                xMove = 1;
                //Debug.Log(name + ":" + transform.localPosition + " Avoiding: " + hazardPosition + "From: " + fishPosition);
            }
            else if (nextHazard.LeftGlobal + (nextHazard.RightGlobal - nextHazard.LeftGlobal) / 2 > fishPosition && nextHazard.LeftGlobal < fishPosition + GetComponentInChildren<CapsuleCollider>().radius + Tolerance)
            {
                xMove = -1;
                //Debug.Log(name + ":" + transform.localPosition + " Avoiding: " + hazardPosition + "From: " + fishPosition);
            }
        }

        float zMove = 0;


        float xDelta = Mathf.Max(Mathf.Min(xMove * Speed * Time.deltaTime, StreamWidth - transform.localPosition.x),
            -1 * StreamWidth - transform.localPosition.x);
        float zDelta = Mathf.Max(Mathf.Min(zMove * Speed * Time.deltaTime, SwimDepth - transform.localPosition.z),
            -1 * SwimDepth - transform.localPosition.z);

        //Debug.Log(name + ":" + transform.localPosition + " MoveX:" + xMove + " MoveZ:" + zMove);

        Swim(new Vector3(xDelta, 0, zDelta));
    }

    private class ClosestUpstream : IComparer<GameObject>
    {
        public int Compare(GameObject x, GameObject y)
        {
            return x.transform.position.z.CompareTo(y.transform.position.z);
        }
    }
}
