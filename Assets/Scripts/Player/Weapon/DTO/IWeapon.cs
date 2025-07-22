public interface IWeapon : IParts.IAttackable, IParts.IEquipable
{
    string WeaponName { get; }          

    AttackType WeaponAttackType { get; }              // SingleAttack, DoubleAttack 등 (근접/원거리 무기 모두에 사용됨)

    float? BulletSpeed { get; }              // 총알 속도 (총기 및 활에 해당)
    float? ReloadSpeed { get; }              // 재장전 속도 (총기류에 해당)
    int? MaxBulletCount { get; }                  // 무기 해제 시 호출
}