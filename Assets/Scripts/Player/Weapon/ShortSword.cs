public class ShortSword : BaseWeapon
{
    private AttackType _attackType;
    
    public AttackType AttackType { get => _attackType; }

    public override void Upgrade(float status)
    {
        
    }

    public override BaseWeapon Init(BaseWeaponDTO data)
    {
        base.Init(data);
        if (data is ShortSwordDTO shortSwordData)
        {
            _attackType = shortSwordData.AttackType;
        }
        return this;
    }
}