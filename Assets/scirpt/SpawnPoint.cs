using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public Transform spawnPoint;
    public float respawnDelay = 1f;

    private bool isDead = false;

    void Start()
    {
        Respawn();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // ปิดการควบคุม (ถ้ามี script movement ก็ปิดตรงนี้)
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        StartCoroutine(RespawnDelay());
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        Respawn();
    }

    void Respawn()
    {
        transform.position = spawnPoint.position;
        isDead = false;
    }
}