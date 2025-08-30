using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// プレイヤーデータ
/// </summary>
public class PlayerData : MonoBehaviourPun
{
    private readonly Color COLOR_RED = new Color(0.92549f, 0.31765f, 0.31765f, 1.0f);
    private readonly Color COLOR_BLUE = new Color(0.31765f, 0.48235f, 0.92549f, 1.0f);
    private readonly Color COLOR_GRAY = new Color(0.8f, 0.8f, 0.8f, 1.0f);
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

    public void DeckShuffle()
    {
        deck.Shuffle();
    }

    public void Init(PlayerEntity playerEntity)
    {
        this.playerId = playerEntity.id;
        deck = DeckCreator.instance.CreateDeck(playerEntity.deckId, playerDeckField, true);
        deck.Shuffle();
        playerInfo.SetName(playerEntity.name);
        playerInfo.playerType = playerEntity.playerType;
        playerInfo.SetPlayerIcon(playerEntity.playerType.GetDescription());
        switch (playerEntity.playerType)
        {
            case PlayerType.RED:
                ChangeBackgroundColor(COLOR_RED);
                break;
            case PlayerType.BLUE:
                ChangeBackgroundColor(COLOR_BLUE);
                break;
        }
    }

    /// <summary>
    /// デッキからカードをドローする
    /// </summary>
    public void Draw()
    {
        deck.Draw();
        GameManager.instance.DrawSE();
    }

    /// <summary>
    /// 手札にカードを追加する
    /// </summary>
    /// <param name="id"></param>
    public async void AddHand(int id)
    {
        if (this.name.Equals("Player"))
        {
            AbstractCardController card = await CardCreator.instance.CreateCard(id, playerHand, true, true, true, false, 0.25f);
            GameManager.instance.CreateEnemyCard(id, playerHand, false, true, false, false, 0.25f, card.guid);
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
    public async void ResetOffline(List<int> cards)
    {
        // オフラインを初期化
        for (int i = 0; i < playerOffline.transform.childCount; i++)
        {
            AbstractCardController card = playerOffline.transform.GetChild(i).GetComponent<AbstractCardController>();
            card.DestroyCard();
        }
        // カードを配置
        foreach (int id in cards)
        {
            AbstractCardController card = await CardCreator.instance.CreateCard(id, playerOffline.transform, true, true, false, false, 0.25f);
            GameManager.instance.CreateEnemyCard(id, playerOffline.transform, true, true, false, false, 0.25f, card.guid);
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

    public void PointChanged(int point)
    {
        playerInfo.PointChanged(point);
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

    public void TurnStart()
    {
        playerInfo.SetEnegry(false);
        playerInfo.SetSupport(false);
        switch (playerInfo.playerType)
        {
            case PlayerType.RED:
                ChangeBackgroundColor(COLOR_RED);
                break;
            case PlayerType.BLUE:
                ChangeBackgroundColor(COLOR_BLUE);
                break;
        }
    }

    public void TurnEnd()
    {
        playerInfo.SetEnegry(true);
        playerInfo.SetSupport(true);
        ChangeBackgroundColor(COLOR_GRAY);
    }
}
