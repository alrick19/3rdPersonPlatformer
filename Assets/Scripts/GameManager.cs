using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance 
    [SerializeField] public GameObject coinCollection; // Parent object containing all coins
    [SerializeField] private TextMeshProUGUI coinCounter;

    private List<GameObject> coins = new List<GameObject>(); // Track coins
    private int totalCoins;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        FindAllCoins();
    }

    void Update()
    {
        CoinCountUI();
    }

    private void FindAllCoins()
    {
        coins.Clear();
        foreach (Transform child in coinCollection.transform)
        {
            if (child.gameObject.activeSelf)
            {
                coins.Add(child.gameObject);
            }
        }

        totalCoins = coins.Count;
    }

    public void RemoveCoin(GameObject coin)
    {
        if (coins.Contains(coin))
        {
            coins.Remove(coin);
        }
    }

    private void CoinCountUI()
    {
        if (coinCounter != null)
        {
            int collectedCoins = totalCoins - coins.Count;
            coinCounter.text = $"Collected: {collectedCoins} / {totalCoins}";
        }
    }

}
