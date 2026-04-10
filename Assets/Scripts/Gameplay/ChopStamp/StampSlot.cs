using UnityEngine;

public class StampSlot : MonoBehaviour
{
    public DocumentDatas docData;              // assign from document
    private SpriteRenderer sr;

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
        // set sprite visually
        if (sr != null)
        {
            sr.sprite = stamp.stampSpriteIcon;
        }

        // check correctness
        if (stamp.stampType == docData.stampAns)
        {
            Debug.Log("Correct stamp");
        }
        else
        {
            Debug.Log("Wrong stamp");
        }
    }
}
