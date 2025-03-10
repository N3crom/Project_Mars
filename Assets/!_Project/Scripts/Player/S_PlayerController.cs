using UnityEngine;

public class S_PlayerController : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_OnPlayerInputMove _rseOnPlayerInputMove;

    //[Header("RSO")]

    //[Header("SSO")]

    private void OnEnable()
    {
        _rseOnPlayerInputMove.action += Move;
    }

    private void OnDisable()
    {
        _rseOnPlayerInputMove.action -= Move;
    }

    void Move(Direction direction)
    {

    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}