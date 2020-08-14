using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<MeshRenderer>().material.color == Color.red)
        {
            GameplayStatics.DealDamage(other.gameObject, 1);
        }
    }
}
