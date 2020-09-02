﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public GameObject[] coins; // coins in this children
    private int thisTowerNumber;

    public GameObject[] diamonds;
    

    private void Start()
    {
        //SUBSCRIPTION
        GameManager.Instance.OnTowerPulling += ModifyCoins;

        if(coins.Length == 0)
        {
            Debug.LogError("Coins has not been set to CoinsManager");
            return;
        }
        else
        {
            // Get the Tower Number from TowerObjectPulling which is the parent of this gameObject
            thisTowerNumber = this.gameObject.transform.parent.gameObject.GetComponentInParent<TowerObjectPulling>().TowerNumber;
        }

        //SHUFFLE COINS (CALL ONCE)
        ModifyCoins(99);
    }

    void ModifyCoins(int TowerNumber)
    {
        if (thisTowerNumber == TowerNumber || TowerNumber == 99)
        {
            int coinsIndex = 0; // coins index to check first and second coins sector
            int diamondIndex = -1; // diamond index in diamonds[] array

            // SET ACTIVE DEACTIVATE COINS
            foreach (GameObject _coins in coins)
            {
                coinsIndex++;
                diamondIndex++;

                // FIRST SECTOR (BACK LINE)
                if(coinsIndex < 5)
                {
                // DIAMONDS SPAWNING

                    //3% succession on diamonds spawning
                    float oddsDiamonds = Random.Range(0f, 1f);
                    if (0.03f >= oddsDiamonds)
                    {
                        diamonds[diamondIndex].SetActive(true);
                        // coins start from 0, so the same as diamondIndex
                        _coins.SetActive(false);
                    }
                //------------------------------

                // COINS SPAWNING
                    else
                    {
                        diamonds[diamondIndex].SetActive(false);

                        //40% succession on coins spawning
                        float odds = Random.Range(0f, 1f);
                        if (0.4f >= odds)
                        {
                            _coins.SetActive(true);
                        }
                        else
                        {
                            _coins.SetActive(false);
                        }
                    }
                //----------------------------
                }

                // SECOND SECTOR (FRONT LINE)
                else
                {
                    //30% succession on coins spawning
                    float odds = Random.Range(0f, 1f);
                    if (0.3f >= odds)
                    {
                        _coins.SetActive(true);
                    }
                    else
                    {
                        _coins.SetActive(false);
                    }
                }
            }
        }
    }
}
