using System;
using Unity.VisualScripting;
using UnityEngine;

public class NoWeapon : IWeapon
{

    #region Fields

    [SerializeField]
    private float _attDmg;

    [SerializeField]
    private float _attSpeed;

    [SerializeField]
    private TierType _tier;

    [SerializeField]
    private int _upgradeFleshCount;

    [SerializeField]
    private float _partDropProbability;

    [SerializeField]
    private WeaponHandType _weaponHandType;

    [SerializeField]
    private Player _owner;

    #endregion

    #region Properties

    public float AttackDamage { get => _attDmg; private set => _attDmg = value; }

    public float AttackSpeed { get => _attSpeed; private set => _attSpeed = value; }

    public TierType Tier { get => _tier; private set => _tier = value; }

    public int UpgradeFleshCount { get => _upgradeFleshCount; private set => _upgradeFleshCount = value; }

    public float PartDropProbability { get => _partDropProbability; private set => _partDropProbability = value; }
    
    public WeaponHandType WeaponHandType { get => _weaponHandType; private set => _weaponHandType = value; }

    public Player Owner { get => _owner; private set => _owner = value; }

    #endregion

    #region implimented methods

    public void Attack(IParts.IDamageable damageable, float dmg) { damageable.TakeDamage(dmg); }

    public void Upgrade(float status) { }

    public void BindPlayer(Player owner)
    {
        UnBindPlayer();

        _owner = owner;
    }

    public void UnBindPlayer() { _owner = null; }

    #endregion

    public void Init(NoWeaponDTO noWeapon)
    {
        _attDmg = noWeapon.AttackDamage;
        _tier = noWeapon.Tier;
        _attSpeed = noWeapon.AttackSpeed;
        _upgradeFleshCount = noWeapon.UpgradeFleshCount;
        _partDropProbability = noWeapon.PartDropProbability;
        _weaponHandType = noWeapon.WeaponHandType;
    }
    
    
}