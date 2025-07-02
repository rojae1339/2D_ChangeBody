using UnityEngine;

public class Bow : BaseWeapon
{
    private float _bulletSpeed;
    public float BulletSpeed { get => _bulletSpeed; }


    public override void Upgrade(float status)
    {
        // Bow-specific upgrade logic
    }

    public override BaseWeapon Init(BaseWeaponDTO data)
    {
        base.Init(data);
        if (data is BowDTO bowData)
        {
            _bulletSpeed = bowData.BulletSpeed;
        }
        return this;
    }
}
