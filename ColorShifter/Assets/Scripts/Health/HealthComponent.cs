using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health = 1;

    float maxHealth;

    [SerializeField] GameObject deathFX = null;

    public MulticastNoParams OnDeath;
    public MulticastOneParam OnDamaged;

    void Awake()
    {
        maxHealth = health;
    }

    public virtual void ApplyDamage(int Damage)
    {
        //DAMAGE
        if(Damage <= 0 || health <= 0)
        {
            return;
        }

        health -= Damage;

        if(health <= 0)
        {
            Death();
        }

        if (OnDamaged != null)
        {
            //?. is a null reference check, it will not call the OnDamaged Delegate if there are no bound functions (meaning the delegate == null)
            OnDamaged?.Invoke(Damage);
        }
        else
        {
            //Debug.LogError(name + " No functions bound to OnDamaged delegate");
        }
    }

    protected virtual void Death()
    {
        // DEATH
        if(OnDeath != null)
        {
            OnDeath?.Invoke();
        }
        else
        {
            //Debug.LogError(name + " No functions bound to OnDeath Delegate");
        }

        //PARTICLES (Currently no particles is made)
        if(deathFX != null)
        {
                       //object      position          rotation. (transform.rotation if you want it to be auto)
            Instantiate(deathFX, transform.position + new Vector3(0, -1, 0), Quaternion.Euler(-90, 0, 0));

            Debug.Log("Health Component - Death Called");
        }
    }
}
