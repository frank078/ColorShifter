using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Delegate is function array
 if Function matches type and parameters, it can be bound to delegate (EX: is of type void, and has no parameters () for this specific delegate)
 Multicast means we're intending to bind multiple functions to the one delegate
 We specify NoParams so we remember this delegate doesn't pass in data
 */

public delegate void MulticastNoParams();

public delegate void MulticastOneParam(int value);
public delegate void MulticastOneFloatParam(float value);

public class Delegates : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
