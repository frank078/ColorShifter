using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timer;
    public float timeSpeed = 1f;
    bool isTimerPlaying = true;

    // Update is called once per frame
    void Update()
    {
        if (isTimerPlaying)
        {
            timer += Time.deltaTime * timeSpeed;
        } 
    }

    // example for dicky
    //if timer = 30
        //increase difficulty
}
