using System;
using UnityEngine;

public class CoinsCounterUI : MonoBehaviour
{
    public int numberOfCoins = 0;
    public String coinsText = "Coins: ";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get text (TMP) and 
        // set text to the number of coins collected
        
        GetComponent<TMPro.TextMeshProUGUI>().text = coinsText + numberOfCoins.ToString();
    }

    public void AddCoins(int coins)
    {
        numberOfCoins += coins;
        print("Coins: " + numberOfCoins);
    }

    public void RemoveCoins(int coins)
    {
        numberOfCoins -= coins;
    }
}
