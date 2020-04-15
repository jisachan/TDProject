using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : ProjectileBase
{
    [SerializeField, Tooltip("Damage per tick.")]
    float dotDamage;

    [SerializeField, Tooltip("How many times the dot damage will be applied to the target.")]
    int nrOfTicks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
    }
}
