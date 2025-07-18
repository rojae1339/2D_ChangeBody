using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasePartsPanel : MonoBehaviour
{
    [SerializeField]
    private RawImage _partsImg;
    [SerializeField]
    private TextMeshProUGUI _partsTitleText;
    [SerializeField]
    private TextMeshProUGUI _partsDescText;


    //todo 유저가 무기 2개 들고있을때 추가하기 + 줄에 맞춰 글자크기 조정
    public void ChangePartInfo(RawImage img, TierType tier, string title, string desc)
    {
        _partsImg = img;
        _partsTitleText.color = ChangeTextColorByTier(tier);
        _partsTitleText.text = title;
        _partsDescText.text = desc;
        
        //todo 무기나  2개일때
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