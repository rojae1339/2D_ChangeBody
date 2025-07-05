using UnityEngine;

[CreateAssetMenu(fileName = "PartsTierFeatureConfig", menuName = "ScriptableObjects/PartsTierFeatureConfig")]
public class PartsTierFeatureSO : ScriptableObject
{
    [SerializeField]
    private TierType tier;

    [SerializeField]
    public GameObject particleGameObject;
}
