using System.Collections;
using UnityEngine;
using TMPro; // 예시용 텍스트 컴포넌트

public class PopupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void Setup(string description)
    {
        descriptionText.text = description;
    }

    public void FollowTarget(Transform target)
    {
        StartCoroutine(UpdatePosition(target));
    }

    private IEnumerator UpdatePosition(Transform target)
    {
        while (target != null)
        {
            // 화면 공간으로 변환
            Vector3 worldPos = target.position + Vector3.up * 2.0f; // 위쪽 오프셋
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            transform.position = screenPos;

            yield return null;
        }
    }
}