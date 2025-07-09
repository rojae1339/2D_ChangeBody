using System;
using Unity.VisualScripting;
using UnityEngine;

public class PartDetector : MonoBehaviour
{
    public event Action<BaseWeapon> OnWeaponDetected;
    public event Action<BaseBody> OnBodyDetected;
    public event Action OnPartNotDetected;

    [SerializeField]
    private float rad = 2f;

    private void Update() { DetectObject(); }

    private void DetectObject()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, rad, LayerMask.GetMask("Parts"));
        bool found = false;
        
        //IParts.IEquipable가 detected되면, 실행
        foreach (Collider2D hit in hits)
        {
            if (hit.tag == "Player") continue; 

            if (hit.tag != "Weapon" && hit.tag != "Body") continue;

            IParts.IEquipable part = hit.gameObject.GetComponent<IParts.IEquipable>();

            if (hit.tag == "Weapon")
            {
                OnWeaponDetected?.Invoke(part as BaseWeapon);
            }
            else if (hit.tag == "Body")
            {
                OnBodyDetected?.Invoke(part as BaseBody);
            }
            
            found = true;
            break;
        }
        
        //IParts.IEquipable가 undetected일때 실행
        if (!found)
        {
            OnPartNotDetected?.Invoke();
        }
    }
}