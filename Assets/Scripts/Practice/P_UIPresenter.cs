using System;
using UnityEngine;
using UnityEngine.UI;

public class P_UIPresenter : MonoBehaviour
{
    [SerializeField]
    private P_UIModel model;

    [SerializeField]
    private P_UIView view;

    [SerializeField]
    private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(ChangeText);
    }


    public void ChangeText() { view.UpdateText(model.GenerateRandomString()); }
}