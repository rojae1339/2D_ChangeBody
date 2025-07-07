using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private float detectRadius = 2.0f;
    [SerializeField] private LayerMask targetLayerMask;
    

    private PopupUI activePopup;
    private GameObject currentTarget;

    void Update()
    {
        
        ShowClosestPartPopup();
    }

    private void ShowClosestPartPopup()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRadius, targetLayerMask);

        GameObject closest = null;
        float minSqrDist = float.MaxValue;

        Vector2 playerPos = transform.position;

        foreach (var hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            string tag = hit.tag;

            if (tag == null) return;
            
            if (tag != nameof(TagType.Weapon) && tag != nameof(TagType.Body)) continue;

            float sqrDist = ((Vector2)hit.transform.position - playerPos).sqrMagnitude;
            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                closest = hit.gameObject;
            }
        }

        if (closest == currentTarget)
            return;

        if (activePopup != null)
        {
            Managers.Managers.Addressable.Destroy(activePopup.gameObject);
            activePopup = null;
            currentTarget = null;
        }

        if (closest != null)
        {
            GameObject popupGO = Managers.Managers.Addressable.Instantiate("PopupCanvas");
            activePopup = popupGO.GetComponent<PopupUI>();

            Vector3 worldPos = closest.transform.position + Vector3.up * 2f;
            activePopup.SetPosition(worldPos);

            switch (closest.tag)
            {
                case nameof(TagType.Weapon):
                    var bw = closest.GetComponent<BaseWeapon>();
                    activePopup.OpenPopup($"이름 : {bw.WeaponName}\n티어 : {bw.Tier}\n공격력 : {bw.AttackDamage}\n공격속도 : {bw.AttackSpeed}\n공격타입 : {bw.WeaponAttackType}");
                    break;
                case nameof(TagType.Body):
                    var bb = closest.GetComponent<BaseBody>();
                    activePopup.OpenPopup($"이름 : {bb.BodyName}\n티어 : {bb.Tier}\n체력 : {bb.Hp}\n공격반감 : {bb.IsDmgHalf}");
                    break;
            }

            currentTarget = closest;
        }
    }
}