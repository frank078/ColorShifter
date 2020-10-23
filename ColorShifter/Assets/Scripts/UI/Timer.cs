using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timer;
    private float timeSpeed = 1f; // This is for the timer speed
    bool isTimerPlaying = true;

    // for get set, that way it will not accidently change number
    [SerializeField] private float m_targetTimer = 15;
    [SerializeField] private int m_targetMultiplier = 1;

    // targetTimer = targetTimer * Multiplier
    public float targetTimer
    {
        get { return m_targetTimer * targetMultiplier; }
        set { m_targetTimer = value; }
    }

    public int targetMultiplier
    {
        get { return m_targetMultiplier; }
        set { m_targetMultiplier = value; }
    }

    private GameObject thePlayer;
    private TowerFall tFall;

    void Start()
    {
        thePlayer = GameManager.Instance.GetPlayer();
        tFall = GameObject.FindObjectOfType<TowerFall>();
    }
    // Update is called once per frame
    void Update()
    {
        // IF OVER TARGET, INCREASE SPEED
        if (isTimerPlaying && thePlayer != null)
        {
            timer += Time.deltaTime * timeSpeed;
            if (timer >= targetTimer)
            {
                targetMultiplier++;
                tFall.increaseFallSpeed();
                CheckTimer();
            }
            //Debug.Log("Timer is " + timer + "         " + targetTimer); 
        }
        // When player switches characters
        else if(thePlayer == null)
        {
            thePlayer = GameManager.Instance.GetPlayer();
            if(thePlayer == null)
            {
                tFall.StopTower();
            }
        }
    }

    void CheckTimer()
    {
        // if target multiplier is 3 or targetTimer is over 30 sec
        if(targetMultiplier == 3)
        {
            // unlocked green
            GameManager.Instance.SetGreen(true);
        }

        // if target multiplier is 5 or targetTimer is over 60 sec
        if(targetMultiplier == 5)
        {
            // unlocked blocker
            GameManager.Instance.SetBlocker(true);
        }

        // if target multiplier is 7 or targetTimer is over 90 sec
        if(targetMultiplier == 7)
        {
            // unlocked pink
            GameManager.Instance.SetPink(true);
        }
    }
}
