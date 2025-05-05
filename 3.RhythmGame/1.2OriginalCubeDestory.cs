using UnityEngine;

public class DestoryCube : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider belongs to a hand (based on tag)
        if (collision.gameObject.CompareTag("Hand"))
        {
            // Destroy this GameObject (the cube)
            Debug.Log("Trigger Detected");
            Destroy(gameObject);
        }
    }
}
