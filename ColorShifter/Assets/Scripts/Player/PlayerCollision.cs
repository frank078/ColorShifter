﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Material[] colorSelection; // TODO: Add new colors the further player went
    private MeshRenderer currentColor;
    private int nextTower;

    //PARTICLES
    public GameObject playerPassEffect;

    // AVOID Double Trigger
    private int totalTriggered = 0;

    // COINS
    private int m_coins = 0;

    private void Start()
    {
        currentColor = gameObject.GetComponent<MeshRenderer>();
        ChangePlayerColor(0);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if other is ColoredWalls
        if (other.CompareTag("Walls"))
        {
            // ColoredWalls Ref  (this gameobject is Cylinder)
            MeshRenderer coloredWallsColor = other.gameObject.GetComponent<MeshRenderer>();
            nextTower = 1 + other.gameObject.transform.parent.parent.GetComponentInParent<TowerObjectPulling>().TowerNumber;

            // if player color is the same as one of the walls that collide
            if(currentColor.material.name == coloredWallsColor.material.name)
            {
                if(totalTriggered == 0) // AVOID DOUBLE TRIGGER
                {
                    totalTriggered++;
                    StartCoroutine(delay(0.1f)); // delay it, to avoid getting killed from changing color while still inside the walls
                }
            }
            else
            {
                GameplayStatics.DealDamage(gameObject, 1);
            }
        }

        if (other.CompareTag("Coins"))
        {
            IncreaseCoins();
        }
    }

    void ChangePlayerColor(int index)
    {
        // If the Index is higher than color selection (ex: want blue, but havent unlocked yet)
        if (colorSelection.Length - 1 < index)
        {
            ShufflePlayerColor();
        }
        else
        {
            currentColor.material = colorSelection[index];
            GameManager.Instance.CheckPlayerColor(nextTower); // call it here so that they could get latest material references
        }
    }

    void ShufflePlayerColor()
    {
        int shuffleColor = Random.Range(0, 3);
        switch (shuffleColor)
        {
            case 0:
                ChangePlayerColor(0); // RED
                break;
            case 1:
                ChangePlayerColor(1); // YELLOW
                break;
            case 2:
                ChangePlayerColor(2); // GREEN
                break;
        }
    }

    IEnumerator delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // will continue after this delay, before will call it first before delay
        ShufflePlayerColor();
        Instantiate(playerPassEffect, transform.position + new Vector3(0, -1, 0), Quaternion.Euler(-90, 0, 0));
        totalTriggered = 0; // RESET
    }


    public void IncreaseCoins()
    {
        coins++;
        Debug.Log(coins);
    }

    
    public int coins // GET SET the value
    {
        get { return m_coins; }
        set { m_coins = value; }
    }
}
