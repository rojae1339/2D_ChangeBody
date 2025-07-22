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
    private PlayerUIView _playerUIView;
    private PartDetector _partDetector;
    private PlayerAnimation _playerAnimation;
    private AnimationType _currentAnimationType;
    private AnimationType _previousAnimationType; // 추가
    private PlayerCamera _playerCamera;
    
    private Vector2 _moveInput;

    private readonly Vector3 _leftPosition = new Vector3(-0.08f, 0, 0);
    private readonly Vector3 _rightPosition = new Vector3(0.03f, -0.03f, 0);
    private readonly Vector3 _bodyPosition = new Vector3(0, 0, 0);

    [SerializeField]
    private float _throwPower = 5f;
    
    public GameObject L_Weapon { get => l_Weapon; private set => l_Weapon = value; }
    public GameObject R_Weapon { get => r_Weapon; private set => r_Weapon = value; }
    public GameObject Body { get => body; private set => body = value; }

    void Awake()
    {
        _playerMove = gameObject.AddComponent<PlayerMove>();
        _playerAnimation = gameObject.AddComponent<PlayerAnimation>();
        _playerController = GetComponent<PlayerController>();
        _partDetector = gameObject.GetComponent<PartDetector>();
        _playerUIView = GetComponent<PlayerUIView>();
        _playerCamera = GetComponent<PlayerCamera>();

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

    //todo
    public void ChangeParts()
    {
        var partType = _partDetector.CurrentDetectedType;
        
        switch (partType)
        {
            //드롭된 아이템 감지된 타입 == 무기 
            case DetectedPartType.Weapon:
                
                //감지된 무기
                BaseWeapon detectedWeapon = _partDetector.CurrentWeapon;

                //현재 UI상의 드랍된 무기 위치(왼, 오)
                UIOnPosition uiPos = _playerUIView.UIUIPosOnPosition;
                
                //현재 왼손무기
                var currentLeftWeapon = l_Weapon.transform.GetChild(0);
                //현재 오른손 무기
                var currentRightWeapon = r_Weapon.transform.GetChild(0);

                switch (uiPos)
                {
                    //UI가 왼쪽에 있을 경우 -> 왼쪽 무기 선택
                    case UIOnPosition.Left:
                        currentLeftWeapon.transform.parent = null;
                        
                        StartCoroutine(ResetToKinematic(currentLeftWeapon.gameObject, 1f));
                        
                        //바꿀 무기가 왼쪽 선택
                        detectedWeapon.transform.SetParent(l_Weapon.transform,false);
                        detectedWeapon.transform.localPosition = _leftPosition;
                        detectedWeapon.transform.localRotation = Quaternion.identity;
                        detectedWeapon.transform.localScale = new Vector3(1, 1, 1);
                        detectedWeapon.GetComponent<SpriteRenderer>().sortingOrder = 10;
                        if (detectedWeapon.transform.GetChild(0).name == detectedWeapon.name)
                        {
                            detectedWeapon.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 10;
                        }
                        
                        //파티클 끄기
                        detectedWeapon.transform.GetChild(detectedWeapon.transform.childCount - 1).gameObject.SetActive(false);
                        

                        _playerUIView.WeaponPresenter.RefreshWeaponModel();
                        break;
                    
                    //UI가 오른쪽에 있을 경우 -> 오른쪽 무기 선택
                    case UIOnPosition.Right: 
                        currentRightWeapon.transform.parent = null;
                        
                        StartCoroutine(ResetToKinematic(currentRightWeapon.gameObject, 1f));
                        
                        //바꿀 무기가 오른쪽 선택
                        detectedWeapon.transform.SetParent(r_Weapon.transform, false);
                        detectedWeapon.transform.localPosition = _rightPosition;
                        detectedWeapon.transform.localRotation = Quaternion.identity;
                        detectedWeapon.transform.localScale = new Vector3(1, 1, 1);
                        detectedWeapon.GetComponent<SpriteRenderer>().sortingOrder = -10;
                        if (detectedWeapon.transform.GetChild(0).name == detectedWeapon.name)
                        {
                            detectedWeapon.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 10;
                        }
                        
                        //파티클 끄기
                        detectedWeapon.transform.GetChild(detectedWeapon.transform.childCount - 1).gameObject.SetActive(false);

                        _playerUIView.WeaponPresenter.RefreshWeaponModel();
                        break;
                }
                break;
            
            //드롭된 아이템 감지된 타입 == 몸통
            case DetectedPartType.Body:
                //감지된 몸통
                BaseBody detectedBody = _partDetector.CurrentBody;

                var parent = body.transform.parent;
                
                body.transform.parent = null;
                
                StartCoroutine(ResetToKinematic(body, 1f));
                        
                //바꿀 무기가 왼쪽 선택
                
                detectedBody.transform.SetParent(parent,false);
                detectedBody.transform.localPosition = _bodyPosition;
                detectedBody.transform.localRotation = Quaternion.identity;
                detectedBody.transform.localScale = new Vector3(1, 1, 1);
                detectedBody.GetComponent<SpriteRenderer>().sortingOrder = -1;

                body = detectedBody.gameObject;
                
                //파티클 끄기
                detectedBody.transform.GetChild(detectedBody.transform.childCount - 1).gameObject.SetActive(false);

                _playerUIView.WeaponPresenter.RefreshWeaponModel();
                break;
        }
        
        _playerUIView.QuitInteractUI();
    }
    
    private IEnumerator ResetToKinematic(GameObject current, float delay)
    {
        //Throwing Layer
        current.layer = 11;
        
        var rigidbody = current.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        
        Debug.Log($"{gameObject.transform.position}");
        rigidbody.AddForce(gameObject.transform.up * _throwPower, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(delay);
        
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.angularVelocity = 0f;
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        current.layer = 9;
    }
}