using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractCardController : MonoBehaviour
{

    /// <summary>
    /// カードビュー
    /// </summary>
    public CardView view; // カードの見た目の処理

    /// <summary>
    /// カードのモデル
    /// </summary>
    public CardModel model; // カードのデータを処理

    [SerializeField] public Button button; // カードボタン

    /// <summary>
    /// カードの動きを処理
    /// </summary>
    public CardMovement movement;

    public void ShowCard()
    {
        GameManager.instance.ShowCardView(model.id);
    }

    public void HideCard()
    {
        GameManager.instance.HideCardView();
    }

    public bool IsParentTrans(string name)
    {
        if (movement != null)
        {
            if (movement.cardParent == null)
            {
                return this.transform.parent.name.Contains(name);
            }
            return movement.cardParent.name.Contains(name);
        }
        else
        {
            return false;
        }
    }

    public void DisableClickEvent()
    {
        button.enabled = false;
        DeleteCardDrop();
    }

    public void DeleteCardDrop()
    {
        Destroy(this.gameObject.GetComponent<CardDropField>());
    }

    /// <summary>
    /// 裏面を無効化
    /// </summary>
    public void DisableBackside()
    {
        Transform belongings = this.transform.Find("Backside");
        belongings.gameObject.SetActive(false);
    }
}
