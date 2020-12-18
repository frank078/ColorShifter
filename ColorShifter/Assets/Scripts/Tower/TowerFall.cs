using System.Collections;
using UnityEngine;

public class TowerFall : MonoBehaviour
{
    // Variables
    public Rigidbody[] rb;
    //get set, that way it will not accidently change number
    [SerializeField] private float m_fallSpeed = 10f; // TODO: Remove SerializeField once the Speed has been Tested
    [SerializeField] private float m_maxFallSpeed = 30f; // TODO: Remove SerializeField once the Speed has been Tested

    public float fallSpeed
    {
        get { return m_fallSpeed; }
        set { m_fallSpeed = value; }
    }

    public float maxFallSpeed
    {
        get { return m_maxFallSpeed; }
        set { m_maxFallSpeed = value; }
    }

    public float speedMultiplier = 1f; // TODO: SET THIS TO PRIVATE ONCE THE SPEED HAS BEEN TESTED

    float storeCurFallSpeed;

    bool hasStoredSpeed;

    void FixedUpdate()
    {
        for (int i = 0; i < rb.Length; i++)
        {
        // This makes the tower fall towards the player
        rb[i].velocity = new Vector3(0, -(fallSpeed), 0);
        }
    }


    public void increaseFallSpeed()
    {
        if(fallSpeed != maxFallSpeed)
        {
            fallSpeed += speedMultiplier;
        }
    }

    public void StopTower() // call the Enumerator delayStop
    {
        if (hasStoredSpeed == false)
        {
            storeCurFallSpeed = fallSpeed;
            Debug.Log(storeCurFallSpeed);
            StartCoroutine(delayStop(1.5f));
            hasStoredSpeed = true;
        }
    }

    IEnumerator delayStop(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        fallSpeed = 0f;
    }

    public void RestoreSpeed()
    {
        fallSpeed = storeCurFallSpeed;
        Debug.Log(storeCurFallSpeed);
        hasStoredSpeed = false;
    }
}
