using UnityEngine;

/// <summary>
/// カードモデル
/// </summary>
public class CardModel
{
    /// <summary>
    /// カードのID
    /// </summary>
    public int id;

    public string name;

    /// <summary>
    /// カードの画像
    /// </summary>
    [SerializeField] public Sprite cardImage;

    /// <summary>
    /// カードの種類
    /// </summary>
    public CardType cardType;

    /// <summary>
    /// カードの種類のイメージ
    /// </summary>
    public Sprite typeFrame;

    /// <summary>
    /// カードの背景
    /// </summary>
    public Sprite background;

    /// <summary>
    /// カードの背面
    /// </summary>
    public Sprite backside;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CardModel()
    {

    }
    public CardModel(int cardId)
    {
        CardEntity cardEntity = Resources.Load<CardEntity>("CardEntity/Card" + cardId);
        id = cardEntity.id;
        name = cardEntity.name;
        cardType = cardEntity.cardType;
        cardImage = cardEntity.cardImage;
        typeFrame = cardEntity.typeFrame;
        background = cardEntity.background;
        backside = cardEntity.backside;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="cardEntity"></param>
    public CardModel(CardEntity cardEntity)
    {
        id = cardEntity.id;
        name = cardEntity.name;
        cardType = cardEntity.cardType;
        background = cardEntity.background;
        backside = cardEntity.backside;
    }
}
