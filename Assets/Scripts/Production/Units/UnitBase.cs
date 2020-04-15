using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    
    [SerializeField]
    protected float health;

    [SerializeField, Range(0.05f, 0.5f)]
    protected float accuracy = 0.1f;

    public int MyProperty { get; set; }

    protected Stack<Node> path;

    protected Vector3 destination;

    public abstract UnitType Type { get; set; }

    public static GridPoint GridPosition { get; set; }

    private void OnEnable()
    {
        TowerBase.targets.Add(this);
        Debug.Log("added this to targets");
    }

    private void OnDisable()
    {
        TowerBase.targets.Remove(this);
        Debug.Log("removed this from targets");
    }

    void Start()
    {
        SetPath(LevelManager.FinalPath);
    }

    public void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }

    void Update()
    {
        Move();

        //move to observer thingie
        if(Mathf.Approximately(Vector3.Distance(transform.position, destination), 0))
        {
            //change to pooler thingie
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination) < accuracy)
        {
            if (path != null && path.Count > 0)
            {
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().WorldPosition;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        TowerBase.targets.Remove(this);
    }
}
