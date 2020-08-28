using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public GameObject[] coins; // coins in this children
    private int thisTowerNumber;

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
            float coinsIndex = 0; // coins index in coins[] array

            // SET ACTIVE DEACTIVATE COINS
            foreach (GameObject _coins in coins)
            {
                coinsIndex++;
                if(coinsIndex < 5)
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
                else
                {
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
            }
        }
    }
}
