using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    private enum AnimationType
    {
        Idle,
        Move,
        Attack,
        Jump,
        Dead
    }

    [SerializeField]
    private GameObject l_Weapon;
    [SerializeField]
    private GameObject r_Weapon;
    [SerializeField]
    private GameObject body;
    
    private PlayerMove _playerMove;
    private PlayerController _playerController;
    private PlayerAnimation _playerAnimation;
    private PlayerUIView _playerUIView;
    private PartDetector _partDetector;
    private AnimationType _currentAnimationType;
    private AnimationType _previousAnimationType; // 추가
    
    private Vector2 _moveInput;
    
    public GameObject L_Weapon { get => l_Weapon; private set => l_Weapon = value; }
    public GameObject R_Weapon { get => r_Weapon; private set => r_Weapon = value; }
    public GameObject Body { get => body; private set => body = value; }

    void Awake()
    {
        _playerMove = gameObject.AddComponent<PlayerMove>();
        _playerAnimation = gameObject.AddComponent<PlayerAnimation>();
        _playerController = gameObject.AddComponent<PlayerController>();
        _partDetector = gameObject.GetComponent<PartDetector>();
        _playerUIView = GetComponent<PlayerUIView>();

        _playerController.Init(this, _partDetector, _playerUIView);
        _currentAnimationType = AnimationType.Idle;
        _previousAnimationType = AnimationType.Idle;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        _playerMove.MoveDirection = _moveInput;

        UpdatePlayerAnimation();
    }

    //업데이트 플레이어 애니메이션
    private void UpdatePlayerAnimation()
    {
        // Attack, Jump, Dead 상태가 아닐 때만 이동 상태 업데이트
        if (_currentAnimationType != AnimationType.Attack && 
            _currentAnimationType != AnimationType.Jump && 
            _currentAnimationType != AnimationType.Dead)
        {
            float speed = _moveInput.magnitude;
            _currentAnimationType = speed > 0 ? AnimationType.Move : AnimationType.Idle;
        }
        
        // 상태가 변경되었을 때만 애니메이션 업데이트
        if (_currentAnimationType != _previousAnimationType)
        {
            UpdateAnimationState();
            _previousAnimationType = _currentAnimationType;
        }
        
        // Move 상태일 때는 매 프레임 speed 업데이트
        if (_currentAnimationType == AnimationType.Move || _currentAnimationType == AnimationType.Idle)
        {
            _playerAnimation.UpdateMove(_moveInput.magnitude);
        }
    }

    private void UpdateAnimationState()
    {
        switch (_currentAnimationType)
        {
            case AnimationType.Idle:
                // Idle 상태 진입 시 로직
                break;
            case AnimationType.Move:
                // Move 상태 진입 시 로직
                break;
            case AnimationType.Attack:
                // Attack 상태 진입 시 로직 (애니메이션은 Attack() 메서드에서 처리)
                break;
            case AnimationType.Jump:
                // Jump 상태 진입 시 로직 (애니메이션은 Jump() 메서드에서 처리)
                break;
            case AnimationType.Dead:
                // Dead 상태 진입 시 로직
                break;
            default:
                _currentAnimationType = AnimationType.Idle;
                break;
        }
        _currentAnimationType = AnimationType.Idle;
    }

    // PlayerController가 호출
    public void SetMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    public void Attack()
    {
        _currentAnimationType = AnimationType.Attack;
        _playerAnimation.TriggerAttack();
        // todo
    }

    public void Jump()
    {
        _currentAnimationType = AnimationType.Jump;
        _playerAnimation.TriggerJump();
        
        // todo
    }

    // InputSystem 전달만 함
    public void OnMove(InputAction.CallbackContext ctx) => _playerController.OnMove(ctx);
    public void OnAttack(InputAction.CallbackContext ctx) => _playerController.OnAttack(ctx);
    public void OnJump(InputAction.CallbackContext ctx) => _playerController.OnJump(ctx);
    public void OnInteractUI(InputAction.CallbackContext ctx) => _playerController.OnInteractDropUI(ctx);
    public void OnPressESC(InputAction.CallbackContext ctx) => _playerController.OnCloseUIOrOpenSetting(ctx);
    public void OnPressLeftWhileUIInteracted(InputAction.CallbackContext ctx) =>
        _playerController.OnInteractUIChangeLeft(ctx);
    public void OnPressRightWhileUIInteracted(InputAction.CallbackContext ctx) =>
        _playerController.OnInteractUIChangeRight(ctx);
}