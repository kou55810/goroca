using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement: MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// カードの親要素
    /// </summary>
    public Transform cardParent;
    public Vector3 position;

    void Start()
    {
        position = transform.position;
    }

    /// <summary>
    /// ドラッグ開始時のイベント
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 元のカードの親要素を取得
        cardParent = transform.parent;
        Transform canvas = GameObject.Find("Canvas").GetComponent<Transform>();
        position = canvas.position;
        transform.SetParent(canvas, false);

        // blocksRaycastsをオフにする
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// ドラッグしたとき
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cardParent == null ){ return; }
        // 親要素を変更
        transform.position = position;
        transform.SetParent(cardParent, false);
        // blocksRaycastsをオンにする
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
