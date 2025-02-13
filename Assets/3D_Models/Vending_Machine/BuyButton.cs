using UnityEngine;
using UnityEngine.UIElements;

public class BuyButton : MonoBehaviour
{
    public Vector3 ButtonTarget;
    public Vector3 OriginalPosition;

    public AudioSource CoinSound;
    public AudioClip CoinSoundClip;

    public int priceOfItem = 0;

    public GameObject spawnPoint;

    bool ButtonPressed = false;
    public GameObject Spoon;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //define Button as own transform
        ButtonTarget = transform.position;
        OriginalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {

        CoinsCounterUI CoinsCounter = FindAnyObjectByType<CoinsCounterUI>();

        if ((other.CompareTag("LeftController") || other.CompareTag("RightController")) && !ButtonPressed && CoinsCounter.GetComponent<CoinsCounterUI>().numberOfCoins >= priceOfItem) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.05f);
            Invoke("resetPosition", 0.5f);
            //spawn Spoon at the position of the button
            GameObject SpoonInstance = Instantiate(Spoon);
            SpoonInstance.transform.position = spawnPoint.transform.position;
           
            //SpoonInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 2.0f, ForceMode.Impulse);
             //get all rigidbody instances in the SpoonInstance and add force to it
            Rigidbody[] rigidbodies = SpoonInstance.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
            }
            ButtonPressed = true;
            CoinSound.PlayOneShot(CoinSoundClip);

            //subtract the price of the item from the coins
            CoinsCounter.GetComponent<CoinsCounterUI>().RemoveCoins(priceOfItem);
            
        }
    }

    void resetPosition() {
        transform.position = OriginalPosition;
        ButtonPressed = false;
    }
}
