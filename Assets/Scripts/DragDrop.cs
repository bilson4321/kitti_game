using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    Transform parentToReturnTo = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag started");
        parentToReturnTo = transform.parent;
        this.transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
       /* rectTransform.anchoredPosition += eventData.delta;*/

        Vector2 localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out localPos);
        transform.position = transform.TransformPoint(localPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag stopped");
        this.transform.SetParent(parentToReturnTo);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("onPointerDown");
    }
}
