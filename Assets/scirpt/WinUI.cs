using TMPro;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Total Coins: " + CoinCollector.totalCoins;
    }
}