using System.Collections;
using UnityEngine;

public class TowerFall : MonoBehaviour
{
    // Variables
    public Rigidbody[] rb;
    //get set, that way it will not accidently change number
    [SerializeField] private float m_fallSpeed = 10f; // TODO: Remove SerializeField IN THE FUTURE
    public float speedMultiplier = 1.0f; // TODO: SET THIS TO PRIVATE ONCE THE SPEED HAS BEEN TESTED

    void FixedUpdate()
    {
        for (int i = 0; i < rb.Length; i++)
        {
        // This makes the tower fall towards the player
        rb[i].velocity = new Vector3(0, -(fallSpeed), 0);
        }
    }

    public float fallSpeed
    {
        get { return m_fallSpeed; }
        set { m_fallSpeed = value; }
    }

    public void increaseFallSpeed()
    {
        fallSpeed += speedMultiplier;
    }

    public void StopTower() // call the Enumerator delayStop
    {
        StartCoroutine(delayStop(1.5f));
    }

    IEnumerator delayStop(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        fallSpeed = 0f;
    }
}
