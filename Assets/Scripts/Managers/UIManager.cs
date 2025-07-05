using System;

public class UIManager
{
    public delegate void OnPickupUIRequest<T>(T t, Action onEquip, Action onIgnore);
    public event OnPickupUIRequest<BaseWeapon> WeaponPickupUIRequested;
    public event OnPickupUIRequest<BaseBody> BodyPickupUIRequested;
    
    public void RequestWeaponPickupUI(BaseWeapon weapon, Action onEquip, Action onIgnore)
    {
        WeaponPickupUIRequested?.Invoke(weapon, onEquip, onIgnore);
    }

    public void RequestBodyPickupUI(BaseBody body, Action onWear, Action onIgnore)
    {
        BodyPickupUIRequested?.Invoke(body, onWear, onIgnore);
    }
}