using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーデータ
/// </summary>
public class PlayerData : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのID
    /// </summary>
    public Guid playerId;

    [SerializeField] PlayerInfo playerInfo;

    /// <summary>
    /// プレイヤーの手札
    /// </summary>
    [SerializeField] Transform playerHand;

    /// <summary>
    /// プレイヤーのオフライン
    /// </summary>
    [SerializeField] Transform playerOffline;

    /// <summary>
    /// プレイヤーのデッキフィールド
    /// </summary>
    [SerializeField] Transform playerDeckField;

    /// <summary>
    /// デッキ
    /// </summary>
    DeckController deck;

    /// <summary>
    /// シングルトン実装
    /// </summary>
    public static PlayerData instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Init(PlayerEntity playerEntity)
    {
        playerId = playerEntity.id;
        deck = DeckCreator.instance.CreateDeck(playerEntity.deckId, playerDeckField, true);
    }

    /// <summary>
    /// デッキからカードをドローする
    /// </summary>
    public void Draw()
    {
        deck.Draw();
    }

    /// <summary>
    /// 手札にカードを追加する
    /// </summary>
    /// <param name="id"></param>
    public void AddHand(int id)
    {
        if (this.name.Equals("Player"))
        {
            CardCreator.instance.CreateCard(id, playerHand, true, true, true, false, 0.25f);
        }
        else if (this.name.Equals("Enemy"))
        {
            CardCreator.instance.CreateCard(id, playerHand, false, true, false, false, 0.25f);
        }
    }

    /// <summary>
    /// オフラインのカードを取得する
    /// </summary>
    /// <returns></returns>
    public List<int> GetOfflineCards()
    {
        List<int> cards = new List<int>();
        for (int i = 0; i < playerOffline.transform.childCount; i++)
        {
            GameObject child = playerOffline.transform.GetChild(i).gameObject;
            AbstractCardController card = child.GetComponent<AbstractCardController>();
            cards.Add(card.model.id);
        }
        return cards;
    }

    /// <summary>
    /// デッキのカードを取得する
    /// </summary>
    /// <returns></returns>
    public List<int> GetDeckCards()
    {
        return deck.GetCardIds();
    }

    /// <summary>
    /// オフラインをリセットする
    /// </summary>
    /// <param name="cards"></param>
    public void ResetOffline(List<int> cards)
    {
        // オフラインを初期化
        for (int i = 0; i < playerOffline.transform.childCount; i++)
        {
            Destroy(playerOffline.transform.GetChild(i).gameObject);
        }
        // カードを配置
        foreach (int id in cards)
        {
            CardCreator.instance.CreateCard(id, playerOffline.transform, true, true, false, false, 0.25f);
        }
    }

    /// <summary>
    /// デッキにカードをセットする
    /// </summary>
    /// <param name="cards"></param>
    public void SetDeckCards(List<int> cards)
    {
        deck.SetCardIds(cards);
    }

    public void AddPoint()
    {
        playerInfo.AddPoint();
    }

    public void RemovePoint()
    {
        playerInfo.RemovePoint();
    }

    public void ChangeBackgroundColor(Color color)
    {
        playerInfo.ChangeBackgroundColor(color);
    }
}
