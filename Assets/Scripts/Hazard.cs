using UnityEngine;

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
