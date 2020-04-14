using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    
    [SerializeField]
    protected float health;

    protected float accuracy = 0.1f;

    protected Stack<Node> path;

    protected Vector3 destination;

    public abstract UnitType Type { get; set; }

    public static GridPoint GridPosition { get; set; }

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

        //move to observer thingie later, listen for this!
        if (Vector3.Distance(transform.position, LevelManager.UnitDespawnTile.WorldPosition) < accuracy)
        {
            Debug.Log("Despawnnnn");
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination) < accuracy)
        {
            //if (path.Pop().GridPosition == LevelManager.UnitDespawnTile.GridPosition)
            //if (path != null && path.Count == 0)
            //{
            //    Debug.Log("DESPAWN! MUAHAHA!");
            //}
            if (path != null && path.Count > 0)
            {
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().WorldPosition;
            }
        }
    }
}
