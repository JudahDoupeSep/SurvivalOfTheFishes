using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StreamGeneration : MonoBehaviour
{
    public static StreamGeneration _instance;
    
    public GameObject initialStream;
    public GameObject emptyStream;
    public GameObject[] streams;
    public float streamSegmentLength = 100f;
    private List<GameObject> existingStreams = new List<GameObject>();

    void Start()
    {
        _instance = this;
        existingStreams.Add(initialStream);
        SpawnNextSegment();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.State == GameState.Playing 
            && transform.position.z > existingStreams.Last().transform.position.z)
        {
            SpawnNextSegment();
        }
    }

    private void SpawnNextSegment()
    {
        GameObject randomStream = streams[Mathf.RoundToInt(Random.Range(0, streams.Length) % streams.Length)];
        SpawnSegment(randomStream);
    }
    
    private void SpawnSegment(GameObject stream)
    {
        var newStream = Instantiate(stream);
        newStream.transform.position += existingStreams.Last().transform.position + (Vector3.forward * streamSegmentLength);
        
        existingStreams.Add(newStream);
        if (existingStreams.Count > 3)
        {
            var oldStream = existingStreams.First();
            existingStreams.Remove(oldStream);
            Destroy(oldStream);
        }
    }
}
