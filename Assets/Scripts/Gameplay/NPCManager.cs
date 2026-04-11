using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPCDatas[] ListOfNPCDatas;
    public NPCs NPCPosition;

    int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CallNextNPC();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishCheck()           //this is for the NEXT button 
    {
        CheckAns();
        CallNextNPC();
    }

    void CheckAns()
    {
        Debug.Log("Check Ans");
        //this function to check all the answer if correct
        //calls from the stampslot script there

        //also calls the healthManager script
        //see if deduct health enot
    }

    void CallNextNPC()
    {
        if (currentIndex >= ListOfNPCDatas.Length)
        {
            Debug.Log("No more NPCs");
            return;
        }

        //this function to let this NPC out (means delete all the docs they have on table rn)
        NPCPosition.DespawnDocs();

        //then only call in the next NPC
        NPCDatas data = ListOfNPCDatas[currentIndex];

        //Debug.Log("Spawning NPC: " + data.name);

        //assign data BEFORE spawning
        NPCPosition.NPCDatas = data;

        NPCPosition.SpawnNPC();

        currentIndex++;
    }
}
