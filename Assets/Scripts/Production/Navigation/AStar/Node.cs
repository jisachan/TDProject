using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public GridPoint GridPosition { get; private set; }

	public Tile TileRef { get; private set; }

	public Vector3 WorldPosition { get; set; }

	public Node Parent { get; private set; }

	public int G { get; set; }

	public int H { get; set; }

	public int F { get; set; }

	public Node(Tile tileRef)
	{
		this.TileRef = tileRef;
		this.GridPosition = tileRef.GridPosition;
		this.WorldPosition = tileRef.pathPosition;
	}

	public void CalcValues(Node parent, Node goal, int gCost)
	{
		this.Parent = parent;
		this.G = parent.G + gCost;
		this.H = (Math.Abs(GridPosition.X - goal.GridPosition.X) +
					Math.Abs(goal.GridPosition.Y - GridPosition.Y) /*need multiplier?*/);
		this.F = G + H;
	}
}
