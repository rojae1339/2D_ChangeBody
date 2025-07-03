using UnityEngine;public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    public string WeaponName { get; private set; }
    public TierType Tier { get; private set; }
    public float AttackDamage { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public AttackType? WeaponAttackType { get; private set; }
    public float? BulletSpeed { get; private set; }
    public float? ReloadSpeed { get; private set; }
    public int? MaxBulletCount { get; private set; }
    public int UpgradeFleshCount { get; private set; }
    public float PartDropProbability { get; private set; }
    public WeaponHandType HandType { get; private set; }

    public Player Owner { get; private set; }

    protected BaseWeapon(WeaponDTO dto)
    {
        WeaponName = dto.WeaponName;
        Tier = dto.Tier;
        AttackDamage = (float)dto.AttackDamage;
        AttackSpeed = (float)dto.AttackSpeed;
        WeaponAttackType = dto.AttackType;
        BulletSpeed = (float?)dto.BulletSpeed;
        ReloadSpeed = (float?)dto.ReloadSpeed;
        MaxBulletCount = dto.MaxBulletCount;
        UpgradeFleshCount = dto.UpgradeFleshCount;
        PartDropProbability = (float)dto.PartDropProbability;
        HandType = dto.WeaponHandType;
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