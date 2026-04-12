using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public enum StampType { Approved, Declined };

public class Stamp : MonoBehaviour
{
    private Camera cam;

    private static Stamp currentDraggingStamp;
    [SerializeField] private LayerMask slotLayer;

    [Header("Input (assign InputActionReferences)")]
    public InputActionReference leftClickAction;

    [Header("Stamp things")]
    private Vector2 offset;
    private Vector2 correctPos;
    private bool isDraggingStamp;

    public Sprite stampSpriteIcon;          //the sprite that will be chopped (accept/declide)
    public Sprite draggingStampIcon;        //the sprite that will follow the mouse cursor, the top view of stamp

    public StampType stampType;

    private SpriteRenderer sr;
    private Sprite originalSprite;
    private int originalSortingOrder;

    [Header("SFX")]
    public string stampSFX;

    private void Awake()
    {
        cam = Camera.main;

        correctPos = transform.position;    //save the starting pos

        sr = GetComponent<SpriteRenderer>();
        originalSprite = sr.sprite;
        originalSortingOrder = sr.sortingOrder;
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

        // prevent multiple stamps dragging
        if (currentDraggingStamp != null && currentDraggingStamp != this)
            return;

        //clicked the mouse, begin drag
        if (leftClickAction.action.WasPressedThisFrame())
        {
            //Debug.Log("left clicked, trybeginDrag now");
            TryBeginDrag();
        }


        //dragging n moving icon
        if (isDraggingStamp)
        {
            //Debug.Log("dragging doc icon rn");
            Vector3 pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = transform.position.z;
            transform.position = pos + (Vector3)offset;
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
        Vector3 worldPos3 = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 worldPos = new Vector2(worldPos3.x, worldPos3.y);

        Collider2D[] hits = Physics2D.OverlapPointAll(worldPos);

        if (hits.Length == 0) return;

        foreach (var hit in hits)
        {
            Stamp stamp = hit.GetComponentInParent<Stamp>();

            if (stamp == this)
            {
                currentDraggingStamp = this;
                isDraggingStamp = true;

                offset = transform.position - (Vector3)worldPos;

                //change stamp sprite
                if (draggingStampIcon != null)
                    sr.sprite = draggingStampIcon;

                //bring sprite to front
                originalSortingOrder = sr.sortingOrder;
                sr.sortingOrder = 9999;

                return;
            }
        }
    }

    private void TryEndDrag()
    {
        if (!isDraggingStamp) return;
        
        if (currentDraggingStamp != this) return;           //only current stamp can activate TryEndDrag()

        Vector3 worldPos3 = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 worldPos = new Vector2(worldPos3.x, worldPos3.y);

        Collider2D hit = Physics2D.OverlapPoint(worldPos, slotLayer);

        //if hit object has the stamp slot script
        if (hit != null)
        {
            //Debug.Log("Hit: " + hit.collider.name);

            if (hit.TryGetComponent(out StampSlot slot))
            {
                AudioManager.Instance.PlaySFX(stampSFX);
                //Debug.Log("Try stamping");
                slot.PlaceStamp(this);
            }
        }

        //put it back to ori place, sorting order, n ori sprite
        transform.position = correctPos;

        sr.sprite = originalSprite;
        sr.sortingOrder = originalSortingOrder;

        isDraggingStamp = false;
        currentDraggingStamp = null;
    }

    public static bool IsAnyStampDragging()
    {
        return currentDraggingStamp != null;
    }
}

