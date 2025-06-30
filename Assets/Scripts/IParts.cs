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
    }

    //todo
    public interface IDamageable
    {
        bool IsDead { get; }
        void TakeDamage(float damage);
        void Dead();

    }

    public interface IMoveable
    {
    }

    public interface IJumpable
    {
    }

    public interface IDodgeable : IJumpable
    {
    }
}