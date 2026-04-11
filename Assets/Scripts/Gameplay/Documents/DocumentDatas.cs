using UnityEngine;


[CreateAssetMenu(fileName = "DocumentDatas", menuName = "Scriptable Objects/DocumentDatas")]

public class DocumentDatas : ScriptableObject
{
    [Header("References")]
    public Sprite docSpriteBig;            //how does the doc look like on big table
    public Sprite docSpriteSmall;            //how does the doc look like on small table
    public string docTexts;           //text for what texts r on this doc
    public DocumentType docType;        //enum for what type of docs this is

    [Header("Answers")]
    public StampType stampAns;

    public Sprite GetDocSpriteBig()
    {
        return docSpriteBig;
    }

    public Sprite GetDocSpriteSmall()
    {
        return docSpriteSmall;
    }

    public DocumentType GetDocType()
    {
        return docType;
    }
}

public enum DocumentType { NPCID, ShipRegistration, GoodsPermit };
