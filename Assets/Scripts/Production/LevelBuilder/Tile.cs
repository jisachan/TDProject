using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GridPoint GridPosition { get; set; }

    public Vector3 WorldPosition => transform.position;

    public bool Walkable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(GridPoint gridPos, Vector3 worldPosition, Transform parent)
    {
        //look into these when needed, might be nonsensical
        GridPosition = gridPos;
        transform.position = worldPosition;
        transform.SetParent(parent);
    }
}
