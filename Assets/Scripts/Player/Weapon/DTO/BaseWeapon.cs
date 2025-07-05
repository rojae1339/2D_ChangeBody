using UnityEngine;public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [field: SerializeField]
    public string WeaponName { get; private set; }
    [field: SerializeField]
    public TierType Tier { get; private set; }
    [field: SerializeField]
    public float AttackDamage { get; protected set; }
    [field: SerializeField]
    public float AttackSpeed { get; protected set; }
    [field: SerializeField]
    public AttackType WeaponAttackType { get; private set; }
    [field: SerializeField]
    public float? BulletSpeed { get; private set; }
    [field: SerializeField]
    public float? ReloadSpeed { get; private set; }
    [field: SerializeField]
    public int? MaxBulletCount { get; private set; }
    [field: SerializeField]
    public int UpgradeFleshCount { get; private set; }
    [field: SerializeField]
    public float PartDropProbability { get; private set; }
    [field: SerializeField]
    public WeaponHandType HandType { get; private set; }

    public PartsTierFeatureSO so;


    public Player Owner { get; private set; }

    protected BaseWeapon(WeaponDTO dto)
    {
        WeaponName = dto.WeaponName;
        Tier = dto.Tier;
        AttackDamage = (float)dto.AttackDamage;
        AttackSpeed = (float)dto.AttackSpeed;
        WeaponAttackType = dto.WeaponAttackType;
        BulletSpeed = (float?)dto.BulletSpeed;
        ReloadSpeed = (float?)dto.ReloadSpeed;
        MaxBulletCount = dto.MaxBulletCount;
        UpgradeFleshCount = dto.UpgradeFleshCount;
        PartDropProbability = (float)dto.PartDropProbability;
        HandType = dto.HandType;
    }

    public BaseWeapon Init(WeaponDTO dto)
    {
        WeaponName = dto.WeaponName;
        Tier = dto.Tier;
        AttackDamage = (float)dto.AttackDamage;
        AttackSpeed = (float)dto.AttackSpeed;
        WeaponAttackType = dto.WeaponAttackType;
        BulletSpeed = (float?)dto.BulletSpeed;
        ReloadSpeed = (float?)dto.ReloadSpeed;
        MaxBulletCount = dto.MaxBulletCount;
        UpgradeFleshCount = dto.UpgradeFleshCount;
        PartDropProbability = (float)dto.PartDropProbability;
        HandType = dto.HandType;
        return this;
    }

    public abstract void Attack(IParts.IDamageable target, float damage);

    public virtual void Upgrade(float status)
    {
        //todo
        AttackDamage += status;
    }

    public void BindPlayer(Player owner)
    {
        UnBindPlayer();
        Owner = owner;
    }

    public void UnBindPlayer()
    {
        Owner = null;
    }
}