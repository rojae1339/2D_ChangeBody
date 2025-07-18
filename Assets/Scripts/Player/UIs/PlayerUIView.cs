using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
    
    private readonly WeaponUIPresenter _weaponPresenter = new WeaponUIPresenter();
    private readonly BodyUIPresenter _bodyPresenter = new BodyUIPresenter();

    private RectTransform _rootRect;
    private RectTransform _dropUIRect;
    private Vector2 _rootBasePosition;
    private readonly Vector2 _rootInteractPosition = new Vector2(0, -500);
    private Vector2 _dropUIBasePosition;
    private readonly Vector2 _dropUILeftPosition = new Vector2(282.5f, -240);
    private readonly Vector2 _dropUIRightPosition = new Vector2(917.5f, -240);
    
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

    public void ChangeDropInfo(RawImage img, TierType tier, string title, string desc)
    {
        _dropBaseCover.ChangePartInfo(img, tier, title, desc);
    }
    public void ChangePlayerLeftWeaponInfo(RawImage img, TierType tier, string title, string desc)
    {
        _weaponBaseLRCovers[0].ChangePartInfo(img, tier, title, desc);
    }
    public void ChangePlayerRightWeaponInfo(RawImage img, TierType tier, string title, string desc)
    {
        _weaponBaseLRCovers[1].ChangePartInfo(img, tier, title, desc);
    }
    public void ChangePlayerBodyInfo(RawImage img, TierType tier, string title, string desc)
    {
        _bodyBaseCover.ChangePartInfo(img, tier, title, desc);
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
        SetBodyUIActive(true);
        SetWeaponUIActive(false);
        SetInteractUIActive(true);
    }
    public void QuitInteractUI()
    {
        _rootRect.anchoredPosition = _rootBasePosition;
        _dropUIRect.anchoredPosition = _dropUIBasePosition;
        SetBodyUIActive(false);
        SetWeaponUIActive(false);
        SetInteractUIActive(false);
    }
    public void Undetected()
    {
        _rootRect.anchoredPosition = _rootBasePosition;
        _dropUIRect.anchoredPosition = _dropUIBasePosition;
        SetBodyUIActive(false);
        SetWeaponUIActive(false);
        SetInteractUIActive(false);
        SetPartUIActive(false);
    }
    public void ChangeDropUIPositionLeft()
    {
        if (_playerBodyPartsPanel.activeSelf == true)
        {
            return;
        }
        
        if (_dropUIRect.anchoredPosition.Equals(_dropUILeftPosition))
        {
            _dropUIRect.anchoredPosition = _dropUIRightPosition;
        }
        else
        {
            _dropUIRect.anchoredPosition = _dropUILeftPosition;
        }
    }
    public void ChangeDropUIPositionRight()
    {
        if (_playerBodyPartsPanel.activeSelf == true)
        {
            return;
        }
        
        if (_dropUIRect.anchoredPosition.Equals(_dropUIRightPosition))
        {
            _dropUIRect.anchoredPosition = _dropUILeftPosition;
        }
        else
        {
            _dropUIRect.anchoredPosition = _dropUIRightPosition;
        }
    }
}