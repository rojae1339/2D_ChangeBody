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
        _view = view;
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
            _player.SetMoveInput(Vector2.zero);
            
            //todo 아무 ui도 없을때 E누르면 true가 됨
            _inputLocked = true;  // <- 나중에 View 이벤트와 연동해서 세팅
        }
    }
    
    //todo InteractUI열린후 무기는 원하는 손, 바디는 그냥 E키 누르면 변경되게 하기, 변경 후 가지고 있던 파츠는 땅에 떨구기

    public void OnInteractUIChangeLeft(InputAction.CallbackContext ctx)
    {
        if (!_inputLocked)
        {
            return;
        }

        if (ctx.performed)
        {
            
            _view.ChangeDropUIPositionLeft();   
        }
    }
    
    public void OnInteractUIChangeRight(InputAction.CallbackContext ctx)
    {
        if (!_inputLocked)
        {
            return;
        }

        if (ctx.performed)
        {
            
            _view.ChangeDropUIPositionRight();   
        }
    }

    public void OnCloseUIOrOpenSetting(InputAction.CallbackContext ctx)
    {
        if (_inputLocked)
        {
            if (ctx.performed)
            {
                _detector.CancelDetect();
                _inputLocked = false;

            }
        }

        //todo ui안열린상태에서는 setting창 열기
        
    }
}

