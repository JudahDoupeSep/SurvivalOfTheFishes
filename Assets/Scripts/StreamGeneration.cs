using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StreamGeneration : MonoBehaviour
{
    public GameObject initialStream;
    public GameObject emptyStream;
    public GameObject waterfall;
    public GameObject[] streams;
    public float streamSegmentLength = 100f;
    private float startingPosition;
    private float generateNextOncePositionReached;
    private GameObject latestStreamSegment;
    private int spawnedSegments = 0;

    void Start()
    {
        startingPosition = transform.position.z;
        generateNextOncePositionReached = startingPosition + streamSegmentLength;
        latestStreamSegment = initialStream;
        SpawnNextSegment();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > generateNextOncePositionReached)
        {
            generateNextOncePositionReached += streamSegmentLength;
            SpawnNextSegment();
        }
    }

    void SpawnNextSegment()
    {
        Vector3 latestPosition = latestStreamSegment.transform.position;
        GameObject randomStream = streams[Mathf.RoundToInt(Random.Range(0, streams.Length) % streams.Length)];
        latestStreamSegment = Object.Instantiate(randomStream);
        latestStreamSegment.transform.position += latestPosition + (Vector3.forward * streamSegmentLength);
        spawnedSegments++;
        Hatchery.AddHazards(randomStream.GetComponentsInChildren<Hazard>());
    }
}
