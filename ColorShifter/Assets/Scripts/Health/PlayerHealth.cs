using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    protected override void Death()
    {
        Destroy(gameObject); // destroy player, then call Death function

        //gameObject.SetActive(false);

        GameManager.Instance.Death(); // call death
        base.Death();
    }
}
