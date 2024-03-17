using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DespawnDelayed(3));
    }

    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * FindObjectOfType<PlayAreaMovement>().speed; 
    }

    private IEnumerator DespawnDelayed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
