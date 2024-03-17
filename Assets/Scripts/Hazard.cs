using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Hazard : MonoBehaviour
{
    public float FrontOffset;
    public float BackOffset;
    public float LeftOffset;
    public float RightOffset;

    public GameObject startSprite;
    public GameObject endSprite;
    public GameObject DeadPlayer;
    public GameObject DeadFish;

    void Start()
    {
        SphereCollider sc = GetComponent<SphereCollider>(); 
        if( sc != null)
        {
            LeftOffset = sc.radius * -1 * transform.localScale.x;
            RightOffset = sc.radius * transform.localScale.x;
            BackOffset = sc.radius * transform.localScale.x;
            FrontOffset = sc.radius * -1 * transform.localScale.x;
        }

        BoxCollider bc = GetComponent<BoxCollider>();
        if (bc != null)
        {
            LeftOffset = bc.size.x/2 * -1;
            RightOffset = bc.size.x / 2;
            BackOffset = bc.size.z / 2;
            FrontOffset = bc.size.z / 2 * -1;
        }

        CapsuleCollider cc = GetComponent<CapsuleCollider>();
        if (bc != null)
        {
            LeftOffset = cc.height / 2 * -1;
            RightOffset = cc.height / 2;
            BackOffset = cc.radius / 2;
            FrontOffset = cc.radius / 2 * -1;
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
