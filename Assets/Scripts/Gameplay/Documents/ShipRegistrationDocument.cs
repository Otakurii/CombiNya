using UnityEngine;
using TMPro;

public class ShipRegistrationDocument : Documents
{
    [Header("Things on NPC ID")]
    [SerializeField] private TMP_Text shipText;
    [SerializeField] private TMP_Text captainText;
    [SerializeField] private TMP_Text originText;
    [SerializeField] private TMP_Text cargoText;

    [SerializeField] private SpriteRenderer signPic;
    [SerializeField] private SpriteRenderer logoPic;

    protected override void ApplyData()
    {
        base.ApplyData();

        shipText.text = docDatas.shipAns;
        captainText.text = docDatas.captainAns;
        originText.text = docDatas.originAns;
        cargoText.text = docDatas.cargoAns;

        signPic.sprite = docDatas.signs;
        logoPic.sprite = docDatas.logoAns;
    }
}
