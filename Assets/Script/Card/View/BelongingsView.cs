using System;
using UnityEngine;
using UnityEngine.UI;

public class BelongingsView : MonoBehaviour
{
    public int id = 0;
    public Guid guid;

    /// <summary>
    /// 道具マーク
    /// </summary>
    [SerializeField] public Image belongingsImage;
    public void SetBelongingsImage(Guid guid, int id)
    {
        this.guid = guid;
        this.id = id;
        belongingsImage.sprite = Resources.Load<Sprite>($"Images/Card/item/item_{id}");
        this.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// 道具を無効化
    /// </summary>
    public void DisableBelongings()
    {
        this.gameObject.SetActive(false);
    }
}
