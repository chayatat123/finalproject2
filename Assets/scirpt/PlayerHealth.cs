using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private Slider hpSlider;

    void Start()
    {
        currentHealth = maxHealth;

        // 🔥 หา HPBar ใหม่ทุก Scene
        GameObject hpObj = GameObject.Find("HPBar");

        if (hpObj != null)
        {
            hpSlider = hpObj.GetComponent<Slider>();

            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("❌ ไม่เจอ HPBar ใน Scene นี้");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (hpSlider != null)
            hpSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}