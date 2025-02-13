using UnityEngine;

public class BucketVacuum : MonoBehaviour
{

    public AudioSource CoinSound;
    public AudioClip CoinSoundClip;

    public GameObject grabJoint;
    public float threshold = 0.1f;
    public int coinsStored = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        bool isHoldingLeft = false;
        bool isHoldingRight = false;
        if (GameObject.FindGameObjectWithTag("LeftController"))
        {
            GameObject leftController = GameObject.FindGameObjectWithTag("LeftController");
            if (Vector3.Distance(leftController.transform.position, grabJoint.transform.position) < threshold)
            {
                isHoldingLeft = true;
            }
        }
        if (GameObject.FindGameObjectWithTag("RightController"))
        {
            GameObject rightController = GameObject.FindGameObjectWithTag("RightController");
            if (Vector3.Distance(rightController.transform.position, grabJoint.transform.position) < threshold)
            {
                isHoldingRight = true;
            }
        }
        //check if object has class KingBob
        if (other.GetComponent<KingBob>() && (isHoldingLeft || isHoldingRight))
        {
            //get CoinWorth from KingBob class
            int CoinWorth = other.GetComponent<KingBob>().CoinWorth;
            //add CoinWorth to coinsStored
            coinsStored += CoinWorth;
            //play CoinSound
            CoinSound.PlayOneShot(CoinSoundClip);
            //destroy the object
            Destroy(other.gameObject);
        }

        if (other.CompareTag("CoinSlot") && (isHoldingLeft || isHoldingRight))
        {
            // Find the CoinsCounterUI object and call the AddCoins method
            CoinsCounterUI CoinsCounter = FindAnyObjectByType<CoinsCounterUI>();
            CoinsCounter.GetComponent<CoinsCounterUI>().AddCoins(coinsStored);
            // find object with tag CoinSlot and call the playCoinSound method
            CoinSlot CoinSlot = FindAnyObjectByType<CoinSlot>();
            CoinSlot.GetComponent<CoinSlot>().playCoinSound();
            //reset coinsStored
            coinsStored = 0;
        }
    }
}
