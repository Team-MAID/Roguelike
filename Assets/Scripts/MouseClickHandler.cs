using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Action<PointerEventData> OnItemClick = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnItemClick != null) OnItemClick(eventData);

    }
}
