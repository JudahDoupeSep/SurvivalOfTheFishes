using System.Collections;
using UnityEngine;

public class DeadFish : MonoBehaviour
{
    public bool ExplodeOnDeath = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (ExplodeOnDeath)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
        }
        StartCoroutine(DespawnDelayed(3));
    }

    private IEnumerator DespawnDelayed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
