using UnityEngine;
using System.Collections;
using Meta.XR.MRUtilityKit;

public class SpoonMineV2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Shekel;
    public AudioSource CoinSound;
    public AudioClip CoinSoundClip;
    public GameObject coinSpawnPoint;
    public float threshold = 0.1f;
    public MRUKAnchor.SceneLabels Labels = ~(MRUKAnchor.SceneLabels)0;
    MRUKRoom room;


    void Start()
    {
        if (MRUK.Instance)
        {
            MRUK.Instance.RegisterSceneLoadedCallback(() =>
            {
                //StartSpawn(MRUK.Instance.GetCurrentRoom());
                room = MRUK.Instance.GetCurrentRoom();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        // print(tipCollider.transform.position + "TIP POSITION");
        // print(transform.position + "SPOON POSITION");
    }

    void OnTriggerEnter(Collider other)
    {   
        bool isHoldingLeft = false;
        bool isHoldingRight = false;
        GameObject leftController = GameObject.FindGameObjectWithTag("LeftController");
        GameObject rightController = GameObject.FindGameObjectWithTag("RightController");
        //get distance to our own transform.position and if it is less than 0.1f then we return
        if (Vector3.Distance(leftController.transform.position, transform.position) < threshold)
        {
            isHoldingLeft = true;
        }
        if (Vector3.Distance(rightController.transform.position, transform.position) < threshold)
        {
            isHoldingRight = true;
        }

        Vector3 surfacePosition;
        MRUKAnchor closestAnchor;
        Vector3 normal;

        room.TryGetClosestSurfacePosition(coinSpawnPoint.transform.position, out surfacePosition, out closestAnchor, out normal);

        //we get what the spoon collided with and check if it has a tag of "MiningSurface" and the collider is the spoonCollider
        if (other.CompareTag("MiningSurface") && (isHoldingLeft || isHoldingRight) && closestAnchor.Label == Labels)
        {   
            GameObject Coin = Instantiate(Shekel);
            // get position of collision and set the coin to that position
            Coin.transform.position = coinSpawnPoint.transform.position;
                // make it fly out from the normal of the collision surface with a small force
            Coin.GetComponent<Rigidbody>().AddForce(other.transform.forward * 1.0f, ForceMode.Impulse);
            CoinSound.PlayOneShot(CoinSoundClip);

            

            if(isHoldingLeft)
            {
                OVRInput.SetControllerVibration(0.01f, 10.1f, OVRInput.Controller.LTouch);
                Invoke("StopVibrationLeft", 0.1f);

            } else {
                OVRInput.SetControllerVibration(0.01f, 10.1f, OVRInput.Controller.RTouch);
                Invoke("StopVibrationRight", 0.1f);
            }
        }
    }

    void StopVibrationLeft()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);

    }

    void StopVibrationRight()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);

    }

}
