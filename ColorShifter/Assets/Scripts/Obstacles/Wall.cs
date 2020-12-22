using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !GameManager.Instance.isImmortality)
        {
            GameplayStatics.DealDamage(other.gameObject, 1);
        }
    }
}
