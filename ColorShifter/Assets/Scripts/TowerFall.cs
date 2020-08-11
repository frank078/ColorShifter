using UnityEngine;

public class TowerFall : MonoBehaviour
{
    // Variables
    public Rigidbody rb;
    public float fallSpeed = -1000;

    void FixedUpdate()
    {
        // This makes the tower fall towards the player
        rb.AddForce(0, fallSpeed * Time.deltaTime, 0);
    }
}
