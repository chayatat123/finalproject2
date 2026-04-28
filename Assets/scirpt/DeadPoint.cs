using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerRespawn>().Die();
        }
    }
}