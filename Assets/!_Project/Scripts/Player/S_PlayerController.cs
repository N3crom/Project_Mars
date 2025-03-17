using UnityEngine;

public class S_PlayerController : MonoBehaviour
{
    //[Header("Parameters")]
    [SerializeField] private int baseStamina = 5;

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_OnPlayerInputMove _rseOnPlayerInputMove;
    [SerializeField] RSE_OnLevelFinish _rseOnLevelFinish;
    [SerializeField] RSE_OnStaminaDepleted _rseOnStaminaDepleted;

    [Header("RSO")]
    [SerializeField] private RSO_PlayerSpawn _rsoPlayerSpawn;
    [SerializeField] private RSO_PlayerPosition _rsoPlayerPosition;
    [SerializeField] private RSO_CurrentGrid _rsoCurrentGrid;
    [SerializeField] private RSO_AllCoinCollected _rsoAllCoinCollected;
    [SerializeField] private RSO_CurrentPlayerStamina _rsoCurrentPlayerStamina;

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

    private void Start()
    {
        _rsoCurrentPlayerStamina.Value = baseStamina;
    }

    void Move(Vector2Int direction)
    {
        if (_rsoCurrentGrid.Value.ContainsKey(_rsoPlayerPosition.Value + direction) && _rsoCurrentGrid.Value[_rsoPlayerPosition.Value + direction].IsWalkable == true)
        {
            _rsoPlayerPosition.Value += direction;
            transform.position += new Vector3(direction.x, direction.y, 0);
            _rsoCurrentPlayerStamina.Value--;

            if (_rsoCurrentGrid.Value[_rsoPlayerPosition.Value].TileType == TileType.Exit && _rsoAllCoinCollected == true)
            {
                _rseOnLevelFinish.RaiseEvent();
            }
            else if (_rsoCurrentPlayerStamina.Value <= 0)
            {
                _rseOnStaminaDepleted.RaiseEvent();
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