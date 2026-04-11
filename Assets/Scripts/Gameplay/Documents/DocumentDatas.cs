using UnityEngine;


public enum DocumentType { NPCID, ShipRegistration, GoodsPermit };

[System.Serializable]
public class DocPrefabEntry
{
    public DocumentType type;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "DocumentDatas", menuName = "Scriptable Objects/DocumentDatas")]

public class DocumentDatas : ScriptableObject
{
    [Header("References")]
    //public GameObject[] docPrefabType;          //how does the doc prefab is 
    public DocPrefabEntry[] prefabEntries;
    public Sprite[] docSpriteSmall;             //how does the doc look like on small table
    public Sprite[] docSpriteBig;             //how does the doc look like on small table
    public DocumentType docType;                //enum for what type of docs this is

    [Header("Answers")]
    public StampType stampAns;

    [Header("NPCID Answers")]                   //text for what texts r on this doc
    public string nameAns;
    public string originAns;
    public string roleAns;
    public Sprite IDPicAns;

    [Header("ShipRegistration Answers")]                   //text for what texts r on this doc
    public string shipAns;
    public string captainAns;
    //public string originAns;
    public string cargoAns;
    public Sprite signs;
    public Sprite logoAns;

    [Header("GoodsPermit Answers")]
    //public string nameAns;
    public string goodsAns;
    public string quantityAns;

    [SerializeField] private SpriteRenderer IDPic;

    public GameObject GetDocPrefab()
    {
        foreach (var entry in prefabEntries)
        {
            if (entry.type == docType)
                return entry.prefab;
        }

        Debug.LogError("No prefab found for type: " + docType);
        return null;
    }


    public Sprite GetDocSpriteSmall()
    {
        if ((int)docType >= docSpriteSmall.Length)
        {
            Debug.LogError("docSpriteSmall array not set correctly!");
            return null;
        }

        return docSpriteSmall[(int)docType];
    }

    public Sprite GetDocSpriteBig()
    {
        if ((int)docType >= docSpriteBig.Length)
        {
            Debug.LogError("docSpriteBig array not set correctly!");
            return null;
        }

        return docSpriteBig[(int)docType];
    }

    public DocumentType GetDocType()
    {
        return docType;
    }

}


