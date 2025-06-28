using UnityEngine;

public class NoWeapon : IWeapon
{
//todo 퍼플렉시티 답변 참고

    #region Fields

    [SerializeField]
    private float _attDmg;

    [SerializeField]
    private float _attSpeed;

    [SerializeField]
    private TierType _tier;

    [SerializeField]
    private int _upgradeFleshCount;

    [SerializeField]
    private float _partDropProbability;

    [SerializeField]
    private Player _owner;

    #endregion

    #region Properties

    public float AttackDamage
    {
        get { return _attDmg; }
        set { _attDmg = value; }
    }

    public float AttackSpeed
    {
        get { return _attSpeed; }
        set { _attSpeed = value; }
    }

    public TierType Tier
    {
        get { return _tier; }
        set { _tier = value; }
    }

    public int UpgradeFleshCount
    {
        get { return _upgradeFleshCount; }
        private set { }
    }

    public float PartDropProbability
    {
        get { return _partDropProbability; }
        private set { }
    }

    public Player Owner
    {
        get { return _owner; }
        set { _owner = value; }
    }

    #endregion

    public void Attack(IParts.IDamageable damageable, float dmg) { }

    public void Upgrade(float status) { }

    public void BindPlayer(Player owner) { }

    public void UnBindPlayer() { }
    
    //todo file IO
}