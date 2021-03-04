using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace kitti
{
    public class CardSlot : MonoBehaviour, IDropHandler
    {
        public int index;
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                transform.parent.parent.GetComponent<PlayerController>().ShiftCardsAtRight(index);
                eventData.pointerDrag.GetComponent<RectTransform>().SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
        }
    }
}

