using UnityEngine;

public class KingBob : MonoBehaviour
{

    public int CoinWorth = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoinSlot"))
        {
          
            Destroy(gameObject);
            
            // Find the CoinsCounterUI object and call the AddCoins method
            CoinsCounterUI CoinsCounter = FindAnyObjectByType<CoinsCounterUI>();
            CoinsCounter.GetComponent<CoinsCounterUI>().AddCoins(CoinWorth);
            // find object with tag CoinSlot and call the playCoinSound method
            CoinSlot CoinSlot = FindAnyObjectByType<CoinSlot>();
            CoinSlot.GetComponent<CoinSlot>().playCoinSound();
        }
    }
}
