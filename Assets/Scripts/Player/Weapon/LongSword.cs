using UnityEngine;

public class LongSword : BaseWeapon
{
    private AttackType _attackType;
    public AttackType AttackType { get => _attackType; }


    public override void Upgrade(float status)
    {
        // LongSword-specific upgrade logic
    }

    public override BaseWeapon Init(BaseWeaponDTO data)
    {
        base.Init(data);
        if (data is LongSwordDTO longSwordData)
        {
            _attackType = longSwordData.AttackType;
        }
        return this;
    }
}
