using UnityEngine;
using UnityEngine.EventSystems;

public class EnergyMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform energyParent;
    public Vector3 position;
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 元のカードの親要素を取得
        energyParent = transform.parent;
        Transform canvas = GameObject.Find("Canvas").GetComponent<Transform>();
        position = canvas.position;
        transform.SetParent(canvas, false);

        // blocksRaycastsをオフにする
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        if (SEPlayer.instance != null)
        {
            SEPlayer.instance.Play18TouchEnergySoundEffect();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (energyParent == null ){ return; }
        // 親要素を変更
        transform.position = position;
        transform.SetParent(energyParent, false);
        // blocksRaycastsをオンにする
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
