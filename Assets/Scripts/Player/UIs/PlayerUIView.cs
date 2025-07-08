using System;
using UnityEngine;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField]
    private Canvas _mainCanvas;
    private GameObject _dropPartsUIPanel;

    private PartsUIPresenter _presenter;
    
    private void Start()
    {
        _presenter = new PartsUIPresenter(this, gameObject.GetComponent<Player>());
    }

    public void CreateDropPartsUI()
    {
        _dropPartsUIPanel = Managers.Managers.Addressable.Instantiate(AddressableUIKeys.DropPartsUIPanel, _mainCanvas.transform);
    }
}