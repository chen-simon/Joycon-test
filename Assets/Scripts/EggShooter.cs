// using UnityEngine;

// public class EggShooter : MonoBehaviour
// {
//     public GameObject eggPrefab; 
//     public Vector3 startOffset = new Vector3(-26, 1.45f, 3.567f); // Start position offset from the camera
//     public float shootForce = 1000f; // Horizontal force
//     public float upwardForce = 300f; // Upward force for the arc

//     private GameObject currentEgg;
//     private bool isShot = false; // Tracks whether the egg has been hit by another projectile

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             ShootEgg();
//         }

//         if (Input.GetKeyDown(KeyCode.LeftShift))
//         {
//             isShot = true;
//             ApplyGravityOnly();
//         }
//     }

//     void ShootEgg()
//     {
//         // Destroy the current egg if it exists
//         if (currentEgg != null)
//         {
//             Destroy(currentEgg);
//         }

//         // Instantiate a new egg
//         currentEgg = Instantiate(eggPrefab, Camera.main.transform.position + startOffset, Quaternion.identity);
//         Rigidbody rb = currentEgg.GetComponent<Rigidbody>();
        
//         // Apply force in the X direction and add upward force for an arc
//         Vector3 force = Vector3.right * shootForce + Vector3.up * upwardForce;
//         rb.AddForce(force);

//         // Reset isShot since a new egg is being shot
//         isShot = false;
//     }

//     void OnCollisionEnter(Collision collision)
//     {
//         // Check if the egg has been hit by a projectile (assuming the projectile has a tag "Projectile")
//         if (collision.gameObject.CompareTag("Projectile") && currentEgg != null)
//         {
//             isShot = true;
//             ApplyGravityOnly();
//         }
//     }

//     void ApplyGravityOnly()
//     {
//         if (isShot && currentEgg != null)
//         {
//             Rigidbody rb = currentEgg.GetComponent<Rigidbody>();

//             // Remove all horizontal forces, making the egg fall straight down
//             rb.velocity = new Vector3(0, rb.velocity.y - 9.81f, 0);
//         }
//     }
// }

using UnityEngine;

public class EggShooter : MonoBehaviour, IShootable
{
    public GameObject eggPrefab;
    public GameObject eggShellPrefab;
    public GameObject yolkPrefab;
    public Vector3 startOffset = new Vector3(-26, 1.45f, 3.567f); // Start position offset from the camera
    public float shootForce = 1000f; // Horizontal force
    public float upwardForce = 300f; // Upward force for the arc
    public float breakForce = 2f; // Additional force applied when the egg breaks

    private GameObject currentEgg;
    private bool isShot = false; // Tracks whether the egg has been hit by another projectile

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootEgg();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isShot = true;
            BreakEgg();
        }
    }

    void ShootEgg()
    {
        // Destroy the current egg if it exists
        if (currentEgg != null)
        {
            Destroy(currentEgg);
        }

        // Instantiate a new egg
        currentEgg = Instantiate(eggPrefab, Camera.main.transform.position + startOffset, Quaternion.identity);
        Rigidbody rb = currentEgg.GetComponent<Rigidbody>();
        
        // Apply force in the X direction and add upward force for an arc
        Vector3 force = Vector3.right * shootForce + Vector3.up * upwardForce;
        rb.AddForce(force);

        // Reset isShot since a new egg is being shot
        isShot = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the egg has been hit by a projectile (assuming the projectile has a tag "Projectile")
        if (collision.gameObject.CompareTag("Projectile") && currentEgg != null)
        {
            isShot = true;
            BreakEgg();
        }
    }

    public void TakeShot()
    {
        BreakEgg();
    }

    void BreakEgg()
    {
        if (currentEgg != null)
        {
            // Get the current velocity of the egg
            Rigidbody rb = currentEgg.GetComponent<Rigidbody>();
            Vector3 currentVelocity = rb.velocity;

            // Destroy the original egg
            Destroy(currentEgg);

            // Instantiate the two egg shells
            GameObject eggShell1 = Instantiate(eggShellPrefab, rb.position, Quaternion.identity);
            GameObject eggShell2 = Instantiate(eggShellPrefab, rb.position, Quaternion.identity);

            // Instantiate the yolk
            GameObject yolk = Instantiate(yolkPrefab, rb.position, Quaternion.identity);

            // Apply the original velocity to the egg shells and yolk
            Rigidbody rbShell1 = eggShell1.GetComponent<Rigidbody>();
            Rigidbody rbShell2 = eggShell2.GetComponent<Rigidbody>();
            Rigidbody rbYolk = yolk.GetComponent<Rigidbody>();

            rbShell1.velocity = currentVelocity + new Vector3(-breakForce, breakForce, 0);
            rbShell2.velocity = currentVelocity + new Vector3(breakForce, breakForce, 0);
            rbYolk.velocity = currentVelocity;

            // Optionally, you can apply some additional forces to the shells to make them fly apart more dramatically
        }
    }

    
}