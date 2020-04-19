using System.Collections;
using UnityEngine;

public class FireProjectile : ProjectileBase
{
    [SerializeField, Tooltip("Damage per tick.")]
    float tickDamage = 5;

    [SerializeField, Tooltip("How many seconds between each damage tick.")]
    float tickTimer = 1.5f;

    [SerializeField, Tooltip("How many times the dot damage will be applied to the target.")]
    int maxTicks = 3;

    int currentTick = 0;

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        StartCoroutine(ApplyDotDamage());
    }

    IEnumerator ApplyDotDamage()
    {
        while (true)
        {
            if (currentTick < maxTicks)
            {
                target.TakeDamage(tickDamage);

                currentTick++;
            }

            if (currentTick == maxTicks)
            {
                DespawnProjectile();
                yield break;
            }
            yield return new WaitForSeconds(tickTimer);
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
        FireTower.returnToPool?.Invoke(this);
    }

    public void ResetVarValues()
    {
        gameObject.SetActive(false);
        hasDealtDamage = false;
        currentTick = 0;
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}