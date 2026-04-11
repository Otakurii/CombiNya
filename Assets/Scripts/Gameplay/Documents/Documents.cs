using TMPro;
using UnityEngine;

public class Documents : MonoBehaviour
{
    public DocumentDatas docDatas;

    [Header("Doc stuff")]
    private SpriteRenderer spritePlace;
    private TMP_Text textPlace;

    private void Start()
    {
        //Debug.Log("Docdata's texts is: " + docDatas.docTexts);

        spritePlace = GetComponent<SpriteRenderer>();
        spritePlace.sprite = docDatas.docSpriteBig;

        //put the string texts onto the TMP_text
        textPlace = GetComponentInChildren<TMP_Text>();
        textPlace.text = docDatas.docTexts;
    }

    public void SetData(DocumentDatas data)
    {
        docDatas = data;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = data.GetDocSpriteBig();
        }
    }
}
