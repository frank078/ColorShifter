using UnityEngine;

public class TowerFall : MonoBehaviour
{
    // Variables
    public Rigidbody[] rb;
    public float fallSpeed = -10f;

    void FixedUpdate()
    {
        for (int i = 0; i < rb.Length; i++)
        {
        // This makes the tower fall towards the player
        rb[i].velocity = new Vector3(0, fallSpeed, 0);
        }
    }
}
