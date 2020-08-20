using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // to call the gamemananger

    [SerializeField] GameObject PlayerPawn = null; // used for the player to spawned

    GameObject Player = null; // after the player spawned it will set the player to this (Use this for ref player)

    public GameObject GetPlayer() { return Player; } // get player
    
    [SerializeField] Transform PlayerStart = null;

    public MulticastOneParam OnTowerPulling; // subscribed from WallManager
    public MulticastOneParam OnTowerChecking; // subscribed from WallManager


    void Awake()
    {
        // SINGLETON
        // WITHOUT THIS, INSTANCE WILL BE NULL
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // will not destroy this gameObject when loading new scene
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // ------------------------------------------


        SpawnPlayer(); // TODO: use this on OnSceneLoaded later    
    }


    void SpawnPlayer()
    {
        GameObject _Player;

        //player                          PlayerStartPos        PlayerStartRot
        _Player = Instantiate(PlayerPawn, PlayerStart.position, PlayerStart.rotation);

        Player = _Player;
    }

    // This way it calls all the ColoredWalls. If you want one of them, has to ref the selected walls
    public void ModifyColoredWalls(int TowerNumber)
    {
        OnTowerPulling?.Invoke(TowerNumber); // call the delegate functions that was subscribed
    }

    // This way it calls all the ColoredWalls. If you want one of them, has to ref the selected walls
    public void CheckPlayerColor(int NextTowerNumber)
    {
        OnTowerChecking?.Invoke(NextTowerNumber); // call the delegate functions that was subscribed
    }
}
