using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup canvasGroup;
    Transform parentToReturnTo = null;
    private RectTransform rectTransform;

    Vector3 positionToReturnTo;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag started");
        parentToReturnTo = this.transform.parent;
        positionToReturnTo = this.transform.position;

        transform.parent.parent.parent.GetComponent<PlayerController>().ShiftCardsAtLeft(transform.parent.GetComponent<CardSlot>().index);

        rectTransform.SetParent(null);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector2 localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out localPos);
        transform.position = transform.TransformPoint(localPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent ==  null)
        {
            Debug.Log("Drag stopped");
            parentToReturnTo.parent.parent.GetComponent<PlayerController>().ShiftCardsAtRight(parentToReturnTo.GetComponent<CardSlot>().index);
            rectTransform.SetParent(parentToReturnTo);
            transform.position = positionToReturnTo;
        }
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("onPointerDown");
    }
}
