using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragnDropSystem : MonoBehaviour
{
    private Camera cam;

    [Header("Input (assign InputActionReferences)")]
    public InputActionReference leftClickAction;

    [Header("State")]
    private Vector2 offset;
    private GameObject dragingDocSprite;          //game object of the icon
    //private Image dragDocIconImage;             //image that appears n follows mouse cursor when dragged

    [SerializeField] private float scaleBigger = 0.6f;
    private Vector3 oriScale;

    [SerializeField] private int maxSortingInt = 0;

    private SpriteRenderer mainSR;
    private Sprite originalSprite;
    private Sprite smallSprite;

    private GameObject contentsWhenBig;         //all the contents r on here when the docs is big

    private bool isOnTable = true;

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
            //Debug.Log("left clicked, trybeginDrag now");
            TryBeginDrag();
        }


        //dragging n moving icon
        if (dragingDocSprite != null)
        {
            //Debug.Log("dragging doc icon rn");
            Vector3 pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = dragingDocSprite.transform.position.z;
            dragingDocSprite.transform.position = pos + (Vector3)offset;

            //Debug.Log("1. curentsprite is " + mainSR.sprite);
            HandleTableCheck();
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


        //if hit object has doc script n main table tag, let the sprite become 
        foreach (var hit in hits)
        {
            Documents doc = hit.GetComponentInParent<Documents>();

            if (doc != null)
            {
                dragingDocSprite = doc.gameObject;

                offset = dragingDocSprite.transform.position - (Vector3)worldPos;

                //store sprite infos
                mainSR = dragingDocSprite.GetComponent<SpriteRenderer>();
                if (mainSR != null)
                {
                    //Debug.Log("2. curentsprite is " + mainSR.sprite);
                    originalSprite = doc.docDatas.GetDocSpriteBig();
                    //Debug.Log("3. oriSprite is " + originalSprite.name + ", curentsprite is " + mainSR.sprite);

                }
                smallSprite = doc.docDatas.GetDocSpriteSmall();

                //find contents object
                Transform contents = dragingDocSprite.transform.Find("Contents");
                if (contents != null)
                    contentsWhenBig = contents.gameObject;

                isOnTable = IsPointerOnTable();         //see what state the docs is in

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

        // reset scale
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

    private void HandleTableCheck()
    {
        bool insideTable = IsPointerOnTable();

        // ENTER TABLE = BIG MODE
        if (insideTable && !isOnTable)
        {
            //Debug.Log("inside table");
            if (mainSR != null && originalSprite != null)
            {
                //Debug.Log("into table, 4. oriSprite is " + originalSprite.name + ", curentsprite is " + mainSR.sprite);
                mainSR.sprite = originalSprite;
            }

            if (contentsWhenBig != null)
                contentsWhenBig.SetActive(true);

            isOnTable = true;
        }
        // EXIT TABLE = SMALL MODE
        else if (!insideTable && isOnTable)
        {
            //Debug.Log("outside table");
            if (mainSR != null && smallSprite != null)
            {
                //Debug.Log("out of table, 5. oriSprite is " + originalSprite.name + ", curentsprite is " + mainSR.sprite);
                mainSR.sprite = smallSprite;
            }

            if (contentsWhenBig != null)
                contentsWhenBig.SetActive(false);

            isOnTable = false;
        }
    }

    private bool IsPointerOnTable()
    {
        Vector2 worldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Collider2D[] hits = Physics2D.OverlapPointAll(worldPos);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("MainTable"))
                return true;
        }

        return false;
    }
}
