using System.Collections;
using UnityEngine;

public class IceProjectile : ProjectileBase
{
    [SerializeField, Tooltip("Multiplier value with which to reduce target's speed."), Range(0, 1)]
    float slowEffectMultiplier = 0.5f;

    [SerializeField, Tooltip("For how long the target has reduced speed")]
    float slowEffectDuration = 2;

    float tempspeed;

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        StartCoroutine(ApplySlowEffect());
    }

    IEnumerator ApplySlowEffect()
    {
        if (target.Slowed == false)
        {
            target.Slowed = true;
            tempspeed = target.Speed;
            target.Speed *= slowEffectMultiplier;
            yield return new WaitForSeconds(slowEffectDuration);
            target.Speed = tempspeed;
            target.Slowed = false;
            DespawnProjectile();
        }
        else
        {
            DespawnProjectile();
            yield return null;
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
        ResetVarValues();
        IceTower.returnToPool?.Invoke(this);
    }
    public void ResetVarValues()
    {
        gameObject.SetActive(false);
        hasDealtDamage = false;
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}