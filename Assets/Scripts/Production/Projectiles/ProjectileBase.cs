using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    float speed = 2;

    [SerializeField]
    float damage = 20;

    UnitBase target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    public void DealDamage(ProjectileType projectileType)
    {
        target.TakeDamage(damage);

        if (projectileType == ProjectileType.Fire)
        {
            //specialabilitystuff
        }
    }

    public virtual void SpecialAbility()
    {

    }

    public void SetTarget(UnitBase target)
    {
        this.target = target;
    }
}

public enum ProjectileType
{
    Fire,
    Ice
}
