public class IParts
{
    public interface IAttackable
    {
        void Attack(IDamageable damageable, float dmg);
        float AttackDamage { get;  }
        float AttackSpeed { get;  }
    }

    public interface IUpgradable
    {
        TierType Tier { get;  }
        void Upgrade(float status);
        int UpgradeFleshCount { get; }
        float PartDropProbability { get; }
    }

    public interface IChangeable
    {
        Player Owner { get; }
        void BindPlayer(Player owner);
        void UnBindPlayer();
    }

    public interface IEquipable : IUpgradable, IChangeable
    {
        //public bool IsEquiped { get; set; }
    }

    //todo
    public interface IDamageable
    {
        int Hp { get; }
        bool IsDead { get; }
        void TakeDamage(float damage);
        void Dead();
    }

    public interface IJumpable
    {
        int JumpCount { get; set; }
        void Jump();
    }

    public interface IDodgeable : IJumpable
    {
        int DodgeCount { get; set; }
        void Dodge();
    }
}