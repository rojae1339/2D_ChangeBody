using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum UIOnPosition
{
    None,
    Left,
    Right,
    Body
}

public class PlayerUIView : MonoBehaviour
{
    [SerializeField]
    private Canvas _mainCanvas;
    [SerializeField]
    private GameObject _partsRootPanel;
    [SerializeField]
    private GameObject _interactPanel;
    [SerializeField]
    private GameObject _dropPartsCover;
    [SerializeField]
    private GameObject _playerWeaponPartsPanel;
    [SerializeField]
    private GameObject _playerBodyPartsPanel;

    private BasePartsPanel _dropBaseCover;
    private BasePartsPanel[] _weaponBaseLRCovers;
    private BasePartsPanel _bodyBaseCover;
    
    private WeaponUIPresenter _weaponPresenter = new WeaponUIPresenter();
    public WeaponUIPresenter WeaponPresenter { get => _weaponPresenter; set => _weaponPresenter = value; }
    private BodyUIPresenter _bodyPresenter = new BodyUIPresenter();
    public BodyUIPresenter BodyPresenter { get => _bodyPresenter; set => _bodyPresenter = value; }

    private RectTransform _rootRect;
    private RectTransform _dropUIRect;
    private Vector2 _rootBasePosition;
    private readonly Vector2 _rootInteractPosition = new Vector2(0, -500);
    private Vector2 _dropUIBasePosition;
    private readonly Vector2 _dropUILeftPosition = new Vector2(282.5f, -240);
    private readonly Vector2 _dropUIRightPosition = new Vector2(917.5f, -240);

    public bool IsLeftWeapon { get; private set; } = false;
    public UIOnPosition UIUIPosOnPosition { get; set; }

    [SerializeField] 
    private float _uiMoveDuration = 0.3f; // 이동 시간
    private Coroutine _uiMoveCoroutine;
    
    private void Start()
    {
        Managers.Managers.OnManagerLoadInitialized += Init;
    }

    private void Init()
    {
        if (this == null) return;
        
        _weaponPresenter.Init(this);
        _bodyPresenter.Init(this);

        _dropBaseCover = _dropPartsCover.GetComponent<BasePartsPanel>();
        _weaponBaseLRCovers = _playerWeaponPartsPanel.GetComponentsInChildren<BasePartsPanel>();
        _bodyBaseCover = _playerBodyPartsPanel.GetComponentInChildren<BasePartsPanel>();

        _rootRect = _partsRootPanel.GetComponent<RectTransform>();
        _rootBasePosition = _rootRect.anchoredPosition;
        _dropUIRect = _dropPartsCover.GetComponent<RectTransform>();
        _dropUIBasePosition = _dropUIRect.anchoredPosition;
        
        Managers.Managers.OnManagerLoadInitialized -= Init;
    }

    #region 무기&바디 드랍, 플레이어 UI 변경

    public void ChangeDropInfo(Sprite spr, TierType tier, string title, string desc)
    {
        _dropBaseCover.ChangePartInfo(spr, tier, title, desc);
    }
    public void ChangePlayerLeftWeaponInfo(Sprite spr, TierType tier, string title, string desc)
    {
        _weaponBaseLRCovers[0].ChangePartInfo(spr, tier, title, desc);
    }
    public void ChangePlayerRightWeaponInfo(Sprite spr, TierType tier, string title, string desc)
    {
        _weaponBaseLRCovers[1].ChangePartInfo(spr, tier, title, desc);
    }
    public void ChangePlayerBodyInfo(Sprite spr, TierType tier, string title, string desc)
    {
        _bodyBaseCover.ChangePartInfo(spr, tier, title, desc);
    }

    #endregion

    #region UI SetActive변경 로직

    private void SetWeaponUIActive(bool isActive)
    {
        _playerWeaponPartsPanel.SetActive(isActive);
    }
    private void SetBodyUIActive(bool isActive)
    {
        _playerBodyPartsPanel.SetActive(isActive);
    }
    private void SetInteractUIActive(bool isActive)
    {
        _interactPanel.SetActive(isActive);
    }
    public void SetPartUIActive(bool isActive)
    {
        _partsRootPanel.SetActive(isActive);
    }

    #endregion

    public void PressedInteractKeyOnWeapon()
    {
        _rootRect.anchoredPosition = _rootInteractPosition;
        SetBodyUIActive(false);
        SetWeaponUIActive(true);
        SetInteractUIActive(true);
    }
    public void PressedInteractKeyOnBody()
    {
        _rootRect.anchoredPosition = _rootInteractPosition;
        UIUIPosOnPosition = UIOnPosition.Body;
        SetBodyUIActive(true);
        SetWeaponUIActive(false);
        SetInteractUIActive(true);
    }
    public void QuitInteractUI()
    {
        _rootRect.anchoredPosition = _rootBasePosition;
        _dropUIRect.anchoredPosition = _dropUIBasePosition;
        UIUIPosOnPosition = UIOnPosition.None;
        SetBodyUIActive(false);
        SetWeaponUIActive(false);
        SetInteractUIActive(false);
    }
    public void Undetected()
    {
        _rootRect.anchoredPosition = _rootBasePosition;
        _dropUIRect.anchoredPosition = _dropUIBasePosition;
        UIUIPosOnPosition = UIOnPosition.None;
        SetBodyUIActive(false);
        SetWeaponUIActive(false);
        SetInteractUIActive(false);
        SetPartUIActive(false);
    }

    #region Weapon UI 변경시 lerp애니메이션

    // 왼쪽으로 토글
    public void ChangeDropUIPositionLeft()
    {
        if (_playerBodyPartsPanel.activeSelf) return;
        Vector2 targetPos;
        if (_dropUIRect.anchoredPosition.Equals(_dropUILeftPosition))
        {
            targetPos = _dropUIRightPosition;
            UIUIPosOnPosition = UIOnPosition.Right;
        }
        else
        {
            targetPos = _dropUILeftPosition;
            UIUIPosOnPosition = UIOnPosition.Left;
        }

        StartMoveUI(targetPos);
    }

    // 오른쪽으로 토글
    public void ChangeDropUIPositionRight()
    {
        if (_playerBodyPartsPanel.activeSelf) return;
        Vector2 targetPos;
        if (_dropUIRect.anchoredPosition.Equals(_dropUIRightPosition))
        {
            targetPos = _dropUILeftPosition;
            UIUIPosOnPosition = UIOnPosition.Left;
        }
        else
        {
            targetPos = _dropUIRightPosition;
            UIUIPosOnPosition = UIOnPosition.Right;
        }

        StartMoveUI(targetPos);
    }

    private void StartMoveUI(Vector2 target)
    {
        // 이전 이동 중이면 중단
        if (_uiMoveCoroutine != null)
            StopCoroutine(_uiMoveCoroutine);

        _uiMoveCoroutine = StartCoroutine(LerpUIPosition(_dropUIRect.anchoredPosition, target));
    }

    private IEnumerator LerpUIPosition(Vector2 start, Vector2 end)
    {
        float elapsed = 0f;

        while (elapsed < _uiMoveDuration)
        {
            elapsed += Time.deltaTime;
            _dropUIRect.anchoredPosition = Vector2.Lerp(start, end, elapsed / _uiMoveDuration);
            yield return null;
        }

        _dropUIRect.anchoredPosition = end;
        _uiMoveCoroutine = null;
    }

    #endregion
}