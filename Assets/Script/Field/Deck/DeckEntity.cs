using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デッキEntityクラス
/// </summary>
[CreateAssetMenu(fileName = "DeckEntity", menuName = "Create DeckEntity")]
public class DeckEntity : ScriptableObject
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
}
