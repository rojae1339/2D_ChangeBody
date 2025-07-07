using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public virtual void OpenUI()
    {
        gameObject.SetActive(true);
    }
    
    public virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }
}