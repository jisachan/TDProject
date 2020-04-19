using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	//for organizing the tiles in the editor
	[SerializeField, Tooltip("Where to organize/spawn game objects in the editor's Scene heirarchy.")]
	Transform map;

	[SerializeField, Tooltip("Array of tiles to be used for the map's layout.")]
	GameObject[] tileLayout;

	public static Tile UnitSpawnTile { get; set; }

	public static Tile UnitDespawnTile { get; set; }

	public static Dictionary<GridPoint, Tile> TilesDictionary { get; set; }

	public float TileSize => 2f;

	static GridPoint mapSize;

	static Vector3 worldStartPosition;

	static Stack<Node> finalPath;

	int mapXSize;

	int mapYSize;

	public static Stack<Node> FinalPath
	{
		get
		{
			if (finalPath == null)
			{
				GeneratePath();
			}
			return new Stack<Node>(new Stack<Node>(finalPath));
		}
	}

	void Start()
	{
		LevelData.GetData();
		CreateLevel();
		AStar.CreateNodes();
	}

	void OnDestroy()
	{
		finalPath = null;
		ClearDictionary();
	}


	public void CreateDictionary()
	{
		TilesDictionary = new Dictionary<GridPoint, Tile>();
	}

	public void ClearDictionary()
	{
		TilesDictionary.Clear();
	}

	public void AddTileToDictionary(Tile newTile, int x, int y)
	{
		TilesDictionary.Add(new GridPoint(x, y), newTile);
	}

	public void SetMapSize()
	{
		mapSize = new GridPoint(LevelData.MapData[0].ToCharArray().Length, LevelData.MapData.Length);

		mapXSize = LevelData.MapData[0].ToCharArray().Length;
		mapYSize = LevelData.MapData.Length;

	}

	public void SetWorldStartPosition()
	{
		worldStartPosition = new Vector3(0, 0, 0);
	}

	public void CreateLevel()
	{
		CreateDictionary();

		SetMapSize();

		SetWorldStartPosition();

		for (int y = 0; y < mapYSize; y++)
		{
			char[] newTiles = LevelData.MapData[y].ToCharArray();

			for (int x = 0; x < mapXSize; x++)
			{
				TileType type = (TileType)int.Parse(newTiles[x].ToString());
				PlaceTile(type, x, y);
			}
		}
	}

	private int GetTileIndex(TileType tileType)
	{
		int tileIndex = (int)tileType;

		if (tileIndex == 8) { tileIndex = 4; }
		if (tileIndex == 9) { tileIndex = 5; }

		return tileIndex;
	}

	private void PlaceTile(TileType tileType, int x, int y)
	{
		//is there a way to use the TileType enum for this or is the enum kind of pointless to use here?
		Tile newTile = Instantiate(tileLayout[GetTileIndex(tileType)]).GetComponent<Tile>();
		
		newTile.Setup(new GridPoint(x, y), new Vector3(worldStartPosition.x + (TileSize * x), 0, worldStartPosition.y - (TileSize * y)), map);

		if (tileType == TileType.Start)
		{
			UnitSpawnTile = newTile;
		}

		if(tileType == TileType.End)
		{
			UnitDespawnTile = newTile;
		}

		//found the IsWalkable method in TileMethods after already having created this. My bad!
		if(tileType == TileType.Start || tileType == TileType.End || tileType == TileType.Path)
		{
			newTile.walkable = true;
			newTile.SetPathPosition();
		}

		AddTileToDictionary(newTile, x, y);
	}

	public static void GeneratePath()
	{
		finalPath = AStar.GetPath(UnitSpawnTile.GridPosition, UnitDespawnTile.GridPosition);
	}

	public static bool InBounds(GridPoint position)
	{
		return position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y;
	}
}
