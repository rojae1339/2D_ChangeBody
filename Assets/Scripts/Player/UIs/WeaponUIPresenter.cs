using System.Text;
using UnityEngine;

public class WeaponUIPresenter
{
    private PlayerUIView _view;

    //플레이어의 무기
    private BaseWeapon _l_weapon;
    private BaseWeapon _r_weapon;

    private PartDetector _detector;

    StringBuilder dropSB = new StringBuilder();
    StringBuilder playerSB = new StringBuilder();

    #region WeaponPresenter Init시에 WeaponModel(_l_weapon, _r_weapon)초기화
    
    public void Init(PlayerUIView view)
    {
        _view = view;
        
        InitializeWeaponModel(view);

        _detector = view.gameObject.GetComponent<PartDetector>();
        _detector.OnWeaponDetected += OnWeaponDetected;
        _detector.OnPartNotDetected += OnPartNotDetected;
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

    private void OnWeaponDetected(BaseWeapon dropWeapon)
    {
        ChangeDropWeaponUI(dropWeapon);
        ChangePlayerWeaponUI(new BaseWeapon[] {_l_weapon, _r_weapon});
        _view.SetPartUIActive(true);
        // 파츠 정보 업데이트
    }

    //todo 게임오브젝트 ui로 img에 넣기
    private void ChangeDropWeaponUI(BaseWeapon dropWeapon)
    {
        dropSB.Clear();
        // 제목
        string title = dropWeapon.Tier + "\n" + dropWeapon.WeaponName;

        // 공격 속도 및 공격력
        dropSB.AppendLine($"공격 속도: {dropWeapon.AttackSpeed}s");
        dropSB.AppendLine($"데미지: {dropWeapon.AttackDamage}");

        // 공격 타입 (None이면 생략)
        if (dropWeapon.WeaponAttackType != AttackType.None)
        {
            dropSB.AppendLine(dropWeapon.WeaponAttackType == AttackType.SingleAttack ? "단일공격" : "연속공격");
        }

        // 리로드 속도
        if (dropWeapon.ReloadSpeed != 0)
            dropSB.AppendLine($"재장전 속도: {dropWeapon.ReloadSpeed}s");

        // 총알 속도
        if (dropWeapon.BulletSpeed != 0)
            dropSB.AppendLine($"총알 속도: {dropWeapon.BulletSpeed}s");

        // 총알 개수
        if (dropWeapon.MaxBulletCount != 0)
            dropSB.AppendLine($"탄창: {dropWeapon.MaxBulletCount}개");

        string finalText = dropSB.ToString();

        _view.ChangeDropPartInfo(null, dropWeapon.Tier, title, finalText);
    }

    //todo 게임오브젝트 ui로 img에 넣기 + 무기 한손/양손 ui조정
    private void ChangePlayerWeaponUI(BaseWeapon[] playerWeapons)
    {
        playerSB.Clear();

        if (playerWeapons == null || (playerWeapons[0] == null && playerWeapons[1] == null))
        {
            _view.ChangePlayerPartInfo(false, null, null, "", "");
            return;
        }

        // Case 1: 왼손 무기가 존재하고 양손 무기인 경우
        if (playerWeapons[0] != null && playerWeapons[0].HandType == WeaponHandType.TwoHanded)
        {
            DrawWeaponUI(playerWeapons[0], isLeft: true);
            return;
        }

        // Case 2: 오른손 무기가 존재하고 양손 무기인 경우
        if (playerWeapons[1] != null && playerWeapons[1].HandType == WeaponHandType.TwoHanded)
        {
            DrawWeaponUI(playerWeapons[1], isLeft: false);
            return;
        }

        // Case 3: 두 개 다 한손 무기인 경우 (Dual wield)
        if (playerWeapons[0] != null && playerWeapons[1] != null)
        {
            DrawWeaponUI(playerWeapons[0], isLeft: true);
            DrawWeaponUI(playerWeapons[1], isLeft: false);
            return;
        }

        // Case 4: 한손 무기 하나만 존재하는 경우
        if (playerWeapons[0] != null)
        {
            DrawWeaponUI(playerWeapons[0], isLeft: true);
            return;
        }

        if (playerWeapons[1] != null)
        {
            DrawWeaponUI(playerWeapons[1], isLeft: false);
            return;
        }
    }
    
    private void DrawWeaponUI(BaseWeapon weapon, bool isLeft)
    {
        if (weapon == null) return;

        string title = weapon.Tier + "\n" + weapon.WeaponName;

        playerSB.Clear();
        playerSB.AppendLine($"공격 속도: {weapon.AttackSpeed}s");
        playerSB.AppendLine($"데미지: {weapon.AttackDamage}");

        if (weapon.WeaponAttackType != AttackType.None)
        {
            playerSB.AppendLine(weapon.WeaponAttackType == AttackType.SingleAttack ? "단일공격" : "연속공격");
        }

        if (weapon.ReloadSpeed != 0)
            playerSB.AppendLine($"재장전 속도: {weapon.ReloadSpeed}s");

        if (weapon.BulletSpeed != 0)
            playerSB.AppendLine($"총알 속도: {weapon.BulletSpeed}s");

        if (weapon.MaxBulletCount != 0)
            playerSB.AppendLine($"탄창: {weapon.MaxBulletCount}개");

        string finalText = playerSB.ToString();

        // isLeft를 활용해서 왼손/오른손 UI 분기 (또는 통합 UI 처리)
        _view.ChangePlayerPartInfo(true, null, weapon.Tier, title, finalText);
    }



    private void OnPartNotDetected()
    {
        dropSB.Clear();
        playerSB.Clear();
        _view.SetPartUIActive(false);
    }
}