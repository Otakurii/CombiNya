using UnityEngine;

public class StampSlot : MonoBehaviour
{
    public DocumentDatas docData;              // assign from document
    private SpriteRenderer sr;

    public bool hasStamp = false;      //see if has stamp enot
    public bool isCorrect = false;      //see if its correct enot

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Documents doc = GetComponentInParent<Documents>();

        if (doc != null)
        {
            docData = doc.docDatas;
        }
        else
        {
            Debug.LogError("No Documents script found in parent!");
        }
    }
    //check if stamp is correct with the doc data's answer

    public void PlaceStamp(Stamp stamp)
    {
        //if already has stamp, prevent overwrite
        //if (hasStamp)
        //{
        //    Debug.Log("Slot already stamped!");
        //    return;
        //}


        // set sprite visually
        if (sr != null)
        {
            sr.sprite = stamp.stampSpriteIcon;
        }

        hasStamp = true;

        //store result of stamp
        //isCorrect = (stamp.stampType == docData.stampAns);

        // check correctness
        if (stamp.stampType == docData.stampAns)
        {
            isCorrect = true;
            Debug.Log("Correct stamp");
        }
        else
        {
            isCorrect = false;
            Debug.Log("Wrong stamp");
        }
    }
}
