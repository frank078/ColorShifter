using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject[] coloredWalls;

    private int thisTowerNumber;

    void Start()
    {
        GameManager.Instance.OnTowerPulling += ModifyColoredWalls;

        if(coloredWalls == null)
        {
            Debug.LogError("ColoredWalls has not been set to WallManager");
            return;
        }
        else
        {
            // Get the Tower Number from TowerObjectPulling which is the parent of this gameObject
            thisTowerNumber = this.gameObject.transform.parent.gameObject.GetComponentInParent<TowerObjectPulling>().TowerNumber;
        }
    }

    // Shuffle Tower Color (Need to replace it with material instead of unity color)
    void ModifyColoredWalls(int TowerNumber)
    {
        // making sure not all of the tower get shuffle
        if(thisTowerNumber == TowerNumber)
        {
            foreach (GameObject _coloredWalls in coloredWalls)
            {
                int shuffleColor = Random.Range(1, 4);
                Debug.Log(shuffleColor);
                switch (shuffleColor)
                {
                    case 1:
                        _coloredWalls.GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    case 2:
                        _coloredWalls.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        break;
                    case 3:
                        _coloredWalls.GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                }
            }
        }
    }
}
