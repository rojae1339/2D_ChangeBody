using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private int damage;
    public int Damage
    {
        get { return damage; }
    }
}
