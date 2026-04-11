using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "NPCDatas", menuName = "Scriptable Objects/NPCDatas")]

public class NPCDatas : ScriptableObject
{
    public DocumentDatas[] docDatas;

    public Sprite NPCSprite;            //sprite of NPC
    //public string[] NPCDialogues;       //dialogues of NPC


    public Sprite GetNPCSprite()
    {
        return NPCSprite;
    }

}
