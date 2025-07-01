using UnityEngine;

[CreateAssetMenu(fileName = "PartsTierFeatureConfig", menuName = "ScriptableObjects/PartsTierFeatureConfig")]
public class PartsTierFeatureSO : ScriptableObject
{
    [SerializeField]
    private string tier;

    [SerializeField]
    private GameObject particleGameObject;
}
