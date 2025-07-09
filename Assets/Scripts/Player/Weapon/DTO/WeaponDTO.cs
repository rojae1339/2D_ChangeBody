public class WeaponDTO
{
    public string WeaponName { get; set; }              
    public TierType Tier { get; set; }                   
    public double AttackDamage { get; set; }           
    public double AttackSpeed { get; set; }            
    public AttackType WeaponAttackType { get; set; }            
    public double? BulletSpeed { get; set; }           
    public double? ReloadSpeed { get; set; }           
    public int? MaxBulletCount { get; set; }           
    public int UpgradeFleshCount { get; set; }         
    public double PartDropProbability { get; set; }    
    public WeaponHandType HandType { get; set; }

    public WeaponDTO() { }

    public WeaponDTO(string weaponName, TierType tier, double attackDamage, double attackSpeed, AttackType weaponAttackType, double? bulletSpeed, double? reloadSpeed, int? maxBulletCount, int upgradeFleshCount, double partDropProbability, WeaponHandType handType)
    {
        WeaponName = weaponName;
        Tier = tier;
        AttackDamage = attackDamage;
        AttackSpeed = attackSpeed;
        WeaponAttackType = weaponAttackType;
        BulletSpeed = bulletSpeed;
        ReloadSpeed = reloadSpeed;
        MaxBulletCount = maxBulletCount;
        UpgradeFleshCount = upgradeFleshCount;
        PartDropProbability = partDropProbability;
        HandType = handType;
    }
}
