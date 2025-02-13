using UnityEngine;

public class SpoonMineV2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Shekel;
    public AudioSource CoinSound;
    public AudioClip CoinSoundClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //we get what the spoon collided with and check if it has a tag of "MiningSurface" and the collider is the spoonCollider
        if (other.CompareTag("MiningSurface"))
        {
            print("Spoon Collision!");
            GameObject Coin = Instantiate(Shekel);
            Coin.transform.position = transform.position;
                // make it fly out from the normal of the collision surface with a small force
            Coin.GetComponent<Rigidbody>().AddForce(other.transform.forward * 1.0f, ForceMode.Impulse);
            CoinSound.PlayOneShot(CoinSoundClip);

        }
    }
}
