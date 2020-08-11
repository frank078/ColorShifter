using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basically create a static class that will be access to everything.
public static class GameplayStatics
{
    public static void DealDamage(GameObject DamagedObject, int Damage)
    {
        HealthComponent health = DamagedObject.GetComponent<HealthComponent>();
        if (health)
        {
            health.ApplyDamage(Damage);
        }
    }
}
