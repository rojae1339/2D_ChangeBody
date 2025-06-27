using UnityEngine;

public class IParts
{
    public interface IAttackable
    {
        void Attack(IDamageable damageable, float dmg);
        float AttackDamage { get; set; }
        float AttackSpeed { get; set; }
    }
    
    public interface IUpgradable
    {
        enum TierType
        {
            Common, Rare, Unique, Legendary
        }
        
        TierType Tier { get; set; }
        void Upgrade(float status);
        int UpgradeFleshCount { get; }
        float PartDropProbability { get; }
    }
    
    public interface IChangeable
    {
        Player Owner { get; set; }
        void BindPlayer(Player owner);
        void UnBindPlayer();
    }

    public interface IEquipable : IUpgradable, IChangeable
    {
        
    }
    
    //todo
    public interface IDamageable
    {
        
    }
    
    public interface IMoveable
    {
        
    }
    
    public interface IJumpable
    {
        
    }
    
    public interface IDodgeable: IJumpable
    {
        
    }
}
