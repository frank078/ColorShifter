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

    // Stored Speed upon death
    float storeCurFallSpeed;
    bool hasStoredSpeed;
    float storeFallSpeed;

    // Beginning TargetSpeed
    float beginningSpeed = 5f;
    float beginningMaxTargetSpeed = 15f;

    private void Start()
    {
        storeFallSpeed = beginningSpeed;
        fallSpeed = 0;
    }

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
        fallSpeed = storeFallSpeed;
        hasStoredSpeed = false;
    }

    public bool IsSpeedSameAsCurSpeed()
    {
        if (storeCurFallSpeed >= fallSpeed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // RESTORE SPEED (Did not use addedSpeed)
    public void IncreaseRestoreSpeed(float addedSpeed)
    {
        if (fallSpeed != maxFallSpeed)
        {
            fallSpeed += speedMultiplier;
            if (fallSpeed >= storeCurFallSpeed)
            {
                fallSpeed = storeCurFallSpeed;
            }
        }
    }

    // BEGINNING SPEED
    public void SetBeginningSpeed()
    {
        fallSpeed = beginningSpeed;
    }

    public void IncreaseBeginningSpeed(float addedSpeed)
    {
        if(fallSpeed != maxFallSpeed)
        {
            fallSpeed += addedSpeed;
            if(fallSpeed >= beginningMaxTargetSpeed)
            {
                fallSpeed = beginningMaxTargetSpeed;
            }
        }
    }

    public bool isSpeedSameAsBeginningMaxTargetSpeed()
    {
        if(beginningMaxTargetSpeed > fallSpeed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // --------------------------------------------------
}
