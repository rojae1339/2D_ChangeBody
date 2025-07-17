using System;
using Unity.VisualScripting;
using UnityEngine;

public enum DetectedPartType
{
    None,
    Weapon,
    Body
}

public class PartDetector : MonoBehaviour
{
    public event Action<BaseWeapon> OnWeaponDetected;
    public event Action<BaseBody> OnBodyDetected;
    public event Action OnInteractWeaponUIToggle;
    public event Action OnInteractBodyUIToggle;
    public event Action OnPartNotDetected;
    public event Action OnCancelDetect;

    [SerializeField]
    private float rad = 2f;
    
    // 현재 감지 상태
    public DetectedPartType CurrentDetectedType { get; private set; } = DetectedPartType.None;
    public BaseWeapon CurrentWeapon { get; private set; }
    public BaseBody CurrentBody { get; private set; }

    private void Update() { DetectObject(); }

    private void DetectObject()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, rad, LayerMask.GetMask("Parts"));
        bool found = false;

        CurrentWeapon = null;
        CurrentBody = null;
        CurrentDetectedType = DetectedPartType.None;

        foreach (Collider2D hit in hits)
        {
            if (!hit || hit.CompareTag("Player")) continue;

            if (hit.CompareTag("Weapon"))
            {
                var part = hit.GetComponent<IParts.IEquipable>() as BaseWeapon;
                if (part != null)
                {
                    CurrentWeapon = part;
                    CurrentDetectedType = DetectedPartType.Weapon;
                    OnWeaponDetected?.Invoke(part);
                    found = true;
                    break;
                }
            }
            else if (hit.CompareTag("Body"))
            {
                var part = hit.GetComponent<IParts.IEquipable>() as BaseBody;
                if (part != null)
                {
                    CurrentBody = part;
                    CurrentDetectedType = DetectedPartType.Body;
                    OnBodyDetected?.Invoke(part);
                    found = true;
                    break;
                }
            }
        }

        if (!found)
        {
            CurrentDetectedType = DetectedPartType.None;
            OnPartNotDetected?.Invoke();
        }
    }
    
    public void ToggleInteractUI()
    {
        switch (CurrentDetectedType)
        {
            case DetectedPartType.Weapon:
                OnInteractWeaponUIToggle?.Invoke();
                break;
            case DetectedPartType.Body:
                OnInteractBodyUIToggle?.Invoke();
                break;
            default:
                // 감지된 게 없을 때는 무시하거나 사운드/메시지를 줄 수 있음
                break;
        }
    }

    public void CancelDetect()
    {
        OnCancelDetect?.Invoke();
        
    }
}