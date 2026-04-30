using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return; // 🔥 กันโค้ดด้านล่างรันซ้ำ
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 🔥 รีเซ็ตเวลาเสมอ (กันปุ่มกดไม่ติด)
        Time.timeScale = 1f;

        // 🔥 หา UI ใหม่ทุก Scene
        GameObject ui = GameObject.Find("GameOverUI");

        if (ui != null)
        {
            gameOverUI = ui;
            gameOverUI.SetActive(false);
        }
        else
        {
            gameOverUI = null;
        }
    }

    // ===== GAME =====
    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ===== MENU =====
    public void StartGame()
    {
        Time.timeScale = 1f;

        CoinCollector.totalCoins = 0;

        SceneManager.LoadScene("1"); // ✔ ต้องตรง Build Settings
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Credits()
    {
        Debug.Log("CLICK CREDIT");

        Time.timeScale = 1f;

        // 🔥 ตัดการอ้าง UI เก่า
        gameOverUI = null;

        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}   