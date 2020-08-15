using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Material[] colorSelection; // TODO: Add new colors the further player went
    private MeshRenderer currentColor;

    private void Start()
    {
        currentColor = gameObject.GetComponent<MeshRenderer>();
        ChangePlayerColor(0);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if other is ColoredWalls
        if (other.CompareTag("Colored"))
        {
            MeshRenderer coloredWalls = other.gameObject.GetComponent<MeshRenderer>();
            
            // if player color is the same as one of the walls that collide
            if(currentColor.material.name == coloredWalls.material.name)
            {
                ShufflePlayerColor();
            }
            else
            {
                GameplayStatics.DealDamage(gameObject, 1);
            }
        }
    }

    void ChangePlayerColor(int index)
    {
        // If the Index is higher than color selection (ex: want blue, but havent unlocked yet)
        if (colorSelection.Length - 1 < index)
        {
            ShufflePlayerColor();
        }
        currentColor.material = colorSelection[index];
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
}
