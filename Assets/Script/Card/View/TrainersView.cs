using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainersView: CardView
{
    [SerializeField] public TextMeshProUGUI trainersType;
    [SerializeField] public TextMeshProUGUI effect;
    public void Show(TrainersModel cardModel) // cardModelのデータ取得と反映
    {
        nameText.text = cardModel.name;
        cardImage.sprite = cardModel.cardImage;
        trainersType.text = cardModel.trainersType.GetDescription();
        typeFrame.sprite = cardModel.typeFrame;
        background.sprite = cardModel.background;
        backside.sprite = cardModel.backside;
        effect.text = cardModel.effect;
    }
}
