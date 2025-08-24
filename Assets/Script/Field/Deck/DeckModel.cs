using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デッキModelクラス
/// </summary>
public class DeckModel
{
    /// <summary>
    /// デッキID
    /// </summary>
    public int deckId;

    /// <summary>
    /// カードリスト
    /// </summary>
    public List<int> cards;

    /// <summary>
    /// カードの画像
    /// </summary>
    [SerializeField] public Sprite deckImage;

    public DeckModel(int deckId)
    {
        DeckEntity deckEntity = Resources.Load<DeckEntity>("EntityList/Deck/deck" + deckId);
        this.deckId = deckEntity.deckId;
        this.cards = new List<int>(deckEntity.cards);
        deckImage = deckEntity.deckImage;
    }
}
