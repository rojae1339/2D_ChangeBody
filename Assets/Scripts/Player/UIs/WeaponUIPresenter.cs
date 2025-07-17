using System.Text;
using UnityEngine;

public class WeaponUIPresenter
{
    private PlayerUIView _view;

    //플레이어의 무기
    private BaseWeapon _l_weapon;
    private BaseWeapon _r_weapon;

    private PartDetector _detector;

    StringBuilder sb = new StringBuilder();

    #region WeaponPresenter Init시에 WeaponModel(_l_weapon, _r_weapon)초기화
    
    public void Init(PlayerUIView view)
    {
        _view = view;
        
        InitializeWeaponModel(view);

        _detector = view.gameObject.GetComponent<PartDetector>();
        
        _detector.OnWeaponDetected += OnWeaponDetected;
        _detector.OnInteractWeaponUIToggle += OnInteractWeaponUIDetected;
        _detector.OnPartNotDetected += OnPartNotDetected;
        _detector.OnCancelDetect += OnCloseAllUI;
    }

    private void InitializeWeaponModel(PlayerUIView view)
    {
        Player _player = view.gameObject.GetComponent<Player>();

        var lWeapon = _player.L_Weapon.transform.GetChild(0).GetComponent<BaseWeapon>();
        var rWeapon = _player.R_Weapon.transform.GetChild(0).GetComponent<BaseWeapon>();;
        
        string MakeName(string name)
        {
            string n = name.Split(" (")[1].Split(')')[0];
            var makeName = n.Substring(0, 1).ToLower() + n.Substring(1);
            return makeName;
        }

        // 무기 이름 기준으로 CommonTier DTO 추출
        var l = Managers.Managers.PartsData.WeaponData[MakeName(lWeapon.ToString())][3];
        var r = Managers.Managers.PartsData.WeaponData[MakeName(rWeapon.ToString())][3];

        WeaponDTO lW = Managers.Managers.PartsData.MakeWeaponDTO(MakeName(lWeapon.ToString()), l);
        WeaponDTO rW = Managers.Managers.PartsData.MakeWeaponDTO(MakeName(rWeapon.ToString()), r);

        lWeapon.Init(lW);
        rWeapon.Init(rW);

        _l_weapon = lWeapon;
        _r_weapon = rWeapon;
    }

    #endregion

    //todo 게임오브젝트 ui로 img에 넣기
    private void ChangeDropWeaponUI(BaseWeapon dropWeapon)
    {
        // 제목
        string title = dropWeapon.Tier + "\n" + dropWeapon.WeaponName;

        // 공격 속도 및 공격력
        string descText = DrawWeaponDescUI(dropWeapon);

        _view.ChangeDropInfo(null, dropWeapon.Tier, title, descText);
    }

    //todo 게임오브젝트 ui로 img에 넣기 + 무기 한손/양손 ui조정
    private void ChangePlayerLeftWeaponUI(BaseWeapon lWeapon)
    {
        // 제목
        string title = lWeapon.Tier + "\n" + lWeapon.WeaponName;

        // 공격 속도 및 공격력
        string descText = DrawWeaponDescUI(lWeapon);

        _view.ChangePlayerLeftWeaponInfo(null, lWeapon.Tier, title, descText);
    }
    
    private void ChangePlayerRightWeaponUI(BaseWeapon rWeapon)
    {
        // 제목
        string title = rWeapon.Tier + "\n" + rWeapon.WeaponName;

        // 공격 속도 및 공격력
        string descText = DrawWeaponDescUI(rWeapon);

        _view.ChangePlayerRightWeaponInfo(null, rWeapon.Tier, title, descText);
    }
    
    private string DrawWeaponDescUI(BaseWeapon weapon)
    {
        sb.Clear();
        
        sb.AppendLine($"공격 속도: {weapon.AttackSpeed}s");
        sb.AppendLine($"데미지: {weapon.AttackDamage}");

        if (weapon.WeaponAttackType != AttackType.None)
        {
            sb.AppendLine(weapon.WeaponAttackType == AttackType.SingleAttack ? "단일공격" : "연속공격");
        }

        if (weapon.ReloadSpeed != 0)
            sb.AppendLine($"재장전 속도: {weapon.ReloadSpeed}s");

        if (weapon.BulletSpeed != 0)
            sb.AppendLine($"총알 속도: {weapon.BulletSpeed}s");

        if (weapon.MaxBulletCount != 0)
            sb.AppendLine($"탄창: {weapon.MaxBulletCount}개");

        return sb.ToString();
    }

    #region Detector Callback Action

    //파트 감지 안됨
    private void OnPartNotDetected()
    {
        sb.Clear();
        _view.Undetected();
    }
    
    //파트 감지 후 상호작용 키 누름
    private void OnInteractWeaponUIDetected()
    {
        _view.PressedInteractKeyOnWeapon();
    }
    
    //파트 감지 성공
    private void OnWeaponDetected(BaseWeapon dropWeapon)
    {
        if (dropWeapon == null) return;
        
        ChangeDropWeaponUI(dropWeapon);
        ChangePlayerLeftWeaponUI(_l_weapon);
        ChangePlayerRightWeaponUI(_r_weapon);
        _view.SetPartUIActive(true);
        // 파츠 정보 업데이트
    }

    private void OnCloseAllUI()
    {
        _view.QuitInteractUI();
    }

    #endregion
}