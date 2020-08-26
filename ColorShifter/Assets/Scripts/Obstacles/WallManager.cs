using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject[] coloredWalls; // the ColoredWalls in the gameobject (Cylinder)
    public Material[] colorSelection; // color that the walls can change
    private int thisTowerNumber;
    private int maxColorSelection;

    // PLAYER REFERENCES
    private GameObject thePlayer;
    private MeshRenderer playerCurrentMaterial;
    // ------------------

    void Start()
    {
        //SUBSCRIPTION
        GameManager.Instance.OnTowerPulling += ModifyColoredWalls;
        GameManager.Instance.OnTowerChecking += CheckPlayerColor;

        // CHECK If its Null for coloredWalls
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


        // PLAYER GET REFERENCES (playerCurrentMaterial will reget every success collision)
        thePlayer = GameManager.Instance.GetPlayer();
        playerCurrentMaterial = thePlayer.GetComponent<MeshRenderer>();
        

        // SET maxColorSelection
        maxColorSelection = colorSelection.Length + 1; // For Max Number gacha we get the max color at start + 1

        // SHUFFLE ALL WALLS AND CHECK NEXT WALL (CALL ONCE)
        ModifyColoredWalls(99);
        CheckPlayerColor(99); // on the start of the function it will set to 1 from the if statements
    }

    // Shuffle Tower Color
    void ModifyColoredWalls(int TowerNumber)
    {
        // making sure not all of the tower get shuffle
        if(thisTowerNumber == TowerNumber || TowerNumber == 99)
        {
            foreach (GameObject _coloredWalls in coloredWalls)
            {
              //int = (min inclusive and max exclusive). has to be 1 number higher
                int shuffleColor = Random.Range(1, maxColorSelection);
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

    public void CheckPlayerColor(int NextTowerNumber)
    {
        if(NextTowerNumber > 5)
        {
            NextTowerNumber = 1;
        }

        // making sure only the next tower number the player has to collide
        if(thisTowerNumber == NextTowerNumber)
        {
            int totalNumber = 0;
            int indexColoredWalls = 0;

            playerCurrentMaterial = thePlayer.GetComponent<MeshRenderer>();

            // if there is a color that matched player and one of the walls, return
            foreach (GameObject _coloredWalls in coloredWalls)
            {
                if (_coloredWalls.GetComponent<MeshRenderer>().material.name == playerCurrentMaterial.material.name)
                {
                    return;
                }
                else
                {
                    totalNumber++;
                }             
            }

            // if there are NO color that matched the player and one of the walls. Below here will force ONE of the walls to change to player color
            if(totalNumber == 8)
            {
                Debug.Log("FORCE COLOR");

                if(PlayerColorScan() == null) 
                {
                    Debug.LogError("ERROR, NO COLOR DETECTION from PlayerColorScan()");
                    return;
                }

                // GACHA
                foreach(GameObject _coloredWalls in coloredWalls)
                {
                    // 33% chances player color will be in one of them. Once one of them is the same, RETURN
                    int playerColorRate = Random.Range(1, 4);
                    
                    if (playerColorRate == 1)
                    {  
                        _coloredWalls.GetComponent<MeshRenderer>().material = PlayerColorScan();
                        return;
                    }
                    else if (playerColorRate == 2 || playerColorRate == 3)
                    {
                        indexColoredWalls++;
                        if (indexColoredWalls == 8)
                        {
                            _coloredWalls.GetComponent<MeshRenderer>().material = PlayerColorScan();
                            Debug.Log("E LUCK");
                            return;
                        }
                    }
                }
            }
        }
    }

    // Return the material from colorSelection arrays
    public Material PlayerColorScan()
    {
        // If there is a color that matched one of the colorSelection index and player color
        foreach(Material _colorSelection in colorSelection)
        {
            // Need the word Instance bcoz once you get or set a material onto a gameobject, it creates a copy which named (Instance) beside the material
            if(_colorSelection.name + " (Instance)" == playerCurrentMaterial.material.name)
            {
                return _colorSelection;
            }
        }
        return null;
    }
}