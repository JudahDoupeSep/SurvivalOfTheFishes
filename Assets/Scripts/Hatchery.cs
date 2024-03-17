using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
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
    private static List<GameObject> _eggs;
    public static List<Hazard> Hazards;

    private static int totalCompetitors = 0;
    public static float minFishSize = 0.6f;
    public static float maxFishSize = 2.0f;

    private float _streamWidth;
    private float _streamDepth;
    
    public GameObject Dory;
    public GameObject Nemo;
    public GameObject Marlin;
    public GameObject GhostPlayer;
    public GameObject Egg;

    
    void Awake()
    {
        _instance = this;
        _competitors = new List<GameObject>();
        _eggs = new List<GameObject>();
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

        foreach (var otherFish in FindObjectsOfType<Fish>())
        {
            otherFish.UpdateFishSize(newSize);
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
            fish.transform.localScale = Vector3.zero;
            fish.transform.localPosition = new Vector3(Random.Range(-_instance._streamWidth, _instance._streamWidth),
                                                       0.1f,
                                                       Random.Range(-_instance._streamDepth, _instance._streamDepth));
        }
    }

    public static void AnimateSpawn()
    {
        _instance.StartCoroutine(_instance.AnimateSpawnCoroutine());
    }

    private IEnumerator AnimateSpawnCoroutine()
    {
        var player = FindObjectOfType<PlayerController>();
        var ghostPlayer  = Instantiate(GhostPlayer);
        ghostPlayer.transform.parent = player.transform.parent;
        ghostPlayer.transform.localPosition = player.transform.localPosition;
        ghostPlayer.transform.localScale = player.transform.localScale;
        
        player.transform.localPosition = Vector3.zero;
        player.transform.localScale = Vector3.zero;

        var eggToFish = new Dictionary<GameObject, GameObject>();
        var eggToStart = new Dictionary<GameObject, Vector3>();

        var playerEgg = Instantiate(Egg, ghostPlayer.transform.parent);
        playerEgg.transform.localPosition = ghostPlayer.transform.localPosition;
        _eggs.Add(playerEgg);
        
        eggToStart.Add(playerEgg.gameObject, playerEgg.transform.localPosition);
        eggToFish.Add(playerEgg.gameObject, player.gameObject);

        foreach (var fish in _competitors)
        {
            var egg = Instantiate(Egg, fish.transform.parent);
            egg.transform.localPosition = ghostPlayer.transform.localPosition;
            _eggs.Add(egg);
        
            eggToStart.Add(egg.gameObject, egg.transform.localPosition);
            eggToFish.Add(egg.gameObject, fish.gameObject);
        }


        var duration = 3;
        var t = 0f;

        while (t < 1)
        {
            foreach (var egg in _eggs)
            {
                egg.transform.localPosition =
                    Vector3.Lerp(eggToStart[egg], eggToFish[egg].transform.localPosition, Ease(t));
            }

            t += (Time.deltaTime) / duration;
            yield return new WaitForEndOfFrame();
        }
        
        GameManager.WaitForInput();
        
        float Ease(float t) => 1f - MathF.Pow(1f - t, 3f);
    }
    
    public static void HatchFish()
    {
        FindObjectOfType<PlayerController>().transform.localScale = Vector3.one * minFishSize;
        foreach (var fish in _competitors)
        {
            fish.transform.localScale = Vector3.one * minFishSize;
        }
        
        foreach (var egg in _eggs)
        {
            Destroy(egg);
        }
        _eggs.Clear();
    }


}
