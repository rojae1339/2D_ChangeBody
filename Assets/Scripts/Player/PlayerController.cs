using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed || ctx.canceled)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            _player.SetMoveInput(input);
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            _player.Attack();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            _player.Jump();
    }
}

