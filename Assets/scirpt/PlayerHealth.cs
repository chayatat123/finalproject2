using UnityEngine;
using UnityEngine.UI; // <--- สำคัญมาก: ต้องเพิ่มอันนี้เพื่อใช้ UI

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Slider hpSlider; // <--- ลาก Slider จากหน้าต่าง Hierarchy มาใส่ในช่องนี้

    void Start()
    {
        currentHealth = maxHealth;

        // ตั้งค่าเริ่มต้นให้ HP Bar
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // อัปเดตค่าใน HP Bar ทุกครั้งที่โดนดาเมจ
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }

        Debug.Log("HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");
        // GameManager.instance.GameOver();
    }
}