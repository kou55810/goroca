using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デッキコントローラー
/// </summary>
public class DeckController : MonoBehaviour
{
    /// <summary>
    /// カードビュー
    /// </summary>
    public DeckView view; // カードの見た目の処理

    /// <summary>
    /// カードのモデル
    /// </summary>
    public DeckModel model; // カードのデータを処理

    /// <summary>
    /// カードのモデル
    /// </summary>
    [SerializeField] public Button button;

    private void Awake()
    {
        view = GetComponent<DeckView>();
    }

    /// <summary>
    /// デッキ生成処理
    /// </summary>
    /// <param name="deckId">Deck ID</param>
    public void Init(int deckId)
    {
        model = new DeckModel(deckId); // カードデータを生成
        view.Show(model); // 表示
    }

    /// <summary>
    /// デッキをシャッフルする
    /// </summary>
    public void Shuffle()
    {

    }

    /// <summary>
    /// カードを引く
    /// </summary>
    /// <returns></returns>
    public void Draw()
    {
        if (CheckDeckCountZero())
        {
            return;
        }
        int topCardIndex = model.cards.Count - 1;
        int drawCardNumber = model.cards[topCardIndex];
        model.cards.RemoveAt(topCardIndex);
        CheckDeckCountZero();
        if (this.transform.parent.name.Equals("PlayerDeck"))
        {
            GameManager.instance.AddHandForPlayer(drawCardNumber);
        }
        else if (this.transform.parent.name.Equals("EnemyDeck"))
        {
            GameManager.instance.AddHandForEnemy(drawCardNumber);
        }
    }

    /// <summary>
    /// デッキのカードを取得する
    /// </summary>
    /// <returns></returns>
    public List<int> GetCardIds()
    {
        return model.cards;
    }

    /// <summary>
    /// デッキにカードをセットする
    /// </summary>
    /// <param name="cards"></param>
    public void SetCardIds(List<int> cards)
    {
        model.cards = cards;
        CheckDeckCountZero();
    }

    /// <summary>
    /// デッキのカードが0枚かチェックする
    /// </summary>
    /// <returns></returns>
    public bool CheckDeckCountZero()
    {
        bool check = model.cards.Count == 0;
        if (check)
        {
            this.GetComponent<CanvasGroup>().alpha = 0;
        }
        return check;
    }
}
