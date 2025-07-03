public class WeaponDTO
{
    public string WeaponName { get; set; }              
    public TierType Tier { get; set; }                   
    public double AttackDamage { get; set; }           
    public double AttackSpeed { get; set; }            
    public AttackType? AttackType { get; set; }            
    public double? BulletSpeed { get; set; }           
    public double? ReloadSpeed { get; set; }           
    public int? MaxBulletCount { get; set; }           
    public int UpgradeFleshCount { get; set; }         
    public double PartDropProbability { get; set; }    
    public WeaponHandType WeaponHandType { get; set; }         
}
