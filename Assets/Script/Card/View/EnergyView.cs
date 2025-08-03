using UnityEngine;

public class EnergyView: CardView
{
    public void Show(CardModel cardModel) // cardModelのデータ取得と反映
    {
        nameText.text = cardModel.name;
        background.sprite = cardModel.background;
        backside.sprite = cardModel.backside;
    }
}
