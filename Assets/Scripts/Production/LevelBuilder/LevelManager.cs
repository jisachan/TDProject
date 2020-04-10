using UnityEngine;

public class LevelManager : MonoBehaviour
{
	static Vector3 worldStartPosition;

	[SerializeField]
	private GameObject[] tileLayout;

	public static Transform UnitSpawnTile { get; set; }

	public static Transform UnitDespawnTile { get; set; }

	// Start is called before the first frame update
	void Start()
	{
		CreateLevel();
	}

	public void CreateLevel()
	{
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
		Debug.Log("tileType: " + tileType + " / x, y:" + x + ", " + y);

		int tileIndex = (int)tileType;

		if (tileIndex == 8) { tileIndex = 4; }
		if (tileIndex == 9) { tileIndex = 5; }

		//is there a way to use the enum for this or is the enum kind of pointless to use at all?
		//TileScript newTile = Instantiate(tileLayout[tileIndex]).GetComponent<TileScript>();
		GameObject newTile = Instantiate(tileLayout[tileIndex]);

		if(tileType == TileType.Start)
		{
			UnitSpawnTile = newTile.transform;
		}

		if(tileType == TileType.End)
		{
			UnitDespawnTile = newTile.transform;
		}

		newTile.transform.position = worldStartPosition + new Vector3(x * 2f, 0, -y * 2f);
	}
}
