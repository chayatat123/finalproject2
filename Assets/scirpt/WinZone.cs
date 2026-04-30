using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("YOU WIN!");

            SceneManager.LoadScene("Win"); // 🔥 ชื่อ Scene หน้า Win
        }
    }
}