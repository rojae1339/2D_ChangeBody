using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PartDetector _detector;
    private PlayerUIView _view;

    private bool _inputLocked = false;

    public void Init(Player player, PartDetector det, PlayerUIView view)
    {
        _player = player;
        _detector = det;
        _view = _view;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (_inputLocked) return;

        if (ctx.performed || ctx.canceled)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            _player.SetMoveInput(input);
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (_inputLocked) return;

        if (ctx.performed)
            _player.Attack();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (_inputLocked) return;

        if (ctx.performed)
            _player.Jump();
    }

    public void OnInteractDropUI(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _detector.ToggleInteractUI();
            _inputLocked = true;  // <- 나중에 View 이벤트와 연동해서 세팅
        }
    }

    public void OnInteractUIChangeAOrD(InputAction.CallbackContext ctx)
    {
        if (!_inputLocked)
        {
            return;
        }

        if (ctx.performed)
        {
            
        }
    }

    public void OnCloseUIOrOpenSetting(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_inputLocked)
            {
                _detector.CancelDetect();
                _inputLocked = false;
            }
        }
        
        //todo ui안열린상태에서는 setting opne
        
    }
}

