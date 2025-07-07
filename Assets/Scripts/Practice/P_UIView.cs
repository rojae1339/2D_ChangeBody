using TMPro;
using UnityEngine;

public class P_UIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    
    public void UpdateText(string text)
    {
        _text.text = text;
    }
}
