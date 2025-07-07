using System.Collections;
using UnityEngine;
using TMPro; // 예시용 텍스트 컴포넌트

public class PopupUI : MonoBehaviour
{
    [SerializeField]
    private float upUI = 100f;

    [SerializeField]
    private RectTransform cover;
    
    [SerializeField]
    private TextMeshProUGUI popupText;

    public void OpenPopup(string desc) 
    {
        popupText.text = desc;
        gameObject.SetActive(true);
    }
    
    
    
    public PopupUI SetPosition(Vector3 worldPos)
    {
        cover.anchoredPosition = new Vector2(0, 85); // 고정된 위치로 설정
        return this;
    }
}