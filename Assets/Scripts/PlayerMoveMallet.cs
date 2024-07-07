using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMallet : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public LayerMask barrierLayer;  // Layer mask to identify barriers
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Make the mallet kinematic
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Continuous collision detection
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Interpolation for smoother movement

        // Lock the cursor to the game window and make it visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void Update()
    {
        // Get the z coordinate of the mallet in screen space
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Get the mouse position in world space
        Vector3 targetPoint = GetMouseAsWorldPoint();

        // Restrict movement to the x and z coordinates only
        Vector3 newPosition = new Vector3(targetPoint.x, transform.position.y, targetPoint.z);

        // Check for collision with barriers
        if (!IsCollidingWithBarrier(newPosition))
        {
            // Move the mallet using Rigidbody
            rb.MovePosition(newPosition);
        }
    }

    bool IsCollidingWithBarrier(Vector3 position)
    {
        // Cast a ray from the current position to the target position
        RaycastHit hit;
        Vector3 direction = position - transform.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(transform.position, direction, out hit, distance, barrierLayer))
        {
            return true; // Collision detected
        }

        return false; // No collision
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Puck"))
        {
            Rigidbody puckRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (puckRigidbody != null)
            {
                // Ensure continuous collision detection
                puckRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

                Vector3 forceDirection = collision.contacts[0].point - transform.position;
                forceDirection = forceDirection.normalized;

                // Apply force gradually
                StartCoroutine(ApplyForceGradually(puckRigidbody, forceDirection * 10f)); // Adjust force as necessary
            }
        }
    }

    IEnumerator ApplyForceGradually(Rigidbody puckRigidbody, Vector3 force)
    {
        float duration = 0.1f; // Duration over which to apply the force
        float elapsed = 0f;

        while (elapsed < duration)
        {
            puckRigidbody.AddForce(force * Time.deltaTime / duration, ForceMode.VelocityChange);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
