using UnityEngine;
using UnityEngine.UI;

public class P_UIController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private P_UIModel _model;
    
    private void Start()
    {
        _button.onClick.AddListener(() => _model.GenerateRandomString());
    }
}