using System;
using System.Collections.Generic;

#region Abstract classes

[Serializable]
public abstract class BaseWeaponDTO
{
    protected TierType _tier;
    protected float _attackDamage;
    protected float _attackSpeed;
    protected int _upgradeFleshCount;
    protected float _partDropProbability;
    protected WeaponHandType _weaponHandType;

    // get property
    public TierType Tier { get => _tier; set => _tier = value; }
    public float AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    public int UpgradeFleshCount { get => _upgradeFleshCount; set => _upgradeFleshCount = value; }
    public float PartDropProbability { get => _partDropProbability; set => _partDropProbability = value; }
    public WeaponHandType WeaponHandType { get => _weaponHandType; private set { } }

    //빌더는 안씀
    //모든 필드가 초기화 되어야하는데,
    //빌더로 모든 필드 초기화 시키려면,
    //기본생성자를 막은 모든 필드를 가진 생성자로 처리하는거랑 똑같은것 같음
    private BaseWeaponDTO() { }

    public BaseWeaponDTO(TierType tier,
                         float attackDamage,
                         float attackSpeed,
                         int upgradeFleshCount,
                         float partDropProbability,
                         WeaponHandType weaponHandType)
    {
        _tier = tier;
        _attackDamage = attackDamage;
        _attackSpeed = attackSpeed;
        _upgradeFleshCount = upgradeFleshCount;
        _partDropProbability = partDropProbability;
        _weaponHandType = weaponHandType;
    }
}

[Serializable]
public abstract class BaseSwordDTO : BaseWeaponDTO
{
    protected AttackType _attackType;

    public AttackType AttackType { get => _attackType; set => _attackType = value; }

    public BaseSwordDTO(TierType tier,
                        float attackDamage,
                        float attackSpeed,
                        int upgradeFleshCount,
                        float partDropProbability,
                        WeaponHandType weaponHandType,
                        AttackType attackType)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, weaponHandType)
    {
        _attackType = attackType;
    }
}

[Serializable]
public abstract class BaseShootWeaponDTO : BaseWeaponDTO
{
    protected float _bulletSpeed;

    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }

    public BaseShootWeaponDTO(TierType tier,
                              float attackDamage,
                              float attackSpeed,
                              int upgradeFleshCount,
                              float partDropProbability,
                              WeaponHandType weaponHandType,
                              float bulletSpeed)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, weaponHandType)
    {
        _bulletSpeed = bulletSpeed;
    }
}

[Serializable]
public abstract class BaseGunDTO : BaseShootWeaponDTO
{
    private float _reloadSpeed;

    public float ReloadSpeed { get => _reloadSpeed; set => _reloadSpeed = value; }

    public BaseGunDTO(TierType tier,
                      float attackDamage,
                      float attackSpeed,
                      int upgradeFleshCount,
                      float partDropProbability,
                      WeaponHandType weaponHandType,
                      float bulletSpeed,
                      float reloadSpeed)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, weaponHandType, bulletSpeed)
    {
        _reloadSpeed = reloadSpeed;
    }
}

#endregion

#region use abstract BaseWeaponDTO

[Serializable]
public class NoWeaponDTO : BaseWeaponDTO
{
    public NoWeaponDTO(TierType tier,
                       float attackDamage,
                       float attackSpeed,
                       int upgradeFleshCount,
                       float partDropProbability)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, WeaponHandType.OneHanded)
    {
    }
}

#endregion

#region use abstract BaseSwordDTO

[Serializable]
public class ShortSwordDTO : BaseSwordDTO
{
    public ShortSwordDTO(TierType tier,
                         float attackDamage,
                         float attackSpeed,
                         int upgradeFleshCount,
                         float partDropProbability,
                         AttackType attackType)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, WeaponHandType.OneHanded,
            attackType)
    {
    }
}

[Serializable]
public class LongSwordDTO : BaseSwordDTO
{
    public LongSwordDTO(TierType tier,
                        float attackDamage,
                        float attackSpeed,
                        int upgradeFleshCount,
                        float partDropProbability,
                        AttackType attackType)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, WeaponHandType.TwoHanded,
            attackType)
    {
    }
}

#endregion

#region use abstract BaseShootWeaponDTO

[Serializable]
public class BowDTO : BaseShootWeaponDTO
{
    public BowDTO(TierType tier,
                  float attackDamage,
                  float attackSpeed,
                  int upgradeFleshCount,
                  float partDropProbability,
                  WeaponHandType weaponHandType,
                  float bulletSpeed)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, WeaponHandType.TwoHanded,
            bulletSpeed)
    {
    }
}

#endregion

#region use abstract BaseGunDTO

[Serializable]
public class PistolDTO : BaseGunDTO
{
    private AttackType _attackType;

    public AttackType AttackType { get => _attackType; set => _attackType = value; }

    public PistolDTO(TierType tier,
                     float attackDamage,
                     float attackSpeed,
                     int upgradeFleshCount,
                     float partDropProbability,
                     WeaponHandType weaponHandType,
                     float bulletSpeed,
                     AttackType attackType,
                     float reloadSpeed)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, WeaponHandType.OneHanded,
            bulletSpeed, reloadSpeed)
    {
        _attackType = attackType;
    }
}

[Serializable]
public class RifleDTO : BaseGunDTO
{
    private int _maxBulletCount;

    public int MaxBulletCount { get => _maxBulletCount; set => _maxBulletCount = value; }

    public RifleDTO(TierType tier,
                    float attackDamage,
                    float attackSpeed,
                    int upgradeFleshCount,
                    float partDropProbability,
                    WeaponHandType weaponHandType,
                    float bulletSpeed,
                    float reloadSpeed,
                    int maxBulletCount)
        : base(tier, attackDamage, attackSpeed, upgradeFleshCount, partDropProbability, WeaponHandType.TwoHanded,
            bulletSpeed, reloadSpeed)
    {
        _maxBulletCount = maxBulletCount;
    }
}

#endregion

[System.Serializable]
public class WeaponDatabase
{
    private List<NoWeaponDTO> _noWeaponDB;
    private List<ShortSwordDTO> _shortSwordDB;
    private List<LongSwordDTO> _longSwordDB;
    private List<BowDTO> _bowDB;
    private List<PistolDTO> _pistolDB;
    private List<RifleDTO> _rifleDB;
    
    public List<NoWeaponDTO> NoWeaponDB
    {
        get => _noWeaponDB;
    }
    public List<ShortSwordDTO> ShortSwordDB
    {
        get => _shortSwordDB;
    }
    public List<LongSwordDTO> LongSwordDB
    {
        get => _longSwordDB;
    }
    public List<BowDTO> BowDB
    {
        get => _bowDB;
    }
    public List<PistolDTO> PistolDB
    {
        get => _pistolDB;
    }
    public List<RifleDTO> RifleDB
    {
        get => _rifleDB;
    }
    
    public void Initialize(List<NoWeaponDTO> noWeapons
                           /*List<ShortSwordDTO> shortSwords,
                           List<LongSwordDTO> longSwords,
                           List<BowDTO> bows,
                           List<PistolDTO> pistols,
                           List<RifleDTO> rifles*/)
    {
        _noWeaponDB = noWeapons;
        /*_shortSwordDB = shortSwords;
        _longSwordDB = longSwords;
        _bowDB = bows;
        _pistolDB = pistols;
        _rifleDB = rifles;*/
    }
}