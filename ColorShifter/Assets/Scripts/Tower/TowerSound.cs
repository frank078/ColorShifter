using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSound : MonoBehaviour
{

    public AudioSource towerTurn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            towerTurn.Play();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            towerTurn.Stop();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            towerTurn.Play();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            towerTurn.Stop();
        }
    }
}
