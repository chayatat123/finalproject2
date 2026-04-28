using UnityEngine;
using TMPro; // สำหรับใช้ TextMeshPro

public class CoinCollector : MonoBehaviour
{
    private int coinCount = 0;
    public TextMeshProUGUI coinText; // ลากตัวหนังสือ UI มาใส่ในช่องนี้

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าสิ่งที่ชนมี Tag ว่า "Coin" หรือไม่
        if (other.gameObject.CompareTag("Coin"))
        {
            coinCount++; // เพิ่มคะแนน
            UpdateUI();  // อัปเดตตัวเลขบนหน้าจอ

            Destroy(other.gameObject); // ลบเหรียญออกจากฉาก
            Debug.Log("เก็บเหรียญแล้ว! ตอนนี้มี: " + coinCount);
        }
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
}
