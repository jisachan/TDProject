using System;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField, Tooltip("How fast the projectile travels.")]
    protected float speed = 8;

    [SerializeField, Tooltip("How much damage the projectile does.")]
    protected float damage = 20;

    protected bool hasDealtDamage = false;

    protected UnitBase target;

    void Update()
    {
        //Possible to put this outside of Update??
        if (target != null)
        {
            ShootProjectile();

            if (ProjectileReachedTarget())
            {
                if (hasDealtDamage == false)
                {
                    DealDamage();
                }
            }
        }
        else
        {
            DespawnProjectile();
        }
    }

    public bool ProjectileReachedTarget()
    {
        if (Mathf.Approximately(Vector3.Distance(transform.position, target.transform.position), 0))
        {
            return true;
        }
        return false;
    }

    public void DealDamage()
    {
        target.TakeDamage(damage);
        hasDealtDamage = true;
        HideVisibility();
        SpecialAbility();
    }

    public virtual void SpecialAbility()
    {
    }

    public void SetTarget(UnitBase target)
    {
        this.target = target;
    }

    public void ShootProjectile()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public virtual void HideVisibility()
    { 
    
    }

    public virtual void DespawnProjectile()
    {
    }
}