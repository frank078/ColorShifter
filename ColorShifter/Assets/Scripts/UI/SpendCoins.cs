using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendCoins : MonoBehaviour
{
    #region SIngleton:SpendCoins

    public static SpendCoins Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public int Coins;

    // Update is called once per frame
    public void UseCoins(int amount)
    {
        Coins -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (Coins >= amount);
    }
}
