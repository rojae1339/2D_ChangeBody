using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour
{
    public event Action OnMoveDropUIPosition; 
    
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
    private readonly Vector3 _rootBasePosition = new Vector2(0, -250);
    private readonly Vector3 _rootInteractPosition = new Vector2(0, -500);
    private readonly Vector3 _dropUILeftPosition;
    private readonly Vector3 _dropUIRightPosition;
    
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
        _dropUIRect = _dropPartsCover.GetComponent<RectTransform>();
        
        Managers.Managers.OnManagerLoadInitialized -= Init;
    }

    public void ChangeDropInfo(Image img, TierType tier, string title, string desc)
    {
        _dropBaseCover.ChangePartInfo(img, tier, title, desc);
    }

    //todo E키 누르면 UI On시키기
    public void ChangePlayerLeftWeaponInfo(Image img, TierType tier, string title, string desc)
    {
        _weaponBaseLRCovers[0].ChangePartInfo(img, tier, title, desc);
    }
    public void ChangePlayerRightWeaponInfo(Image img, TierType tier, string title, string desc)
    {
        _weaponBaseLRCovers[1].ChangePartInfo(img, tier, title, desc);
    }
    public void ChangePlayerBodyInfo(Image img, TierType tier, string title, string desc)
    {
        _bodyBaseCover.ChangePartInfo(img, tier, title, desc);
    }

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
        SetBodyUIActive(false);
        SetWeaponUIActive(false);
        SetInteractUIActive(false);
    }
    public void Undetected()
    {
        _rootRect.anchoredPosition = _rootBasePosition;
        SetBodyUIActive(false);
        SetWeaponUIActive(false);
        SetInteractUIActive(false);
        SetPartUIActive(false);
    }
}