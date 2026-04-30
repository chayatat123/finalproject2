using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public static int totalCoins = 0; // 🔥 ต้องมีตัวนี้

    private int coinCount = 0;
    public TextMeshProUGUI coinText;

    void Start()
    {
        if (coinText == null)
        {
            coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        }

        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            totalCoins++; // 🔥 เพิ่มคะแนนรวม

            UpdateUI();
            Destroy(other.gameObject);
        }
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + totalCoins;
        }
    }
}