using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    protected float speed = 5;

    [SerializeField]
    protected float damage = 20;

    protected UnitBase target;

    [SerializeField]
    protected bool hasDealtDamage = false;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            ShootProjectile();

            //move to observer later
            if (Mathf.Approximately(Vector3.Distance(transform.position, target.transform.position), 0))
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

    public void DealDamage()
    {
        target.TakeDamage(damage);
        SpecialAbility();
        hasDealtDamage = true;
        HideVisibility();
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
        hasDealtDamage = false;
    }
}
