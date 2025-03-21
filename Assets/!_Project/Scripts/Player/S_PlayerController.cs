using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class S_PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _ghostBar;

    [Header("Parameters")]
    [SerializeField] private int baseStamina = 5;
    [SerializeField] private float ghostTime = 2f;

    [Header("RSE")]
    public RSE_OnPlayerSpawned _rseOnPlayerSpawned;
    [SerializeField] private RSE_OnPlayerInputMove _rseOnPlayerInputMove;
    [SerializeField] private RSE_OnLevelFinish _rseOnLevelFinish;
    [SerializeField] private RSE_OnGhostPickUpCollected _rseOnGhostPickUpCollected;
    [SerializeField] private RSE_OnStaminaDepleted _rseOnStaminaDepleted;
    [SerializeField] private RSE_OnPlayerStuckInsideWall _rseOnPlayerStuckInsideWall;

    [Header("RSO")]
    [SerializeField] private RSO_PlayerSpawn _rsoPlayerSpawn;
    [SerializeField] private RSO_PlayerPosition _rsoPlayerPosition;
    [SerializeField] private RSO_CurrentGrid _rsoCurrentGrid;
    [SerializeField] private RSO_AllCoinCollected _rsoAllCoinCollected;
    [SerializeField] private RSO_CurrentPlayerStamina _rsoCurrentPlayerStamina;

    private void OnEnable()
    {
        _rseOnPlayerInputMove.action += Move;
        _rseOnPlayerSpawned.action += InitialiseSpawnPosition;
        _rseOnGhostPickUpCollected.action += OnGhostPickUpCollected;
    }

    private void OnDisable()
    {
        _rseOnPlayerInputMove.action -= Move;
        _rseOnPlayerSpawned.action -= InitialiseSpawnPosition;
        _rseOnGhostPickUpCollected.action -= OnGhostPickUpCollected;
    }

    private void Start()
    {
        _rsoCurrentPlayerStamina.Value = baseStamina;
    }

    private void Move(Vector2Int direction)
    {
        if (CanMove(direction) || isGhost && _rsoCurrentPlayerStamina.Value > 0 && _rsoCurrentGrid.Value.ContainsKey(_rsoPlayerPosition.Value + direction))
        {
            _rsoPlayerPosition.Value += direction;
            transform.position += new Vector3(direction.x, direction.y, 0);
            _rsoCurrentPlayerStamina.Value--;

            if (ReachedExitWithAllCoinsCollected())
            {
                _rseOnLevelFinish.RaiseEvent();
            }
            else if (StaminaDepleted())
            {
                _rseOnStaminaDepleted.RaiseEvent();
            }
        }
    }

    private bool CanMove(Vector2Int direction)
    {
        return _rsoCurrentGrid.Value[_rsoPlayerPosition.Value + direction].IsWalkable;
    }

    private bool ReachedExitWithAllCoinsCollected()
    {
        return _rsoCurrentGrid.Value[_rsoPlayerPosition.Value].TileType == TileType.Exit && _rsoAllCoinCollected.Value;
    }

    private bool StaminaDepleted()
    {
        return _rsoCurrentPlayerStamina.Value <= 0;
    }

    private void InitialiseSpawnPosition()
    {
        _rsoPlayerPosition.Value = _rsoPlayerSpawn.Value;
        transform.position = new Vector3(_rsoPlayerPosition.Value.x, _rsoPlayerPosition.Value.y, 0);
    }

    private void OnGhostPickUpCollected()
    {
        StartCoroutine(GhostCoroutine());
    }

    private bool isGhost = false;
    private IEnumerator GhostCoroutine()
    {
        isGhost = true;
        yield return new WaitForSeconds(ghostTime);
        isGhost = false;

        if (!_rsoCurrentGrid.Value[_rsoPlayerPosition.Value].IsWalkable)
            _rseOnPlayerStuckInsideWall.RaiseEvent();
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
