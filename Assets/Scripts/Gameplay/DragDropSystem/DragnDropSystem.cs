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
        if (Mouse.current == null) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        //clicked the mouse, begin drag
        if (leftClickAction.action.WasPressedThisFrame())
        {
            Debug.Log("left clicked, trybeginDrag now");
            TryBeginDrag();
        }


        //dragging n moving icon
        if (dragingDocSprite != null)
        {
            Debug.Log("dragging doc icon rn");
            Vector3 pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = dragingDocSprite.transform.position.z;
            dragingDocSprite.transform.position = pos + (Vector3)offset;
        }

        //release the mouse, stop dragging
        if (leftClickAction.action.WasReleasedThisFrame())
        {
            Debug.Log("released left click, tryendDrag now");
            TryEndDrag();
        }

    }

    

    private void TryBeginDrag()
    {
        //raycast2D from camera to click position
        Vector3 worldPos3 = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 worldPos = new Vector2(worldPos3.x, worldPos3.y);
        
        Collider2D[] hits = Physics2D.OverlapPointAll(worldPos);

        if (hits.Length == 0) return;


        //if hit object has doc script n main table tag, let the sprite become 
        foreach (var hit in hits)
        {
            Documents doc = hit.GetComponentInParent<Documents>();

            if (doc != null)
            {
                dragingDocSprite = doc.gameObject;

                offset = dragingDocSprite.transform.position - (Vector3)worldPos;

                StartDraggingVisuals();

                // ---------- PLAY SOUND ----------
                //if (audioSource != null && pickItemSfx != null)
                //{
                //    audioSource.PlayOneShot(pickItemSfx);
                //}

                return;
            }
        }

        
    
    }


    private void TryEndDrag()
    {
        if (dragingDocSprite == null) return;

        // reset sorting
        SpriteRenderer[] renderers = dragingDocSprite.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var r in renderers)
        {
            r.sortingOrder = maxSortingInt;
        }

        Canvas canvas = dragingDocSprite.GetComponentInChildren<Canvas>(true);
        if (canvas != null)
        {
            canvas.sortingOrder = maxSortingInt;
        }

        //scale
        dragingDocSprite.transform.localScale = oriScale;

        //put the item at the place of mouse is last at
        dragingDocSprite = null;
    }

    private void StartDraggingVisuals()
    {
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

        // scale
        oriScale = dragingDocSprite.transform.localScale;
        dragingDocSprite.transform.localScale = Vector3.one * scaleBigger;
    }
}
