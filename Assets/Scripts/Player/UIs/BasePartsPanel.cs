using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasePartsPanel : MonoBehaviour
{
    [SerializeField]
    private Image _partsImg;
    [SerializeField]
    private TextMeshProUGUI _partsTitleText;
    [SerializeField]
    private TextMeshProUGUI _partsDescText;


    //todo 상위옵션 화살표 추가
    public void ChangePartInfo(Sprite spr, TierType tier, string title, string desc)
    {
        _partsImg.sprite = spr;
        _partsTitleText.color = ChangeTextColorByTier(tier);
        _partsTitleText.text = title;
        _partsDescText.text = desc;
        
    }

    private Color ChangeTextColorByTier(TierType tier)
    {
        switch (tier)
        {
            //드롭된 파츠의 tier에 따라 글씨색 변경
            case TierType.Legendary: return Color.crimson;
            case TierType.Unique: return Color.yellowNice;
            case TierType.Rare: return Color.deepSkyBlue;
            case TierType.Common: return Color.gray3;
            default: throw new ArgumentOutOfRangeException(nameof(tier), tier, null);
        }
    }
}