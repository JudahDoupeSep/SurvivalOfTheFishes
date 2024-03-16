using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ArtiFishalIntelligence
{
    Dory,
    Nemo,
    Marlin,
}

public class Hatchery : MonoBehaviour
{
    private static Hatchery _instance;
    private static List<GameObject> _competitors;

    private static int totalCompetitors = 0;
    public static float minFishSize = 0.4f;
    public static float maxFishSize = 2f;

    private float _streamWidth;
    private float _streamDepth;
    
    public GameObject Dory;
    public GameObject Nemo;
    public GameObject Marlin;

    
    void Start()
    {
        _instance = this;
        _competitors = new List<GameObject>();
        var playerMovement = FindObjectOfType<PlayerController>();
        _streamWidth = playerMovement.StreamWidth;
        _streamDepth = playerMovement.SwimDepth;
    }

    public static int CompetitorCount => _competitors.Count;
    
    public static void KillFish(GameObject fish)
    {
        _competitors.Remove(fish);
        Destroy(fish);

        float newSize = minFishSize + (maxFishSize - minFishSize) * (totalCompetitors - CompetitorCount) / totalCompetitors;
        Vector3 newSizeVector = new Vector3(newSize, newSize, newSize);
        FindObjectOfType<PlayerController>().gameObject.transform.localScale = newSizeVector;
        foreach (var competitor in _competitors)
        {
            competitor.transform.localScale = newSizeVector;
        }
        if (CompetitorCount == 0)
        {
            totalCompetitors = 0;
        }
    }

    public static void SpawnFish(int fishCount, ArtiFishalIntelligence intelligence)
    {
        totalCompetitors += fishCount;
        for (var i = 0; i < fishCount; i++)
        {
            GameObject fish;
            switch (intelligence)
            {
                case ArtiFishalIntelligence.Dory:
                    fish = Instantiate(_instance.Dory);
                    break;
                case ArtiFishalIntelligence.Nemo:
                    fish = Instantiate(_instance.Nemo);
                    break;
                case ArtiFishalIntelligence.Marlin:
                    fish = Instantiate(_instance.Marlin);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(intelligence), intelligence, null);
            }
            _competitors.Add(fish);
            fish.transform.parent = _instance.transform;
            fish.transform.localPosition = new Vector3(Random.Range(-_instance._streamWidth, _instance._streamWidth),
                                                       0.1f,
                                                       Random.Range(-_instance._streamDepth, _instance._streamDepth));
        }
    }
}
