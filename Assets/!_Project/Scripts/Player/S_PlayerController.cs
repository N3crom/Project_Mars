using UnityEngine;

public class S_PlayerController : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_OnPlayerInputMove _rseOnPlayerInputMove;
    [SerializeField] RSE_OnLevelFinish _rseOnLevelFinish;

    [Header("RSO")]
    [SerializeField] private RSO_PlayerSpawn _rsoPlayerSpawn;
    [SerializeField] private RSO_PlayerPosition _rsoPlayerPosition;
    [SerializeField] private RSO_CurrentGrid _rsoCurrentGrid;
    [SerializeField] private RSO_AllCoinCollected _rsoAllCoinCollected;

    //[Header("SSO")]


    private void OnEnable()
    {
        _rseOnPlayerInputMove.action += Move;
        _rsoPlayerSpawn.onValueChanged += InitialiseSpawnPosition;
    }

    private void OnDisable()
    {
        _rseOnPlayerInputMove.action -= Move;
        _rsoPlayerSpawn.onValueChanged -= InitialiseSpawnPosition;
    }

    void Move(Vector2Int direction)
    {
        if (_rsoCurrentGrid.Value.ContainsKey(_rsoPlayerPosition.Value + direction) && _rsoCurrentGrid.Value[_rsoPlayerPosition.Value + direction].IsWalkable == true)
        {
            _rsoPlayerPosition.Value += direction;
            transform.position += new Vector3(direction.x, direction.y, 0);

            if(_rsoCurrentGrid.Value[_rsoPlayerPosition.Value].TileType == TileType.Exit && _rsoAllCoinCollected == true)
            {
                _rseOnLevelFinish.RaiseEvent();
            }

        }
    }

    void InitialiseSpawnPosition(Vector2Int position)
    {

        _rsoPlayerPosition.Value = position;
        transform.position = new Vector3(_rsoPlayerPosition.Value.x, _rsoPlayerPosition.Value.y, 0);

    }

}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}