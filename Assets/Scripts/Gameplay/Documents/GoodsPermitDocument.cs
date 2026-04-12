using UnityEngine;
using TMPro;

public class GoodsPermitDocument : Documents
{
    [Header("Things on NPC ID")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text goodsText;
    [SerializeField] private TMP_Text quantityText;

    [SerializeField] private SpriteRenderer signPic;

    protected override void ApplyData()
    {
        base.ApplyData();

        nameText.text = docDatas.nameAns;
        goodsText.text = docDatas.goodsAns;
        quantityText.text = docDatas.quantityAns;

        signPic.sprite = docDatas.signs;
    }
}
