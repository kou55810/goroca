using TMPro;
using UnityEngine;

/// <summary>
/// メンバーカードビュー
/// </summary>
public class MemberView : CardView
{
    /// <summary>
    /// メンバーの体力
    /// </summary>
    [SerializeField] public TextMeshProUGUI hpText;

    /// <summary>
    /// メンバーの種類
    /// </summary>
    [SerializeField] public TextMeshProUGUI memberType;

    /// <summary>
    /// カードの表示
    /// </summary>
    /// <param name="cardModel"></param>
    public void Show(MemberModel cardModel) // cardModelのデータ取得と反映
    {
        nameText.text = cardModel.name;
        typeFrame.sprite = cardModel.typeFrame;
        background.sprite = cardModel.background;
        backside.sprite = cardModel.backside;
        hpText.text = cardModel.hp.ToString();
        memberType.text = cardModel.memberType.GetDescription();
    }
}
