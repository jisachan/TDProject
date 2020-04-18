using System.Collections;
using UnityEngine;

public class FireProjectile : ProjectileBase
{
    [SerializeField, Tooltip("Damage per tick.")]
    float dotDamage = 5;

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
                target.TakeDamage(dotDamage);

                currentTick++;
            }
            //Debug.Log(currentTick);
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
        TowerBase.returnToPool?.Invoke(this);
        hasDealtDamage = false;
        gameObject.SetActive(false);
        gameObject.GetComponent<Renderer>().enabled = true;
        currentTick = 0;
    }
}
