using TMPro;
using UnityEngine;

public class NPCs : MonoBehaviour
{
    public NPCDatas NPCDatas;

    private SpriteRenderer spriteRenderer;
    [SerializeField] public TMP_Text dialogueTexts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SpawnDocs();
        SpawnNPC();
    }

    void SpawnDocs()
    {
        //instantiate docs 
        for (int i = 0; i < NPCDatas.docDatas.Length; i++)
        {
            
            Vector3 randPos = new Vector3(Random.Range(0,7), Random.Range(-3,6));           //this is to randomize the x and y position


            DocumentDatas data = NPCDatas.docDatas[i];

            //get correct prefab from the documentData itself
            GameObject prefab = data.GetDocPrefab();

            if (prefab == null)
            {
                Debug.LogError("Prefab is null for doc type: " + data.docType);
                continue;
            }

            GameObject docObj = Instantiate(prefab, randPos, Quaternion.identity);

            //assign data after instantiate prefab
            Documents doc = docObj.GetComponent<Documents>();
            if (doc != null)
            {
                doc.SetData(data);
            }
        }
    }

    void SpawnNPC()
    {
        spriteRenderer.sprite = NPCDatas.NPCSprite;
    }

}
