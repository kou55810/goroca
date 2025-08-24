using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardListField: MonoBehaviour
{
    private readonly float CARD_SIZE = 0.22f;
    private CardListType cardListType = CardListType.NONE;
    [SerializeField] GameObject content;
    public void Hide()
    {
        List<int> cards = new List<int>();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject child = content.transform.GetChild(i).gameObject;
            AbstractCardController card = child.GetComponent<AbstractCardController>();
            cards.Add(card.model.id);
            Destroy(child);
        }
        switch (this.cardListType)
        {
            case CardListType.PLAYER_OFFLINE:
                GameManager.instance.ResetOffline(cards);
                break;
            case CardListType.PLAYER_DECK:
                GameManager.instance.SetCardIds(cards);
                break;
            default:
                break;
        }
        this.gameObject.SetActive(false);
    }

    public void Show(List<int> cards, CardListType cardListType)
    {
        bool isMovement = false;
        switch (cardListType)
        {
            case CardListType.ENEMY_OFFLINE:
                this.cardListType = cardListType;
                isMovement = false;
                break;
            case CardListType.PLAYER_OFFLINE:
                this.cardListType = cardListType;
                isMovement = true;
                break;
            case CardListType.PLAYER_DECK:
                this.cardListType = cardListType;
                isMovement = true;
                break;
            default:
                this.cardListType = CardListType.NONE;
                break;
        }
        this.gameObject.SetActive(true);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject child = content.transform.GetChild(i).gameObject;
            Destroy(child);
        }
        foreach (int i in cards) {
            CardCreator.instance.CreateCard(i, content.transform, true, true, isMovement, false, CARD_SIZE);
        }
    }
}
