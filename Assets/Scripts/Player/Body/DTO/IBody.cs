public interface IBody : IParts.IDamageable, IParts.IEquipable
{           
    int Shield { get; }          // 추가 방어력 (ex. 근육형만 있음)
    bool IsDmgHalf { get; }             // 받는 데미지를 절반으로 줄이는 여부

    void Heal(int amount);              // 체력 회복
}