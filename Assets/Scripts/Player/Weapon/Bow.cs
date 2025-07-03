public class Bow : BaseWeapon
{
    public Bow(WeaponDTO dto) : base(dto) { }

    public override void Attack(IParts.IDamageable target, float damage)
    {
        // 원거리 화살 발사 로직 (예: 화살 프리팹 발사 등)
        target?.TakeDamage(damage);
    }
}