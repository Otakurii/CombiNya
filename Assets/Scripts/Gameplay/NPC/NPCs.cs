using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCs : MonoBehaviour
{
    public NPCDatas NPCDatas;

    private List<GameObject> spawnedDocs = new List<GameObject>();

    private SpriteRenderer spriteRenderer;
    //[SerializeField] public TMP_Text dialogueTexts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();

        //SpawnDocs();
        //SpawnNPC();
    }

    public void SpawnNPC()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer on NPC!");
            return;
        }

        if (NPCDatas == null)
        {
            Debug.LogError("NPCDatas is NULL!");
            return;
        }

        spriteRenderer.sprite = NPCDatas.NPCSprite;

        SpawnDocs();
    }

    void SpawnDocs()
    {
        spawnedDocs.Clear();    //clear the list just in case

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

            //store the docs object inside the list
            spawnedDocs.Add(docObj);

            //assign data after instantiate prefab
            Documents doc = docObj.GetComponent<Documents>();
            if (doc != null)
            {
                doc.SetData(data);
            }
        }
    }

    public void DespawnDocs()
    {
        foreach (GameObject doc in spawnedDocs)
        {
            if (doc != null)
            {
                Destroy(doc);
            }
        }

        spawnedDocs.Clear();
    }
}
