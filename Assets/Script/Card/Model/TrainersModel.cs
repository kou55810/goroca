using UnityEngine;

public class TrainersModel: CardModel
{
    public TrainersType trainersType;
    public string effect;
    public TrainersModel(int cardId)
    {
        Debug.Log(cardId);
        TrainersEntity cardEntity = Resources.Load<TrainersEntity>("EntityList/Trainers/Card" + cardId);

        id = cardEntity.id;
        name = cardEntity.name;
        cardImage = cardEntity.cardImage;
        cardType = cardEntity.cardType;
        background = cardEntity.background;
        backside = cardEntity.backside;
        typeFrame = cardEntity.typeFrame;
        trainersType = cardEntity.trainersType;
        effect = cardEntity.effect;
    }
}
