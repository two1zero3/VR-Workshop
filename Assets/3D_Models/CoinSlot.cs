using UnityEngine;

public class CoinSlot : MonoBehaviour
{

    public AudioSource CoinSound;
    public AudioClip CoinSoundClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCoinSound()
    {
        CoinSound.PlayOneShot(CoinSoundClip);
    }
}
