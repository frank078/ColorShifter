using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] float lifeTime = 3.0f;

    [SerializeField] GameObject[] VFX; // that takes the particles

    List<GameObject> spawnedEffects = new List<GameObject>(); // once a particle spawned, add it to here

    void Start()
    {
        if (VFX.Length == 0)
        {
            Debug.LogError("VFX has not been set to Effects");
            return;
        }

        // foreach all gameobject in VFX and instantiate it and add it to the spawnedEffects list
        foreach (GameObject effect in VFX)
        {
            GameObject _neweffect = Instantiate(effect, transform.position, transform.rotation);

            spawnedEffects.Add(_neweffect);
        }
        // Destroy particles by the amount of lifetime, also destroy this gameobject
        Invoke("KillObjects", lifeTime);
    }

    void KillObjects()
    {
        foreach(GameObject effect in spawnedEffects)
        {
            Destroy(effect);
        }
        Destroy(gameObject);
    }

}
