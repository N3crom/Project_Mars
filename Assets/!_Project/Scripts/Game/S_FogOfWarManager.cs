using System.Collections.Generic;
using UnityEngine;



public class S_FogOfWarManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _fogTile;

    [Header("RSE")]
    public RSE_OnPlayerSpawned _rseOnPlayerSpawned;

    [Header("RSO")]
    public RSO_PlayerPosition _rsoPlayerPosition;
    public RSO_PlayerSpawn _rsoPlayerSpawn;

    private Dictionary<Vector2Int, GameObject> _fogTiles = new Dictionary<Vector2Int, GameObject>();
    private bool isPlayerSpawned = false;

    private void OnEnable()
    {
        _rsoPlayerPosition.onValueChanged += DeleteFogTilesAroundPosition;
        _rseOnPlayerSpawned.action += Init;
    }

    private void OnDisable()
    {
        _rsoPlayerPosition.onValueChanged -= DeleteFogTilesAroundPosition;
        _rseOnPlayerSpawned.action -= Init;
    }

    private void Init()
    {
        for (int i = -30; i < 30; i++)
        {
            for (int j = -30; j < 30; j++)
            {
                _fogTiles.Add(new Vector2Int(i, j), InstantiateFogTile(i, j));
            }
        }

        isPlayerSpawned = true;
        DeleteFogTilesAroundPosition(_rsoPlayerSpawn.Value);
    }

    private GameObject InstantiateFogTile(int x, int y)
    {
        Vector3 position = new Vector3(x, y, 0);
        return Instantiate(_fogTile, position, Quaternion.identity);
    }

    private void DeleteFogTilesAroundPosition(Vector2Int position)
    {
        if (!isPlayerSpawned)
            return;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector2Int tilePosition = new Vector2Int(position.x + i, position.y + j);
                if (_fogTiles.ContainsKey(tilePosition))
                {
                    GameObject fogTile = _fogTiles[tilePosition];
                    _fogTiles.Remove(tilePosition);
                    Destroy(fogTile);
                }
            }
        }
    }
}