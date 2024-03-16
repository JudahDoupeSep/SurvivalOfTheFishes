using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI2 : Fish
{
    public float Tolerance = .05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(name + " Hazards Seen:" + Hazards.Length); 
        float xMove = 0; //todo detect obstacles and avoid
        var nextHazard = Hazards
            .Where(h => h.transform.position.z + h.GetComponent<BoxCollider>().size.z/2 > transform.position.z - GetComponentInChildren<CapsuleCollider>().height/2 - Tolerance)
            .OrderBy(h => h.transform.position.z).FirstOrDefault();
        if (nextHazard != null)
        {
            var hazardPosition = nextHazard.transform.position.x;
            var fishPosition = transform.position.x;
            Debug.Log(name + " Checking Hazard: " + nextHazard.name + "Location: " + hazardPosition + "From: " + fishPosition);

            Debug.Log("Hazard Left Edge:" + (hazardPosition - nextHazard.GetComponent<BoxCollider>().size.x / 2));
            Debug.Log("Fish Right Edge:" + (fishPosition + GetComponentInChildren<CapsuleCollider>().radius + Tolerance));
            if (hazardPosition <= fishPosition && hazardPosition + nextHazard.GetComponent<BoxCollider>().size.x / 2 > fishPosition - GetComponentInChildren<CapsuleCollider>().radius - Tolerance)
            {
                xMove = 1;
                Debug.Log(name + ":" + transform.localPosition + " Avoiding: " + hazardPosition + "From: " + fishPosition);
            }
            else if (hazardPosition > fishPosition && hazardPosition - nextHazard.GetComponent<BoxCollider>().size.x / 2 < fishPosition + GetComponentInChildren<CapsuleCollider>().radius + Tolerance)
            {
                xMove = -1;
                Debug.Log(name + ":" + transform.localPosition + " Avoiding: " + hazardPosition + "From: " + fishPosition);
            }
        }

        float zMove = 0;


        float xDelta = Mathf.Max(Mathf.Min(xMove * Speed * Time.deltaTime, StreamWidth - transform.localPosition.x),
            -1 * StreamWidth - transform.localPosition.x);
        float zDelta = Mathf.Max(Mathf.Min(zMove * Speed * Time.deltaTime, SwimDepth - transform.localPosition.z),
            -1 * SwimDepth - transform.localPosition.z);

        //Debug.Log(name + ":" + transform.localPosition + " MoveX:" + xMove + " MoveZ:" + zMove);

        transform.localPosition += new Vector3(xDelta, 0, zDelta);
    }

    private class ClosestUpstream : IComparer<GameObject>
    {
        public int Compare(GameObject x, GameObject y)
        {
            return x.transform.position.z.CompareTo(y.transform.position.z);
        }
    }
}
