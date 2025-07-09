using System.Text;
using UnityEngine;

public class WeaponUIPresenter
{
    private PlayerUIView _view;

    //플레이어의 무기
    private BaseWeapon _modelPlayer;

    private PartDetector _detector;

    StringBuilder dropSB = new StringBuilder();
    StringBuilder playerSB = new StringBuilder();

    public WeaponUIPresenter(PlayerUIView view, BaseWeapon model)
    {
        _view = view;
        _modelPlayer = model;
        _detector = view.gameObject.GetComponent<PartDetector>();
        _detector.OnWeaponDetected += OnWeaponDetected;
        _detector.OnPartNotDetected += OnPartNotDetected;
    }

    private void OnWeaponDetected(BaseWeapon dropWeapon)
    {
        Debug.Log($"{dropWeapon.WeaponName}, {dropWeapon.Tier}");

        ChangeDropWeapon(dropWeapon);
        //ChangePlayerWeapon(_modelPlayer);
        _view.SetPartUIActive(true);
        // 파츠 정보 업데이트
    }

    private void ChangeDropWeapon(BaseWeapon dropWeapon)
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

    //todo 수정 + 런하면 null값이 됨 + 무기 한손
    private void ChangePlayerWeapon(BaseWeapon playerWeapon)
    {
        playerSB.Clear();
        // 제목
        string title = playerWeapon.Tier + "\n" + playerWeapon.WeaponName;

        // 공격 속도 및 공격력
        playerSB.AppendLine($"공격 속도: {playerWeapon.AttackSpeed}s");
        playerSB.AppendLine($"데미지: {playerWeapon.AttackDamage}");

        // 공격 타입 (None이면 생략)
        if (playerWeapon.WeaponAttackType != AttackType.None)
        {
            playerSB.AppendLine(playerWeapon.WeaponAttackType == AttackType.SingleAttack ? "단일공격" : "연속공격");
        }

        // 리로드 속도
        if (playerWeapon.ReloadSpeed != 0)
            playerSB.AppendLine($"재장전 속도: {playerWeapon.ReloadSpeed}s");

        // 총알 속도
        if (playerWeapon.BulletSpeed != 0)
            playerSB.AppendLine($"총알 속도: {playerWeapon.BulletSpeed}s");

        // 총알 개수
        if (playerWeapon.MaxBulletCount != 0)
            playerSB.AppendLine($"탄창: {playerWeapon.MaxBulletCount}개");

        string finalText = playerSB.ToString();
        
        _view.ChangePlayerPartInfo(true, null, playerWeapon.Tier, title, finalText);
    }

    private void OnPartNotDetected()
    {
        dropSB.Clear();
        playerSB.Clear();
        _view.SetPartUIActive(false);
    }
}