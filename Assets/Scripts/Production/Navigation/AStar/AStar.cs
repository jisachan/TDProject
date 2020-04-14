using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
	private static Dictionary<GridPoint, Node> nodes;

	static Tile neighbourTile;

	static int gCost = 10;

	private static void CreateNodes()
	{
		nodes = new Dictionary<GridPoint, Node>();

		foreach(Tile tile in LevelManager.TilesDictionary.Values)
		{
			nodes.Add(tile.GridPosition, new Node(tile));
		}
	}

	public static Stack<Node> GetPath(GridPoint start, GridPoint goal)
	{
		if (nodes == null)
		{
			CreateNodes();
		}

		HashSet<Node> openList = new HashSet<Node>();

		HashSet<Node> closedList = new HashSet<Node>();

		Stack<Node> finalPath = new Stack<Node>();

		Node currentNode = nodes[start];

		openList.Add(currentNode);

		while(openList.Count>0)
		{
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (Math.Abs(x + y) == 1)
					{

						GridPoint neighbourPos = new GridPoint(currentNode.GridPosition.X - x,
														currentNode.GridPosition.Y - y);

						if (LevelManager.InBounds(neighbourPos))
						{

							neighbourTile = LevelManager.TilesDictionary[neighbourPos];

							if (neighbourTile.Walkable == true && neighbourPos != currentNode.GridPosition)
							{
								Node neighbour = nodes[neighbourPos];

								if (openList.Contains(neighbour))
								{
									if (currentNode.G + gCost < neighbour.G)
									{
										neighbour.CalcValues(currentNode, nodes[goal], gCost);
									}
								}

								else if (!closedList.Contains(neighbour))
								{
									openList.Add(neighbour);
									neighbour.CalcValues(currentNode, nodes[goal], gCost);
								}
							}
						}
					}
				}
			}
			openList.Remove(currentNode);
			closedList.Add(currentNode);

			if (openList.Count > 0)
			{
				currentNode = openList.OrderBy(n => n.F).First();
			}

			if (currentNode == nodes[goal])
			{
				while ((currentNode).GridPosition != start)
				{
					finalPath.Push(currentNode);
					currentNode = currentNode.Parent;
				}
				break;
			}
		}
		return finalPath;
	}
}
