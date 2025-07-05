using UnityEngine;

public class PlayerTrigger: MonoBehaviour
{
    public void TriggerObject(GameObject triggerGO)
    {
        Managers.Managers.UI.ShowPopupTrigger(triggerGO);
    }
}