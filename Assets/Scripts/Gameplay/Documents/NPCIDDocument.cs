using UnityEngine;
using TMPro;

public class NPCIDDocument : Documents
{
    [Header("Things on NPC ID")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text originText;
    [SerializeField] private TMP_Text roleText;

    [SerializeField] private SpriteRenderer IDPic;

    protected override void ApplyData()
    {
        base.ApplyData();

        nameText.text = docDatas.nameAns;
        originText.text = docDatas.originAns;
        roleText.text = docDatas.roleAns;

        IDPic.sprite = docDatas.IDPicAns;
    }

    
}
