using System;
using UnityEngine;
using System.Collections.Generic;

namespace AI
{
	//TODO: Implement IPathFinder using Dijsktra algorithm.
	//((Sorry, I implemented A* instead since I needed to learn it more ><))
	public class Dijkstra : IPathFinder
	{        
		public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
		{
			throw new NotImplementedException();
		}
	}    
}
