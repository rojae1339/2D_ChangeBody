public class Rifle : BaseWeapon
{
    public Rifle(WeaponDTO dto) : base(dto) { }

    public override void Attack(IParts.IDamageable target, float damage)
    {
        // 일반적으로 연사 처리 또는 한 발 발사
        target?.TakeDamage(damage);
    }

    public void Reload()
    {
        // 탄약 장전 처리
    }
}