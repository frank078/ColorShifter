using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Update is called once per frame

    public GameObject target;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameplayStatics.DealDamage(target, 1);
        }
    }
}
