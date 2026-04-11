using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;         //name of NPC

    //is is make text area bigger for sentences to be written into editor
    //1st one is min lines of big, 2nd one is max lines of big
    [TextArea(3, 10)]
    public string[] sentences;
}