public class NoWeapon : BaseWeapon
{
    public NoWeapon(WeaponDTO dto) : base(dto) { }

    public override void Attack(IParts.IDamageable target, float damage)
    {
        target.TakeDamage(damage);
    }
}