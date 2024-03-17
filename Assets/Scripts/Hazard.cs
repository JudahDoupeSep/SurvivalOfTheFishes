using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Hazard : MonoBehaviour
{
    public float FrontGlobal;
    public float BackGlobal;
    public float LeftGlobal;
    public float RightGlobal;

    public GameObject startSprite;
    public GameObject endSprite;
    public GameObject DeadPlayer;
    public GameObject DeadFish;

    void Update()
    {
        var globalPos = transform.parent.TransformPoint(transform.localPosition);
        SphereCollider sc = GetComponent<SphereCollider>(); 
        if( sc != null)
        {
            LeftGlobal = globalPos.x - (sc.radius * transform.localScale.x);
            RightGlobal = globalPos.x + (sc.radius * transform.localScale.x);
            BackGlobal = globalPos.z + (sc.radius * transform.localScale.z);
            FrontGlobal = globalPos.z - ( sc.radius * transform.localScale.z);
        }

        BoxCollider bc = GetComponent<BoxCollider>();
        if (bc != null)
        {
            LeftGlobal = globalPos.x - (bc.size.x/ 2 * transform.localScale.x);
            RightGlobal = globalPos.x + (bc.size.x / 2 * transform.localScale.x);
            BackGlobal = globalPos.z + (bc.size.z / 2 * transform.localScale.z);
            FrontGlobal = globalPos.z - (bc.size.z / 2 * transform.localScale.z);
        }

        CapsuleCollider cc = GetComponent<CapsuleCollider>();
        if (cc != null)
        {
            LeftGlobal = globalPos.x - (cc.height / 2 * transform.localScale.x);
            RightGlobal = globalPos.x + (cc.height / 2 * transform.localScale.x);
            BackGlobal = globalPos.z + (cc.radius / 2 * transform.localScale.z);
            FrontGlobal = globalPos.z - (cc.radius / 2 * transform.localScale.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var deadFish = Instantiate(DeadPlayer);
            deadFish.transform.position = collision.gameObject.transform.position;
            deadFish.transform.localScale = collision.gameObject.transform.localScale;
            GameManager.LoseGame();
        }
        else if (collision.gameObject.CompareTag("AI"))
        {
            var deadFish = Instantiate(DeadFish);
            deadFish.transform.position = collision.gameObject.transform.position;
            deadFish.transform.localScale = collision.gameObject.transform.localScale;
            Hatchery.KillFish(collision.gameObject);
        }
        if (endSprite != null)
        {
            endSprite.SetActive(true);
            if (startSprite != null)
            {
                startSprite.SetActive(false);
            }
        }
    }
}
