using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DragnDropSystem : MonoBehaviour
{
    private Camera cam;


    [Header("Input (assign InputActionReferences)")]
    public InputActionReference leftClickAction;

    [Header("State")]
    private Vector2 offset;
    private DocumentDatas draggingDoc = null;
    private GameObject dragingDocSprite;          //game object of the icon
    //private Image dragDocIconImage;             //image that appears n follows mouse cursor when dragged

    [SerializeField] private float scaleBigger;
    private Vector3 oriScale;
    [SerializeField] private int maxSortingInt = 0;

    private void Awake()
    {
        cam = Camera.main;
    }

    //enables input actions when script is active
    //.? is when actions isnt assign, it does nothing
    void OnEnable()
    {
        leftClickAction?.action?.Enable();
    }

    //disables input actions when script is inactive
    void OnDisable()
    {
        leftClickAction?.action?.Disable();
    }

    private void Update()
    {
        if (Mouse.current == null && EventSystem.current.IsPointerOverGameObject()) return;

        //clicked the mouse, begin drag
        if (leftClickAction.action.WasPressedThisFrame())
        {
            //Debug.Log("left clicked, trybeginDrag now");
            TryBeginDrag();
        }


        //dragging n moving icon
        if (draggingDoc != null && dragingDocSprite != null)
        {
            //Debug.Log("dragging doc icon rn");
            Vector3 pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = -0.5f;
            dragingDocSprite.transform.position = pos + (Vector3)offset;
        }

        //release the mouse, stop dragging
        if (leftClickAction.action.WasReleasedThisFrame())
        {
            //Debug.Log("released left click, tryendDrag now");
            TryEndDrag();
        }

    }

    

    private void TryBeginDrag()
    {
        //raycast2D from camera to click position
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = cam.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);


        if (hit.collider == null) return;
        
        //if hit object has doc script n main table tag, let the sprite become 
        if (hit.collider.TryGetComponent(out Documents doc))
        {
            //Debug.Log("dis world item is a Document, can drag");

            
            draggingDoc = doc.docDatas;
            dragingDocSprite = hit.collider.gameObject;

            //offset so it wouldnt snap mouse to the pivot
            offset = dragingDocSprite.transform.position - (Vector3)worldPos;

            maxSortingInt++;

            // sprite renderers
            SpriteRenderer[] renderers = dragingDocSprite.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var r in renderers)
            {
                r.sortingOrder = maxSortingInt + 1;
            }

            // canvas
            Canvas canvas = dragingDocSprite.GetComponentInChildren<Canvas>(true);
            if (canvas != null)
            {
                canvas.overrideSorting = true;
                canvas.sortingOrder = maxSortingInt + 1;
            }
            //scale
            oriScale = dragingDocSprite.transform.localScale;
            dragingDocSprite.transform.localScale = new Vector3(scaleBigger, scaleBigger, scaleBigger);
            
          

            // ---------- PLAY SOUND ----------
            //if (audioSource != null && pickItemSfx != null)
            //{
            //    audioSource.PlayOneShot(pickItemSfx);
            //}
        }
    }


    private void TryEndDrag()
    {
        if (draggingDoc == null || dragingDocSprite == null) return;

        //when put, put the image on the most top
        Vector3 pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        pos.z = -0.5f;

        dragingDocSprite.transform.position = pos + (Vector3)offset;

        SpriteRenderer sr = dragingDocSprite.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = maxSortingInt;
        }
        Canvas canvas = dragingDocSprite.GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = maxSortingInt;
        }
        SpriteRenderer srStamp = dragingDocSprite.GetComponent<SpriteRenderer>();

        if (srStamp == null)
        {
            srStamp = dragingDocSprite.GetComponentInChildren<SpriteRenderer>(true);
        }
        if (srStamp != null)
        {
            srStamp.sortingOrder = maxSortingInt;
        }

        //scale
        dragingDocSprite.transform.localScale = oriScale;

        //put the item at the place of mouse is last at
        draggingDoc = null;
        dragingDocSprite = null;
    }
}
