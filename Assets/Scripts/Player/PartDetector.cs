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

        BaseWeapon closestWeapon = null;
        BaseBody closestBody = null;
        float closestWeaponDistanceSqr = float.MaxValue;
        float closestBodyDistanceSqr = float.MaxValue;

        Vector2 playerPos = transform.position;

        foreach (Collider2D hit in hits)
        {
            if (!hit || hit.CompareTag("Player")) continue;

            if (hit.transform.IsChildOf(gameObject.transform)) continue;

            // 제곱 거리로 계산 (Sqrt 연산 생략으로 성능 향상)
            float distanceSqr = (playerPos - (Vector2)hit.transform.position).sqrMagnitude;

            if (hit.CompareTag("Weapon"))
            {
                var part = hit.GetComponent<IParts.IEquipable>() as BaseWeapon;
                if (part != null && distanceSqr < closestWeaponDistanceSqr)
                {
                    closestWeapon = part;
                    closestWeaponDistanceSqr = distanceSqr;
                }
            }
            else if (hit.CompareTag("Body"))
            {
                var part = hit.GetComponent<IParts.IEquipable>() as BaseBody;
                if (part != null && distanceSqr < closestBodyDistanceSqr)
                {
                    closestBody = part;
                    closestBodyDistanceSqr = distanceSqr;
                }
            }
        }

        // 가장 가까운 아이템 중에서 우선순위 결정 (Weapon 우선)
        if (closestWeapon != null && (closestBody == null || closestWeaponDistanceSqr <= closestBodyDistanceSqr))
        {
            CurrentWeapon = closestWeapon;
            CurrentDetectedType = DetectedPartType.Weapon;
            OnWeaponDetected?.Invoke(closestWeapon);
            found = true;
        }
        else if (closestBody != null)
        {
            CurrentBody = closestBody;
            CurrentDetectedType = DetectedPartType.Body;
            OnBodyDetected?.Invoke(closestBody);
            found = true;
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

    public void CancelDetect() { OnCancelDetect?.Invoke(); }
}