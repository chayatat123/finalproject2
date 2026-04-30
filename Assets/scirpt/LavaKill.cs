using UnityEngine;

public class LavaKill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
            else
            {
                Debug.LogError("GameManager is NULL");
            }
        }
    }
}