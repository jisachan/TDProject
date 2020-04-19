using UnityEngine;

public class Tile : MonoBehaviour
{
    public GridPoint GridPosition { get; set; }

    public Vector3 WorldPosition => transform.position;

    public Vector3 PathPosition { get => pathPosition; set => pathPosition = value; }
 
    Vector3 pathPosition;

    [HideInInspector]
    public bool walkable = false;

    float pathOffset = 0.6f;
       
    public void Setup(GridPoint gridPos, Vector3 worldPosition, Transform parent)
    {
        GridPosition = gridPos;
        transform.position = worldPosition;
        transform.SetParent(parent);
    }

    public void SetPathPosition()
    {
        PathPosition = new Vector3(WorldPosition.x, WorldPosition.y + pathOffset, WorldPosition.z);
    }
}
