using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


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
    public Sprite stampSpriteIcon;
    private bool isDraggingStamp;

    public StampType stampType;

    private void Awake()
    {
        cam = Camera.main;

        correctPos = transform.position;    //save the starting pos
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
            //Debug.Log("left clicked, trybeginDrag now");
            TryBeginDrag();
        }


        //dragging n moving icon
        if (isDraggingStamp)
        {
            //Debug.Log("dragging doc icon rn");
            Vector3 pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = -0.5f;
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
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = cam.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider == null) return;

        // only start dragging if clicking THIS stamp
        if (hit.collider.gameObject == gameObject)
        {
            currentDraggingStamp = this;
            isDraggingStamp = true;
            offset = transform.position - (Vector3)worldPos;
        }
    }

    private void TryEndDrag()
    {
        if (!isDraggingStamp) return;
        //only current stamp can activate TryEndDrag()
        if (currentDraggingStamp != this) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = cam.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, slotLayer);

        //if hit object has the stamp slot script
        if (hit.collider != null)
        {
            //Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.TryGetComponent(out StampSlot slot))
            {
                //Debug.Log("Try stamping");
                slot.PlaceStamp(this);
            }
        }

        //put it back to ori place
        transform.position = correctPos;
        isDraggingStamp = false;
        currentDraggingStamp = null;
    }


}

