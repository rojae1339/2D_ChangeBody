public class Pistol : BaseWeapon
{
    public Pistol(WeaponDTO dto) : base(dto) { }

    public override void Attack(IParts.IDamageable target, float damage)
    {
        if (WeaponAttackType == AttackType.DoubleAttack)
        {
            target?.TakeDamage(damage / 2f);
            target?.TakeDamage(damage / 2f);
        }
        else
        {
            target?.TakeDamage(damage);
        }
    }

    public void Reload()
    {
        // 탄약 장전 처리
    }
}