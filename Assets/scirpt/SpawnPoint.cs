using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform spawnPoint; // จุดเกิด

    public void Respawn()
    {
        transform.position = spawnPoint.position;
    }
}