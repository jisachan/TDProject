using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GridPoint GridPosition { get; set; }

    public Vector3 WorldPosition => transform.position;

    public Vector3 pathPosition;

    float pathOffset = 0.6f;

    public bool Walkable = false;

    public void Setup(GridPoint gridPos, Vector3 worldPosition, Transform parent)
    {
        //look into these when needed, might be nonsensical
        GridPosition = gridPos;
        transform.position = worldPosition;
        transform.SetParent(parent);
    }

    public void SetPathPosition()
    {
        pathPosition = new Vector3(WorldPosition.x, WorldPosition.y + pathOffset, WorldPosition.z);
    }
}
