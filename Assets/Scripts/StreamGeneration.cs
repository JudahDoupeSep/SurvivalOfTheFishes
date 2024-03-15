using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamGeneration : MonoBehaviour
{
    public GameObject[] streamSegments;
    public float streamSegmentLength = 100f;
    private float startingZ;
    private float nextGenerationZ;
    private int nextMovedSegment = 0;

    void Start()
    {
        startingZ = transform.position.z;
        nextGenerationZ = startingZ + streamSegmentLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > nextGenerationZ)
        {
            nextGenerationZ += streamSegmentLength;
            streamSegments[nextMovedSegment].transform.position += Vector3.forward * streamSegmentLength * streamSegments.Length;
            nextMovedSegment = (nextMovedSegment + 1) % streamSegments.Length;
        }
    }
}
