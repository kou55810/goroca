using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デッキViewクラス
/// </summary>
public class DeckView : MonoBehaviour
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
    [SerializeField] public Image deckImage;

    public void Show(DeckModel model)
    {
        deckId = model.deckId;
        cards = model.cards;
        deckImage.sprite = model.deckImage;
    }
}
