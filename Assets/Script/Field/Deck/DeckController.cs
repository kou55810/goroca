using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デッキコントローラー
/// </summary>
public class DeckController : MonoBehaviourPun
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
        int seed = (int)DateTime.Now.Ticks;
        System.Random rng = new System.Random(seed);
        int n = model.cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = model.cards[k];
            model.cards[k] = model.cards[n];
            model.cards[n] = value;
        }
    }

    /// <summary>
    /// カードを引く
    /// </summary>
    /// <returns></returns>
    public void Draw()
    {
        string parentName = this.transform.parent.name;
        if (!parentName.Equals("PlayerDeck"))
        {
            return;
        }
        if (CheckDeckCountZero())
        {
            return;
        }
        GameManager.instance.DrawSE();
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
    /// デッキボトムにカードをセットする
    /// </summary>
    /// <param name="cards"></param>
    public void SetCardIds(List<int> cards)
    {
        model.cards = cards;
        CheckDeckCountZero();
    }

    public void InsertBottomCard(int id)
    {
        model.cards.Insert(0, id);
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
        else
        {
            this.GetComponent<CanvasGroup>().alpha = 100;
        }
        return check;
    }
}
