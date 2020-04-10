using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public static Point GridPosition { get; set; }

    public Vector3 WorldPosition => transform.position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPosition, Transform parent)
    {
        //look into these when needed, might be nonsensical
        GridPosition = gridPos;
        transform.position = worldPosition;
        transform.SetParent(parent);
    }
}
