using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerInputManager : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_OnPlayerInputMove _rseOnPlayerInputMove;

    //[Header("RSO")]

    //[Header("SSO")]

    IA_PlayerInput _playerInputActions;

    void Awake()
    {
        _playerInputActions = new IA_PlayerInput();

        _playerInputActions.Disable();
        _playerInputActions.Game.Enable();
    }

    private void OnEnable()
    {
        _playerInputActions.Game.Move.performed += ctx => DirectionWanted(ctx);
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    void DirectionWanted(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>().normalized;

        if (Mathf.Abs(inputValue.x) > Mathf.Abs(inputValue.y))
        {
            if (inputValue.x > 0)
            {
                _rseOnPlayerInputMove.RaiseEvent(Direction.Right);
            }
            else
            {
                _rseOnPlayerInputMove.RaiseEvent(Direction.Left);
            }
        }
        else
        {
            if (inputValue.y > 0)
            {
                _rseOnPlayerInputMove.RaiseEvent(Direction.Up);
            }
            else
            {
                _rseOnPlayerInputMove.RaiseEvent(Direction.Down);
            }
        }
    }
}