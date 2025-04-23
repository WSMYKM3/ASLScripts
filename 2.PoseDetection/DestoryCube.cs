//both hand and the Cube should add collider, and the hand collider should be a little bit bigger so that better detection for VR
//!!!Add rigidbody to Cube, or your life will be fucked!
//!!!Add rigidbody to Cube, or your life will be fucked!
//!!!Add rigidbody to Cube, or your life will be fucked!
//!!!Add rigidbody to Cube, or your life will be fucked!
//!!!Add rigidbody to Cube, or your life will be fucked!
//!!!Add rigidbody to Cube, or your life will be fucked!
//!!!Add rigidbody to Cube, or your life will be fucked!
//!!!Add rigidbody to Cube, or your life will be fucked!
//!!!Add rigidbody to Cube, or your life will be fucked!

//set hand's tag to Hand as "if" below
//I don not use hand, I use a gameobject set under OpenXRLeftHand and OpenXRRightHand 
//Do not waste your time to find which gameobject to set as "Hand", building blocks = shit!

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