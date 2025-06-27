# 2D_PartsChange

This game is body_parts_changeable game.

You can change body, weapon(arm), legs, and each parts has different status.

## What's important system?

- parts change system
- json to object
- map generate
- delegate ui

# Parts Change System

1. 단순하게 스크립터블오브젝트(이하 SO)로 하려고 했으나 지금 당장은 10개의 데이터만 있다고 하더라도 나중에 100개가 추가되면 그때마다 새로운 에셋을 맞춰서 추가해줘야 하는게 너무 시간낭비

2. 따라서 json을 이용해 데이터를 불러온 후, 파싱하여 파싱된 값에 알맞게 컴포넌트를 붙여 파츠의 등급, 데이터 등을 관리하기로 함.
    1. 서버에서 http요청을 통해 json형식으로 값을 가져온다는 가정하에 Json Format으로 데이터를 처리하기로 함

3. tier(Legendary, Unique, Rare, Common)에 따른 파티클은 SO로 만들어서 관리할 것.

4. 자세한 데이터는 /Assets/JsonData/~~~.json 에서 확인가능

# Interface

각 파츠와 몬스터는 여러가지 인터페이스로 이루어져 있다.

```cs
interface IDamageable
{
    void TakeDamage(int dmg);
    void Dead();
    bool IsDead{get;}
}

interface IAttackable
{
    void Attack(IDamageable damageable, float dmg);
    float Damage{get;}
}

//...todo add later
```


# Json Format

using Unity's package called

`JsonUtility`.

Comparison with `NewtonJson`, and this game doesn't need complex and complicated json data type.

Also, each json format has its own variable. 
(e.g 
    ShortSword -> tier, attackDamage, attackSpeed, isDoubleAttack, 
    Rifle -> tier, attackDamage, attackSpeed, maxBulletCount
)

## parts
- Weapon
    - tier (string)
    - attackDamage (float)
    - attackSpeed (float)
    - upgradeFleshCount (int)
    - partDropProbability (float)

- Body
    - tier (string)
    - health (float)
    - shieldCount (int)
    - upgradeFleshCount (int)
    - partDropProbability (float)

- Leg
    - tier (string)
    - stamina (float)
    - moveSpeed (float)
    - jumpCount (int)
    - jumpPower (float)
    - dodgeCount (int)
    - dodgeCooldown (float)
    - upgradeFleshCount (int)
    - partDropProbability (float)

## common monster
- ShortSwordMonster
    - health (float)
    - moveSpeed (float)
    - attackDamage (float)
    - strongAttackDamage (float)
    - attackSpeed (float)
    - strongAttackCooldown (float)
    - fleshDropProbability (float)
    - dropGold (int)

- LongSwordMonster
    - health (float)
    - moveSpeed (float)
    - attackDamage (float)
    - strongAttackDamage (float)
    - attackSpeed (float)
    - strongAttackCooldown (float)
    - fleshDropProbability (float)
    - dropGold (int)

- PistolMonster
    - health (float)
    - moveSpeed (float)
    - attackDamage (float)
    - attackSpeed (float)
    - bulletSpeed (float)
    - fleshDropProbability (float)
    - dropGold (int)

- RifleMonster
    - health (float)
    - moveSpeed (float)
    - attackDamage (float)
    - strongAttackDamage (float)
    - attackSpeed (float)
    - bulletSpeed (float)
    - strongAttackCooldown (float)
    - fleshDropProbability (float)
    - dropGold (int)

- ShotgunMonster
    - health (float)
    - moveSpeed (float)
    - bulletPerAttack (int)
    - attackDamage (float)
    - attackSpeed (float)
    - bulletSpeed (float)
    - fleshDropProbability (float)
    - dropGold (int)

## mini boss
// todo