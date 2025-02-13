using UnityEngine;

public class SpoonMine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Collider spoonCollider;
    public Rigidbody spoonRb;
    public float breakThreshold = 5.0f;
    public GameObject Shekel;
    void Start()
    {
        spoonRb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.contacts[0].thisCollider);
        print("Spoon Collision!");
        if (collision.contacts[0].thisCollider == spoonCollider && collision.gameObject.CompareTag("MiningSurface"))

        {
            
            float impactAngleFactor = Vector3.Dot(spoonCollider.transform.forward , collision.contacts[0].normal);

            impactAngleFactor = Mathf.Abs(impactAngleFactor);

            Vector3 tipVelocity = spoonRb.GetPointVelocity(collision.contacts[0].point);

            float modifiedForce = tipVelocity.magnitude * impactAngleFactor;

            print("Modified force: " + modifiedForce);

            if (modifiedForce > breakThreshold)
            {
                print("Spoon broke!");
                //Destroy(gameObject);
                GameObject Coin = Instantiate(Shekel);
                Coin.transform.position = transform.position;
                // make it fly out from the normal of the collision with a small force
                Coin.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 1.0f, ForceMode.Impulse);

            }
        }	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
