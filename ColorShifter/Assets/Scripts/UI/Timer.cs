using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timer;
    private float timeSpeed = 1f; // This is for the timer speed

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

    // CONTINUE FROM DEATH
    public static bool isContinue;
    float continueTimer;
    bool isTimerMoving;
    float continueTargetTimer = 2;

    // NO SPEED AT START GAME
    public static bool isArrowPressed; // set to true after arrow button pressed;
    float beginningTimer;
    bool isBeginningTimerMoving;
    float beginningTargetTimer = 3;
    bool isSpeedHasBeenSet; // for setting beginning speed

    public Animator theArrows;


    void Start()
    {
        thePlayer = GameManager.Instance.GetPlayer();
        tFall = GameObject.FindObjectOfType<TowerFall>();
    }
    // Update is called once per frame
    void Update()
    {
        // IF OVER TARGET, INCREASE SPEED
        if (thePlayer != null && isArrowPressed)
        {
            TowerMoving();
            isBeginningTimerMoving = true;
        }
        // When player switches characters
        else if(thePlayer == null)
        {
            thePlayer = GameManager.Instance.GetPlayer();
            if(thePlayer == null)
            {
                tFall.StopTower();
                isTimerMoving = false;
            }
        }

        // Only run this when player just start the game or continue from death, stop after exceeding BeginnerMaxTargetSpeed
        if (isBeginningTimerMoving)
        {
            SetTFallBeginningSpeed();
            BeginningTimerMoving();
        }

        if (isContinue)
        {
            tFall.RestoreSpeed();
            isContinue = false;
            isTimerMoving = true;
        }

        // If Continue from the Death, Restore Speed
        if (isTimerMoving)
        {
            if(tFall.IsSpeedSameAsCurSpeed())
            {
                // Increase speed every 2 seconds until it reach last speed bfor death
                continueTimer += Time.deltaTime * timeSpeed;
                if (continueTimer > continueTargetTimer)
                {
                    tFall.IncreaseRestoreSpeed(2);
                    continueTimer = 0;
                }
            }
            else
            {
                isTimerMoving = false;
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

    public void TowerMoving()
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

    // Increase beginning speed
    public void BeginningTimerMoving()
    {
        if (isArrowPressed && thePlayer != null)
        {
            if (tFall.isSpeedSameAsBeginningMaxTargetSpeed())
            {
                // Increase speed every 3 sec
                beginningTimer += Time.deltaTime * timeSpeed;
                if (beginningTimer >= beginningTargetTimer)
                {
                    tFall.IncreaseBeginningSpeed(1);
                    beginningTimer = 0;
                }
            }
            else
            {
                isBeginningTimerMoving = false;
            }

        }
    }

    // Set Beggining Speed (RUN ONLY ONCE)
    public void SetTFallBeginningSpeed()
    {
        if (!isSpeedHasBeenSet)
        {
            tFall.SetBeginningSpeed();
            isSpeedHasBeenSet = true;
            theArrows.SetTrigger("SetIdle");
        }
    }
}
