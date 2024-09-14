using UnityEngine;

public class LaserSight : MonoBehaviour
{
    public Transform gunBarrel;  // The position where the laser starts (e.g., gun barrel)
    public float laserRange = 50f;  // The max distance the laser can reach
    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        
        // Set the number of positions in the line (start and end point)
        lineRenderer.positionCount = 2;

    }

    void Update()
    {
        // Set the start position of the laser at the gun barrel
        lineRenderer.SetPosition(0, gunBarrel.position);

        // Perform a raycast to detect what the laser hits
        RaycastHit hit;
        if (Physics.Raycast(gunBarrel.position, gunBarrel.forward, out hit, laserRange))
        {
            // If the laser hits something, set the end position at the hit point
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // If the laser doesn't hit anything, set the end position at the max range
            lineRenderer.SetPosition(1, gunBarrel.position + gunBarrel.forward * laserRange);
        }
    }

    // Public method to call when shooting
    public void Shoot()
    {
        Ray ray = new Ray(gunBarrel.position, gunBarrel.forward);
        RaycastHit hitInfo;

        // Debug the ray’s origin and direction
        Debug.DrawRay(ray.origin, ray.direction * laserRange, Color.red, 1f);

        if (Physics.Raycast(ray, out hitInfo, laserRange))
        {
            // Raycast hit something
            Debug.Log("Hit: " + hitInfo.collider.name);
            GameObject hitObject = hitInfo.collider.gameObject;

            Destroy(hitObject);
        }
        else
        {
            // Raycast missed
            Debug.Log("Missed!");
        }
    }

}
