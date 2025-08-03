using UnityEngine;

/// <summary>
/// カードEntity
/// </summary>
[CreateAssetMenu(fileName = "CardEntity", menuName = "Create CardEntity")]
public class CardEntity : ScriptableObject
{
    /// <summary>
    /// カードの識別子
    /// </summary>
    public int id;

    /// <summary>
    /// カード名
    /// </summary>
    public string name;

    /// <summary>
    /// カードの種類
    /// </summary>
    public CardType cardType;

    /// <summary>
    /// カードの画像
    /// </summary>
    [SerializeField] public Sprite cardImage;

    /// <summary>
    /// カードの背景
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
}
