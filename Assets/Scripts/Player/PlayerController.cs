using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMove _playerMove;
    private Vector2 _moveInput = Vector2.zero;

    public void Init(PlayerMove move)
    {
        _playerMove = move;
    }

    private void Update()
    {
        _playerMove.MoveDirection= _moveInput;
    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        if (callback.performed || callback.canceled)
        {
            _moveInput = callback.ReadValue<Vector2>();
        }
    }
}
