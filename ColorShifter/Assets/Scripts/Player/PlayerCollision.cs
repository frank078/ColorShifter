using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Material[] colorSelection;
    private MeshRenderer currentColor;
    private void Start()
    {
        currentColor = gameObject.GetComponent<MeshRenderer>();
        ChangeColor();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }

    void ChangeColor()
    {
        currentColor.material = colorSelection[0];
    }
}
