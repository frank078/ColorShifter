using UnityEngine;

public class TowerFall : MonoBehaviour
{
    // Variables
    public Rigidbody[] rb = new Rigidbody[0];
    public float fallSpeed = -1000;

    void FixedUpdate()
    {
        for (int i = 0; i < rb.Length; i++)
        {
            // This makes the tower fall towards the player
            rb[i].AddForce(0, fallSpeed * Time.deltaTime, 0);
        }
    }
}
