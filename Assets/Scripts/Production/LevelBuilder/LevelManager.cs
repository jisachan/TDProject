using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] tileLayout;

	[SerializeField]
	private Transform map;

	private static GridPoint mapSize;

	static Vector3 worldStartPosition;

	public static Tile UnitSpawnTile { get; set; }

	public static Tile UnitDespawnTile { get; set; }

	public static Dictionary<GridPoint, Tile> TilesDictionary { get; set; }

	public float TileSize => 2f;

	private static Stack<Node> finalPath;

	public static Stack<Node> FinalPath
	{
		get
		{
			if(finalPath == null)
			{
				GeneratePath();
			}
			return new Stack<Node>(new Stack<Node>(finalPath));
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		CreateLevel();
	}

	public void CreateLevel()
	{
		TilesDictionary = new Dictionary<GridPoint, Tile>();

		mapSize = new GridPoint(LevelData.MapData[0].ToCharArray().Length, LevelData.MapData.Length);

		int mapXSize = LevelData.MapData[0].ToCharArray().Length;
		int mapYSize = LevelData.MapData.Length;

		//worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 50));
		worldStartPosition = new Vector3(0, 0, 0);

		for (int y = 0; y < mapYSize; y++)
		{
			char[] newTiles = LevelData.MapData[y].ToCharArray();

			for (int x = 0; x < mapXSize; x++)
			{
				//double check tomorrow
				TileType type = (TileType)int.Parse(newTiles[x].ToString());
				PlaceTile(type, x, y);
			}
		}
	}

	private void PlaceTile(TileType tileType, int x, int y)
	{
		//Debug.Log("tileType: " + tileType + " / x, y:" + x + ", " + y);

		int tileIndex = (int)tileType;

		if (tileIndex == 8) { tileIndex = 4; }
		if (tileIndex == 9) { tileIndex = 5; }

		//is there a way to use the enum for this or is the enum kind of pointless to use at all?
		//TileScript newTile = Instantiate(tileLayout[tileIndex]).GetComponent<TileScript>();
		//GameObject newTile = Instantiate(tileLayout[tileIndex]);
		
		Tile newTile = Instantiate(tileLayout[tileIndex]).GetComponent<Tile>();
		
		newTile.Setup(new GridPoint(x, y), new Vector3(worldStartPosition.x + (TileSize * x), 0, worldStartPosition.y - (TileSize * y)), map);

		if (tileType == TileType.Start)
		{
			UnitSpawnTile = newTile;
		}

		if(tileType == TileType.End)
		{
			UnitDespawnTile = newTile;
		}

		if(tileType == TileType.Start || tileType == TileType.End || tileType == TileType.Path)
		{
			newTile.Walkable = true;
		}
		//newTile.transform.position = worldStartPosition + new Vector3(x * 2f, 0, -y * 2f);

		TilesDictionary.Add(new GridPoint(x, y), newTile);
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
