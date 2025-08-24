using UnityEngine;
using UnityEngine.EventSystems;

public class DamageCounterMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform dameconParent;
    public Vector3 position;
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 元のカードの親要素を取得
        dameconParent = transform.parent;
        Transform canvas = GameObject.Find("Canvas").GetComponent<Transform>();
        position = canvas.position;
        transform.SetParent(canvas, false);

        // blocksRaycastsをオフにする
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dameconParent == null ){ return; }
        // 親要素を変更
        transform.position = position;
        transform.SetParent(dameconParent, false);
        // blocksRaycastsをオンにする
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
