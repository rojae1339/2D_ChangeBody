using UnityEngine;

public class Pistol : BaseWeapon
{
    private float _reloadSpeed;
    private float _bulletSpeed;
    private AttackType _attackType;

    public float ReloadSpeed { get => _reloadSpeed; }
    public float BulletSpeed { get => _bulletSpeed; }
    public AttackType AttackType { get => _attackType; }

    public override void Attack(IParts.IDamageable damageable, float dmg)
    {
        // Pistol-specific attack logic
        damageable.TakeDamage(dmg);
    }

    public override void Upgrade(float status)
    {
        // Pistol-specific upgrade logic
    }

    public override BaseWeapon Init(BaseWeaponDTO data)
    {
        base.Init(data);
        if (data is PistolDTO pistolData)
        {
            _bulletSpeed = pistolData.BulletSpeed;
            _reloadSpeed = pistolData.ReloadSpeed;
            _attackType = pistolData.AttackType;
        }

        return this;
    }
}