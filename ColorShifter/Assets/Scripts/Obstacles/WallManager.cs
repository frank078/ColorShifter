﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnTowerPulling += ModifyColoredWalls;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ModifyColoredWalls()
    {
        Debug.Log("GACHAS");
    }
}
