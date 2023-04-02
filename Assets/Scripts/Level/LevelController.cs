using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private static LevelController instance = null;
    public static LevelController Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<LevelController>();
            return instance;
        }
    }

    private LevelData _data;
    private GameObject _levelRoot = null;

    private List<LevelTile> _tiles = new List<LevelTile>();
    private Dictionary<(int, int), LevelTile> _posToTilelookUpDictionary = new Dictionary<(int, int), LevelTile>();

    public void CreateEmptyLevel(ConfigDatabaseScriptableObject config)
    {
        _data = new LevelData(config._levelWidth,config._levelHeight);

        _levelRoot = InstantiateLevel(_data, Databases.Instance.GetResourceDatabase()._tilePrefab, config._gridSizeInWorldSpace);
    }

    public LevelTile GetTileAtPos(int x, int y)
    {
        return _posToTilelookUpDictionary.TryGetValue((x, y), out LevelTile tile) ? tile : null;
    }

    private GameObject InstantiateLevel(LevelData data, LevelTile tilePrefab, float gridSizeInWorldSpace) {

        GameObject levelRoot = new GameObject("LevelRoot");
        levelRoot.transform.position = Vector3.zero;
        levelRoot.transform.rotation = Quaternion.identity;

        for (int x = 0; x < data.GetWidth(); x++)
        {
            for (int y = 0; y < data.GetHeight(); y++)
            {
                Quaternion rot = Quaternion.identity;
                Vector3 pos = new Vector3((gridSizeInWorldSpace / 2f) + gridSizeInWorldSpace * x, (gridSizeInWorldSpace / 2f) + gridSizeInWorldSpace * y,0f);

                //Center level
                pos -= new Vector3(data.GetWidth() * gridSizeInWorldSpace / 2f, data.GetHeight() * gridSizeInWorldSpace / 2f, 0f);

                LevelTile tile = Instantiate(tilePrefab, pos, rot, levelRoot.transform) as LevelTile;
                tile.SetPosition(new Vector2Int(x, y));

                _tiles.Add(tile);
                _posToTilelookUpDictionary[(x, y)] = tile;
            }
        }

        return levelRoot;
    }
}
