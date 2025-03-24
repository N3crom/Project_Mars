using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_GridManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _ghostPickUpPrefab;
    [SerializeField] private Transform _gridParent;
    [SerializeField] private Transform _coinParent;

    //[Header("References")]

    [Header("RSE")]
    public RSE_OnPlayerSpawned _rseOnPlayerSpawned;

    [Header("RSO")]
    [SerializeField] private RSO_PlayerSpawn _rsoPlayerSpawn;
    [SerializeField] private RSO_CurrentGrid _rsoCurrentGrid;

    [Header("SSO")]
    [SerializeField] private SSO_LevelGridData _ssoLevelGridData;

    private Dictionary<Vector2Int, S_Tile> _tileDictionary = new Dictionary<Vector2Int, S_Tile>();

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (_rsoCurrentGrid.Value == null)
        {
            _rsoCurrentGrid.Value = new Dictionary<Vector2Int, S_Tile>();
        }
        _rsoCurrentGrid.Value.Clear();

        HashSet<Vector2Int> definedPositions = new HashSet<Vector2Int>();

        // Générer les tuiles définies de _ssoLevelGridData
        foreach (TileData tileData in _ssoLevelGridData.Value)
        {
            CreateTile(tileData.Position, tileData.IsWalkable, tileData.Type);
            definedPositions.Add(tileData.Position);

            if(tileData.HasCoin)
            {
                int rnd = Random.Range(0, 2);
                if(rnd == 1)
                {
                    GameObject coinObj = Instantiate(_coinPrefab, new Vector3(tileData.Position.x, tileData.Position.y, 0), Quaternion.identity, _coinParent);
                }

            }

            if (tileData.HasGhostPickUp)
            {
                GameObject ghostPickUpObj = Instantiate(_ghostPickUpPrefab, new Vector3(tileData.Position.x, tileData.Position.y, 0), Quaternion.identity, _coinParent);
            }
        }

        // Créé des murs par défaut sur toutes les cases où on a pas défini de tuile
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (!definedPositions.Contains(pos))
                {
                    CreateTile(pos, false, TileType.Wall);
                }
            }
        }

        //_rsoCurrentGrid.Value.FirstOrDefault(x => x.Value.TileType == TileType.Spawn);
        var posSpawnPossible = _rsoCurrentGrid.Value.Values
                                         .Where(item => item.TileType == TileType.Spawn)
                                         .ToList();

        //_rsoPlayerSpawn.Value = default;

        if (posSpawnPossible.Count == 0)
        {
            _rsoPlayerSpawn.Value = Vector2Int.zero;
            Debug.LogError("No spawn tile found");
            return;
        }
        else
        {
            _rsoPlayerSpawn.Value = posSpawnPossible[Random.Range(0, posSpawnPossible.Count)].GridPosition;
            Debug.Log("Spawn tile found");
        }

        _rseOnPlayerSpawned.RaiseEvent();
    }

    private void CreateTile(Vector2Int position, bool isWalkable, TileType type)
    {
        GameObject tileObj = Instantiate(_tilePrefab, new Vector3(position.x, position.y, 0), Quaternion.identity, _gridParent);
        S_Tile tile = tileObj.GetComponent<S_Tile>();
        tile.Initialize(position, isWalkable, type);
        _rsoCurrentGrid.Value[position] = tile;
    }

    
}

[System.Serializable]
public class TileData
{
    public Vector2Int Position;
    public bool IsWalkable = true;
    public TileType Type;
    public bool HasCoin;
    public bool HasGhostPickUp;
}