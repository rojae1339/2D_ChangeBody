using UnityEngine;
using static IParts;

public abstract class BaseWeapon : IWeapon
{
    #region Fields

    [SerializeField]
    protected float _attDmg;

    [SerializeField]
    protected float _attSpeed;

    [SerializeField]
    protected TierType _tier;

    [SerializeField]
    protected int _upgradeFleshCount;

    [SerializeField]
    protected float _partDropProbability;

    [SerializeField]
    protected WeaponHandType _weaponHandType;

    [SerializeField]
    protected Player _owner;

    #endregion

    #region Properties

    public float AttackDamage { get => _attDmg; }
    public float AttackSpeed { get => _attSpeed; }
    public TierType Tier { get => _tier; }
    public int UpgradeFleshCount { get => _upgradeFleshCount; }
    public float PartDropProbability { get => _partDropProbability; }
    public WeaponHandType WeaponHandType { get => _weaponHandType; }
    public Player Owner { get => _owner; }

    #endregion

    #region IWeapon Implementation

    public virtual void Attack(IDamageable damageable, float dmg)
    {
        damageable.TakeDamage(dmg);
    }

    public abstract void Upgrade(float status);

    public virtual void BindPlayer(Player owner)
    {
        UnBindPlayer();
        _owner = owner;
    }

    public virtual void UnBindPlayer()
    {
        _owner = null;
    }

    public virtual BaseWeapon Init(BaseWeaponDTO data)
    {
        _attDmg = data.AttackDamage;
        _tier = data.Tier;
        _attSpeed = data.AttackSpeed;
        _upgradeFleshCount = data.UpgradeFleshCount;
        _partDropProbability = data.PartDropProbability;
        _weaponHandType = data.WeaponHandType;
        return this;
    }

    #endregion
}
