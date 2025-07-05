using System;
using System.Collections.Generic;
using UnityEngine;



public class UIManager
{
    public delegate string OnShowUI();
    public event OnShowUI ShowUI;

    public void ShowPopupTrigger(GameObject go)
    {
        GameObject uiObj = Managers.Managers.Addressable.Instantiate("PopupCanvas");

        // UI의 부모를 Canvas로 설정 (중요!)
        uiObj.transform.SetParent(GameObject.Find("Canvas").transform, false);

        PopupUI popup = uiObj.GetComponent<PopupUI>();

        if (popup != null)
        {
            string description = ShowUI?.Invoke() ?? "기본 메시지";
            popup.Setup(description);
            popup.FollowTarget(go.transform); // 계속 따라가게
        }
    }

    public void OffPopupTrigger(GameObject go)
    {
        
    }
}