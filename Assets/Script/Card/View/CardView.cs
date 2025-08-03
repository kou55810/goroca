using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カードビュー
/// </summary>
public class CardView : MonoBehaviour
{
    /// <summary>
    /// 名前
    /// </summary>
    [SerializeField] public TextMeshProUGUI nameText;

    /// <summary>
    /// カードの画像
    /// </summary>
    [SerializeField] public Image cardImage;

    /// <summary>
    /// カードの種類
    /// </summary>
    [SerializeField] public CardType cardType;

    [SerializeField] public Image typeFrame;

    /// <summary>
    /// 背景
    /// </summary>
    [SerializeField] public Image background;

    /// <summary>
    /// カードの裏面
    /// </summary>
    [SerializeField] public Image backside;

    public void Show(CardModel cardModel) // cardModelのデータ取得と反映
    {
        nameText.text = cardModel.name;
        cardImage.sprite = cardModel.cardImage;
        typeFrame.sprite = cardModel.typeFrame;
        background.sprite = cardModel.background;
        backside.sprite = cardModel.backside;
    }
}
