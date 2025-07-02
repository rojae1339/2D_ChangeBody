using UnityEngine;

public class NoWeapon : BaseWeapon
{
    public override void Attack(IParts.IDamageable damageable, float dmg)
    {
        damageable.TakeDamage(dmg);
    }

    public override void Upgrade(float status)
    {
        // NoWeapon does not upgrade.
    }
}

