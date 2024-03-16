using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayAreaMovement : MonoBehaviour
{
    private static PlayAreaMovement _instance;
    
    public float speed = 1.0f;

    private float _activeSpeed = 0;

    void Start()
    {
        _instance = this;
    }
    
    void Update()
    {
        transform.position += _activeSpeed * Vector3.forward * Time.deltaTime;
    }

    public static void StartMoving()
    {
        _instance.StartCoroutine(_instance.ChangeSpeed(_instance.speed));
    }

    public static void StopMoving()
    {
        _instance.StartCoroutine(_instance.ChangeSpeed(0));
    }

    private IEnumerator ChangeSpeed(float newSpeed)
    {
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            _activeSpeed = math.lerp(_activeSpeed, newSpeed, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
