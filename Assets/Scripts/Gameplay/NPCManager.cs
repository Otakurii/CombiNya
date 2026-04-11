using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPCDatas[] ListOfNPCDatas;
    public NPCs NPCPosition;

    int currentIndex = 0;

    //reference to other scripts
    public HealthManager healthManager;
    public BoatStocks boatStocks;

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

        StampSlot[] slots = FindObjectsOfType<StampSlot>();
        Debug.Log("Slots found: " + slots.Length);

        bool hasWrong = false;
        int wrongCount = 0;


        foreach (StampSlot slot in slots)
        {
            // ignore empty slots
            if (!slot.hasStamp)
            {
                Debug.Log("Missing stamp!");
                wrongCount++;
                hasWrong = true;
                continue;
            }

            if (!slot.isCorrect)    //if stamp is not correct
            {
                Debug.Log("there is wrong answer");
                wrongCount++;
                hasWrong = true;
            }
        }
        Debug.Log("hasWrong is " + hasWrong + ", the wrongCount is " + wrongCount);
        if (hasWrong)       //if there is wrong
        {
            Debug.Log("Wrong answer, will lose health. Call HealthManager now");

            healthManager.health -= wrongCount;
            healthManager.Health();
        }
        else
        {
            Debug.Log("All correct!");
        }
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
        boatStocks.SetNPCData(ListOfNPCDatas[currentIndex]);

        //Debug.Log("Spawning NPC: " + data.name);

        //assign data BEFORE spawning
        NPCPosition.NPCDatas = data;

        NPCPosition.SpawnNPC();

        currentIndex++;
    }
}
