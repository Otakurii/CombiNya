using TMPro;
using UnityEngine;

public class BoatStocks : MonoBehaviour
{
    private NPCDatas currentNPC;

    [Header("Boat UI")]
    public TMP_Text boatNameText;

    [Header("Goods")]
    public Goods[] listOfGoods;              // all possible goods
    public Transform[] goodsParent;            // where to spawn UI       i think can make it into an array
    public GameObject goodsPrefab;           // UI prefab (Image)

    private int currentSlotIndex = 0;

    public void SetNPCData(NPCDatas npc)
    {
        currentNPC = npc;

        DisplayBoatData();
    }

    void DisplayBoatData()
    {
        if (currentNPC == null) return;

        boatNameText.text = "";     //clear boats name
        currentSlotIndex = 0;

        //clear the previous goods
        foreach (Transform slot in goodsParent)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (DocumentDatas doc in currentNPC.docDatas)
        {
            switch (doc.docType)
            {
                case DocumentType.ShipRegistration:
                    boatNameText.text = doc.shipAns;
                    break;

                case DocumentType.GoodsPermit:
                    HandleGoods(doc);
                    break;
            }
        }
    }

    //check the stamp type
    void HandleGoods(DocumentDatas doc)
    {
        int quantity;

        //convert string to int 
        if (!int.TryParse(doc.quantityAns, out quantity))
        {
            Debug.LogWarning("Invalid quantity: " + doc.quantityAns);
            quantity = 1; // fallback so game doesn't break
        }

        Sprite sprite;

        if (doc.stampAns == StampType.Approved)
        {
            // spawn correct goods
            sprite = GetGoodsSpriteByName(doc.goodsAns);
        }
        else
        {
            // spawn WRONG random goods
            sprite = GetRandomWrongGoods(doc.goodsAns);
        }

        for(int i = 0; i < quantity; i++)
        {
            if (currentSlotIndex >= goodsParent.Length)
            {
                Debug.LogWarning("Not enough goods slots!");
                return;
            }

            SpawnGoods(sprite, goodsParent[currentSlotIndex]);
            currentSlotIndex++;
        }
        
    }

    void SpawnGoods(Sprite sprite, Transform parent)
    {
        if (sprite == null || parent == null) return;

        GameObject obj = Instantiate(goodsPrefab);  //instantiate first
        obj.transform.SetParent(parent, false);     //then only assign it to a transform

        UnityEngine.UI.Image img = obj.GetComponent<UnityEngine.UI.Image>();
        if (img != null)
        {
            img.sprite = sprite;
            img.SetNativeSize();
        }
    }

    Sprite GetGoodsSpriteByName(string goodsName)
    {
        foreach (var g in listOfGoods)
        {
            if (g.goodsNameInDocs == goodsName)
                return g.goodsSprite;
        }

        Debug.LogWarning("No matching goods found for: " + goodsName);
        return null;
    }

    //make sure not to get the same kind of goods as written in the DocsData
    Sprite GetRandomWrongGoods(string correctName)
    {
        Goods random;

        do
        {
            random = listOfGoods[Random.Range(0, listOfGoods.Length)];
        }
        while (random.goodsNameInDocs == correctName);

        return random.goodsSprite;
    }


}
