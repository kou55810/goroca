using UnityEngine;

public class DeckCreator : MonoBehaviour
{
    /// <summary>
    /// メンバーカードのprefab
    /// </summary>
    [SerializeField] DeckController deckPrefab;

    /// <summary>
    /// シングルトン実装
    /// </summary>
    public static DeckCreator instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// デッキ生成
    /// </summary>
    /// <param name="number"></param>
    /// <param name="trans"></param>
    /// <param name="isNormalPosition"></param>
    /// <returns></returns>
    public DeckController CreateDeck(int number, Transform trans, bool isNormalPosition)
    {
        DeckController deck = Instantiate(deckPrefab, trans);
        deck.Init(number);
        if (!isNormalPosition)
        {
            deck.transform.transform.Rotate(0, 0, 180);
        }
        return deck;
    }
}
