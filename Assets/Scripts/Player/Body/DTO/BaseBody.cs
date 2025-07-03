using System;

public abstract class BaseBody : IBody
{
    public TierType Tier { get; protected set; }
    public float Hp { get; protected set; }
    public float Shield { get; protected set; }
    public bool IsDead { get; protected set; }
    public bool IsDmgHalf { get; protected set; }

    public int UpgradeFleshCount { get; protected set; }
    public float PartDropProbability { get; protected set; }

    public Player Owner { get; private set; }

    public void BindPlayer(Player owner) => Owner = owner;
    public void UnBindPlayer() => Owner = null;

    public virtual void Upgrade(float status)
    {
        Hp += (int)status;
    }

    public virtual void TakeDamage(float damage)
    {
        float finalDamage = IsDmgHalf ? damage * 0.5f : damage;

        if (Shield > 0)
        {
            float absorbed = Math.Min(Shield, finalDamage);
            Shield -= absorbed;
            finalDamage -= absorbed;
        }

        Hp -= (int)finalDamage;

        if (Hp <= 0)
        {
            Hp = 0;
            Dead();
        }
    }

    public virtual void Dead()
    {
        IsDead = true;
        // 사망 로직
    }

    public void Heal(int amount)
    {
        if (IsDead == true) return;

        Hp += amount;
    }

    protected BaseBody(BodyDTO dto)
    {
        Tier = dto.Tier;
        Hp = dto.HeartCount;
        Shield = dto.ShieldCount;
        IsDead = dto.IsDead;
        IsDmgHalf = dto.IsDmgHalf;
        UpgradeFleshCount = dto.UpgradeFleshCount;
        PartDropProbability = dto.PartDropProbability;
    }
}