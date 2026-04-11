using TMPro;
using UnityEngine;

public class Documents : MonoBehaviour
{
    public DocumentDatas docDatas;

    private void Start()
    {

    }

    public void SetData(DocumentDatas data)
    {
        docDatas = data;

        ApplyData();
    }
    protected virtual void ApplyData()
    {
        // base logic (optional)
        //individual things r written by the respective docs prefab's script themselves
    }
    
}
