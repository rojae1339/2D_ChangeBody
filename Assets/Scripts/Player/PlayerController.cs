using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerInput _input;
    private PartDetector _detector;
    private PlayerUIView _view;

    private bool _inputLocked = false;

    public void Init(Player player, PartDetector det, PlayerUIView view)
    {
        _player = player;
        _input = gameObject.GetComponent<PlayerInput>();
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
        if (!ctx.performed) return;
        
        if (_detector == null) return;

        //UI 인터랙션 전
        if (_inputLocked == false)
        {
            if (_detector.CurrentDetectedType == DetectedPartType.None) return; // 감지 없으면 무시

            _detector.ToggleInteractUI();
            _player.SetMoveInput(Vector2.zero);
            
            //인터랙션시 인풋맵 UI로 변경
            _input.SwitchCurrentActionMap("UI");

            _inputLocked = true;
        }
        //UI 인터랙션 후 무기교체
        else if (_inputLocked == true)
        {
            if (_view.UIUIPosOnPosition == UIOnPosition.None)
            {
                return;
            }
            
            _player.ChangeParts();
            _input.SwitchCurrentActionMap("Player");
            _inputLocked = false;
        }
    }

    public void OnInteractUIChangeLeft(InputAction.CallbackContext ctx)
    {
        if (!_inputLocked) { return; }

        if (ctx.performed)
        {
            _view.ChangeDropUIPositionLeft();
        }
    }

    public void OnInteractUIChangeRight(InputAction.CallbackContext ctx)
    {
        if (!_inputLocked) { return; }

        if (ctx.performed)
        {
            _view.ChangeDropUIPositionRight();
        }
    }

    public void OnCloseUIOrOpenSetting(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_inputLocked)
            {
                _detector.CancelDetect();
                
                //인터랙션 아웃시 인풋맵 player로 변경
                _input.SwitchCurrentActionMap("Player");
                
                _inputLocked = false;
                return;
            }
        }

        //todo ui안열린상태에서는 setting창 열기
    }
}