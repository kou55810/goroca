using UnityEngine;
using UnityEngine.EventSystems;

public class BelongingsMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        // blocksRaycastsをオフにする
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // blocksRaycastsをオンにする
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
