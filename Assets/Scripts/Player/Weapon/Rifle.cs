using UnityEngine;

public class Rifle : BaseWeapon
{
    private float _reloadSpeed;
    public float ReloadSpeed { get => _reloadSpeed; }

    private int _maxBulletCount;
    public int MaxBulletCount { get => _maxBulletCount; }

    private float _bulletSpeed;
    public float BulletSpeed { get => _bulletSpeed; }

    public override void Attack(IParts.IDamageable damageable, float dmg)
    {
        // Rifle-specific attack logic
        damageable.TakeDamage(dmg);
    }

    public override void Upgrade(float status)
    {
        // Rifle-specific upgrade logic
    }

    public override BaseWeapon Init(BaseWeaponDTO data)
    {
        base.Init(data);
        if (data is RifleDTO rifleData)
        {
            _bulletSpeed = rifleData.BulletSpeed;
            _reloadSpeed = rifleData.ReloadSpeed;
            _maxBulletCount = rifleData.MaxBulletCount;
        }
        return this;
    }
}
