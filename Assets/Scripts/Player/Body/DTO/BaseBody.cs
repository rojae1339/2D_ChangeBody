using System;using UnityEngine;

public abstract class BaseBody : MonoBehaviour, IBody
{
    [field: SerializeField]
    public string BodyName { get; set; }   
    [field: SerializeField]
    public TierType Tier { get; protected set; }
    [field: SerializeField]
    public int Hp { get; protected set; }
    [field: SerializeField]
    public int Shield { get; protected set; }
    [field: SerializeField]
    public bool IsDead { get; protected set; }
    [field: SerializeField]
    public bool IsDmgHalf { get; protected set; }
    [field: SerializeField]
    public int UpgradeFleshCount { get; protected set; }
    [field: SerializeField]
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
            int absorbed = (int)Math.Min(Shield, finalDamage);
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
        BodyName = dto.BodyName;
        Tier = dto.Tier;
        Hp = dto.Hp;
        Shield = dto.Shield;
        IsDead = dto.IsDead;
        IsDmgHalf = dto.IsDmgHalf;
        UpgradeFleshCount = dto.UpgradeFleshCount;
        PartDropProbability = dto.PartDropProbability;
    }

    public BaseBody Init(BodyDTO dto)
    {
        BodyName = dto.BodyName;
        Tier = dto.Tier;
        Hp = dto.Hp;
        Shield = dto.Shield;
        IsDead = dto.IsDead;
        IsDmgHalf = dto.IsDmgHalf;
        UpgradeFleshCount = dto.UpgradeFleshCount;
        PartDropProbability = dto.PartDropProbability;
        return this;
    }
}