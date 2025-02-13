using UnityEngine;
using Meta.XR.MRUtilityKit;
public class SpoonMineV3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Shekel;
    public AudioSource CoinSound;
    public AudioClip CoinSoundClip;
    public GameObject coinSpawnPoint;

    public GameObject grabJoint;
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
        //get distance to our own transform.position and if it is less than 0.1f then we return

        Vector3 surfacePosition;
        MRUKAnchor closestAnchor;
        Vector3 normal;

        room.TryGetClosestSurfacePosition(coinSpawnPoint.transform.position, out surfacePosition, out closestAnchor, out normal);
        Debug.DrawRay(surfacePosition, normal, Color.red, 10);

        print("ON TRIGGER ENTER");
        print(other.CompareTag("MiningSurface") + "MINING SURFACE");
        print(isHoldingLeft + "HOLDING LEFT");
        print(isHoldingRight + "HOLDING RIGHT");
        print(closestAnchor.Label + "LABELS");

        //we get what the spoon collided with and check if it has a tag of "MiningSurface" and the collider is the spoonCollider
        if (other.CompareTag("MiningSurface") && (isHoldingLeft || isHoldingRight) && closestAnchor.Label == Labels)
        {   

            print("MINING SURFACE COLLISION");
            GameObject Coin = Instantiate(Shekel);
            // get position of collision and set the coin to that position
            Vector3 normalizedNormal = normal.normalized;
            Coin.transform.position = coinSpawnPoint.transform.position + (normalizedNormal * 0.1f);
                // make it fly out from the normal of the collision surface with a small force
            Coin.GetComponent<Rigidbody>().AddForce(normal * 2.0f, ForceMode.Impulse);
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