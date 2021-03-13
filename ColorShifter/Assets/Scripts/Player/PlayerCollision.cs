using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Material[] colorSelection; // TODO: Add new colors the further player went
    private SkinnedMeshRenderer currentColor;
    private int nextTower;
    public Material immortalityColor; // Immortality color

    //PARTICLES
    public GameObject playerPassEffect;
    public GameObject coinPickup;
    public GameObject invincibilityFX;

    // AVOID Double Trigger
    private int totalTriggered = 0;


    private void Start()
    {
        currentColor = gameObject.GetComponent<SkinnedMeshRenderer>();

        // if player is restored from death (immortal) 
        if (GameManager.Instance.isImmortality)
        {
            ChangeToImmortalColor();
            Instantiate(invincibilityFX, transform.position + new Vector3(0, -1, 0), Quaternion.Euler(-90, 0, 0));
        }
        else // else set it to default color (red)
        {
            ChangePlayerColor(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // if other is ColoredWalls
        if (other.CompareTag("Walls"))
        {
            // ColoredWalls Ref  (this gameobject is Cylinder)
            MeshRenderer coloredWallsColor = other.gameObject.GetComponent<MeshRenderer>();
            nextTower = 1 + other.gameObject.transform.parent.parent.GetComponentInParent<TowerObjectPulling>().TowerNumber;

            // if player color is the same as one of the walls that collide OR player is immortal
            if (currentColor.material.name == coloredWallsColor.material.name || GameManager.Instance.isImmortality)
            {
                if (totalTriggered == 0) // AVOID DOUBLE TRIGGER
                {
                    totalTriggered++;
                    StartCoroutine(delayWallTrigger(0.15f)); // delay it, to avoid getting killed from changing color while still inside the walls
                }
            }
            else
            {
                GameplayStatics.DealDamage(gameObject, 1);
            }
        }

        // For Coins and Diamonds
        if (other.CompareTag("Coins"))
        {
            IncreaseCoins(other.gameObject);
        }
    }

    void ChangePlayerColor(int index)
    {
        // If the Index is higher than color selection (ex: want green, but havent unlocked yet)
        if (colorSelection.Length - 1 < index)
        {
            Debug.LogError("COLOR OUT OF INDEX");
            return;
        }

        // Check if the Game unlocks Green yet
        if(colorSelection[index].name == "Green")
        {
            if (!GameManager.Instance.isGreen)
            {
                ShufflePlayerColor();
                return; // has to put return since it calls the function below which was changing color
            }
        }

        // Check if the Game unlocks Pink yet
        if (colorSelection[index].name == "Pink")
        {
            if (!GameManager.Instance.isPink)
            {
                ShufflePlayerColor();
                return; // has to put return since it calls the function below which was changing color
            }
        }

        currentColor.material = colorSelection[index];
        GameManager.Instance.CheckPlayerColor(nextTower); // call it here so that they could get latest material references
    }

    void ShufflePlayerColor()
    {
        // Check gacha on each color
        for (int i = 0; i < colorSelection.Length; i++)
        {
            // 20% succession
            float odds = Random.Range(0f, 1f);
            if (0.20f >= odds)
            {
                ChangePlayerColor(i);
                return;
            }
            else
            {
                // Shuffle again if this is last element
                if (i == colorSelection.Length - 1)
                {
                    ShufflePlayerColor();
                }
            }
        }
    }

    IEnumerator delayWallTrigger(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // will continue after this delay, before will call it first before delay
        ShufflePlayerColor();

        // if player is immortal, will not gain point / score
        if (GameManager.Instance.isImmortality)
        {
            GameManager.Instance.ModifyScoreUI(false); // call the delegate functions for ScoreUI
        }
        else
        {
            GameManager.Instance.ModifyScoreUI(true); // call the delegate functions for ScoreUI
        }
        
        //Play particles
        Instantiate(playerPassEffect, transform.position + new Vector3(0, -1, 0), Quaternion.Euler(-90, 0, 0));

        totalTriggered = 0; // RESET
    }


    public void IncreaseCoins(GameObject coin)
    {
        if (coin.gameObject.GetComponent<Diamonds>() != null)
        {
            //call the delegate functions for CoinsUI
            GameManager.Instance.ModifyCoinsUI(5); // DIAMONDS
        }
        else
        {
            GameManager.Instance.ModifyCoinsUI(1); // COINS
        }

        // Play particles
        Instantiate(coinPickup, coin.transform.position, Quaternion.Euler(-90, 0, 0));
    }

    public void ChangeToImmortalColor()
    {
        currentColor.material = immortalityColor;
    }
}
