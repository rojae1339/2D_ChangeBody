using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PartDetector _detector;
    private PlayerUIView _view;

    private bool _inputLocked = false;
    
    public bool InputLocked { get => _inputLocked; set => _inputLocked = value; }

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
        if (!ctx.performed) return;
        if (_detector == null) return;

        //UI 인터랙션 전
        if (!_inputLocked)
        {
            if (_detector.CurrentDetectedType == DetectedPartType.None) return; // 감지 없으면 무시

            _detector.ToggleInteractUI();
            _player.SetMoveInput(Vector2.zero);

            _inputLocked = true;
            return;
        }

        
        //UI 인터랙션 후 무기교체
        _player.ChangeParts();
    }

    public void OnInteractUIChangeLeft(InputAction.CallbackContext ctx)
    {
        if (!_inputLocked) { return; }

        if (ctx.performed)
        {
            Debug.Log("UI켜진후 좌우 눌림");
            _view.ChangeDropUIPositionLeft();
        }
    }

    public void OnInteractUIChangeRight(InputAction.CallbackContext ctx)
    {
        if (!_inputLocked) { return; }

        if (ctx.performed)
        {
            Debug.Log("UI켜진후 좌우 눌림");
            _view.ChangeDropUIPositionRight();
        }
    }

    public void OnCloseUIOrOpenSetting(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("ESC");
            if (_inputLocked)
            {
                Debug.Log("UI켜진후 ESC눌림");
                _detector.CancelDetect();
                _inputLocked = false;
                return;
            }
        }

        //todo ui안열린상태에서는 setting창 열기
    }
}