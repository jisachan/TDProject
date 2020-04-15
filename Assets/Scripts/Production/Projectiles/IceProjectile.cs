using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : ProjectileBase
{
    [SerializeField, Tooltip("Multiplier value with which to reduce target's speed."), Range(0, 1)]
    float slowEffectMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
