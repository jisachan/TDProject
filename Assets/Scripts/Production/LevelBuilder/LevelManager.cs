using UnityEngine;

public class LevelManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	public static void CreateLevel()
	{
		int mapXSize = LevelData.MapData[0].ToCharArray().Length;
		int mapYSize = LevelData.MapData.Length;

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

	private static void PlaceTile(TileType tileType, int x, int y)
	{
		Debug.Log("tileType: " + tileType + " / x, y:" + x + ", " + y);

	}
}
