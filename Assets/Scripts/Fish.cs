using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fish : MonoBehaviour
{
    public float Speed = 4f;
    public float StreamWidth = 100f;
    public float SwimDepth = 30f;
    public float growthDuration = 2f;
    public float SwimForce = 50f;

    protected void StartFish()
    {
        UpdateAnimationSpeed(Random.Range(0.5f, 1));
    }

    protected void UpdateFish()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Waterfall")
        {
            GetComponentInChildren<MeshRenderer>().gameObject.transform.Rotate(-90, 0, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Waterfall")
        {
            GetComponentInChildren<MeshRenderer>().gameObject.transform.Rotate(90, 0, 0);
        }
    }

    public void UpdateAnimationSpeed(float speed)
    {
        GetComponentInChildren<Animator>().SetFloat("SwimSpeed", speed);
    }

    protected void Swim(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = direction * SwimForce * (1 + (transform.localScale.x-1)/2) ;
        UpdateAnimationSpeed(Math.Max((Math.Abs(direction.x) + Math.Abs(direction.y) + Math.Abs(direction.z)) * 25, .25f));
    }

    private Coroutine growCoroutine;
    public void UpdateFishSize(float newSize)
    {
        if (growCoroutine != null)
        {
            StopCoroutine(growCoroutine);
        }
        growCoroutine = StartCoroutine(ScaleOverTime(newSize));
    }

    private IEnumerator ScaleOverTime(float scale)
    {
        var startScale = transform.localScale;
        var endScale = Vector3.one * scale;
        var elapsed = 0f;

        while (elapsed < growthDuration)
        {
            var t = elapsed / growthDuration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
    }
}
