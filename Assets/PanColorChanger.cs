using UnityEngine;

public class PanColorChanger : MonoBehaviour
{
    public Material panOG; // The default material of the pan
    public Material panCook;    // The material to change to when the yolk touches the pan
    public GameObject sparklePrefab; // The prefab for the sparkle effect
    
    private Renderer panRenderer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            panRenderer.material = panOG;
        }
    }

    void Start()
    {
        // Get the Renderer component of the pan
        panRenderer = GetComponent<Renderer>();

        // Set the pan's material to its normal state
        panRenderer.material = panOG;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object that collided with the pan is the yolk
        if (collision.gameObject.CompareTag("Yolk"))
        {
            // Change the pan's material to the red material
            panRenderer.material = panCook;


            // Spawn the sparkle effect at the point of collision
            Vector3 collisionPoint = collision.contacts[0].point; // Get the point of collision
            Instantiate(sparklePrefab, collisionPoint, Quaternion.identity);
        }
    }
}