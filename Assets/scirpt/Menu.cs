using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // 🔥 กันเกมค้าง

        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Scene1"); // 👈 เปลี่ยนชื่อด่าน
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}