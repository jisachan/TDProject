using System.Collections;
using UnityEngine;

public class IceProjectile : ProjectileBase
{
    //remember to attach script to projectile in engine

    [SerializeField, Tooltip("Multiplier value with which to reduce target's speed."), Range(0, 1)]
    float slowEffectMultiplier = 0.5f;

    [SerializeField, Tooltip("For how long the target has reduced speed")]
    float slowEffectDuration = 2;

    bool slowed = false;

    float tempspeed;

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        StartCoroutine(ApplySlowEffect());
    }

    IEnumerator ApplySlowEffect()
    {
        if (slowed == false)
        {
            tempspeed = target.Speed;
            target.Speed *= slowEffectMultiplier;
            slowed = true;
            yield return new WaitForSeconds(slowEffectDuration);
            target.Speed = tempspeed;
            slowed = false;
            DespawnProjectile();
        }             
    }

    public override void HideVisibility()
    {
        base.HideVisibility();
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    public override void DespawnProjectile()
    {
        base.DespawnProjectile();
        TowerBase.returnToPool?.Invoke(this);
        hasDealtDamage = false;
        gameObject.SetActive(false);
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}
