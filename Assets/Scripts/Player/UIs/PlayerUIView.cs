using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField]
    private Canvas _mainCanvas;

    [SerializeField]
    private GameObject _partsUIPanel;
    [SerializeField]
    private Image _dropPartsImg;
    [SerializeField]
    private Image _playerPartsImg;
    [SerializeField]
    private TextMeshProUGUI _dropPartsTitleText;
    [SerializeField]
    private TextMeshProUGUI _playerPartsTitleText;
    [SerializeField]
    private TextMeshProUGUI _dropPartsDescText;
    [SerializeField]
    private TextMeshProUGUI _playerPartsDescText;

    private WeaponUIPresenter _l_weaponPresenter;
    private WeaponUIPresenter _r_weaponPresenter;
    private BodyUIPresenter _bodyPresenter;

    private Player _player;

    private void Start()
    {
        _player = gameObject.GetComponent<Player>();
        _l_weaponPresenter = new WeaponUIPresenter(this, _player.L_Weapon.GetComponent<BaseWeapon>());
        _r_weaponPresenter = new WeaponUIPresenter(this, _player.L_Weapon.GetComponent<BaseWeapon>());
        _bodyPresenter = new BodyUIPresenter(this, _player.L_Weapon.GetComponent<BaseBody>());
    }

    public void ChangeDropPartInfo(Image img, TierType tier, string title, string desc)
    {
        _dropPartsImg = img;
        _dropPartsTitleText.color = ChangeTextColorByTier(tier);
        _dropPartsTitleText.text = title;
        _dropPartsDescText.text = desc;
    }

    //todo 유저가 무기 2개 들고있을때 추가하기
    public void ChangePlayerPartInfo(bool isWeaponOnly, Image img, TierType tier, string title, string desc)
    {
        if (isWeaponOnly)
        {
            _playerPartsImg = img;
            _playerPartsTitleText.color = ChangeTextColorByTier(tier);
            _playerPartsTitleText.text = title;
            _playerPartsDescText.text = desc;
            return;
        }
        
        //todo 무기나  2개일때
    }
    
    

    private Color ChangeTextColorByTier(TierType tier)
    {
        switch (tier)
        {
            //드롭된 파츠의 tier에 따라 글씨색 변경
            case TierType.Legendary: return Color.crimson;
            case TierType.Unique: return Color.yellowNice;
            case TierType.Rare: return Color.deepSkyBlue;
            case TierType.Common: return Color.gray3;
            default: throw new ArgumentOutOfRangeException(nameof(tier), tier, null);
        }
    }

    public void SetPartUIActive(bool isActive)
    {
        _partsUIPanel.SetActive(isActive);
    }
}