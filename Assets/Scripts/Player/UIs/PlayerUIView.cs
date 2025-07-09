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
        Managers.Managers.OnManagerLoadInitialized += Init;
    }

    private void Init()
    {
        if (this == null) return;
        
        _player = gameObject.GetComponent<Player>();

        var lWeapon = _player.L_Weapon.transform.GetChild(0).GetComponent<BaseWeapon>();
        var rWeapon = _player.R_Weapon.transform.GetChild(0).GetComponent<BaseWeapon>();;
        var body = _player.Body.GetComponent<BaseBody>();
        
        string MakeName(string name)
        {
            string n = name.Split(" (")[1].Split(')')[0];
            var makeName = n.Substring(0, 1).ToLower() + n.Substring(1);
            return makeName;
        }

        // 무기/바디 이름 기준으로 CommonTier DTO 추출
        var l = Managers.Managers.PartsData.WeaponData[MakeName(lWeapon.ToString())][3];
        var r = Managers.Managers.PartsData.WeaponData[MakeName(rWeapon.ToString())][3];
        var b = Managers.Managers.PartsData.BodyData[MakeName(body.ToString())][3];

        WeaponDTO lW = Managers.Managers.PartsData.MakeWeaponDTO(MakeName(lWeapon.ToString()), l);
        WeaponDTO rW = Managers.Managers.PartsData.MakeWeaponDTO(MakeName(rWeapon.ToString()), r);
        BodyDTO bB = Managers.Managers.PartsData.MakeBodyDTO(MakeName(body.ToString()), b);

        lWeapon.Init(lW);
        rWeapon.Init(rW);
        body.Init(bB);
        

        _l_weaponPresenter = new WeaponUIPresenter(this, lWeapon);
        _r_weaponPresenter = new WeaponUIPresenter(this, rWeapon);
        _bodyPresenter = new BodyUIPresenter(this, body);

        Managers.Managers.OnManagerLoadInitialized -= Init;
    }

    //todo 줄에 맞춰 글자크기 조정
    public void ChangeDropPartInfo(Image img, TierType tier, string title, string desc)
    {
        _dropPartsImg = img;
        _dropPartsTitleText.color = ChangeTextColorByTier(tier);
        _dropPartsTitleText.text = title;
        _dropPartsDescText.text = desc;
    }

    //todo 유저가 무기 2개 들고있을때 추가하기 + 줄에 맞춰 글자크기 조정
    public void ChangePlayerPartInfo(bool isPartsOnlyOne, Image img, TierType tier, string title, string desc)
    {
        if (isPartsOnlyOne)
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