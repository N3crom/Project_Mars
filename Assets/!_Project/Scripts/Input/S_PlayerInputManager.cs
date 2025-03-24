using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerInputManager : MonoBehaviour
{
    [Header("Parameters")]
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    [SerializeField] private float swipeThreshold;
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
    private void Update()
    {
        SwipeDirection();
    }

    // If you want Keyboard Input
    void DirectionWanted(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>().normalized;

        if (Mathf.Abs(inputValue.x) > Mathf.Abs(inputValue.y))
        {
            if (inputValue.x > 0)
            {
                _rseOnPlayerInputMove.RaiseEvent(Vector2Int.right);
            }
            else
            {
                _rseOnPlayerInputMove.RaiseEvent(Vector2Int.left);
            }
        }
        else
        {
            if (inputValue.y > 0)
            {
                _rseOnPlayerInputMove.RaiseEvent(Vector2Int.up);
            }
            else
            {
                _rseOnPlayerInputMove.RaiseEvent(Vector2Int.down);
            }
        }
    }

    void SwipeDirection()
    {
        //For mobile device

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if(touch.phase == UnityEngine.TouchPhase.Began)
        //    {
        //        touchStartPos = touch.position;
        //    }
        //    else if(touch.phase == UnityEngine.TouchPhase.Ended)
        //    {
        //        touchEndPos = touch.position;
        //        DetectSwipe();
        //    }
        //}
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            DetectSwipe();
        }
    }

    private void DetectSwipe()
    {
        Vector2 swipeDirection = touchEndPos - touchStartPos;
        if(swipeDirection.magnitude > swipeThreshold)
        {
            float angle = Mathf.Atan2(swipeDirection.y, swipeDirection.x) * Mathf.Rad2Deg;
            if(angle >= -45 && angle < 45)
            {
                _rseOnPlayerInputMove.RaiseEvent(Vector2Int.right);
            }
            else if(angle >= 45 && angle < 135)
            {
                _rseOnPlayerInputMove.RaiseEvent(Vector2Int.up);
            }
            else if(angle >=  -135 && angle < -45)
            {
                _rseOnPlayerInputMove.RaiseEvent(Vector2Int.down);
            }
            else
            {
                _rseOnPlayerInputMove.RaiseEvent(Vector2Int.left);
            }
        }
    }
}