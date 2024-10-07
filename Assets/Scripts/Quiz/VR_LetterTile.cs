using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VR_LetterTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Font font;
    public string alphabetLetter;
    private Transform parentPos;
    private QuizManager manager;
    private Vector3 dragOffset;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private Canvas canvas;

    private void Awake()
    {
        CanvasGroup charCanvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        if (charCanvasGroup)
        {
            charCanvasGroup.interactable = true;
            charCanvasGroup.blocksRaycasts = true;
        }
    }

    void Start()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
        canvas = GetComponentInParent<Canvas>();
        if (eventSystem == null)
        {
            Debug.LogError("No EventSystem found in the scene. Please add one.");
        }
        manager = GameObject.FindGameObjectWithTag("Quiz Manager")?.GetComponent<QuizManager>();
        if (manager == null)
        {
            Debug.LogError("Could not find QuizManager. Make sure there's a GameObject with the 'Quiz Manager' tag and a QuizManager component.");
        }

        Text letterText = transform.GetChild(0).GetComponent<Text>();
        if (letterText != null)
        {
            letterText.text = alphabetLetter;
            letterText.font = font;
        }
        else
        {
            Debug.LogError("Child Text component not found. Make sure the tile has a child with a Text component.");
        }

        if (parentPos == null)
        {
            parentPos = transform.parent;
            Debug.Log("Initializing parentPos in Start method");
        }

        SetZPositionToZero(transform);
    }
    private void SetZPositionToZero(Transform trans)
    {
        Vector3 position = trans.localPosition;
        position.z = 0;
        trans.localPosition = position;

        // Set z-position to 0 for all children recursively
        for (int i = 0; i < trans.childCount; i++)
        {
            SetZPositionToZero(trans.GetChild(i));
        }
    }
    public void setParentPos(Transform tr)
    {
        if (tr != null)
        {
            parentPos = tr;
            GetComponent<CanvasGroup>().interactable = false;
        }
        else
        {
            Debug.LogError("Attempted to set parentPos to null in setParentPos method.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        if (parentPos != null)
        {
            dragOffset = transform.position - GetVRPointerWorldPosition(eventData);
            transform.SetParent(canvas.transform); 
        }
        else
        {
            Debug.LogWarning("parentPos is null in OnBeginDrag. Make sure setParentPos is called.");
        }

        if (QuizManager.state != null)
        {
            QuizManager.state = InputState.isHold;
        }
        else
        {
            Debug.LogError("QuizManager.state is null. Make sure it's properly initialized.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            Vector3 newPos = GetVRPointerWorldPosition(eventData) + dragOffset;
            transform.position = newPos;
        }
        else
        {
            Debug.LogError("Canvas reference is null. Cannot update position.");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (parentPos != null)
        {
            transform.SetParent(parentPos);
            transform.position = parentPos.position;
        }
        else
        {
            Debug.LogWarning("parentPos is null in OnEndDrag. The tile will not be repositioned.");
        }

        if (QuizManager.state != null)
        {
            QuizManager.state = InputState.isFree;
        }
        else
        {
            Debug.LogError("QuizManager.state is null. Make sure it's properly initialized.");
        }

        if (manager != null)
        {
            manager.PopupActive();
        }
        else
        {
            Debug.LogError("QuizManager reference is null. Make sure it's properly assigned.");
        }
    }

    private Vector3 GetVRPointerWorldPosition(PointerEventData eventData)
    {
        Vector3 worldPosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out worldPosition
        ))
        {
            return worldPosition;
        }
        else
        {
            Debug.LogWarning("Failed to convert screen point to world point. Returning current position.");
            return transform.position;
        }
    }
}