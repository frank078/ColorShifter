using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject[] coloredWalls;
    public Material[] colorSelection;
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
                switch (shuffleColor)
                {
                    case 1:                                                   //RED
                        _coloredWalls.GetComponent<MeshRenderer>().material = colorSelection[0];
                        break;
                    case 2:                                                   //YELLOW
                        _coloredWalls.GetComponent<MeshRenderer>().material = colorSelection[1];
                        break;
                    case 3:                                                   //GREEN
                        _coloredWalls.GetComponent<MeshRenderer>().material = colorSelection[2];
                        break;
                }
            }
        }
    }
}
